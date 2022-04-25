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

namespace OSBilişim
{
    public partial class Sipariskontrolform : Form
    {
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
            System.Windows.Forms.Application.Exit();
        }
        public void Siparisonayla()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                    if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Beklemede")
                    {
                        Kullanılacakürünlerinserinokontrolü();
                        if (snkontrol == false)
                        {
                            if (çıkacak_olan_parçalar_listesi_listbox.Items.Count > 0)
                            {

                                SqlDataAdapter komut = new SqlDataAdapter("select count(*) from notebook_siparisler where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
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
                                        SqlCommand ürüncikarilanparcalarıgüncelle = new SqlCommand("update notebook_siparisler set urun_cikarilan_parcalar = '" + çıkarılanparcalarlistesi + "' where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                                        ürüncikarilanparcalarıgüncelle.ExecuteNonQuery();


                                        // KULLANILACAK MALZEMELERİN SERİ NUMARALARI GİRİLDİKTEN SONRA SİSTEME AKTARMA
                                        string kullanilacakparçalarlistesi = kullanilacak_malzemeler_listbox.Items.Cast<string>().Aggregate((current, next) => $"{current} {"/"} {next}");
                                        SqlCommand kullanilanparcalarigüncelle = new SqlCommand("update notebook_siparisler set kullanilacak_malzemeler = '" + kullanilacakparçalarlistesi + "' where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
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
                                        SqlCommand siparisdurumunugüncelle = new SqlCommand("update notebook_siparisler set urun_hazirlik_durumu = '" + "Sipariş Gönderim İçin Hazır" + "' where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                                        siparisdurumunugüncelle.ExecuteNonQuery();
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
                            MessageBox.Show("Seri numaraları girilmemiş parçalar mevcut, seri numaraları girip tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    Siparisgetir();
                    connection.Close();
                }
                else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Onaylandı")
                {

                    MessageBox.Show("Sipariş onaylandı. Malzeme çıkartmak için çok geç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Gönderim İçin Hazır")
                {
                    MessageBox.Show("Sipariş gönderim için hazırlandı. Malzeme çıkartmak için çok geç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş İade")
                {
                    MessageBox.Show("Sipariş iade olmuş. Ürünü orjinal haline getirip siparişin açıklamasını değiştirebilirsin.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Arızalı")
                {
                    MessageBox.Show("Sipariş arızalı. Ürünü orjinal haline getirip siparişin açıklmasını değiştirebilirsin.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün durumu güncellenirken bir hata oluştu lütfen tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    SqlDataAdapter komut = new SqlDataAdapter("select * from notebook_siparisler", connection);
                    DataSet datagorev = new DataSet();
                    komut.Fill(datagorev);
                    sipariskontrolview.DataSource = datagorev.Tables[0];
                    connection.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sipariş listesi çekilirken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private readonly string kelime = "SN: ";
        private bool snkontrol = true;
        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.118,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        private void Sipariskontrol_Load(object sender, EventArgs e)
        {
            
            timer1.Start();
            Kullanicigirisiform Kullanicigirisiform = new Kullanicigirisiform();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
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
                    connection.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Program başlatılmadı.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            try
            {
                SqlCommand komut = new SqlCommand("SELECT * FROM kullanilacak_malzemeler ", connection);
                SqlDataReader veriokuyucu;
                connection.Open();
                veriokuyucu = komut.ExecuteReader();
                while (veriokuyucu.Read())
                {
                    çıkacak_olan_parçalar_listbox.Items.Add(veriokuyucu["malzeme_adi"]);
                }
                connection.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Çıkarılan parçalar listesi çekilirken hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            SipariskontrolviewSetting(sipariskontrolview);
            Siparisgetir();
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
           
            ürünaditextbox.Text = sipariskontrolview.CurrentRow.Cells[1].Value.ToString();
            ürünstokkodutextbox.Text = sipariskontrolview.CurrentRow.Cells[2].Value.ToString();
            ürün_seri_no_textbox.Text = sipariskontrolview.CurrentRow.Cells[3].Value.ToString();
            ürünadetitextbox.Text = sipariskontrolview.CurrentRow.Cells[4].Value.ToString();
            aliciaditextbox.Text = sipariskontrolview.CurrentRow.Cells[5].Value.ToString();
            alicisoyaditextboxt.Text = sipariskontrolview.CurrentRow.Cells[6].Value.ToString();
            satisyapilanfirmatextbox.Text = sipariskontrolview.CurrentRow.Cells[7].Value.ToString();
            ürününsatildigifirmatextbox.Text = sipariskontrolview.CurrentRow.Cells[8].Value.ToString();
            ürünhazirlikdurumu_combobox.SelectedItem = sipariskontrolview.CurrentRow.Cells[10].Value.ToString();
            aciklama_textbox.Text = sipariskontrolview.CurrentRow.Cells[11].Value.ToString();
            try
            {
                çıkacak_olan_parçalar_listesi_listbox.Items.Clear();
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    kullanilacak_malzemeler_listbox.Items.Clear();
                    SqlCommand komut3 = new SqlCommand("SELECT * FROM notebook_siparisler where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                    SqlDataReader veriokuyucu3;
                    veriokuyucu3 = komut3.ExecuteReader();
                    while (veriokuyucu3.Read())
                    {

                        string kullanilacakmalzemeler1 = (string)veriokuyucu3["kullanilacak_malzemeler"];
                        string[] kelimeler1 = kullanilacakmalzemeler1.Split('/');
                        foreach (var kelime in kelimeler1)
                        {
                            kullanilacak_malzemeler_listbox.Items.Add(kelime.Trim());
                        }
                    }
                    veriokuyucu3.Close();
                    veriokuyucu3 = komut3.ExecuteReader();
                    while (veriokuyucu3.Read())
                    {
                        if (veriokuyucu3["urun_cikarilan_parcalar"] == DBNull.Value)
                        {
                            string result = String.Empty;
                            çıkacak_olan_parçalar_listesi_listbox.Items.Clear();
                        }
                        else
                        {
                            string cikmisparcalar = (string)veriokuyucu3["urun_cikarilan_parcalar"];
                            string[] cikacakparcalar = cikmisparcalar.Split('/');
                            foreach (var cikacakparca in cikacakparcalar)
                            {
                                çıkacak_olan_parçalar_listesi_listbox.Items.Add(cikacakparca.Trim());
                            }
                        }
                    }
                    connection.Close();
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
            Kullanılacakürünlerinserinokontrolü();
            try
            {

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    SqlDataAdapter komut = new SqlDataAdapter("select count(*) from notebook_siparisler where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                    DataTable dtablo = new DataTable();
                    komut.Fill(dtablo);
                    if (dtablo.Rows[0][0].ToString() == "1")
                    {
                        SqlCommand ürüngüncelle = new SqlCommand("update notebook_siparisler set urun_hazirlik_durumu = '" + ürünhazirlikdurumu_combobox.SelectedItem + "' where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                        ürüngüncelle.ExecuteNonQuery();
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
            int siparisadeti, güncelsiparisadet,günceltoplam = 0, toplam = 0;
            try
            {
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
                    MessageBox.Show("Yeni bir siparişiniz var kontrol ediniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                ürünadetitextbox.Text = "";
                cikacakürün_serino_textbox.Text = "";
                kullanilacak_malzemeler_seri_no_textbox.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün listesi yenilenirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Siparis_sil_btn_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Sipariş silinecektir onaylıyor musunuz?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        SqlCommand verisil = new SqlCommand("delete from notebook_siparisler where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                        verisil.ExecuteNonQuery();

                        // LOG DOYASI //
                        using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                        {
                            Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürünaditextbox.Text + " / " + ürün_seri_no_textbox + " / " + ürünstokkodutextbox.Text + " ürünün siparişini sildi.", w);
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
                        ürünadetitextbox.Text = "";
                        cikacakürün_serino_textbox.Text = "";
                        ürün_seri_no_textbox.Text = "";
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
            string cikarilacakmalzemekontrol;
            if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Beklemede")
            {
                if (kullanilacak_malzeme_adeti_textbox.Text == "")
                {
                    MessageBox.Show("Kullanılacak malzeme adeti boş bırakılmaz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (Convert.ToInt32(kullanilacak_malzeme_adeti_textbox.Text) < 1)
                {
                    MessageBox.Show("Kullanılacak malzeme adeti 1'den küçük olamaz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (cikacakürün_serino_textbox.Text == "")
                {
                    MessageBox.Show("Çıkartılan parçanın seri numarası girilmemiştir, lütfen seri numarası giriniz.\nÜrünün seri numarası yoksa seri no kısmna 'Seri numarası yoktur' yazınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            }
            else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Onaylandı")
            {
                çıkacak_olan_parçalar_listesi_listbox.Enabled = true;
                MessageBox.Show("Sipariş onaylandı. Malzeme çıkartmak için çok geç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Gönderim İçin Hazır")
            {
                MessageBox.Show("Sipariş gönderim için hazırlandı. Malzeme çıkartmak için çok geç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş İade")
            {
                MessageBox.Show("Sipariş iade olmuş. Ürünü orjinal haline getirip siparişin açıklamasını değiştirebilirsin.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Arızalı")
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
            if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Beklemede")
            {
                if (çıkacak_olan_parçalar_listesi_listbox.SelectedIndex > -1)
                    çıkacak_olan_parçalar_listesi_listbox.Items.RemoveAt(çıkacak_olan_parçalar_listesi_listbox.SelectedIndex);
                else if (çıkacak_olan_parçalar_listesi_listbox.SelectedIndex == -1)
                    MessageBox.Show("Kullanmayacağın malzemeyi seç!", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Onaylandı")
            {

                MessageBox.Show("Sipariş onaylandı. Çıkarılan parçaları silemezsiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Gönderim İçin Hazır")
            {

                MessageBox.Show("Sipariş gönderim için hazırlandı. Çıkarılan parçaları silemezsiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş İade")
            {

                MessageBox.Show("Sipariş iade olmuş. Üründen çıkarılan parçaları silemezsiniz, ürünün iade olduğuna dair kısa bir ürün açıklaması girebilirsiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (sipariskontrolview.CurrentRow.Cells[10].Value.ToString() == "Sipariş Arızalı")
            {

                MessageBox.Show("Sipariş arızalı. Siparişten çıkarılan parçaları silemezsiniz, ürünün arızalı olduğuna dair ürün açıklamasını değiştiriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Siparis_aciklama_güncelleme_btn_Click(object sender, EventArgs e)
        {

            try
            {
                if (aciklama_textbox.Text == "")
                {
                    MessageBox.Show("Sipariş açıklamasını değiştirmek için sipariş açıklaması girmen gerekmez mi?", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        SqlDataAdapter komut = new SqlDataAdapter("select count(*) from notebook_siparisler where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
                        DataTable dtablo = new DataTable();
                        komut.Fill(dtablo);
                        if (dtablo.Rows[0][0].ToString() == "1")
                        {
                            SqlCommand ürüngüncelle = new SqlCommand("update notebook_siparisler set urun_hakkinda_aciklama = '" + aciklama_textbox.Text + "' where siparis_id = '" + sipariskontrolview.CurrentRow.Cells[0].Value.ToString() + "'", connection);
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
        private void Kullanılacakürünlerinserinokontrolü()
        {
            foreach (string item in kullanilacak_malzemeler_listbox.Items)
            {
                if (item.ToLower().Contains(kelime.ToLower()))
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

        #region veritabanı_satışraporu_rapor_alıp_gmail_gönderme
        public void PDF_Disa_Aktar(DataGridView sipariskontrolview)
        {
            PdfPTable pdfTable = new PdfPTable(sipariskontrolview.ColumnCount);

            // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
            pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
            pdfTable.WidthPercentage = 100; // hücre genişliği
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
            pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
             // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.

            foreach (DataGridViewColumn column in sipariskontrolview.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText))
                {
                    BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240) // hücre arka plan rengi
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
            if (fis.Length == 0 || (DateTime.Now - fis.Max(d => d.CreationTime)).TotalDays >= 7 || pdf.Length == 0 || (DateTime.Now - pdf.Max(d => d.CreationTime)).TotalDays >= 7)
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
                        MessageBox.Show(DateTime.Now.ToString("dd.MM.yyyy") + " tarihinde sipariş raporu, veritabanı yedeği başarılı şekilde alınmıştır.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        SmtpClient sc = new SmtpClient
                        {
                            Port = 587,
                            Host = "delta.veriyum.net",
                            EnableSsl = true
                        };

                        string kime = "selcuksahin158@gmail.com";
                        string konu = "OSBilişim Satış, veritabanı yedeği";
                        string icerik = "Bu mesaj sistem tarafından otamatik olarak gönderilmektedir. " + DateTime.Now.ToString("dd.MM.yyyy") + " tarihinde OS BİLİŞİM satış raporu, veritabanı yedeği kullanıcılara bilgi amaçlı yedek olarak gönderilmiştir.";

                        sc.Credentials = new NetworkCredential("teknik@trentatek.com.tr", "H35nYH63RS");
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress("teknik@trentatek.com.tr", "OS BİLİŞİM")
                        };
                        mail.To.Add(kime);
                        mail.To.Add("teknik@trentatek.com.tr");
                        mail.To.Add("onur.demir@osbilisim.com.tr");
                        mail.To.Add("alper.sonmez@osbilisim.com.tr");
                        mail.Subject = konu;
                        mail.IsBodyHtml = true;
                        mail.Body = icerik;
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            int siparisadeti,toplam = 0;
            int güncelsiparisadet, günceltoplam = 0;
            if (Kullanicigirisiform.username == "Admin" || Anaform.statü == "Ana Bilgisayar" || Anaform.isim == "ANA PC")
            {
                for ( siparisadeti = 0; sipariskontrolview.Rows.Count > siparisadeti; siparisadeti++)
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
                    MessageBox.Show("Yeni bir siparişiniz var kontrol ediniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Veritabanı_SatışRaporu();
            }
        }
        #endregion
    }
}