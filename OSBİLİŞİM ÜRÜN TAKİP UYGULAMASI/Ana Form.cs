using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace OSBilişim
{
    public partial class Anaform : Form
    {
        public static string isim, statü;
        public Anaform()
        {
            InitializeComponent();
        }

        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.106,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        private void Anaform_Load(object sender, EventArgs e)
        {
            isim = isim_label.Text;
            statü = statü_label.Text;
            Kullanicigirisiform Kullanicigirisiform = new Kullanicigirisiform();
            timer1.Start();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                FileVersionInfo programversion = FileVersionInfo.GetVersionInfo(@"OSBilişim.exe");
                SqlCommand üründurum = new SqlCommand("select version,versiyon_aciklama,yeni_program_indirme_linki from version", connection);
                SqlDataReader üründurumusorgulama;
                üründurumusorgulama = üründurum.ExecuteReader();
                if (üründurumusorgulama.Read())
                {
                    Kullanicigirisiform.güncelversiyon = ((string)üründurumusorgulama["version"]);
                    if (Convert.ToInt32(Kullanicigirisiform.güncelversiyon) >= Convert.ToInt32(programversion.FileVersion))
                    {
                        DialogResult dialog = MessageBox.Show("Uygulamanızın yeni sürümünü indirmek ister misiniz?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            string dosya_dizini = AppDomain.CurrentDomain.BaseDirectory.ToString() + "OSUpdate.exe";
                            File.WriteAllBytes(@"OSUpdate.exe", new System.Net.WebClient().DownloadData("http://192.168.1.106/Update/OSUpdate.exe"));
                            Process.Start("OSUpdate.exe");
                            System.Threading.Thread.Sleep(1000);
                            Environment.Exit(0);
                        }
                        else
                        {
                            MessageBox.Show("Uygulamanızı güncellemediğiniz için, program çalışmayacaktır.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Uygulama başlatılırken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand kullanicikomut = new SqlCommand("SELECT kullanici_isim, kullanici_statü FROM kullanicilar where durum = '1'", connection);
                SqlDataReader aktifkullanici;
                aktifkullanici = kullanicikomut.ExecuteReader();
                aktifkullanicilar_listbox.Items.Clear();
                while (aktifkullanici.Read())
                {
                    aktifkullanicilar_listbox.Items.Add(aktifkullanici["kullanici_isim"] + " (" + (string)aktifkullanici["kullanici_statü"] + ")".ToString());
                    string kendikullaniciisminikaldirma = Kullanicigirisiform.username + " (" + (string)aktifkullanici["kullanici_statü"] + ")";
                    if (aktifkullanicilar_listbox.Items.Contains(kendikullaniciisminikaldirma))
                    {
                        aktifkullanicilar_listbox.Items.Remove(kendikullaniciisminikaldirma);
                    }
                }
                aktifkullanici.Close();

                SqlCommand kullanicilar = new SqlCommand("SELECT *FROM kullanicilar where k_adi = '" + Kullanicigirisiform.username + "'", connection);
                SqlDataReader kullaniciaciklamasi;
                kullaniciaciklamasi = kullanicilar.ExecuteReader();
                while (kullaniciaciklamasi.Read())
                {
                    isim_label.Text = ((string)kullaniciaciklamasi["kullanici_isim"]);
                    soyisim_label.Text = ((string)kullaniciaciklamasi["kullanici_soyisim"]);
                    statü_label.Text = ((string)kullaniciaciklamasi["kullanici_statü"]);
                    kayit_tarihi_label.Text = ((string)kullaniciaciklamasi["kullanici_kayit_tarihi"]);

                }
                connection.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Kullanıcı bilgileri alınırken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            if (statü_label.Text == "Teknik Görevli" || statü_label.Text == "Teknisyen" || statü_label.Text == "Teknik")
            {
                diğer_malzeme_grubları.Visible = true;
                siparis_olustur_btn.Visible = true;
                ürün_ekle_ve_düzenle_btn.Visible = true;
                Diğer_malzeme_grubları_ekle_ve_düzenleme_btn.Visible = true;
                siparis_kontrol_btn.Location = new System.Drawing.Point(591, 216);
                siparis_olustur_btn.Location = new System.Drawing.Point(474, 216);
                ürün_ekle_ve_düzenle_btn.Location = new System.Drawing.Point(474, 267);
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
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    Kullanicigirisiform kullanicigirisiform = new Kullanicigirisiform();
                    SqlCommand kullanicidurumgüncelle = new SqlCommand("Update kullanicilar set durum='" + 0 + "' where k_adi = '" + Kullanicigirisiform.username + "'", connection);
                    kullanicidurumgüncelle.ExecuteNonQuery();
                    kullanicigirisiform.Show();
                    Hide();
                }
            }
            catch (Exception kullaniciaktifligi)
            {
                MessageBox.Show("Kullanıcı bilgileri çekilmedi tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + kullaniciaktifligi.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            foreach (var process in Process.GetProcessesByName("OSBilişim"))
            {
                process.Kill();
            }
        }
        private void Logout_label_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                Kullanicigirisiform kullanicigirisiform = new Kullanicigirisiform();
                SqlCommand kullanicidurumgüncelle = new SqlCommand("Update kullanicilar set durum='" + 0 + "' where k_adi = '" + Kullanicigirisiform.username + "'", connection);
                kullanicidurumgüncelle.ExecuteNonQuery();
            }
            catch (Exception kullaniciaktifligi)
            {
                MessageBox.Show("Kullanıcı bilgileri çekilirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + kullaniciaktifligi.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            Application.Exit();
        }
        private void Label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          Process.Start("https://mail.google.com/");
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
        #region renkayarları
        private void Siparis_olustur_btn_MouseMove(object sender, MouseEventArgs e)
        {
            siparis_olustur_btn.BackColor = Color.DarkGreen;
        }

        private void Siparis_olustur_btn_MouseLeave(object sender, EventArgs e)
        {
            siparis_olustur_btn.BackColor = Color.MediumSeaGreen;
        }

        private void Siparis_kontrol_btn_MouseLeave(object sender, EventArgs e)
        {
            siparis_kontrol_btn.BackColor = Color.MediumSeaGreen;
        }

        private void Siparis_kontrol_btn_MouseMove(object sender, MouseEventArgs e)
        {
            siparis_kontrol_btn.BackColor = Color.DarkGreen;
        }

        private void Ürün_ekle_ve_düzenle_btn_MouseLeave(object sender, EventArgs e)
        {
            ürün_ekle_ve_düzenle_btn.BackColor = Color.MediumSeaGreen;
        }

        private void Ürün_ekle_ve_düzenle_btn_MouseMove(object sender, MouseEventArgs e)
        {
            ürün_ekle_ve_düzenle_btn.BackColor = Color.DarkGreen;
        }

        private void Diğer_malzeme_grubları_MouseMove(object sender, MouseEventArgs e)
        {
            diğer_malzeme_grubları.BackColor = Color.DarkGreen;
        }

        private void Diğer_malzeme_grubları_MouseLeave(object sender, EventArgs e)
        {
            diğer_malzeme_grubları.BackColor = Color.MediumSeaGreen;
        }

        private void Diğer_malzeme_grubları_ekle_ve_düzenleme_btn_MouseMove(object sender, MouseEventArgs e)
        {
            Diğer_malzeme_grubları_ekle_ve_düzenleme_btn.BackColor = Color.DarkGreen;
        }

        private void Diğer_malzeme_grubları_ekle_ve_düzenleme_btn_MouseLeave(object sender, EventArgs e)
        {
            Diğer_malzeme_grubları_ekle_ve_düzenleme_btn.BackColor = Color.MediumSeaGreen;
        }

        private void Btn_cikis_MouseLeave(object sender, EventArgs e)
        {
            btn_cikis.BackColor = Color.MediumSeaGreen;
        }
        private void Logout_label_MouseMove(object sender, MouseEventArgs e)
        {
            logout_label.ForeColor = Color.Black;
        }

        private void Label3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.ForeColor = Color.Black;
        }

        private void Logout_label_MouseLeave(object sender, EventArgs e)
        {
            logout_label.ForeColor = Color.Gray;
        }

        private void Label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Gray;
        }

        private void Btn_cikis_MouseMove(object sender, MouseEventArgs e)
        {
            btn_cikis.BackColor = Color.DarkGreen;
        }
        #endregion
       
    }
}