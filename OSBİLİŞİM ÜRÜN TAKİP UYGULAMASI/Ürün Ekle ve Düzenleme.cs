﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;
using System.IO;

namespace OSBilişim
{
    public partial class Ürüneklemedüzenlemeform : Form
    {
        public Ürüneklemedüzenlemeform()
        {
            InitializeComponent();
        }

        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.118,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        public void Listeyenile()
        {
            ürün_adi_textbox.Text = "";
            ürün_seri_no_textbox.Text = "";
            ürün_stok_kodu_textbox.Text = "";
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    int tekrarsec = notebook_ürünler_listbox.SelectedIndex;
                    
                    notebook_ürünler_listbox.Items.Clear();
                    SqlCommand komut = new SqlCommand("SELECT * FROM notebook_urunler ", connection);
                    SqlDataReader veriokuyucu;
                    veriokuyucu = komut.ExecuteReader();
                    while (veriokuyucu.Read())
                    {
                        notebook_ürünler_listbox.Items.Add(veriokuyucu["urun_adi"]);
                    }
                    veriokuyucu.Close();

                    if (notebook_ürünler_listbox.SelectedIndex > tekrarsec)
                    {
                        notebook_ürünler_listbox.SelectedIndex = -1;
                    }
                    else { notebook_ürünler_listbox.SelectedIndex = tekrarsec; }

                    notebook_ürün_kodlari_listbox.Items.Clear();
                    SqlCommand komut3 = new SqlCommand("SELECT * FROM notebook_urun_kodları where urun_adi = '" + notebook_ürünler_listbox.SelectedItem + "'", connection);
                    SqlDataReader veriokuyucu3;
                    veriokuyucu3 = komut3.ExecuteReader();
                    while (veriokuyucu3.Read())
                    {
                        notebook_ürün_kodlari_listbox.Items.Add(veriokuyucu3["urun_kodu"]);
                    }
                   
                    veriokuyucu3.Close();

                    notebook_ürün_seri_no_listbox.Items.Clear();
                    SqlCommand üründurum = new SqlCommand("SELECT * FROM notebook_urun_seri_no_stok where urun_adi = '" + notebook_ürünler_listbox.SelectedItem + "'", connection);
                    SqlDataReader üründurumusorgulama;
                    üründurumusorgulama = üründurum.ExecuteReader();
                    while (üründurumusorgulama.Read())
                    {
                        notebook_ürün_seri_no_listbox.Items.Add(üründurumusorgulama["urun_seri_no"]);
                    }
                    üründurumusorgulama.Close();
                    connection.Close();
                }
                
            }
            catch (Exception hata)
            {
                MessageBox.Show("Sipariş listesi güncellenirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void Ürün_Ekle_ve_Düzenleme_Load(object sender, EventArgs e)
        {
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
                MessageBox.Show("Uygulama başlatılırken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            if (notebook_ürünler_listbox.SelectedIndex == -1)
            {
                notebook_ürün_kodlari_listbox.Items.Add("Lütfen bir ürün seçin.");
                notebook_ürün_seri_no_listbox.Items.Add("Lütfen bir ürün seçin.");
            }
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    SqlCommand komut = new SqlCommand("SELECT * FROM notebook_urunler ", connection);
                    SqlDataReader veriokuyucu1;
                    veriokuyucu1 = komut.ExecuteReader();
                    while (veriokuyucu1.Read())
                    {
                        notebook_ürünler_listbox.Items.Add(veriokuyucu1["urun_adi"]);
                    }
                    veriokuyucu1.Close();
                    connection.Close();
                }
            }

            catch (Exception hata)
            {
                MessageBox.Show("Ürün kodları çekilirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Notebook_ürünler_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notebook_ürünler_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Geçerli bir ürün seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ürün_adi_textbox.Text = notebook_ürünler_listbox.SelectedItem.ToString();
            }
            ürün_stok_kodu_textbox.Text = "";
            ürün_seri_no_textbox.Text = "";
            try
            {
                if (notebook_ürünler_listbox.SelectedIndex == -1)
                {
                    notebook_ürün_kodlari_listbox.Items.Clear();
                    notebook_ürün_seri_no_listbox.Items.Clear();
                }
                else
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                        notebook_ürün_kodlari_listbox.Items.Clear();
                        SqlCommand komut3 = new SqlCommand("SELECT * FROM notebook_urun_kodları where urun_adi = '" + notebook_ürünler_listbox.SelectedItem + "'", connection);
                        SqlDataReader veriokuyucu3;
                        veriokuyucu3 = komut3.ExecuteReader();
                        while (veriokuyucu3.Read())
                        {
                            notebook_ürün_kodlari_listbox.Items.Add(veriokuyucu3["urun_kodu"]);
                        }
                        veriokuyucu3.Close();

                        notebook_ürün_seri_no_listbox.Items.Clear();
                        SqlCommand üründurum = new SqlCommand("SELECT * FROM notebook_urun_seri_no_stok where urun_adi = '" + notebook_ürünler_listbox.SelectedItem + "'", connection);
                        SqlDataReader üründurumusorgulama;
                        üründurumusorgulama = üründurum.ExecuteReader();
                        while (üründurumusorgulama.Read())
                        {
                            notebook_ürün_seri_no_listbox.Items.Add(üründurumusorgulama["urun_seri_no"]);

                        }
                        üründurumusorgulama.Close();
                        connection.Close();
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün kodu, seri numarası çekilirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Ürün_Ekle_ve_Düzenleme_FormClosed(object sender, FormClosedEventArgs e)
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
                MessageBox.Show("Kullanıcı bilgileri alınırken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + kullaniciaktifligi.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            Application.Exit();
        }
        private void Yeni_ürün_ekle_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    if (notebook_ürünler_listbox.Items.Contains(ürün_adi_textbox.Text) && notebook_ürün_kodlari_listbox.Items.Contains(ürün_stok_kodu_textbox.Text))
                    {
                        if (notebook_ürün_seri_no_listbox.Items.Contains(ürün_seri_no_textbox.Text))
                        {
                            MessageBox.Show("Bu seri numaralı ürün zaten mevcut. Farklı seri numara giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ürün_adi_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün adını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ürün_seri_no_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün seri numarasını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ürün_stok_kodu_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün stok kodunu boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string yeniürünserinokayit = "insert into notebook_urun_seri_no_stok(urun_adi,urun_seri_no,urun_adeti,urun_durumu) values " + "" + "(@urun_adi,@urun_seri_no,@urun_adeti,@urun_durumu)";
                            SqlCommand yeniürünserinokayitkomut = new SqlCommand(yeniürünserinokayit, connection);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_adi", ürün_adi_textbox.Text);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_seri_no", ürün_seri_no_textbox.Text);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_adeti", "1");
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_durumu", "Kullanılmadı");
                            yeniürünserinokayitkomut.ExecuteNonQuery();
                            MessageBox.Show("Yeni seri numaralı ürünün kaydı başarılı şekilde oluşturuldu.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int tekrarsec = notebook_ürünler_listbox.SelectedIndex;
                            notebook_ürünler_listbox.SelectedIndex = tekrarsec;

                            // LOG DOYASI //
                            using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı "+ürün_adi_textbox.Text +" ürününün " + ürün_seri_no_textbox.Text +" yeni seri numarasını ekledi.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }
                            // LOG DOSYASI //
                            
                            ürün_seri_no_textbox.Focus();
                        }
                    }
                    else if (notebook_ürünler_listbox.Items.Contains(ürün_adi_textbox.Text))
                    {
                        if (notebook_ürün_kodlari_listbox.Items.Contains(ürün_stok_kodu_textbox.Text))
                        {
                            MessageBox.Show("Bu ürün kodu zaten mevcut farklı ürün kodu giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (notebook_ürün_seri_no_listbox.Items.Contains(ürün_seri_no_textbox.Text))
                        {
                            MessageBox.Show("Bu seri numaralı ürün zaten mevcut. Farklı seri numara giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ürün_adi_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün adını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ürün_seri_no_textbox.Text == "" && ürün_stok_kodu_textbox.Text.Length > 0)
                        {
                            string yeniürünkodukayit = "insert into notebook_urun_kodları(urun_adi,urun_kodu) values " + "" + "(@urun_adi,@urun_kodu)";
                            SqlCommand kayitkomut = new SqlCommand(yeniürünkodukayit, connection);
                            kayitkomut.Parameters.AddWithValue("@urun_adi", ürün_adi_textbox.Text);
                            kayitkomut.Parameters.AddWithValue("@urun_kodu", ürün_stok_kodu_textbox.Text);
                            MessageBox.Show("Yeni ürün stok kodu oluşturuldu.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            kayitkomut.ExecuteNonQuery();
                            int tekrarsec = notebook_ürünler_listbox.SelectedIndex;
                            notebook_ürünler_listbox.SelectedIndex = tekrarsec;
                            ürün_stok_kodu_textbox.Focus();
                            // LOG DOYASI //
                            using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürün_adi_textbox.Text + " ürününün " + ürün_stok_kodu_textbox.Text + " yeni ürün kodunu ekledi.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }
                            // LOG DOSYASI //
                        }
                        else if (ürün_stok_kodu_textbox.Text == "" && ürün_seri_no_textbox.Text.Length >= 0)
                        {
                            string yeniürünserinokayit = "insert into notebook_urun_seri_no_stok(urun_adi,urun_seri_no,urun_adeti,urun_durumu) values " + "" + "(@urun_adi,@urun_seri_no,@urun_adeti,@urun_durumu)";
                            SqlCommand yeniürünserinokayitkomut = new SqlCommand(yeniürünserinokayit, connection);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_adi", ürün_adi_textbox.Text);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_seri_no", ürün_seri_no_textbox.Text);
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_adeti", "1");
                            yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_durumu", "Kullanılmadı");
                            yeniürünserinokayitkomut.ExecuteNonQuery();
                            MessageBox.Show("Yeni ürün seri numarası oluşturuldu.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            int tekrarsec = notebook_ürünler_listbox.SelectedIndex;
                            notebook_ürünler_listbox.SelectedIndex = tekrarsec;
                            // LOG DOYASI //
                            using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürün_adi_textbox.Text + " ürününün " + ürün_seri_no_textbox.Text + " yeni seri numarasını ekledi.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }
                            // LOG DOSYASI //
                        }
                        else
                        {
                            if (ürün_seri_no_textbox.Text.Length > 0 && ürün_stok_kodu_textbox.Text.Length > 0)
                            {
                                string yeniürünkodukayit = "insert into notebook_urun_kodları(urun_adi,urun_kodu) values " + "" + "(@urun_adi,@urun_kodu)";
                                SqlCommand kayitkomut = new SqlCommand(yeniürünkodukayit, connection);
                                kayitkomut.Parameters.AddWithValue("@urun_adi", ürün_adi_textbox.Text);
                                kayitkomut.Parameters.AddWithValue("@urun_kodu", ürün_stok_kodu_textbox.Text);
                                kayitkomut.ExecuteNonQuery();

                                string yeniürünserinokayit = "insert into notebook_urun_seri_no_stok(urun_adi,urun_seri_no,urun_adeti,urun_durumu) values " + "" + "(@urun_adi,@urun_seri_no,@urun_adeti,@urun_durumu)";
                                SqlCommand yeniürünserinokayitkomut = new SqlCommand(yeniürünserinokayit, connection);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_adi", ürün_adi_textbox.Text);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_seri_no", ürün_seri_no_textbox.Text);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_adeti", "1");
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_durumu", "Kullanılmadı");
                                yeniürünserinokayitkomut.ExecuteNonQuery();
                                // LOG DOYASI //
                                using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                                {
                                    Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürün_adi_textbox.Text + " ürününün " + ürün_stok_kodu_textbox.Text + " ürün kodunu ve " + ürün_seri_no_textbox.Text + " yeni seri numarasını ekledi.", w);
                                }
                                using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                {
                                    Kullanicigirisiform.DumpLog(r);
                                }
                                // LOG DOSYASI //
                                MessageBox.Show("Yeni ürün kodu, seri numarası oluşturuldu.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ürün_stok_kodu_textbox.Focus();
                            }
                        }
                    }
                    else
                    {
                        if (notebook_ürün_seri_no_listbox.Items.Contains(ürün_seri_no_textbox.Text))
                        {
                            MessageBox.Show("Bu seri numaralı ürün zaten mevcut. Farklı seri numara giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (ürün_adi_textbox.Text == "")
                        {
                            MessageBox.Show("Ürün adını boş bırakmayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (notebook_ürünler_listbox.Items.Contains(ürün_adi_textbox.Text))
                        {
                            MessageBox.Show("Böyle bir ürün zaten mevcut.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string yeniürünnotebookkayit = "insert into notebook_urunler(urun_adi) values " + "" + "(@urun_adi)";
                            SqlCommand yeniürünnotebookkayitkomut = new SqlCommand(yeniürünnotebookkayit, connection);
                            yeniürünnotebookkayitkomut.Parameters.AddWithValue("@urun_adi", ürün_adi_textbox.Text);
                            yeniürünnotebookkayitkomut.ExecuteNonQuery();
                            ürün_adi_textbox.Focus();
                            // YENİ ÜRÜN NOTEBOOK İSİM VE KODU KAYİT //
                            if (ürün_stok_kodu_textbox.Text == ""){}
                            else
                            {
                                string yeniürünkayit = "insert into notebook_urun_kodları(urun_adi,urun_kodu) values " + "" + "(@urun_adi,@urun_kodu)";
                                SqlCommand yeniürünkayitkomut = new SqlCommand(yeniürünkayit, connection);
                                yeniürünkayitkomut.Parameters.AddWithValue("@urun_adi", ürün_adi_textbox.Text);
                                yeniürünkayitkomut.Parameters.AddWithValue("@urun_kodu", ürün_stok_kodu_textbox.Text);
                                yeniürünkayitkomut.ExecuteNonQuery();
                            }
                            // YENİ ÜRÜN NOTEBOOK İSİM KAYIT //
                            // YENİ ÜRÜN NOTEBOOK İSİM VE SERİ NO KAYİT //
                            if (ürün_seri_no_textbox.Text == ""){}
                            else
                            {
                                string yeniürünserinokayit = "insert into notebook_urun_seri_no_stok(urun_adi,urun_seri_no,urun_adeti,urun_durumu) values " + "" + "(@urun_adi,@urun_seri_no,@urun_adeti,@urun_durumu)";
                                SqlCommand yeniürünserinokayitkomut = new SqlCommand(yeniürünserinokayit, connection);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_adi", ürün_adi_textbox.Text);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_seri_no", ürün_seri_no_textbox.Text);
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_adeti", "1");
                                yeniürünserinokayitkomut.Parameters.AddWithValue("@urun_durumu", "Kullanılmadı");
                                yeniürünserinokayitkomut.ExecuteNonQuery();
                            }
                            // LOG DOYASI //
                            using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürün_adi_textbox.Text + " yeni ürünü ekledi.", w);
                            }
                            using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                            {
                                Kullanicigirisiform.DumpLog(r);
                            }
                            // LOG DOSYASI //
                            MessageBox.Show("Yeni ürün oluşturuldu.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                connection.Close();
                Listeyenile();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Ürün oluşturulurken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\n Hata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void Notebook_ürün_kodlari_listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (notebook_ürün_kodlari_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Geçerli ürün kodu seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ürün_stok_kodu_textbox.Text = notebook_ürün_kodlari_listbox.SelectedItem.ToString();
            }
        }

        private void Ana_menü_btn_Click(object sender, EventArgs e)
        {
            Anaform Anaform = new Anaform();
            Anaform.Show();
            Hide();
        }
        private void Notebook_ürün_seri_no_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (notebook_ürün_seri_no_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Geçerli seri numarası seçiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ürün_seri_no_textbox.Text = notebook_ürün_seri_no_listbox.SelectedItem.ToString();
            }
        }
        private void Secili_ürünü_sil_btn_Click(object sender, EventArgs e)
        {
            if (notebook_ürünler_listbox.SelectedIndex == -1 && notebook_ürün_kodlari_listbox.SelectedIndex == -1 && notebook_ürün_seri_no_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Silmek istediğiniz bir ürünü seçmeniz gerekmektedir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (notebook_ürünler_listbox.SelectedIndex >= 0)
                {
                    DialogResult dialog = MessageBox.Show("Eğer seçili ürünü kaldırmak istiyorsanız o ürüne ait ürün kodları, seri numaraları otamatik silinecektir. Ürün kaldırılsın mı?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                                SqlCommand ürünadikontrol = new SqlCommand("Select * From notebook_urunler where urun_adi='" + ürün_adi_textbox.Text +"'",connection);
                                SqlDataReader ürünadikontrolokuyucu;
                                ürünadikontrolokuyucu = ürünadikontrol.ExecuteReader();
                                if (ürünadikontrolokuyucu.Read())
                                {
                                    SqlCommand ürünstokverisil = new SqlCommand("delete from notebook_urun_seri_no_stok where urun_adi = '" + ürün_adi_textbox.Text + "'", connection);
                                    ürünstokverisil.ExecuteNonQuery();
                                    SqlCommand notebookürünverisil = new SqlCommand("delete from notebook_urunler where urun_adi = '" + ürün_adi_textbox.Text + "'", connection);
                                    notebookürünverisil.ExecuteNonQuery();
                                    SqlCommand notebookürünkodverisil = new SqlCommand("delete from notebook_urun_kodları where urun_adi = '" + ürün_adi_textbox.Text + "'", connection);
                                    notebookürünkodverisil.ExecuteNonQuery();

                                    // LOG DOYASI //
                                    using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                                    {
                                        Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürün_adi_textbox.Text + " ürününü sildi.", w);
                                    }
                                    using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                    {
                                        Kullanicigirisiform.DumpLog(r);
                                    }
                                    // LOG DOSYASI //
                                }
                                else
                                {
                                    MessageBox.Show("Seçmiş olduğunuz ürün sisteme kayıtlı değildir. Ürün isminin doğru olup olmadığını kontrol ederek tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                connection.Close();
                            }
                            else
                            {
                                MessageBox.Show("Seçtiğiniz ürün silinmedi.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception hata)
                        {
                            MessageBox.Show("Ürün silinirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                }     
                Listeyenile();
            }
        }

        private void Secili_ürünün_kod_seri_no_sil_btn_Click(object sender, EventArgs e)
        {
            if (notebook_ürün_kodlari_listbox.SelectedIndex == -1 && notebook_ürün_seri_no_listbox.SelectedIndex == -1)
            {
                MessageBox.Show("Silmek istediğiniz ürün kodunu, seri numarasını seçmeniz gerekmektedir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (notebook_ürün_kodlari_listbox.SelectedIndex >= 0)
                {
                    DialogResult dialog = MessageBox.Show("Seçili olan ürün kodu kaldırılacaktır. Kaldırılsın mı?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                                SqlCommand ürünstokverisil = new SqlCommand("delete from notebook_urun_kodları where urun_kodu = '" + ürün_stok_kodu_textbox.Text + "'", connection);
                                ürünstokverisil.ExecuteNonQuery();
                                MessageBox.Show("Seçmiş olduğunuz ürün kodu sistemden kaldırıldı.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                connection.Close();
                                // LOG DOYASI //
                                using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                                {
                                    Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürün_adi_textbox.Text + " ürününün " + ürün_stok_kodu_textbox.Text +" ürün kodunu sildi.", w);
                                }
                                using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                {
                                    Kullanicigirisiform.DumpLog(r);
                                }
                                // LOG DOSYASI //
                            }
                        }
                        catch (Exception hata)
                        {
                            MessageBox.Show("Ürün kodu kaldırılırken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                    else { MessageBox.Show("Seçili olan ürün kodu kaldırılmadı.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                if (notebook_ürün_seri_no_listbox.SelectedIndex >= 0)
                {
                    DialogResult dialog = MessageBox.Show("Seçili olan ürünün seri numarası kaldırılacaktır. Kaldırılsın mı?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        try
                        {
                            if (connection.State == ConnectionState.Closed)
                            {
                                connection.Open();
                                SqlCommand ürünstokverisil = new SqlCommand("delete from notebook_urun_seri_no_stok where urun_seri_no = '" + ürün_seri_no_textbox.Text + "'", connection);
                                ürünstokverisil.ExecuteNonQuery();
                                MessageBox.Show("Seçili ürün seri numarası kaldırıldı.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                connection.Close();
                                // LOG DOYASI //
                                using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                                {
                                    Kullanicigirisiform.Log(Kullanicigirisiform.username + " adlı kullanıcı " + ürün_adi_textbox.Text + " ürününün " + ürün_seri_no_textbox.Text + " seri numarasını sildi.", w);
                                }
                                using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                {
                                    Kullanicigirisiform.DumpLog(r);
                                }
                                // LOG DOSYASI //
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
                connection.Close();
                Listeyenile();
            }
        }

        private void Logout_label_Click(object sender, EventArgs e)
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
                MessageBox.Show("Kullanıcı bilgileri çekilirken bir hata oluştu.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + kullaniciaktifligi.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            Application.Exit();
        }

        private void Windows_kücültme_label_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        new
        #region forumharaketettirme
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Ürün_Ekle_ve_Düzenleme_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;

        }

        private void Ürün_Ekle_ve_Düzenleme_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Ürün_Ekle_ve_Düzenleme_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }
        #endregion
        #region forumharaketettirme2 
        private void Panel4_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Panel4_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }

        }

        private void Panel4_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }

        #endregion

    }
}
