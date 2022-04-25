using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Principal;
using System.Windows;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace OSBilişim
{
    public partial class Anaform : Form
    {
        public static string isim,statü;
        public Anaform()
        {
            InitializeComponent();
        }

        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.118,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        private void Anaform_Load(object sender, EventArgs e)
        {
            isim = isim_label.Text;
            statü = statü_label.Text;

            Kullanicigirisiform Kullanicigirisiform = new Kullanicigirisiform();
            timer1.Start();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    Kullanicigirisiform.username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Kullanicigirisiform.username);
                    isim_label.Text = Kullanicigirisiform.username;

                    SqlCommand versiyonkontrol = new SqlCommand("Select * From version where version='" + Kullanicigirisiform.versiyon + "'", connection);
                    SqlDataReader versiyonkontrolokuyucu;
                    versiyonkontrolokuyucu = versiyonkontrol.ExecuteReader();
                    if (versiyonkontrolokuyucu.Read())
                    {
                        MessageBox.Show(versiyonkontrolokuyucu["versiyon_aciklama"].ToString(), "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        System.Diagnostics.Process.Start(versiyonkontrolokuyucu["yeni_program_indirme_linki"].ToString());
                        Environment.Exit(0);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Uygulama başlatılırken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            try
            {
                aktifkullanicilar_listbox.Items.Clear();
                SqlCommand kullaniciaktifligi = new SqlCommand("SELECT *FROM kullanicilar where durum = '1' ", connection);
                SqlDataReader aktifkullanicilar;
                aktifkullanicilar = kullaniciaktifligi.ExecuteReader();
                while (aktifkullanicilar.Read())
                {
                    aktifkullanicilar_listbox.Items.Add(((string)aktifkullanicilar["kullanici_isim"] + " (" + (string)aktifkullanicilar["kullanici_statü"]+")"));
                   string kendikullaniciisminikaldirma;
                    kendikullaniciisminikaldirma = Kullanicigirisiform.username + " (" + (string)aktifkullanicilar["kullanici_statü"]+")";
                    if (aktifkullanicilar_listbox.Items.Contains(kendikullaniciisminikaldirma))
                    {
                        aktifkullanicilar_listbox.Items.Remove(kendikullaniciisminikaldirma);
                    }
                    if (aktifkullanicilar["kullanici_statü"] == DBNull.Value)
                    {
                        string result = String.Empty;
                        aktifkullanicilar_listbox.Items.Add("Aktif kullanıcı yoktur.");
                    }
                }

                SqlCommand kullanicilar = new SqlCommand("SELECT *FROM kullanicilar where k_adi = '" + Kullanicigirisiform.username + "'", connection);
                SqlDataReader kullaniciaciklamasi;
                kullaniciaciklamasi = kullanicilar.ExecuteReader();
                while (kullaniciaciklamasi.Read ())
                {
                    isim_label.Text = ((string)kullaniciaciklamasi["kullanici_isim"]);
                    soyisim_label.Text = ((string)kullaniciaciklamasi["kullanici_soyisim"]);
                    statü_label.Text = ((string)kullaniciaciklamasi["kullanici_statü"]); 
                    kayit_tarihi_label.Text = ((string)kullaniciaciklamasi["kullanici_kayit_tarihi"]);

                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kullanıcı bilgileri alınırken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            connection.Close();

            if (statü_label.Text == "Teknik Görevli" || statü_label.Text == "Teknisyen" || statü_label.Text == "Teknik")
            {
                siparis_kontrol_btn.Location = new System.Drawing.Point(533, 216);
                siparis_olustur_btn.Visible = false;
                ürün_ekle_ve_düzenle_btn.Visible = false;
                diğer_malzeme_grubları.Visible = false;
                Diğer_malzeme_grubları_ekle_ve_düzenleme_btn.Visible = false;
                if (isim_label.Text == "Selçuk")
                {
                    diğer_malzeme_grubları.Visible = true;
                    siparis_olustur_btn.Visible = true;
                    ürün_ekle_ve_düzenle_btn.Visible = true;
                    Diğer_malzeme_grubları_ekle_ve_düzenleme_btn.Visible = true;
                    siparis_kontrol_btn.Location = new System.Drawing.Point(591, 216);
                    siparis_olustur_btn.Location = new System.Drawing.Point(474, 216);
                    ürün_ekle_ve_düzenle_btn.Location = new System.Drawing.Point(474, 267);
                }
            }
            if (statü_label.Text == "Ana Bilgisayar" || isim_label.Text == "ANA PC")
            {
                isim_label.Location = new System.Drawing.Point(60, 220);
                label.Location = new System.Drawing.Point(12, 220);
                soyisim_label.Visible = false;
                label_6.Visible = false;
            }
            else
            {
                Diğer_malzeme_grubları_ekle_ve_düzenleme_btn.Visible = true;
                diğer_malzeme_grubları.Visible = true;
                siparis_kontrol_btn.Location = new System.Drawing.Point(591, 216);
                siparis_olustur_btn.Location = new System.Drawing.Point(474, 216);
                ürün_ekle_ve_düzenle_btn.Location = new System.Drawing.Point(474, 267);
               
            }



            tarih_label.Text = DateTime.Now.ToLongDateString();
           
            
        }
        private void Anaform_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void Logout_label_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    Kullanicigirisiform Kullanicigirisiform = new Kullanicigirisiform();
                    SqlCommand kullanicidurumgüncelle = new SqlCommand("Update kullanicilar set durum='" + 0 + "' where k_adi = '" + Kullanicigirisiform.username + "'", connection);
                    kullanicidurumgüncelle.ExecuteNonQuery();
                    Kullanicigirisiform.Show();
                    Hide();
                }
            }
            catch (Exception kullaniciaktifligi)
            {
                MessageBox.Show("Kullanıcı bilgileri alınırken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + kullaniciaktifligi.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Application.Exit();
        }
        private void Label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/");
        }


        private void LinkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/");
        }
    
        private void Btn_cikis_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    Kullanicigirisiform Kullanicigirisiform = new Kullanicigirisiform();
                    SqlCommand kullanicidurumgüncelle = new SqlCommand("Update kullanicilar set durum='" + 0 + "' where k_adi = '" + Kullanicigirisiform.username + "'", connection);
                    kullanicidurumgüncelle.ExecuteNonQuery();
                    Kullanicigirisiform.Show();
                    Hide();
                }
            }
            catch (Exception kullaniciaktifligi)
            {
                MessageBox.Show("Kullanıcı bilgileri alınırken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + kullaniciaktifligi.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Siparis_olustur_btn_Click(object sender, EventArgs e)
        {
            Siparisolusturmaform siparisolusturmaform = new Siparisolusturmaform();
            siparisolusturmaform.Show();
            Hide();
        }

        private void Siparis_kontrol_btn_Click(object sender, EventArgs e)
        {
            Sipariskontrolform sipariskontrolforum = new Sipariskontrolform();
            sipariskontrolforum.Show();
            Hide();
        }

        private void Ürün_ekle_ve_düzenle_btn_Click(object sender, EventArgs e)
        {
            Ürüneklemedüzenlemeform Ürüneklemedüzenlemeform = new Ürüneklemedüzenlemeform();
            Ürüneklemedüzenlemeform.Show();
            Hide();
        }

        new
        #region forumharaketettirme
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;

        }

        private void Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }
        #endregion

        #region forumharaketettirme2
        private void Anaform_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Anaform_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }

        private void Anaform_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        #endregion

        private void Diğer_malzeme_grubları_Click(object sender, EventArgs e)
        {
            Diğer_Malzeme_Grubları diğer_Malzeme_Grubları = new Diğer_Malzeme_Grubları();
            diğer_Malzeme_Grubları.Show();
            Hide();
        }

        private void Diğer_malzeme_grubları_ekle_ve_düzenleme_btn_Click(object sender, EventArgs e)
        {
            Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme diğer_Malzeme_Grubları = new Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme();
            diğer_Malzeme_Grubları.Show();
            Hide();
        }
        #region veritabanı yedekleme
        public void VeritabanıYedek()
        {
            DateTime thisDay = DateTime.Today;
            string günlükveritabanıadı = "OSBilişim_veritabanı_yedeği - " + thisDay.ToString("d") + ".bak";
            int hour = DateTime.Now.TimeOfDay.Hours;
            if (statü_label.Text == "Ana Bilgisayar" && Kullanicigirisiform.username == "Admin")
            {
                if (hour >= 9 && hour <= 18)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        MessageBox.Show("Veritabanı yedeği alınıyor, lütfen bekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string komut = "backup database model to disk='" + günlükveritabanıadı + "'";
                        SqlCommand sqlCommand = new SqlCommand(komut, connection);
                        sqlCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            VeritabanıYedek();
        }
        #endregion
    }
}

