using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace OSBilişim
{
    public partial class Siparisolusturmaform : Form
    {
        public Siparisolusturmaform()
        {
            InitializeComponent();

        }

        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.106,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        private void Siparisolusturmaform_Load(object sender, EventArgs e)
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
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Program başlatılmadı.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand komut = new SqlCommand("SELECT * FROM notebook_urunler ", connection);
                SqlDataReader veriokuyucu;
                veriokuyucu = komut.ExecuteReader();
                while (veriokuyucu.Read())
                {
                    ürünadi.Items.Add(veriokuyucu["urun_adi"]);
                }

                veriokuyucu.Close();
                SqlCommand komut2 = new SqlCommand("SELECT * FROM notebook_kullanilacak_malzemeler ", connection);
                SqlDataReader veriokuyucu2; ;
                veriokuyucu2 = komut2.ExecuteReader();
                while (veriokuyucu2.Read())
                {
                    kullanilacak_malzemeler_listbox.Items.Add(veriokuyucu2["malzeme_adi"]);
                }
                connection.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Server ile bağlantı kurulmadı.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Silbtn_Click(object sender, EventArgs e)
        {
            if (kullanilacak_malzemeler_listesi.SelectedIndex > -1)
                kullanilacak_malzemeler_listesi.Items.RemoveAt(kullanilacak_malzemeler_listesi.SelectedIndex);
            else if (kullanilacak_malzemeler_listesi.SelectedIndex == -1)
                MessageBox.Show("Kullanmayacağınız malzemeyi seçmeniz gerekiyor.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void Liste_temizle_Click(object sender, EventArgs e)
        {
            if (kullanilacak_malzemeler_listesi.Items.Count > 0)
            {
                DialogResult dialog = MessageBox.Show("Kullanılacak malzemeler listesini temizlemek istediğinize emin misiniiz?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    MessageBox.Show("Malzeme listesi temizlendi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    kullanilacak_malzemeler_listesi.Items.Clear();
                }
                else
                {
                    MessageBox.Show("Malzeme listesi temizlenmedi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Malzeme listesi temizlenemez çünkü liste boş!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Ürunadi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ürünadi.SelectedIndex == -1)
                {
                    ürünstokkodu.Items.Clear();
                    ürünserino_checklistbox.Items.Clear();
                }
                else
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        ürünstokkodu.Items.Clear();
                        ürünserino_checklistbox.Items.Clear();

                        SqlCommand komut3 = new SqlCommand("SELECT urun_kodu FROM notebook_urun_kodları where urun_adi ='" + ürünadi.SelectedItem + "'", connection);
                        SqlDataReader veriokuyucu3;
                        veriokuyucu3 = komut3.ExecuteReader();
                        while (veriokuyucu3.Read())
                        {
                            ürünstokkodu.Items.Add(veriokuyucu3["urun_kodu"]);

                        }
                        veriokuyucu3.Close();

                        ürünserino_checklistbox.Items.Clear();
                        SqlCommand üründurum = new SqlCommand("SELECT urun_seri_no FROM notebook_urun_seri_no_stok where urun_adi = '" + ürünadi.SelectedItem + "' AND urun_durumu = 'Kullanılmadı'", connection);
                        SqlDataReader üründurumusorgulama;
                        üründurumusorgulama = üründurum.ExecuteReader();
                        while (üründurumusorgulama.Read())
                        {
                            ürünserino_checklistbox.Items.Add(üründurumusorgulama["urun_seri_no"]);

                        }
                        if (ürünserino_checklistbox.Items.Count < 1)
                        {
                            ürünserino_checklistbox.Items.Clear();
                            ürünserino_checklistbox.Items.Add("Ürün kalmamıştır");
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Serverden ürün stok kodları çekilmedi lütfen tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Siparis_gonder_Click(object sender, EventArgs e)
        {
            if (ürünadi.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen geçerli bir ürün seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ürünstokkodu.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen geçerli bir ürün kodu seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ürünserino_checklistbox.CheckedItems.Count <= 0)
            {
                MessageBox.Show("Lütfen ürünün seri numarasını seçip devam ediniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if ((string)ürünserino_checklistbox.SelectedItem == "Ürün kalmamıştır")
            {
                MessageBox.Show("Ürün stoğu tükenmiştir, yeni ürün tedarik ediniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ürünadetitextbox.Text == "")
            {
                MessageBox.Show("Lütfen geçerli bir ürün adeti giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (kullanilacak_malzemeler_listesi.Items.Count < 1)
            {
                MessageBox.Show("Ürün için kullanılacak malzemeler eksik veya yoktur, lütfen geçerli malzeme ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (aliciaditextbox.Text == "")
            {
                MessageBox.Show("Ürünün alıcı adını giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (alicisoyaditextboxt.Text == "")
            {
                MessageBox.Show("Ürünün alıcı soyadını giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (satış_yapılan_firma.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen ürün hangi firma tarafından satılıyor seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ürünün_satıldığı_firma.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen ürün hangi firma adı altında satılıyor seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (sipariş_numarası_textbox.Text == "")
            {
                MessageBox.Show("Sipariş numarasını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show("Ürün siparişi oluşturulacaktır siparişin doğruluğunu onaylıyor musunuz?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    try
                    {
                        if (ürünün_satıldığı_firma.SelectedItem.ToString() == "Diğer")
                        {
                            using (var cn = new SqlConnection("server=192.168.1.106,1433;database=OSBİLİSİM;UId=Admin;Pwd=1;MultipleActiveResultSets=True;"))
                            using (var cmd = new SqlCommand(@"select max(try_cast(sip_no as int)) from siparisler where urunun_satildigi_firma = 'Diğer'", cn))
                            {
                                cn.Open();
                                var id = Convert.ToInt32(cmd.ExecuteScalar());
                                int sipno = id + 1;
                                if (id >= Convert.ToUInt16(sipariş_numarası_textbox.Text))
                                {
                                    MessageBox.Show($"Girdiğiniz sipariş numarası küçüktür, girebileceğiniz en düşük sipariş numarası: {id + 1}\nSizin için sipariş numarası otamatik olarak girilmiştir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    sipariş_numarası_textbox.Text = $"{id + 1}";
                                }
                                else if (Convert.ToInt16(sipariş_numarası_textbox.Text) > sipno)
                                {
                                    MessageBox.Show($"Girdiğiniz sipariş numarası büyüktür, girebileceğiniz en yüksek sipariş numarası: {id + 1}\nSizin için sipariş numarası otamatik olarak girilmiştir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    sipariş_numarası_textbox.Text = $"{id + 1}";
                                }
                                else
                                {
                                    PrintDialog printDialog1 = new PrintDialog
                                    {
                                        Document = printDocument1
                                    };
                                    DialogResult result = printDialog1.ShowDialog();
                                    if (result == DialogResult.OK)
                                    {
                                        printDocument1.Print();
                                    }
                                    if (connection.State == ConnectionState.Closed)
                                        connection.Open();
                                    string sipariskayit = "insert into siparisler(sip_no,urun_adi,urun_stok_kodu,urun_seri_no,urun_adeti,teslim_alacak_kisi_adi,teslim_alacak_kisi_soyadi,satis_yapilan_firma,urunun_satildigi_firma,kullanilacak_malzemeler,urun_hazirlik_durumu,urun_hakkinda_aciklama,siparis_tarihi) values " + "" + "(@sip_no,@urun_adi,@urun_stok_kodu,@urun_seri_no,@urun_adeti,@teslim_alacak_kisi_adi,@teslim_alacak_kisi_soyadi,@satis_yapilan_firma,@urunun_satildigi_firma,@kullanilacak_malzemeler,@urun_hazirlik_durumu,@urun_hakkinda_aciklama,@siparis_tarihi)";
                                    SqlCommand kayitkomut = new SqlCommand(sipariskayit, connection);
                                    kayitkomut.Parameters.AddWithValue("@sip_no", sipariş_numarası_textbox.Text);
                                    kayitkomut.Parameters.AddWithValue("@urun_adi", ürünadi.SelectedItem);
                                    kayitkomut.Parameters.AddWithValue("@urun_stok_kodu", ürünstokkodu.SelectedItem);
                                    kayitkomut.Parameters.AddWithValue("@urun_adeti", ürünadetitextbox.Text);
                                    kayitkomut.Parameters.AddWithValue("@teslim_alacak_kisi_adi", aliciaditextbox.Text);
                                    kayitkomut.Parameters.AddWithValue("@teslim_alacak_kisi_soyadi", alicisoyaditextboxt.Text);
                                    kayitkomut.Parameters.AddWithValue("@satis_yapilan_firma", satış_yapılan_firma.SelectedItem);
                                    kayitkomut.Parameters.AddWithValue("@urunun_satildigi_firma", ürünün_satıldığı_firma.SelectedItem);
                                    kayitkomut.Parameters.AddWithValue("@kullanilacak_malzemeler", kullanilacak_malzemeler_listesi.Items.Cast<string>().Aggregate((current, next) => $"{current} {"/"} {next}"));
                                    kayitkomut.Parameters.AddWithValue("@urun_hazirlik_durumu", ürün_hazirlik_durumu_textbox.Text);
                                    kayitkomut.Parameters.AddWithValue("@siparis_tarihi", DateTime.Now);
                                    string serino = "";
                                    foreach (object item in ürünserino_checklistbox.CheckedItems)
                                    {
                                        string checkedItem = item.ToString();
                                        serino = serino + checkedItem + " / ";
                                    }
                                    kayitkomut.Parameters.AddWithValue("@urun_seri_no", serino.Trim(new Char[] { ' ', '/', '.' }));
                                    if (aciklama_textbox.Text == "")
                                    {
                                        kayitkomut.Parameters.AddWithValue("@urun_hakkinda_aciklama", "Ürün hakkında açıklama girilmemiştir.");
                                    }
                                    else
                                    {
                                        kayitkomut.Parameters.AddWithValue("@urun_hakkinda_aciklama", aciklama_textbox.Text);
                                    }
                                    kayitkomut.ExecuteNonQuery();

                                    for (int i = 0; i < ürünserino_checklistbox.CheckedItems.Count; i++)
                                    {
                                        SqlCommand ürüngüncelle = new SqlCommand("update notebook_urun_seri_no_stok set urun_durumu = '" + "Ürün Kullanıldı" + "' where urun_seri_no = '" + ürünserino_checklistbox.CheckedItems[i] + "'", connection);
                                        ürüngüncelle.ExecuteNonQuery();
                                    }
                                    connection.Close();

                                   
                                    using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                                    {
                                        Kullanicigirisiform.Log(aliciaditextbox.Text + " " + alicisoyaditextboxt.Text + " adlı alıcı " + ürünadi.SelectedItem.ToString() + " ürününün " + serino.Trim(new Char[] { ' ', '/', '.' }) + " seri numarasını seçerek, " + Kullanicigirisiform.username + " tarafından alıcı için sipariş oluşturuldu.", w);
                                    }
                                    using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                                    {
                                        Kullanicigirisiform.DumpLog(r);
                                    }

                                    ürünadetitextbox.Text = "0";
                                    ürünadi.SelectedIndex = -1;
                                    ürünstokkodu.SelectedIndex = -1;
                                    kullanilacak_malzemeler_listesi.Items.Clear();
                                    aliciaditextbox.Text = "";
                                    alicisoyaditextboxt.Text = "";
                                    satış_yapılan_firma.SelectedIndex = -1;
                                    ürünün_satıldığı_firma.SelectedIndex = -1;
                                    kullanilacak_malzemeler_listbox.SelectedIndex = -1;
                                    kullanilacak_malzeme_adeti_textbox.Text = "0";
                                    aciklama_textbox.Text = "";
                                    ürünserino_checklistbox.Items.Clear();
                                    sipariş_numarası_textbox.Text = "";
                                    MessageBox.Show("Sipariş oluşturuldu, hazırlanmasını bekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            if (connection.State == ConnectionState.Closed)
                                connection.Open();
                            string sipariskayit = "insert into siparisler(sip_no,urun_adi,urun_stok_kodu,urun_seri_no,urun_adeti,teslim_alacak_kisi_adi,teslim_alacak_kisi_soyadi,satis_yapilan_firma,urunun_satildigi_firma,kullanilacak_malzemeler,urun_hazirlik_durumu,urun_hakkinda_aciklama,siparis_tarihi) values " + "" + "(@sip_no,@urun_adi,@urun_stok_kodu,@urun_seri_no,@urun_adeti,@teslim_alacak_kisi_adi,@teslim_alacak_kisi_soyadi,@satis_yapilan_firma,@urunun_satildigi_firma,@kullanilacak_malzemeler,@urun_hazirlik_durumu,@urun_hakkinda_aciklama,@siparis_tarihi)";
                            SqlCommand kayitkomut = new SqlCommand(sipariskayit, connection);
                            kayitkomut.Parameters.AddWithValue("@sip_no", sipariş_numarası_textbox.Text);
                            kayitkomut.Parameters.AddWithValue("@urun_adi", ürünadi.SelectedItem);
                            kayitkomut.Parameters.AddWithValue("@urun_stok_kodu", ürünstokkodu.SelectedItem);
                            kayitkomut.Parameters.AddWithValue("@urun_adeti", ürünadetitextbox.Text);
                            kayitkomut.Parameters.AddWithValue("@teslim_alacak_kisi_adi", aliciaditextbox.Text);
                            kayitkomut.Parameters.AddWithValue("@teslim_alacak_kisi_soyadi", alicisoyaditextboxt.Text);
                            kayitkomut.Parameters.AddWithValue("@satis_yapilan_firma", satış_yapılan_firma.SelectedItem);
                            kayitkomut.Parameters.AddWithValue("@urunun_satildigi_firma", ürünün_satıldığı_firma.SelectedItem);
                            kayitkomut.Parameters.AddWithValue("@kullanilacak_malzemeler", kullanilacak_malzemeler_listesi.Items.Cast<string>().Aggregate((current, next) => $"{current} {"/"} {next}"));
                            kayitkomut.Parameters.AddWithValue("@urun_hazirlik_durumu", ürün_hazirlik_durumu_textbox.Text);
                            kayitkomut.Parameters.AddWithValue("@siparis_tarihi", DateTime.Now);
                            string serino = "";
                            foreach (object item in ürünserino_checklistbox.CheckedItems)
                            {
                                string checkedItem = item.ToString();
                                serino = serino + checkedItem + " / ";
                            }
                            kayitkomut.Parameters.AddWithValue("@urun_seri_no", serino.Trim(new Char[] { ' ', '/', '.' }));
                            if (aciklama_textbox.Text == "")
                            {
                                kayitkomut.Parameters.AddWithValue("@urun_hakkinda_aciklama", "Ürün hakkında açıklama girilmemiştir.");
                            }
                            else
                            {
                                kayitkomut.Parameters.AddWithValue("@urun_hakkinda_aciklama", aciklama_textbox.Text);
                            }
                            kayitkomut.ExecuteNonQuery();

                            for (int i = 0; i < ürünserino_checklistbox.CheckedItems.Count; i++)
                            {
                                SqlCommand ürüngüncelle = new SqlCommand("update notebook_urun_seri_no_stok set urun_durumu = '" + "Ürün Kullanıldı" + "' where urun_seri_no = '" + ürünserino_checklistbox.CheckedItems[i] + "'", connection);
                                ürüngüncelle.ExecuteNonQuery();
                            }
                            connection.Close();

                            // LOG DOYASI //
                            using (StreamWriter w = File.AppendText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.Log(aliciaditextbox.Text + " " + alicisoyaditextboxt.Text + " adlı alıcı " + ürünadi.SelectedItem.ToString() + " ürününün " + serino.Trim(new Char[] { ' ', '/', '.' }) + " seri numarasını seçerek, " + Kullanicigirisiform.username + " tarafından alıcı için sipariş oluşturuldu.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.log"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }
                            // LOG DOSYASI //

                            ürünadetitextbox.Text = "0";
                            ürünadi.SelectedIndex = -1;
                            ürünstokkodu.SelectedIndex = -1;
                            kullanilacak_malzemeler_listesi.Items.Clear();
                            aliciaditextbox.Text = "";
                            alicisoyaditextboxt.Text = "";
                            satış_yapılan_firma.SelectedIndex = -1;
                            ürünün_satıldığı_firma.SelectedIndex = -1;
                            kullanilacak_malzemeler_listbox.SelectedIndex = -1;
                            kullanilacak_malzeme_adeti_textbox.Text = "0";
                            aciklama_textbox.Text = "";
                            ürünserino_checklistbox.Items.Clear();
                            sipariş_numarası_textbox.Text = "";
                            MessageBox.Show("Sipariş oluşturuldu, hazırlanmasını bekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception hata)
                    {
                        MessageBox.Show("Siparişiniz oluşturulmadı.\nİnternet bağlantınızı ya da server bağlantınızı kontrol ediniz.\n Hata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show("Sipariş oluşturulmadı, gerekli bilgileri tekrar tamamlayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            connection.Close();
        }
        private void Ürünadetitextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Ürün adeti sadece rakam & sayı ile giriş yapılabilir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { }
        }
        private void Malzeme_ekle_btn_Click(object sender, EventArgs e)
        {
            string kullanilackamalzemekontrol;
            if (kullanilacak_malzeme_adeti_textbox.Text == "")
            {
                if (kullanilacak_malzemeler_listbox.SelectedItem.ToString() == "Ürün orjinal hali ile gönderilecektir")
                {
                    if (kullanilacak_malzemeler_listesi.Items.Contains(kullanilacak_malzemeler_listbox.SelectedItem))
                    {
                        MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        kullanilacak_malzemeler_listesi.Items.Add(kullanilacak_malzemeler_listbox.SelectedItem);
                    }
                }
                else
                {
                    MessageBox.Show("Kullanılacak malzeme adetini giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else if (kullanilacak_malzemeler_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen geçerli malzeme seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (kullanilacak_malzemeler_listbox.Text == "")
            {
                MessageBox.Show("Lütfen geçerli malzeme seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToInt32(kullanilacak_malzeme_adeti_textbox.Text) < 1)
            {
                if (kullanilacak_malzemeler_listbox.SelectedItem.ToString() == "Ürün orjinal hali ile gönderilecektir")
                {
                    if (kullanilacak_malzemeler_listesi.Items.Contains(kullanilacak_malzemeler_listbox.SelectedItem))
                    {
                        MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        kullanilacak_malzemeler_listesi.Items.Add(kullanilacak_malzemeler_listbox.SelectedItem);
                    }
                }
                else
                {
                    MessageBox.Show("Kullanılacak malzeme adeti 1'den küçük olamaz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string mouse = "Mouse";
                string canta = "Çanta";
                string lisans = "Windows";

                foreach (string item in kullanilacak_malzemeler_listbox.SelectedItems)
                {
                    if (item.ToLower().Contains(mouse.ToLower()))
                    {
                        kullanilackamalzemekontrol = kullanilacak_malzemeler_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Verildi";
                        if (kullanilacak_malzemeler_listesi.Items.Contains(kullanilackamalzemekontrol))
                        {
                            MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            kullanilacak_malzemeler_listesi.Items.Add(kullanilacak_malzemeler_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Verildi");
                        }

                    }
                    else if (item.ToLower().Contains(canta.ToLower()))
                    {
                        kullanilackamalzemekontrol = kullanilacak_malzemeler_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Verildi";
                        if (kullanilacak_malzemeler_listesi.Items.Contains(kullanilackamalzemekontrol))
                        {
                            MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            kullanilacak_malzemeler_listesi.Items.Add(kullanilacak_malzemeler_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Verildi");
                        }
                    }
                    else if (item.ToLower().Contains(lisans.ToLower()))
                    {
                        kullanilackamalzemekontrol = kullanilacak_malzemeler_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Kullanıldı";
                        if (kullanilacak_malzemeler_listesi.Items.Contains(kullanilackamalzemekontrol))
                        {
                            MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            kullanilacak_malzemeler_listesi.Items.Add(kullanilacak_malzemeler_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Kullanıldı");
                        }
                    }
                    else
                    {
                        kullanilackamalzemekontrol = kullanilacak_malzemeler_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Takılacak";
                        if (kullanilacak_malzemeler_listesi.Items.Contains(kullanilackamalzemekontrol))
                        {
                            MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            kullanilacak_malzemeler_listesi.Items.Add(kullanilacak_malzemeler_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Takılacak");
                        }
                    }
                }

            }
        }
        private void Aliciaditextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                MessageBox.Show("Alıcı adı sadece harf ile giriş yapılabilir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { }
        }

        private void Alicisoyaditextboxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                MessageBox.Show("Alıcı soyadı sadece harf ile giriş yapılabilir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { }
        }
        private void Ana_menü_btn_Click(object sender, EventArgs e)
        {
            Anaform anaform = new Anaform();
            anaform.Show();
            Hide();
        }
        private void Siparisolusturmaform_FormClosed(object sender, FormClosedEventArgs e)
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
                MessageBox.Show("Kullanıcı bilgileri çekilmedi tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + kullaniciaktifligi.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            foreach (var process in Process.GetProcessesByName("OSBilişim"))
            {
                process.Kill();
            }
        }
        public string urunkontrol;
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
        private void Windows_kücültme_label_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Kullanilacak_malzeme_adeti_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                MessageBox.Show("Ürün adeti sadece rakam & sayı ile giriş yapılabilir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { }
        }
        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Font myFont = new Font("Calibri", 20, FontStyle.Bold);
                Font tarihfont = new Font("Calibri", 8, FontStyle.Bold);
                SolidBrush sbrush = new SolidBrush(Color.Black);
                Pen myPen = new Pen(Color.Black);
                StringFormat ortala = new StringFormat
                {
                    Alignment = StringAlignment.Center
                };

                e.Graphics.DrawRectangle(myPen, 10, 10, 785, 160);
                e.Graphics.DrawImage(Properties.Resources.footer, 15, 15, 220, 150);
                e.Graphics.DrawLine(myPen, new Point(250, 170), new Point(250, 10));
                e.Graphics.DrawString("ÜRÜN/HİZMET\nSİPARİŞ FORMU", myFont, sbrush, 320, 55);
                e.Graphics.DrawLine(myPen, new Point(570, 170), new Point(570, 10));
                e.Graphics.DrawString($"Oruç Reis Mah. Tekstil Kent Cad. Tekstilkent Sit.\nA 13 Blok No:63 Esenler / İSTANBUL\n+90 531 263 89 16\nwww.osbilisim.com.tr\ninfo@osbilisim.com.tr\nTarih: {DateTime.Now:dd.MM.yyyy HH:mm:ss} ", tarihfont, sbrush, 683, 55, ortala);

                myFont = new Font("Calibri", 20, FontStyle.Bold);
                e.Graphics.DrawString("MÜŞTERİ BİLGİLERİ", myFont, sbrush, 300, 200);
                myFont = new Font("Calibri", 12, FontStyle.Bold);
                e.Graphics.DrawRectangle(myPen, 15, 240, 785, 35);
                e.Graphics.DrawString("Müşteri Adı:", myFont, sbrush, 250, 250);
                e.Graphics.DrawRectangle(myPen, 15, 275, 785, 35);
                e.Graphics.DrawString("Sipariş No:", myFont, sbrush, 250, 285);
                e.Graphics.DrawRectangle(myPen, 15, 310, 785, 35);
                e.Graphics.DrawString("Sipariş Tarihi:", myFont, sbrush, 250, 320);


                e.Graphics.DrawString(aliciaditextbox.Text.ToUpper() + " " + alicisoyaditextboxt.Text.ToUpper(), myFont, sbrush, 342, 250);
                e.Graphics.DrawString(sipariş_numarası_textbox.Text.ToUpper(), myFont, sbrush, 329, 285);
                e.Graphics.DrawString($"{ DateTime.Now:dd.MM.yyyy HH:mm}", myFont, sbrush, 348, 320);

                myFont = new Font("Calibri", 20, FontStyle.Bold);
                e.Graphics.DrawString("ÜRÜN BİLGİLERİ", myFont, sbrush, 300, 355);
                myFont = new Font("Calibri", 12, FontStyle.Bold);
                e.Graphics.DrawRectangle(myPen, 15, 395, 785, 35);
                e.Graphics.DrawString("Ürün Adı:", myFont, sbrush, 250, 405);
                e.Graphics.DrawRectangle(myPen, 15, 430, 785, 35);
                e.Graphics.DrawString("Ürün Stok Kodu:", myFont, sbrush, 250, 440);
                e.Graphics.DrawRectangle(myPen, 15, 465, 785, 35);
                e.Graphics.DrawString("Ürün Seri No:", myFont, sbrush, 250, 475);
                e.Graphics.DrawRectangle(myPen, 15, 500, 785, 35);
                e.Graphics.DrawString("Ürün Adeti:", myFont, sbrush, 250, 510);

                e.Graphics.DrawString(ürünadi.SelectedItem.ToString().ToUpper(), myFont, sbrush, 322, 405);
                e.Graphics.DrawString(ürünstokkodu.SelectedItem.ToString().ToUpper(), myFont, sbrush, 371, 440);
                string serino = "";
                foreach (object item in ürünserino_checklistbox.CheckedItems)
                {
                    string checkedItem = item.ToString();
                    serino = serino + checkedItem + " / ";
                }
                e.Graphics.DrawString(serino.ToUpper().Trim(new Char[] { ' ', '/', '.' }), myFont, sbrush, 348, 475);
                e.Graphics.DrawString(ürünadetitextbox.Text, myFont, sbrush, 336, 510);


                e.Graphics.DrawRectangle(myPen, 15, 587, 785, 200);
                e.Graphics.DrawString("Teslim Eden", myFont, sbrush, 93, 600);
                e.Graphics.DrawString("Teslim Alan", myFont, sbrush, 600, 600);
            }
            catch (Exception hata)
            {
                MessageBox.Show("Yazıcı çıktısı alınırken bir hata oluştu.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mail.google.com/");
        }
        new
        #region forumharaketettirme
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Siparisolusturmaform_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Siparisolusturmaform_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Siparisolusturmaform_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }
        #endregion
        #region forumharaketettirme2 
        private void Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }
        private void Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }



        #endregion
        #region renkayarı
        private void Silbtn_MouseMove(object sender, MouseEventArgs e)
        {
            silbtn.BackColor = Color.DarkGreen;
        }

        private void Liste_temizle_MouseMove(object sender, MouseEventArgs e)
        {
            liste_temizle.BackColor = Color.DarkGreen;
        }

        private void Siparis_gonder_MouseMove(object sender, MouseEventArgs e)
        {
            siparis_gonder.BackColor = Color.DarkGreen;
        }

        private void Malzeme_ekle_btn_MouseMove(object sender, MouseEventArgs e)
        {
            malzeme_ekle_btn.BackColor = Color.DarkGreen;
        }

        private void Ana_menü_btn_MouseMove(object sender, MouseEventArgs e)
        {
            ana_menü_btn.BackColor = Color.DarkGreen;
        }

        private void Liste_temizle_MouseLeave(object sender, EventArgs e)
        {
            liste_temizle.BackColor = Color.MediumSeaGreen;
        }

        private void Silbtn_MouseLeave(object sender, EventArgs e)
        {
            silbtn.BackColor = Color.MediumSeaGreen;
        }

        private void Siparis_gonder_MouseLeave(object sender, EventArgs e)
        {
            siparis_gonder.BackColor = Color.MediumSeaGreen;
        }

        private void Malzeme_ekle_btn_MouseLeave(object sender, EventArgs e)
        {
            malzeme_ekle_btn.BackColor = Color.MediumSeaGreen;
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