using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Linq;

namespace OSBilişim
{
    public partial class Sipariskontrolform : Form
    {
        public static int lastId = -1;
        public Sipariskontrolform()
        {
            InitializeComponent();
        }

        private void Sipariskontrol_FormClosed(object sender, FormClosedEventArgs e)
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
        private readonly string kelime = "SN: ";
        private readonly string orjinal = "Bu üründen parça çıkartılmayacaktır";
        private readonly string canta = "Çanta";
        private readonly string lisans = "Windows";
        private bool snkontrol = true;
        private void Kullanılacakürünlerinserinokontrolü()
        {
            foreach (string item in kullanilacak_malzemeler_listbox.Items)
            {

                if (item.ToLower().Contains(kelime.ToLower()))
                {
                    snkontrol = false;
                }
                else if (item.ToLower().Contains(orjinal.ToLower()))
                {
                    snkontrol = false;
                }
                else if (item.ToLower().Contains(canta.ToLower()))
                {
                    snkontrol = false;
                }
                else if (item.ToLower().Contains(lisans.ToLower()))
                {
                    snkontrol = false;
                }
                else
                {
                    snkontrol = true;
                    break;
                }
            }
        }
        public void Siparisonayla()
        {
            var row = ((DataTable)sipariskontrolview.DataSource).Rows[sipariskontrolview.CurrentRow.Index];
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                    if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Beklemede")
                    {
                        Kullanılacakürünlerinserinokontrolü();
                        if (snkontrol == false)
                        {
                            if (çıkacak_olan_parçalar_listesi_listbox.Items.Count > 0)
                            {

                                SqlDataAdapter komut = new SqlDataAdapter("select count(*) from siparisler where siparis_id = '" + (int)row["siparis_id"] + "'", connection);
                                DataTable dtablo = new DataTable();
                                komut.Fill(dtablo);
                                if (dtablo.Rows[0][0].ToString() == "1")
                                {
                                    DialogResult dialog = new DialogResult();
                                    dialog = MessageBox.Show("Çıkarılan malzemeler doğruluğunu onaylayıp eklemek istiyor musunuz?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        //KULLANILAN MALZEMELERİ SİSTEME AKTARMA
                                        string çıkarılanparcalarlistesi = çıkacak_olan_parçalar_listesi_listbox.Items.Cast<string>().Aggregate((current, next) => $"{current} {"/"} {next}");
                                        SqlCommand ürüncikarilanparcalarıgüncelle = new SqlCommand("update siparisler set urun_cikarilan_parcalar = '" + çıkarılanparcalarlistesi + "' where siparis_id = '" + (int)row["siparis_id"] + "'", connection);
                                        ürüncikarilanparcalarıgüncelle.ExecuteNonQuery();


                                        // KULLANILACAK MALZEMELERİN SERİ NUMARALARI GİRİLDİKTEN SONRA SİSTEME AKTARMA
                                        string kullanilacakparçalarlistesi = kullanilacak_malzemeler_listbox.Items.Cast<string>().Aggregate((current, next) => $"{current} {"/"} {next}");
                                        SqlCommand kullanilanparcalarigüncelle = new SqlCommand("update siparisler set kullanilacak_malzemeler = '" + kullanilacakparçalarlistesi + "' where siparis_id = '" +(int)row["siparis_id"] + "'", connection);
                                        kullanilanparcalarigüncelle.ExecuteNonQuery();



                                        // LOG DOYASI //
                                        using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                                        {
                                            Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı tarafından hazırlanan ürün: " + ürünaditextbox.Text + " / " + ürünstokkodutextbox.Text + " / " + "SN: " + ürün_seri_no_textbox.Text, w);
                                        }
                                        using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                        {
                                            Kullanicigirisiform.DumpLog(r);
                                        }
                                        using (StreamWriter x = File.AppendText("OSBilisim-log.xml"))
                                        {
                                            Kullanicigirisiform.Parcalarlog("-------------------------------", x);
                                            Kullanicigirisiform.Parcalarlog("Sipariş için takılacak parçalar; ", x);
                                            Kullanicigirisiform.Parcalarlog("-------------------------------", x);
                                        }
                                        using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                        {
                                            Kullanicigirisiform.DumpLog(r);
                                        }
                                        for (int i = 0; kullanilacak_malzemeler_listbox.Items.Count > i; i++)
                                        {
                                            using (StreamWriter x = File.AppendText("OSBilisim-log.xml"))
                                            {
                                                Kullanicigirisiform.Parcalarlog(kullanilacak_malzemeler_listbox.Items[i].ToString(), x);
                                            }
                                            using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                            {
                                                Kullanicigirisiform.DumpLog(r);
                                            }
                                        }
                                        using (StreamWriter x = File.AppendText("OSBilisim-log.xml"))
                                        {
                                            Kullanicigirisiform.Parcalarlog("-------------------------------", x);
                                            Kullanicigirisiform.Parcalarlog("Sipariş için çıkarılacak parçalar; ", x);
                                            Kullanicigirisiform.Parcalarlog("-------------------------------", x);
                                        }
                                        using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                        {
                                            Kullanicigirisiform.DumpLog(r);
                                        }
                                        for (int i = 0; çıkacak_olan_parçalar_listesi_listbox.Items.Count > i; i++)
                                        {
                                            using (StreamWriter x = File.AppendText("OSBilisim-log.xml"))
                                            {
                                                Kullanicigirisiform.Parcalarlog(çıkacak_olan_parçalar_listesi_listbox.Items[i].ToString(), x);


                                            }
                                            using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                            {
                                                Kullanicigirisiform.DumpLog(r);
                                            }
                                        }
                                        using (StreamWriter x = File.AppendText("OSBilisim-log.xml"))
                                        {
                                            Kullanicigirisiform.Parcalarlog("-------------------------------", x);
                                        }
                                        using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                        {
                                            Kullanicigirisiform.DumpLog(r);
                                        }
                                        // LOG DOSYASI //

                                        MessageBox.Show("Çıkarılan parçaların seri numarası başarılı şekilde eklendi.\nGörmek için sipariş listesini güncelleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        MessageBox.Show("Kullanacağınız parçanın seri numarası başarılı şekilde eklendi.\nGörmek için sipariş listesini güncelleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        // SİPARİŞ ONAYLANIRSA SİPARİŞ DURUMUNU DEĞİŞTİRME
                                        SqlCommand siparisdurumunugüncelle = new SqlCommand("update siparisler set urun_hazirlik_durumu = '" + "Sipariş Gönderim İçin Hazır" + "' where siparis_id = '" + (int)row["siparis_id"] + "'", connection);

                                        siparisdurumunugüncelle.ExecuteNonQuery();
                                        var items = kullanilacak_malzemeler_listbox.Items.Cast<string>();
                                        var pattern = @"SN: .+";
                                        var result = items
                                            .Where(i => Regex.IsMatch(i, pattern))
                                            .Select(i => Regex.Match(i, pattern).Value.Remove(0, "SN: ".Length))
                                            .SelectMany(x => x.Split('-'));
                                        using (var cmd = new SqlCommand(@"update diger_ürün_stok set diger_urun_durumu = 'Ürün Kullanıldı' where diger_urun_serino = @sn", connection))
                                        {
                                            cmd.Parameters.Add("@sn", SqlDbType.VarChar);
                                            foreach (var sn in result)
                                            {
                                                cmd.Parameters["@sn"].Value = sn;
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("İsteğiniz üzere parçalar sisteme eklenmedi. Kontrol edip tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Parçalar sisteme eklenirken bir sorun oluştu daha sonra tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Çıkarttığınız bir parça bulunmamaktadır, çıkartılan parçaları ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Takılan ya da çıkarılan parçalar arasında seri numaraları girilmemiş parçalar mevcut, seri numaraları girip tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    Siparisgetir();
                    connection.Close();
                }
                else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Onaylandı")
                {

                    MessageBox.Show("Sipariş onaylandı. Malzeme çıkartmak için çok geç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Gönderim İçin Hazır")
                {
                    MessageBox.Show("Sipariş gönderim için hazırlandı. Malzeme çıkartmak için çok geç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş İade")
                {
                    MessageBox.Show("Sipariş iade olmuş. Ürünü orjinal haline getirip siparişin açıklamasını değiştirebilirsin.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Arızalı")
                {
                    MessageBox.Show("Sipariş arızalı. Ürünü orjinal haline getirip siparişin açıklmasını değiştirebilirsin.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sipariş onaylanırken bir hata oluştu lütfen tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        public void Siparisgetir()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {

                    connection.Open();
                    SqlDataAdapter komut = new SqlDataAdapter("select * from siparisler", connection);
                    DataTable datagorev = new DataTable();
                    komut.Fill(datagorev);
                    sipariskontrolview.DataSource = datagorev;
                    connection.Close();


                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sipariş listesi çekilirken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.132,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        private void Sipariskontrol_Load(object sender, EventArgs e)
        {
            string connectionString = connection.ConnectionString;
            var maxId = new DataContext(connectionString)
                   .ExecuteQuery<int?>("select max(siparis_id) from siparisler")
                   .First();
            lastId = maxId == null ? -1 : (int)maxId;

            var siparisControlTimer = new System.Timers.Timer( 5 * 1000) { Enabled = false };
            siparisControlTimer.Elapsed += Handle_SiparisControl;
            siparisControlTimer.Start();
            //MessageBox.Show($"Son ID:{lastId}");
            üründurumukontrol = false;
            timer1.Start();

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
                            File.WriteAllBytes(@"OSUpdate.exe", new System.Net.WebClient().DownloadData("http://192.168.1.132/Update/OSUpdate.exe"));
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
                MessageBox.Show("Program başlatılmadı.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                SqlCommand komut = new SqlCommand("SELECT * FROM notebook_cikartilacak_malzemeler ", connection);
                SqlDataReader veriokuyucu;

                veriokuyucu = komut.ExecuteReader();
                while (veriokuyucu.Read())
                {
                    çıkacak_olan_parçalar_listbox.Items.Add(veriokuyucu["malzeme_adi"]);
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("Çıkarılan parçalar listesi çekilirken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            connection.Close();
            SipariskontrolviewSetting(sipariskontrolview);
            Siparisgetir();
        }

        public void Handle_SiparisControl(object sender, System.Timers.ElapsedEventArgs e)
        {
            string connectionString = "server=192.168.1.132,1433;database=OSBİLİSİM;UId=Admin;Pwd=1;MultipleActiveResultSets=True;";

            var siparisler = new DataContext(connectionString)
                   .ExecuteQuery<int>("select siparis_id from siparisler where siparis_id > {0}", lastId)
                   .ToList();
            //MessageBox.Show($"Siparis sayisi:{siparisler.Count()}");

            if (siparisler.Count() > 0)
            {
                lastId = siparisler.Max(s => s);
                NotifyIcon trayIcon = new NotifyIcon
                {
                    Icon = new Icon(@"alt-logo.ico"),
                    Text = "OS BİLİŞİM",
                    Visible = true
                };
                trayIcon.ShowBalloonTip(1000, "Bilgi", "Yeni sipariş bulunuyor, sipariş listesini güncelleyin!", ToolTipIcon.Info);
            }
        }

        public void SipariskontrolviewSetting(DataGridView dataGridView)
        {
            if (dataGridView is null)
            {
                throw new ArgumentNullException(nameof(dataGridView));
            }

            sipariskontrolview.BorderStyle = BorderStyle.None;
            sipariskontrolview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(118, 195, 215);
            sipariskontrolview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            sipariskontrolview.DefaultCellStyle.SelectionForeColor = Color.White;
            sipariskontrolview.EnableHeadersVisualStyles = false;
            sipariskontrolview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            sipariskontrolview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(118, 195, 215);
            sipariskontrolview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            sipariskontrolview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            sipariskontrolview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void Sipariskontrolview_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var row = ((DataTable)sipariskontrolview.DataSource).Rows[sipariskontrolview.CurrentRow.Index];

            siparis_numarasi_textbox.Text = row["sip_no"].ToString();
            ürünaditextbox.Text = row["urun_adi"].ToString();
            ürünstokkodutextbox.Text = row["urun_stok_kodu"].ToString();
            ürün_seri_no_textbox.Text = row["urun_seri_no"].ToString();
            ürünadetitextbox.Text = row["urun_adeti"].ToString();
            aliciaditextbox.Text = row["teslim_alacak_kisi_adi"].ToString();
            alicisoyaditextboxt.Text = row["teslim_alacak_kisi_soyadi"].ToString();
            satisyapilanfirmatextbox.Text = row["satis_yapilan_firma"].ToString();
            ürününsatildigifirmatextbox.Text = row["urunun_satildigi_firma"].ToString();
            ürünhazirlikdurumu_combobox.SelectedItem = row["urun_hazirlik_durumu"].ToString();
            aciklama_textbox.Text = row["urun_hakkinda_aciklama"].ToString();
            satis_tarihi_textbox.Text = row["siparis_tarihi"].ToString();
            try
            {
                çıkacak_olan_parçalar_listesi_listbox.Items.Clear();

                string cikmisparcalar = null;
                string kullanilacakmalzemeler1 = null;
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                using (var komut3 = new SqlCommand("SELECT kullanilacak_malzemeler, urun_cikarilan_parcalar FROM siparisler where siparis_id = @sipId", connection))
                {
                    //komut3.Parameters.Add("@sipId", SqlDbType.Int).Value = sipariskontrolview.CurrentRow.Cells["siparis_id"].Value;
                    
                    komut3.Parameters.Add("@sipId", SqlDbType.Int).Value = (int)row["siparis_id"];
                    // yapılacak ^^


                    var veriokuyucu3 = komut3.ExecuteReader();
                    if (veriokuyucu3.Read())
                    {
                        if (veriokuyucu3["urun_cikarilan_parcalar"] != DBNull.Value)
                        {
                            cikmisparcalar = (string)veriokuyucu3["urun_cikarilan_parcalar"];
                        }
                        kullanilacakmalzemeler1 = (string)veriokuyucu3["kullanilacak_malzemeler"];
                    }
                    connection.Close();
                    veriokuyucu3.Close();
                }

                if (!string.IsNullOrEmpty(cikmisparcalar))
                {
                    string[] cikacakparcalar = cikmisparcalar.Split('/');
                    foreach (var cikacakparca in cikacakparcalar)
                    {
                        çıkacak_olan_parçalar_listesi_listbox.Items.Add(cikacakparca.Trim());
                    }
                }
                else
                {
                    çıkacak_olan_parçalar_listesi_listbox.Items.Clear();
                }
                kullanilacak_malzemeler_listbox.Items.Clear();
                string[] kelimeler1 = kullanilacakmalzemeler1.Split('/');
                foreach (var kelime in kelimeler1)
                {
                    kullanilacak_malzemeler_listbox.Items.Add(kelime.Trim());
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün malzemeleri çekilirken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void Ürünhazirlikdurumu_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ürünhazirlikdurumu_combobox.Text == "Sipariş Onaylandı")
            {
                kullanilacak_malzeme_adeti_textbox.ReadOnly = true;
                cikacakürün_serino_textbox.ReadOnly = true;
                çıkacak_olan_parçalar_listesi_listbox.Enabled = false;
                malzeme_ekle_btn.Enabled = false;
                Siparisi_onayla_btn.Enabled = false;
                kullanilmayacak_malzemeyi_sil_btn.Enabled = false;
                aciklama_textbox.ReadOnly = false;
                siparis_aciklama_güncelleme_btn.Enabled = true;
                kullanilacak_malzemeler_seri_no_textbox.ReadOnly = true;
                kullanilacak_malzeme_serino_ekle_btn.Enabled = false;
                kullanilacak_malzemeler_listbox.Enabled = false;
                çıkacak_olan_parçalar_listbox.Enabled = false;
            }
            else if (ürünhazirlikdurumu_combobox.Text == "Sipariş Gönderim İçin Hazır")
            {

                kullanilacak_malzeme_adeti_textbox.ReadOnly = true;
                cikacakürün_serino_textbox.ReadOnly = true;
                çıkacak_olan_parçalar_listesi_listbox.Enabled = false;
                malzeme_ekle_btn.Enabled = false;
                Siparisi_onayla_btn.Enabled = false;
                kullanilmayacak_malzemeyi_sil_btn.Enabled = false;
                aciklama_textbox.ReadOnly = false;
                siparis_aciklama_güncelleme_btn.Enabled = true;
                kullanilacak_malzemeler_seri_no_textbox.ReadOnly = true;
                kullanilacak_malzeme_serino_ekle_btn.Enabled = false;
                kullanilacak_malzemeler_listbox.Enabled = false;
                çıkacak_olan_parçalar_listbox.Enabled = false;
            }
            else if (ürünhazirlikdurumu_combobox.Text == "Sipariş Arızalı")
            {
                kullanilacak_malzeme_adeti_textbox.ReadOnly = true;
                cikacakürün_serino_textbox.ReadOnly = true;
                çıkacak_olan_parçalar_listesi_listbox.Enabled = false;
                malzeme_ekle_btn.Enabled = false;
                Siparisi_onayla_btn.Enabled = false;
                kullanilmayacak_malzemeyi_sil_btn.Enabled = false;
                aciklama_textbox.ReadOnly = false;
                siparis_aciklama_güncelleme_btn.Enabled = true;
                kullanilacak_malzemeler_seri_no_textbox.ReadOnly = true;
                kullanilacak_malzeme_serino_ekle_btn.Enabled = false;
                kullanilacak_malzemeler_listbox.Enabled = false;
                çıkacak_olan_parçalar_listbox.Enabled = false;
            }
            else if (ürünhazirlikdurumu_combobox.Text == "Sipariş İade")
            {
                kullanilacak_malzeme_adeti_textbox.ReadOnly = true;
                cikacakürün_serino_textbox.ReadOnly = true;
                çıkacak_olan_parçalar_listesi_listbox.Enabled = false;
                malzeme_ekle_btn.Enabled = false;
                Siparisi_onayla_btn.Enabled = false;
                kullanilmayacak_malzemeyi_sil_btn.Enabled = false;
                aciklama_textbox.ReadOnly = false;
                siparis_aciklama_güncelleme_btn.Enabled = true;
                kullanilacak_malzemeler_seri_no_textbox.ReadOnly = true;
                kullanilacak_malzeme_serino_ekle_btn.Enabled = false;
                kullanilacak_malzemeler_listbox.Enabled = false;
                çıkacak_olan_parçalar_listbox.Enabled = false;
            }
            else if (ürünhazirlikdurumu_combobox.Text == "Sipariş Beklemede")
            {
                kullanilacak_malzeme_adeti_textbox.ReadOnly = false;
                cikacakürün_serino_textbox.ReadOnly = false;
                çıkacak_olan_parçalar_listesi_listbox.Enabled = true;
                malzeme_ekle_btn.Enabled = true;
                Siparisi_onayla_btn.Enabled = true;
                kullanilmayacak_malzemeyi_sil_btn.Enabled = true;
                aciklama_textbox.ReadOnly = true;
                siparis_aciklama_güncelleme_btn.Enabled = false;
                kullanilacak_malzemeler_seri_no_textbox.ReadOnly = false;
                kullanilacak_malzeme_serino_ekle_btn.Enabled = true;
                kullanilacak_malzemeler_listbox.Enabled = true;
                çıkacak_olan_parçalar_listbox.Enabled = true;
            }
        }
        private void Siparis_listesi_güncelle_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int siparisadeti, güncelsiparisadet, günceltoplam = 0, toplam = 0;
                for (siparisadeti = 0; sipariskontrolview.Rows.Count > siparisadeti; siparisadeti++)
                {
                    toplam += siparisadeti;
                }
                Siparisgetir();
                for (güncelsiparisadet = 0; sipariskontrolview.Rows.Count > güncelsiparisadet; güncelsiparisadet++)
                {
                    günceltoplam += güncelsiparisadet;
                }
                if (toplam < günceltoplam)
                {
                    NotifyIcon trayIcon = new NotifyIcon
                    {
                        Icon = new Icon(@"alt-logo.ico"),
                        Text = "OS BİLİŞİM",
                        Visible = true
                    };
                    trayIcon.ShowBalloonTip(1, "Bilgi", "Yeni bir sipariş geldi, lütfen kontrol ediniz.", ToolTipIcon.Info);

                }
                ürünaditextbox.Text = "";
                ürünadetitextbox.Text = "0";
                ürünstokkodutextbox.Text = "";
                ürünadetitextbox.Text = "";
                kullanilacak_malzemeler_listbox.Items.Clear();
                aliciaditextbox.Text = "";
                alicisoyaditextboxt.Text = "";
                aciklama_textbox.Text = "";
                satisyapilanfirmatextbox.Text = "";
                ürününsatildigifirmatextbox.Text = "";
                çıkacak_olan_parçalar_listesi_listbox.Items.Clear();
                çıkacak_olan_parçalar_listbox.SelectedIndex = -1;
                ürünhazirlikdurumu_combobox.SelectedIndex = -1;
                üründurumunugüncelle_combobox.SelectedIndex = -1;
                ürün_seri_no_textbox.Text = "";
                ürünadetitextbox.Text = "";
                kullanilacak_malzeme_adeti_textbox.Text = "";
                cikacakürün_serino_textbox.Text = "";
                kullanilacak_malzemeler_seri_no_textbox.Text = "";
                satis_tarihi_textbox.Text = "";
                siparis_numarasi_textbox.Text = "";
                
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün listesi yenilenirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void Siparis_sil_btn_Click(object sender, EventArgs e)
        {
            var row = ((DataTable)sipariskontrolview.DataSource).Rows[sipariskontrolview.CurrentRow.Index];

            DialogResult dialog = MessageBox.Show("Sipariş silinecektir onaylıyor musunuz?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        string kullanilacakmalzemeler1 = ürün_seri_no_textbox.Text;
                        string[] kelimeler1 = kullanilacakmalzemeler1.Split('/');
                        foreach (var kelime in kelimeler1)
                        {
                            SqlCommand ürünserinogüncelle = new SqlCommand("update notebook_urun_seri_no_stok set urun_durumu = '" + "Kullanılmadı" + "' where urun_seri_no = '" + kelime.Trim() + "'", connection);
                            ürünserinogüncelle.ExecuteNonQuery();
                            SqlCommand digerürünserinogüncelle = new SqlCommand("update diger_ürün_stok set diger_urun_durumu = '" + "Kullanılmadı" + "' where diger_urun_serino = '" + kelime.Trim() + "'", connection);
                            digerürünserinogüncelle.ExecuteNonQuery();
                        }

                        SqlCommand verisil = new SqlCommand("delete from siparisler where siparis_id = '" + (int)row["siparis_id"] + "'", connection);
                        verisil.ExecuteNonQuery();

                        MessageBox.Show("Siparişiniz başarılı ile silindi ve stok olarak geri eklendi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // LOG DOYASI //
                        using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                        {
                            Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürünaditextbox.Text + " / " + ürün_seri_no_textbox.Text + " / " + ürünstokkodutextbox.Text + " ürünün siparişini sildi.", w);
                        }
                        using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                        {
                            Kullanicigirisiform.DumpLog(r);
                        }
                        // LOG DOSYASI //

                        connection.Close();
                        ürünaditextbox.Text = "";
                        ürünadetitextbox.Text = "0";
                        ürünstokkodutextbox.Text = "";
                        ürünadetitextbox.Text = "";
                        kullanilacak_malzemeler_listbox.Items.Clear();
                        aliciaditextbox.Text = "";
                        alicisoyaditextboxt.Text = "";
                        aciklama_textbox.Text = "";
                        satisyapilanfirmatextbox.Text = "";
                        ürününsatildigifirmatextbox.Text = "";
                        çıkacak_olan_parçalar_listesi_listbox.Items.Clear();
                        çıkacak_olan_parçalar_listbox.SelectedIndex = -1;
                        ürünhazirlikdurumu_combobox.SelectedIndex = -1;
                        üründurumunugüncelle_combobox.SelectedIndex = -1;
                        ürün_seri_no_textbox.Text = "";
                        ürünadetitextbox.Text = "";
                        kullanilacak_malzeme_adeti_textbox.Text = "";
                        cikacakürün_serino_textbox.Text = "";
                        kullanilacak_malzemeler_seri_no_textbox.Text = "";
                        satis_tarihi_textbox.Text = "";
                        siparis_numarasi_textbox.Text = "";
                        Siparisgetir();
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show("Ürün silinirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Sipariş silinmedi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Malzeme_ekle_btn_Click(object sender, EventArgs e)
        {
            var row = ((DataTable)sipariskontrolview.DataSource).Rows[sipariskontrolview.CurrentRow.Index];
            string cikarilacakmalzemekontrol;
            if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Beklemede")
            {
                if (kullanilacak_malzeme_adeti_textbox.Text == "")
                {
                    if (çıkacak_olan_parçalar_listbox.SelectedItem.ToString() == "Bu üründen parça çıkartılmayacaktır")
                    {
                        if (çıkacak_olan_parçalar_listesi_listbox.Items.Contains(çıkacak_olan_parçalar_listbox.SelectedItem))
                        {
                            MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            çıkacak_olan_parçalar_listesi_listbox.Items.Add(çıkacak_olan_parçalar_listbox.SelectedItem);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanılacak malzeme adetini giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (Convert.ToInt32(kullanilacak_malzeme_adeti_textbox.Text) < 1)
                {
                    if (çıkacak_olan_parçalar_listbox.SelectedItem.ToString() == "Bu üründen parça çıkartılmayacaktır")
                    {
                        if (çıkacak_olan_parçalar_listesi_listbox.Items.Contains(çıkacak_olan_parçalar_listbox.SelectedItem))
                        {
                            MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            çıkacak_olan_parçalar_listesi_listbox.Items.Add(çıkacak_olan_parçalar_listbox.SelectedItem);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Kullanılacak malzeme adeti 1'den küçük olamaz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (cikacakürün_serino_textbox.Text == "")
                {
                    if (çıkacak_olan_parçalar_listbox.SelectedItem.ToString() == "Bu üründen parça çıkartılmayacaktır")
                    {
                        if (çıkacak_olan_parçalar_listesi_listbox.Items.Contains(çıkacak_olan_parçalar_listbox.SelectedItem))
                        {
                            MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            çıkacak_olan_parçalar_listesi_listbox.Items.Add(çıkacak_olan_parçalar_listbox.SelectedItem);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Çıkartılan parçanın seri numarası girilmemiştir, lütfen seri numarası giriniz.\nÜrünün seri numarası yoksa seri no kısmna 'Seri numarası yoktur' yazınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (çıkacak_olan_parçalar_listbox.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen geçerli malzeme seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cikarilacakmalzemekontrol = çıkacak_olan_parçalar_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Çıkarıldı" + " SN: " + cikacakürün_serino_textbox.Text;
                    if (çıkacak_olan_parçalar_listesi_listbox.Items.Contains(cikarilacakmalzemekontrol))
                    {
                        MessageBox.Show("Malzemeler listesinde aynı ürün mevcut ürünü silip güncelledikten sonra tekrar ekleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        for (int i = 0; çıkacak_olan_parçalar_listesi_listbox.Items.Count > i; i++)
                        {
                            object v = çıkacak_olan_parçalar_listesi_listbox.Items[i];
                            if (v == "")
                            {
                                çıkacak_olan_parçalar_listesi_listbox.Items.RemoveAt(i);

                            }
                        }

                        çıkacak_olan_parçalar_listesi_listbox.Items.Add(çıkacak_olan_parçalar_listbox.SelectedItem + " (" + kullanilacak_malzeme_adeti_textbox.Text + " Adet)" + " Çıkarıldı" + " SN: " + cikacakürün_serino_textbox.Text);
                    }
                }
                kullanilacak_malzeme_adeti_textbox.Text = "";
                cikacakürün_serino_textbox.Text = "";
            }
            else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Onaylandı")
            {
                çıkacak_olan_parçalar_listesi_listbox.Enabled = true;
                MessageBox.Show("Sipariş onaylandı. Malzeme çıkartmak için çok geç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Gönderim İçin Hazır")
            {
                MessageBox.Show("Sipariş gönderim için hazırlandı. Malzeme çıkartmak için çok geç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş İade")
            {
                MessageBox.Show("Sipariş iade olmuş. Ürünü orjinal haline getirip siparişin açıklamasını değiştirebilirsin.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Arızalı")
            {
                MessageBox.Show("Sipariş arızalı. Ürünü orjinal haline getirip siparişin açıklmasını değiştirebilirsin.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Siparisi_onayla_btn_Click(object sender, EventArgs e)
        {
            Siparisonayla();
        }
        private void Kullanilmayacak_malzemeyi_sil_btn_Click(object sender, EventArgs e)
        {
            var row = ((DataTable)sipariskontrolview.DataSource).Rows[sipariskontrolview.CurrentRow.Index];
            if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Beklemede")
            {
                if (çıkacak_olan_parçalar_listesi_listbox.SelectedIndex > -1)
                    çıkacak_olan_parçalar_listesi_listbox.Items.RemoveAt(çıkacak_olan_parçalar_listesi_listbox.SelectedIndex);
                else if (çıkacak_olan_parçalar_listesi_listbox.SelectedIndex == -1)
                    MessageBox.Show("Kullanmayacağınız malzemeyi seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Onaylandı")
            {

                MessageBox.Show("Sipariş onaylandı. Çıkarılan parçaları silemezsiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Gönderim İçin Hazır")
            {

                MessageBox.Show("Sipariş gönderim için hazırlandı. Çıkarılan parçaları silemezsiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş İade")
            {

                MessageBox.Show("Sipariş iade olmuş. Üründen çıkarılan parçaları silemezsiniz, ürünün iade olduğuna dair kısa bir ürün açıklaması girebilirsiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (row["urun_hazirlik_durumu"].ToString() == "Sipariş Arızalı")
            {

                MessageBox.Show("Sipariş arızalı. Siparişten çıkarılan parçaları silemezsiniz, ürünün arızalı olduğuna dair ürün açıklamasını değiştiriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Siparis_aciklama_güncelleme_btn_Click(object sender, EventArgs e)
        {

            try
            {
                var row = ((DataTable)sipariskontrolview.DataSource).Rows[sipariskontrolview.CurrentRow.Index];
                if (aciklama_textbox.Text == "")
                {
                    MessageBox.Show("Sipariş açıklamasını değiştirmek için sipariş açıklaması girmen gerekmez mi?", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        SqlDataAdapter komut = new SqlDataAdapter("select count(*) from siparisler where siparis_id = '" + (int)row["siparis_id"] + "'", connection);
                        DataTable dtablo = new DataTable();
                        komut.Fill(dtablo);
                        if (dtablo.Rows[0][0].ToString() == "1")
                        {
                            SqlCommand ürüngüncelle = new SqlCommand("update siparisler set urun_hakkinda_aciklama = '" + aciklama_textbox.Text + "' where siparis_id = '" + (int)row["siparis_id"] + "'", connection);
                            ürüngüncelle.ExecuteNonQuery();
                            MessageBox.Show("Ürün açıklaması değiştirildi, lütfen sipariş listesini güncelleyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // LOG DOYASI //
                            using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürünaditextbox.Text + " ürünün açıklamasını şu şekilde değiştirdi: " + aciklama_textbox.Text, w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }
                            // LOG DOSYASI //
                        }
                        else
                        {
                            MessageBox.Show("Sipariş açıklaması değiştirilirken hata oluştu tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sipariş açıklaması değiştirilmedi, hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Kullanilacak_malzeme_serino_ekle_btn_Click(object sender, EventArgs e)
        {
            if (kullanilacak_malzemeler_seri_no_textbox.Text == "")
            {
                MessageBox.Show("Kullanılacak malzeme'nin seri numarasını boş bırakmayınız. Eğer ürünün seri numarası okunmuyor veya yok ise. Seri numarası yoktur yazınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (kullanilacak_malzemeler_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Seri numarası ekleyeceğiniz bir ürün seçmediniz. Ürün seçip tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Kullanılacakürünlerinserinokontrolü();
                if (snkontrol == false)
                {
                    string txt = kullanilacak_malzemeler_listbox.SelectedItem.ToString();
                    int pos = txt.IndexOf("SN:");
                    txt = txt.Substring(0, pos);
                    kullanilacak_malzemeler_listbox.Items.Add(txt + "SN: " + kullanilacak_malzemeler_seri_no_textbox.Text);
                    kullanilacak_malzemeler_listbox.Items.RemoveAt(kullanilacak_malzemeler_listbox.SelectedIndex);
                  
                }
                else
                {
                    kullanilacak_malzemeler_listbox.Items.Add(kullanilacak_malzemeler_listbox.SelectedItem + " SN: " + kullanilacak_malzemeler_seri_no_textbox.Text);
                    kullanilacak_malzemeler_listbox.Items.RemoveAt(kullanilacak_malzemeler_listbox.SelectedIndex);
                }
            }
            kullanilacak_malzemeler_seri_no_textbox.Text = "";
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
                MessageBox.Show("Kullanıcı bilgileri çekilmedi tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + kullaniciaktifligi.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            Application.Exit();
        }

        private void Windows_kücültme_label_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Ana_menü_btnn_Click(object sender, EventArgs e)
        {
            Anaform Anaform = new Anaform();
            Anaform.Show();
            Hide();
        }

        new
        #region forumharaketettirme
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Sipariskontrolform_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Sipariskontrolform_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Sipariskontrolform_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }
        #endregion

        #region forumharaketettirme2 
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

        public void PDF_Disa_Aktar(DataGridView sipariskontrolview)
        {
            PdfPTable pdfTable = new PdfPTable(sipariskontrolview.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
            foreach (DataGridViewColumn column in sipariskontrolview.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText))
                {
                    BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240)
                };
                pdfTable.AddCell(cell);
            }
            try
            {
                foreach (DataGridViewRow row in sipariskontrolview.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        pdfTable.AddCell(cell.Value.ToString());
                    }
                }
            }
            catch (NullReferenceException)
            {
            }
            using (FileStream stream = new FileStream("D:\\VeritabanıYedek/" + DateTime.Now.ToString("dd.MM.yyyy") + "-OSBilisim_satisraporu" + ".pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 42f, 30f);// sayfa boyutu.
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }

        }
        public void Veritabanı_SatışRaporu()
        {
            string klasorYeri = "D:\\VeritabanıYedek";
            string klasorolustur = klasorYeri + @"\";
            Directory.CreateDirectory(klasorolustur);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(klasorYeri);
            System.IO.FileInfo[] fis = di.GetFiles("*.bak");
            System.IO.FileInfo[] pdf = di.GetFiles("*.pdf");
            if (fis.Length == 0 || (DateTime.Now - fis.Max(d => d.CreationTime)).TotalDays >= 1 || pdf.Length == 0 || (DateTime.Now - pdf.Max(d => d.CreationTime)).TotalDays >= 1)
            {
                string database = connection.Database.ToString();
                try
                {
                    string yol = "D:\\VeritabanıYedek";
                    string cmd = "BACKUP DATABASE [" + database + "] TO DISK='" + yol + "\\" + DateTime.Now.ToString("dd.MM.yyyy") + "-OSBilisim_yedek" + ".bak'";
                    using (SqlCommand command = new SqlCommand(cmd, connection))
                    {
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }
                        command.ExecuteNonQuery();
                        connection.Close();
                        PDF_Disa_Aktar(sipariskontrolview);
                        //Excel_Disa_Aktar(sipariskontrolview);
                        MessageBox.Show(DateTime.Now.ToString("dd.MM.yyyy") + " tarihinde sipariş raporu, veritabanı yedeği başarılı şekilde alınmıştır.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        SmtpClient sc = new SmtpClient
                        {
                            Port = 587,
                            Host = "delta.veriyum.net",
                            EnableSsl = true
                        };

                        string kime = "selcuksahin158@gmail.com";
                        string konu = "OSBİLİŞİM - Yedek Dosyaları";
                        sc.Credentials = new NetworkCredential("teknik@trentatek.com.tr", "H35nYH63RS");
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress("teknik@trentatek.com.tr", "OS BİLİŞİM")
                        };
                        mail.To.Add(kime);
                        mail.To.Add("teknik@trentatek.com.tr");
                        mail.To.Add("onur.demir@osbilisim.com.tr");
                        mail.To.Add("alper@osbilisim.com.tr");
                        mail.To.Add("yener.guner@trentatek.com.tr");
                        mail.Subject = konu;
                        mail.IsBodyHtml = true;
                        string htmlString = "<html>" +
                             " <head>" +
                             " <meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>" +
                             " <style type='text/css'>" +
                             " .sayfa" +
                             " {" +
                             " background: white;" +
                             " text-align:center;" +
                             " width: 605px;" +
                             " border: solid 2px solid black;" +
                             " border-radius: 5px;" +
                             " font: small/1.5 Arial,Helvetica,sans-serif;" +
                             " font-weight: bold;" +
                             " }" +
                             " .fotograf" +
                             " {" +
                             " text-align:left;" +
                             " margin-bottom: -200px;" +
                             " height: 0px;" +
                             " }" +
                             " .üstalan" +
                             " {" +
                             " background: #3ea2e6;" +
                             " height:2px;" +
                             " color: #3ea2e6;" +
                             " border-radius: 5px;" +
                             " }" +
                             " .teknikservis" +
                             " {" +
                             " border-left: 4px solid #3ea2e6;" +
                             " height: 180px;" +
                             " margin-left: 312px;" +
                             " margin-bottom: 20px;" +
                             " }" +
                             " .altalan" +
                             " {" +
                             " background: #3ea2e6;" +
                             " height:2px;" +
                             " color: #3ea2e6;" +
                             " border-radius: 5px;" +
                             " color: red;" +
                             " }" +
                             " .tarih" +
                             " {" +
                             " text-align:right;" +
                             " margin-bottom: -20px;" +
                             " }" +
                             " </style>" +
                             " </head>" +
                             " <body>" +
                             " <div class='sayfa'>" +
                             " <div class='üstalan'> </div>" +
                             " <div class='tarih'>" +
                             " <p>Tarih: " + DateTime.Now + " </p>" +
                             " </div>" +
                             " <div class= 'fotograf'>" +
                             " <img style = ' margin-left: -430px; ' src=\"https://www.osbilisim.com.tr/wp-content/uploads/2021/05/footer.png\" />" +
                             " </div>" +
                             " <div class='teknikservis'>" +
                             " <p style = 'font-size: 19px; text-align: center;  padding-top: 70px;' > SELÇUK ŞAHİN <br> teknik@trentatek.com.tr</br> </p>" +
                             " </div> " +
                             " <p> OS BİLİŞİM tarafından kullanıcılara bilgi amaçlı satış raporu ve veritabanı dosyaları gönderilmiştir.</p> " +
                             " <div class='altalan'></div>" +
                             " <p>" +
                             " Bu e-posta otomatik oluşturulmuştur. Lütfen cevap vermeyiniz." +
                             " </p>" +
                             " </div>" +
                             " </body>" +
                             " </html>";
                        mail.Body = htmlString;
                        mail.Attachments.Add(new Attachment(@"D:\VeritabanıYedek\" + DateTime.Now.ToString("dd.MM.yyyy") + "-OSBilisim_yedek.bak"));
                        mail.Attachments.Add(new Attachment(@"D:\VeritabanıYedek\" + DateTime.Now.ToString("dd.MM.yyyy") + "-OSBilisim_satisraporu.pdf"));
                        sc.Send(mail);

                    }
                }
                catch (Exception HATA)
                {
                    MessageBox.Show(HATA.Message);
                    return;
                }
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Kullanicigirisiform.username == "Admin" || Anaform.statü == "Ana Bilgisayar" || Anaform.isim == "ANA PC")
            {
                Veritabanı_SatışRaporu();
            }
        }
        private void Üründurumunugüncelle_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var row = ((DataTable)sipariskontrolview.DataSource).Rows[sipariskontrolview.CurrentRow.Index];
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    SqlDataAdapter komut = new SqlDataAdapter("select count(*) from siparisler where siparis_id = '" + (int)row["siparis_id"]+ "'", connection);
                    DataTable dtablo = new DataTable();
                    komut.Fill(dtablo);
                    if (dtablo.Rows[0][0].ToString() == "1")
                    {
                        if (üründurumukontrol == true)
                        {
                            if (üründurumunugüncelle_combobox.SelectedIndex == -1) { }
                            else
                            {
                                if ((string)üründurumunugüncelle_combobox.SelectedItem == "Sipariş İade" || (string)üründurumunugüncelle_combobox.SelectedItem == "Sipariş Arızalı")
                                {
                                    SqlCommand ürümdurumunugüncelle = new SqlCommand("update siparisler set urun_hazirlik_durumu = '" + üründurumunugüncelle_combobox.SelectedItem + "' where siparis_id = '" + (int)row["siparis_id"] + "'", connection);
                                    ürümdurumunugüncelle.ExecuteNonQuery();

                                    string kullanilacakmalzemeler1 = ürün_seri_no_textbox.Text;
                                    string[] kelimeler1 = kullanilacakmalzemeler1.Split('/');
                                    foreach (var kelime in kelimeler1)
                                    {
                                        SqlCommand ürünserinogüncelle = new SqlCommand("update notebook_urun_seri_no_stok set urun_durumu = '" + "Kullanılmadı" + "' where urun_seri_no = '" + kelime.Trim() + "'", connection);
                                        ürünserinogüncelle.ExecuteNonQuery();
                                        SqlCommand digerürünserinogüncelle = new SqlCommand("update diger_ürün_stok set diger_urun_durumu = '" + "Kullanılmadı" + "' where diger_urun_serino = '" + kelime.Trim() + "'", connection);
                                        digerürünserinogüncelle.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    SqlCommand ürüngüncelle = new SqlCommand("update siparisler set urun_hazirlik_durumu = '" + üründurumunugüncelle_combobox.SelectedItem + "' where siparis_id = '" + (int)row["siparis_id"] + "'", connection);
                                    ürüngüncelle.ExecuteNonQuery();

                                    string kullanilacakmalzemeler1 = ürün_seri_no_textbox.Text;
                                    string[] kelimeler1 = kullanilacakmalzemeler1.Split('/');
                                    foreach (var kelime in kelimeler1)
                                    {
                                        SqlCommand ürünserinogüncelle = new SqlCommand("update notebook_urun_seri_no_stok set urun_durumu = '" + "Ürün Kullanıldı" + "' where urun_seri_no = '" + kelime.Trim() + "'", connection);
                                        ürünserinogüncelle.ExecuteNonQuery();
                                        SqlCommand digerürünserinogüncelle = new SqlCommand("update diger_ürün_stok set diger_urun_durumu = '" + "Ürün Kullanıldı" + "' where diger_urun_serino = '" + kelime.Trim() + "'", connection);
                                        digerürünserinogüncelle.ExecuteNonQuery();
                                    }
                                }
                                // LOG DOSYASI
                                using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                                {
                                    Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürünaditextbox.Text + " ürününün, " + ürünhazirlikdurumu_combobox.SelectedItem + " durumundan " + üründurumunugüncelle_combobox.SelectedItem + " durumuna çevirdi.", w);
                                }
                                using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                {
                                    Kullanicigirisiform.DumpLog(r);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ürün hazırlık durumu güncellenirken bir hata oluştu lütfen tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                    connection.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün durumu güncellenirken bir hata oluştu lütfen tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        bool üründurumukontrol = false;
        private void Üründurumunugüncelle_combobox_Click(object sender, EventArgs e)
        {
            üründurumukontrol = true;
        }
    }
}
