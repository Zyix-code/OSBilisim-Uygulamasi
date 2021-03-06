using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OSBilişim
{
    public partial class Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme : Form
    {
        public Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme()
        {
            InitializeComponent();
        }
        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.106,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        public void Listeyenile()
        {
            diger_ürün_adi_textbox.Text = "";
            diger_ürün_kodu_textbox.Text = "";
            diger_ürün_serino_textbox.Text = "";

            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    int tekrarsec = diger_ürün_adi_listbox.SelectedIndex;

                    diger_ürün_adi_listbox.Items.Clear();
                    SqlCommand komut = new SqlCommand("SELECT * FROM diger_ürünler ", connection);
                    SqlDataReader veriokuyucu;
                    veriokuyucu = komut.ExecuteReader();
                    while (veriokuyucu.Read())
                    {
                        diger_ürün_adi_listbox.Items.Add(veriokuyucu["diger_urun_adi"]);
                    }

                    if (diger_ürün_adi_listbox.SelectedIndex < tekrarsec)
                    { diger_ürün_adi_listbox.SelectedIndex = -1; }
                    else { diger_ürün_adi_listbox.SelectedIndex = tekrarsec; }

                    veriokuyucu.Close();

                    diger_ürün_kodu_listbox.Items.Clear();
                    SqlCommand komut3 = new SqlCommand("SELECT * FROM diger_ürün_kodları where diger_urun_adi = '" + diger_ürün_adi_listbox.SelectedItem + "'", connection);
                    SqlDataReader veriokuyucu3;
                    veriokuyucu3 = komut3.ExecuteReader();
                    while (veriokuyucu3.Read())
                    {
                        diger_ürün_kodu_listbox.Items.Add(veriokuyucu3["diger_urun_stok_kodu"]);
                    }
                    veriokuyucu3.Close();

                    diger_ürün_serino_listbox.Items.Clear();
                    SqlCommand üründurum = new SqlCommand("SELECT * FROM diger_ürün_stok where diger_urun_adi = '" + diger_ürün_serino_listbox.SelectedItem + "'", connection);
                    SqlDataReader üründurumusorgulama;
                    üründurumusorgulama = üründurum.ExecuteReader();
                    while (üründurumusorgulama.Read())
                    {
                        diger_ürün_serino_listbox.Items.Add(üründurumusorgulama["urun_seri_no"]);
                    }
                    üründurumusorgulama.Close();
                    connection.Close();
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("Sipariş listesi güncellenmedi.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme_Load(object sender, EventArgs e)
        {
            Kullanicigirisiform Kullanicigirisiform = new Kullanicigirisiform();
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
                    connection.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Program başlatılmadı.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            if (diger_ürün_adi_listbox.SelectedIndex == -1)
            {
                diger_ürün_kodu_listbox.Items.Add("Lütfen bir ürün seçin.");
                diger_ürün_serino_listbox.Items.Add("Lütfen bir ürün seçin.");
            }
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand komut = new SqlCommand("SELECT * FROM diger_ürünler ", connection);
                SqlDataReader veriokuyucu1;
                veriokuyucu1 = komut.ExecuteReader();
                while (veriokuyucu1.Read())
                {
                    diger_ürün_adi_listbox.Items.Add(veriokuyucu1["diger_urun_adi"]);
                }
                veriokuyucu1.Close();
                connection.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün kodlar çekilmedi.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Diger_ürünler_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (diger_ürün_adi_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Geçerli ürün seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                diger_ürün_adi_textbox.Text = diger_ürün_adi_listbox.SelectedItem.ToString();
                try
                {
                    using (var cn = new SqlConnection("server=192.168.1.106,1433;database=OSBİLİSİM;UId=Admin;Pwd=1;MultipleActiveResultSets=True;"))
                    using (var cmd = new SqlCommand(@"select COUNT(diger_urun_serino) from diger_ürün_stok where diger_urun_durumu = 'Kullanılmadı' and diger_urun_adi = '" + diger_ürün_adi_listbox.SelectedItem.ToString() + "'", cn))
                    {
                        cn.Open();
                        var kalan = Convert.ToInt32(cmd.ExecuteScalar());
                        ürünkalankullanımlabel.Text = "Kullanılmamış ürün sayısı: " + kalan.ToString();
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Ürün bilgileri çekilirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            diger_ürün_kodu_textbox.Text = "";
            try
            {
                if (diger_ürün_adi_listbox.SelectedIndex == -1)
                {
                    diger_ürün_kodu_listbox.Items.Clear();
                    diger_ürün_serino_listbox.Items.Clear();
                }
                else
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        diger_ürün_kodu_listbox.Items.Clear();
                        SqlCommand komut3 = new SqlCommand("SELECT * FROM diger_ürün_kodları where diger_urun_adi = '" + diger_ürün_adi_listbox.SelectedItem + "'", connection);
                        SqlDataReader veriokuyucu3;
                        veriokuyucu3 = komut3.ExecuteReader();
                        while (veriokuyucu3.Read())
                        {
                            diger_ürün_kodu_listbox.Items.Add(veriokuyucu3["diger_urun_stok_kodu"]);
                        }
                        veriokuyucu3.Close();

                        diger_ürün_serino_listbox.Items.Clear();
                        SqlCommand üründurum = new SqlCommand("SELECT * FROM diger_ürün_stok where diger_urun_adi = '" + diger_ürün_adi_listbox.SelectedItem + "'", connection);
                        SqlDataReader üründurumusorgulama;
                        üründurumusorgulama = üründurum.ExecuteReader();
                        while (üründurumusorgulama.Read())
                        {
                            diger_ürün_serino_listbox.Items.Add(üründurumusorgulama["diger_urun_serino"]);

                        }
                        üründurumusorgulama.Close();

                    }
                    connection.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Serverden ürün stok kodları çekilmedi lütfen tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Diger_ürün_kodları_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (diger_ürün_kodu_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Geçerli ürün kodu seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                diger_ürün_kodu_textbox.Text = diger_ürün_kodu_listbox.SelectedItem.ToString();
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

        private void Secili_ürünün_kod_seri_no_sil_btn_Click(object sender, EventArgs e)
        {
            if (diger_ürün_kodu_listbox.SelectedIndex == -1 && diger_ürün_serino_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Silinmesi gereken geçerli ürün kodunu, seri numarasını seçmeniz gerekmektedir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (diger_ürün_kodu_listbox.SelectedIndex >= 0)
                {
                    DialogResult dialog = MessageBox.Show("Seçili olan ürün kodu kaldırılacaktır. Kaldırılsın mı?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                                SqlCommand ürünstokverisil = new SqlCommand("delete from diger_ürün_kodları where diger_urun_stok_kodu = '" + diger_ürün_kodu_textbox.Text + "'", connection);
                                ürünstokverisil.ExecuteNonQuery();
                                MessageBox.Show("Seçili ürün kodu kaldırıldı.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                connection.Close();

                                using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                                {
                                    Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + diger_ürün_adi_listbox.SelectedItem.ToString() + " ürününün " + diger_ürün_kodu_listbox.SelectedItem.ToString() + " ürün kodunu sildi.", w);
                                }
                                using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                                {
                                    Kullanicigirisiform.DumpLog(r);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Seçili olan ürün kodu kaldırılmadı.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception hata)
                        {
                            MessageBox.Show("Ürün kodu kaldırılmadı lütfen tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                    else { MessageBox.Show("Seçili olan ürün kodu kaldırılmadı.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }

                if (diger_ürün_serino_listbox.SelectedIndex >= 0)
                {
                    DialogResult dialog = MessageBox.Show("Seçili olan ürünün seri numarası kaldırılacaktır. Kaldırılsın mı?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                                SqlCommand ürünstokverisil = new SqlCommand("delete from diger_ürün_stok where diger_urun_serino = '" + diger_ürün_serino_textbox.Text + "'", connection);
                                ürünstokverisil.ExecuteNonQuery();
                                MessageBox.Show("Seçili ürün seri numarası kaldırıldı.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                connection.Close();
                               
                                using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                                {
                                    Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + diger_ürün_adi_listbox.SelectedItem.ToString() + " ürününün " + diger_ürün_serino_listbox.SelectedItem.ToString() + " seri numarasını sildi.", w);
                                }
                                using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                                {
                                    Kullanicigirisiform.DumpLog(r);
                                }
                            }
                        }
                        catch (Exception hata)
                        {
                            MessageBox.Show("Ürün seri numarası kaldırılırken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                    else { MessageBox.Show("Seçili olan ürün seri numarası kaldırılmadı.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }

                Listeyenile();
                connection.Close();
            }
        }

        private void Ana_menü_btn_Click(object sender, EventArgs e)
        {
            Anaform anaform = new Anaform();
            anaform.Show();
            Hide();
        }

        private void Windows_kücültme_label_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Secili_ürünü_sil_btn_Click(object sender, EventArgs e)
        {
            if (diger_ürün_adi_listbox.SelectedIndex == -1 && diger_ürün_kodu_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Silinmesi gereken geçerli ürünleri seçmeniz gerekmektedir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (diger_ürün_adi_listbox.SelectedIndex >= 0)
                {
                    DialogResult dialog = MessageBox.Show("Eğer seçili ürünü kaldırmak istiyorsanız o ürüne ait ürün kodları, seri numaraları otamatik silinecektir. Ürün kaldırılsın mı?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                                SqlCommand ürünadikontrol = new SqlCommand("Select * From diger_ürünler where diger_urun_adi = '" + diger_ürün_adi_textbox.Text + "'", connection);
                                SqlDataReader ürünadikontrolokuyucu;
                                ürünadikontrolokuyucu = ürünadikontrol.ExecuteReader();
                                if (ürünadikontrolokuyucu.Read())
                                {
                                    SqlCommand notebookürünverisil = new SqlCommand("delete from diger_ürünler where diger_urun_adi = '" + diger_ürün_adi_textbox.Text + "'", connection);
                                    notebookürünverisil.ExecuteNonQuery();
                                    SqlCommand notebookürünkodverisil = new SqlCommand("delete from diger_ürün_kodları where diger_urun_adi = '" + diger_ürün_adi_textbox.Text + "'", connection);
                                    notebookürünkodverisil.ExecuteNonQuery();

                                    using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                                    {
                                        Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + diger_ürün_adi_listbox.SelectedItem.ToString() + " ürününü sildi.", w);
                                    }
                                    using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                                    {
                                        Kullanicigirisiform.DumpLog(r);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Yazılan ürün sisteme kayıtlı değildir. Geçerli ürünü silmek için ürün adını düzeltiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                connection.Close();
                            }
                            else
                            {
                                MessageBox.Show("Seçili ürün silinmedi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception hata)
                        {
                            MessageBox.Show("Ürün silinmedi lütfen tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                }
                Listeyenile();
                connection.Close();
            }
        }

        private void Yeni_ürün_ekle_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    if (diger_ürün_adi_listbox.Items.Contains(diger_ürün_adi_textbox.Text) && diger_ürün_kodu_listbox.Items.Contains(diger_ürün_kodu_textbox.Text))
                    {
                        if (diger_ürün_serino_listbox.Items.Contains(diger_ürün_serino_textbox.Text))
                        {
                            MessageBox.Show("Bu seri numarası mevcuttur. Başka seri numarası kullanınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_adi_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün adını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_serino_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün seri numarasını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_kodu_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün stok kodunu boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string yeniürünserinokayit = "insert into diger_ürün_stok(diger_urun_adi,diger_urun_serino,diger_urun_adeti,diger_urun_durumu) values " + "" + "(@diger_urun_adi,@diger_urun_serino,@diger_urun_adeti,@diger_urun_durumu)";
                            SqlCommand yeniürünserinokayitkomut = new SqlCommand(yeniürünserinokayit, connection);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_adi", diger_ürün_adi_textbox.Text);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_serino", diger_ürün_serino_textbox.Text);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_adeti", "1");
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_durumu", "Kullanılmadı");
                            yeniürünserinokayitkomut.ExecuteNonQuery();
                            MessageBox.Show("Yeni seri numaralı ürünün kaydı başarılı şekilde oluşturuldu.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + diger_ürün_adi_textbox.Text + " ürününün " + diger_ürün_serino_textbox.Text + " yeni seri numarasını ekledi.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }

                            int tekrarsec = diger_ürün_serino_listbox.SelectedIndex;
                            MessageBox.Show("Yeni seri numarası başarılı bir şekilde eklendi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            diger_ürün_serino_listbox.SelectedIndex = tekrarsec;
                            diger_ürün_serino_textbox.Focus();
                        }
                    }
                    else if (diger_ürün_adi_listbox.Items.Contains(diger_ürün_adi_textbox.Text))
                    {
                        if (diger_ürün_kodu_listbox.Items.Contains(diger_ürün_kodu_textbox.Text))
                        {
                            MessageBox.Show("Bu ürün kodu mevcuttur. Başka ürün kodu kullanınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_serino_listbox.Items.Contains(diger_ürün_serino_textbox.Text))
                        {
                            MessageBox.Show("Bu seri numarası mevcuttur. Başka seri numarası kullanınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_adi_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün adını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_kodu_textbox.Text == "" && diger_ürün_serino_textbox.Text.Length <= 0)
                        {
                            MessageBox.Show("Ürün stok kodunu boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_serino_textbox.Text == "" && diger_ürün_kodu_textbox.Text.Length <= 0)
                        {
                            MessageBox.Show("Ürün seri numarasını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_serino_textbox.Text == "" && diger_ürün_kodu_textbox.Text.Length > 0)
                        {
                            string yeniürünkodukayit = "insert into diger_ürün_kodları(diger_urun_adi,diger_urun_stok_kodu) values " + "" + "(@diger_urun_adi,@diger_urun_stok_kodu)";
                            SqlCommand kayitkomut = new SqlCommand(yeniürünkodukayit, connection);
                            kayitkomut.Parameters.AddWithValue("@diger_urun_adi", diger_ürün_adi_textbox.Text);
                            kayitkomut.Parameters.AddWithValue("@diger_urun_stok_kodu", diger_ürün_kodu_textbox.Text);
                            kayitkomut.ExecuteNonQuery();

                            using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + diger_ürün_adi_textbox.Text + " ürününün " + diger_ürün_kodu_textbox.Text + " yeni ürün kodunu ekledi.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }

                            MessageBox.Show("Yeni ürün stok kodu başarılı bir şekilde eklendi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int tekrarsec = diger_ürün_kodu_listbox.SelectedIndex;
                            diger_ürün_kodu_listbox.SelectedIndex = tekrarsec;
                            diger_ürün_kodu_textbox.Focus();
                        }
                        else if (diger_ürün_kodu_textbox.Text == "" && diger_ürün_serino_textbox.Text.Length >= 0)
                        {
                            string yeniürünserinokayit = "insert into diger_ürün_stok(diger_urun_adi,diger_urun_serino,diger_urun_adeti,diger_urun_durumu) values " + "" + "(@diger_urun_adi,@diger_urun_serino,@diger_urun_adeti,@diger_urun_durumu)";
                            SqlCommand yeniürünserinokayitkomut = new SqlCommand(yeniürünserinokayit, connection);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_adi", diger_ürün_adi_textbox.Text);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_serino", diger_ürün_serino_textbox.Text);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_adeti", "1");
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_durumu", "Kullanılmadı");
                            yeniürünserinokayitkomut.ExecuteNonQuery();

                            using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + diger_ürün_adi_textbox.Text + " ürününün " + diger_ürün_serino_textbox.Text + " yeni seri numarasını ekledi.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }

                            MessageBox.Show("Yeni seri numarası başarılı bir şekilde eklendi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int tekrarsec = diger_ürün_serino_listbox.SelectedIndex;
                            diger_ürün_serino_listbox.SelectedIndex = tekrarsec;
                            diger_ürün_serino_textbox.Focus();
                        }
                        else
                        {
                            if (diger_ürün_serino_textbox.Text.Length > 0 && diger_ürün_kodu_textbox.Text.Length > 0)
                            {
                                string yeniürünkodukayit = "insert into diger_ürün_kodları(diger_urun_adi,diger_urun_stok_kodu) values " + "" + "(@diger_urun_adi,@diger_urun_stok_kodu)";
                                SqlCommand kayitkomut = new SqlCommand(yeniürünkodukayit, connection);
                                kayitkomut.Parameters.AddWithValue("@diger_urun_adi", diger_ürün_adi_textbox.Text);
                                kayitkomut.Parameters.AddWithValue("@diger_urun_stok_kodu", diger_ürün_kodu_textbox.Text);
                                kayitkomut.ExecuteNonQuery();

                                string yeniürünserinokayit = "insert into diger_ürün_stok(diger_urun_adi,diger_urun_serino,diger_urun_adeti,diger_urun_durumu) values " + "" + "(@diger_urun_adi,@diger_urun_serino,@diger_urun_adeti,@diger_urun_durumu)";
                                SqlCommand yeniürünserinokayitkomut = new SqlCommand(yeniürünserinokayit, connection);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_adi", diger_ürün_adi_textbox.Text);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_serino", diger_ürün_serino_textbox.Text);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_adeti", "1");
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_durumu", "Kullanılmadı");
                                yeniürünserinokayitkomut.ExecuteNonQuery();

                                using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                                {
                                    Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + diger_ürün_adi_textbox.Text + " ürününün " + diger_ürün_serino_textbox.Text + " ürün kodunu ve " + diger_ürün_serino_textbox.Text + " yeni seri numarasını ekledi.", w);
                                }
                                using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                                {
                                    Kullanicigirisiform.DumpLog(r);
                                }

                                MessageBox.Show("Yeni ürün stok kodu ve ürün seri numarası başarılı bir şekilde eklendi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                int tekrarsec = diger_ürün_adi_listbox.SelectedIndex;
                                diger_ürün_adi_listbox.SelectedIndex = tekrarsec;
                                diger_ürün_adi_textbox.Focus();
                            }
                        }
                    }
                    else
                    {
                        if (diger_ürün_adi_listbox.Items.Contains(diger_ürün_adi_textbox.Text))
                        {
                            MessageBox.Show("Bu ürün adı mevcuttur. Başka ürün adı kullanınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_kodu_listbox.Items.Contains(diger_ürün_kodu_textbox.Text))
                        {
                            MessageBox.Show("Bu ürün kodu mevcuttur. Başka ürün kodu kullanınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_serino_listbox.Items.Contains(diger_ürün_serino_textbox.Text))
                        {
                            MessageBox.Show("Bu seri numarası mevcuttur. Başka seri numarası kullanınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (diger_ürün_adi_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün adını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string yeniürünnotebookkayit = "insert into diger_ürünler(diger_urun_adi) values " + "" + "(@diger_urun_adi)";
                            SqlCommand yeniürünnotebookkayitkomut = new SqlCommand(yeniürünnotebookkayit, connection);
                            yeniürünnotebookkayitkomut.Parameters.AddWithValue("@diger_urun_adi", diger_ürün_adi_textbox.Text);
                            yeniürünnotebookkayitkomut.ExecuteNonQuery();
                            diger_ürün_adi_textbox.Focus();

                            if (diger_ürün_kodu_textbox.Text.Length > 0)
                            {
                                string yeniürünkayit = "insert into diger_ürün_kodları(diger_urun_adi,diger_urun_stok_kodu) values " + "" + "(@diger_urun_adi,@diger_urun_stok_kodu)";
                                SqlCommand yeniürünkayitkomut = new SqlCommand(yeniürünkayit, connection);
                                yeniürünkayitkomut.Parameters.AddWithValue("@diger_urun_adi", diger_ürün_adi_textbox.Text);
                                yeniürünkayitkomut.Parameters.AddWithValue("@diger_urun_stok_kodu", diger_ürün_kodu_textbox.Text);
                                yeniürünkayitkomut.ExecuteNonQuery();
                            }
                            if (diger_ürün_serino_textbox.Text.Length > 0)
                            {
                                string yeniürünserinokayit = "insert into diger_ürün_stok(diger_urun_adi,diger_urun_serino,diger_urun_adeti,diger_urun_durumu) values " + "" + "(@diger_urun_adi,@diger_urun_serino,@diger_urun_adeti,@diger_urun_durumu)";
                                SqlCommand yeniürünserinokayitkomut = new SqlCommand(yeniürünserinokayit, connection);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_adi", diger_ürün_adi_textbox.Text);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_serino", diger_ürün_serino_textbox.Text);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_adeti", "1");
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@diger_urun_durumu", "Kullanılmadı");
                                yeniürünserinokayitkomut.ExecuteNonQuery();
                            }
                            using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + diger_ürün_adi_textbox.Text + " yeni ürünü ekledi.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }

                            MessageBox.Show("Yeni ürün oluşturuldu.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            diger_ürün_adi_textbox.Focus();
                        }
                    }
                }
                Listeyenile();
                connection.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün oluşturulurken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\n Hata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void Diger_ürün_serino_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (diger_ürün_serino_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Geçerli seri numarası seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                diger_ürün_serino_textbox.Text = diger_ürün_serino_listbox.SelectedItem.ToString();
            }
        }

       

        private void Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme_FormClosed(object sender, FormClosedEventArgs e)
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

        private void Ürünkalankullanımlabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                if (diger_ürün_adi_listbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Geçerli bir ürün seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlCommand komut3 = new SqlCommand("SELECT diger_urun_serino FROM diger_ürün_stok where diger_urun_durumu = 'Kullanılmadı' and diger_urun_adi = '" + diger_ürün_adi_listbox.SelectedItem.ToString() + "'", connection);
                    SqlDataReader veriokuyucu3;
                    veriokuyucu3 = komut3.ExecuteReader();
                    diger_ürün_serino_listbox.Items.Clear();
                    while (veriokuyucu3.Read())
                    {
                        diger_ürün_serino_listbox.Items.Add(veriokuyucu3["diger_urun_serino"]);
                    }
                    if (diger_ürün_serino_listbox.Items.Count < 1)
                    {
                        diger_ürün_serino_listbox.Items.Add("Kullanılacak ürün yok.");
                    }
                    veriokuyucu3.Close();
                    connection.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mail.google.com/");
        }

        #region forumharaketettirme
        new
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Diğer_malzeme_Grubları_Ürün_ekle_ve_Düzenleme_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }
        private void Panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }

        }

        private void Panel4_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Panel4_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }

        #endregion
        #region renkayarları
        private void Secili_ürünün_kod_seri_no_sil_btn_MouseMove(object sender, MouseEventArgs e)
        {
            secili_ürünün_kod_seri_no_sil_btn.BackColor = Color.DarkGreen;
        }

        private void Yeni_ürün_ekle_btn_MouseMove(object sender, MouseEventArgs e)
        {
           yeni_ürün_ekle_btn.BackColor = Color.DarkGreen;
        }

        private void Secili_ürünü_sil_btn_MouseMove(object sender, MouseEventArgs e)
        {
            secili_ürünü_sil_btn.BackColor = Color.DarkGreen;
        }

        private void Ana_menü_btn_MouseMove(object sender, MouseEventArgs e)
        {
            ana_menü_btn.BackColor = Color.DarkGreen;
        }

        private void Yeni_ürün_ekle_btn_MouseLeave(object sender, EventArgs e)
        {
            yeni_ürün_ekle_btn.BackColor = Color.MediumSeaGreen;
        }

        private void Secili_ürünün_kod_seri_no_sil_btn_MouseLeave(object sender, EventArgs e)
        {
             secili_ürünün_kod_seri_no_sil_btn.BackColor = Color.MediumSeaGreen;
        }

        private void Secili_ürünü_sil_btn_MouseLeave(object sender, EventArgs e)
        {
             secili_ürünü_sil_btn.BackColor = Color.MediumSeaGreen;
        }

        private void Ana_menü_btn_MouseLeave(object sender, EventArgs e)
        {
             ana_menü_btn.BackColor = Color.MediumSeaGreen;
        }
        private void Logout_label_MouseMove(object sender, MouseEventArgs e)
        {
            logout_label.ForeColor = Color.Black;
        }

        private void Windows_kücültme_label_MouseMove(object sender, MouseEventArgs e)
        {
            windows_kücültme_label.ForeColor = Color.Black;
        }

        private void Logout_label_MouseLeave(object sender, EventArgs e)
        {
            logout_label.ForeColor = Color.Gray;
        }

        private void Windows_kücültme_label_MouseLeave(object sender, EventArgs e)
        {
            windows_kücültme_label.ForeColor = Color.Gray;
        }
        #endregion
    }
}