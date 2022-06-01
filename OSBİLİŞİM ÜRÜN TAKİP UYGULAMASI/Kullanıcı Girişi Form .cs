using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Drawing;

namespace OSBilişim
{
 
    public partial class Kullanicigirisiform : Form
    {
        SqlDataReader dr;
        SqlCommand com;
        readonly Anaform anaform = new Anaform(); 
        readonly sifresıfırlamaforum sifresıfırlamaforum = new sifresıfırlamaforum();

        public static string username;
        public string versiyon = "17";
        public string güncelversiyon = "";
        public Kullanicigirisiform()
        {
            InitializeComponent();
            Kullanici_Data();
            
        }
        private void Kullanici_Data()
        {
            if (Properties.Settings.Default.kullaniciadi != string.Empty)
            {
                
                if (Properties.Settings.Default.benihatirla == true)
                {
                    kullaniciaditextbox.Text = Properties.Settings.Default.kullaniciadi;
                    sifretextbox.Text = Properties.Settings.Default.sifre;
                    beni_hatirla_checkbox.Checked = true;
                }
                else
                {
                    kullaniciaditextbox.Text = Properties.Settings.Default.kullaniciadi;
                    sifretextbox.Text = Properties.Settings.Default.sifre;
                }
            }
        }
        private void BeniHatırla()
        {
            if (beni_hatirla_checkbox.Checked)
            {
                
                Properties.Settings.Default.kullaniciadi = kullaniciaditextbox.Text.Trim();
                Properties.Settings.Default.sifre = sifretextbox.Text.Trim();
                Properties.Settings.Default.benihatirla = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.kullaniciadi = "";
                Properties.Settings.Default.sifre = "";
                Properties.Settings.Default.benihatirla = false;
                Properties.Settings.Default.Save();
            }
        }
        public static void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("-------------------------------");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine($"{logMessage}");
        }
        public static void Parcalarlog (string logMessage, TextWriter x)
        {
            x.WriteLine($"{logMessage}");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        private void Btn_giris_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    username = kullaniciaditextbox.Text;
                    string password = sifretextbox.Text;

                   
                    com = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "Select * From kullanicilar where k_adi='" + kullaniciaditextbox.Text + "'and sifre ='" + sifretextbox.Text + "'"
                    };
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        string adi, soyadi;
                        SqlCommand kullanicilar = new SqlCommand("SELECT *FROM kullanicilar where k_adi = '" + Kullanicigirisiform.username + "'", connection);
                        SqlDataReader kullaniciaciklamasi;
                        kullaniciaciklamasi = kullanicilar.ExecuteReader();
                        while (kullaniciaciklamasi.Read())
                        {
                            adi = ((string)kullaniciaciklamasi["kullanici_isim"]);
                            soyadi = ((string)kullaniciaciklamasi["kullanici_soyisim"]);
                            if (username == "Admin")
                            {
                                MessageBox.Show("Admin olarak giriş yaptınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Merhaba hoş geldin, " + adi + " " + soyadi + " sisteme yönlendiriliyorsun.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // LOG DOSYASI //
                                using (StreamWriter w = File.AppendText("OSBilisim-log.xml"))
                                {
                                    Log(adi + " " + soyadi + " sisteme " + "(" + username + ")" + " kullanıcı adı ile giriş yaptı.", w);
                                }
                                using (StreamReader r = File.OpenText("OSBilisim-log.xml"))
                                {
                                    DumpLog(r);
                                }
                                // LOG DOSYASI //
                            }
                        }

                        kullaniciaciklamasi.Close();

                        SqlCommand kullanicidurumgüncelle = new SqlCommand("Update kullanicilar set durum='" + 1 + "' where k_adi = '" + username + "'", connection);
                        kullanicidurumgüncelle.ExecuteNonQuery();

                        username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Kullanicigirisiform.username);
                        username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Kullanicigirisiform.username);

                      

                        anaform.Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Şifre ya da kullanıcı adı hatalıdır tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        kullaniciaditextbox.Clear();
                        sifretextbox.Clear();
                        kullaniciaditextbox.Focus();
                    }
                    connection.Close();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Server ile bağlantı kurulmadı lütfen internet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        private void Yoneticizni()
        {
            if (!Yoneticiznikontrol())
            {
                ProcessStartInfo program = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Assembly.GetEntryAssembly().CodeBase,
                    Verb = "runas"
                };
                try
                {
                    Process.Start(program);
                    Environment.Exit(0);
                }
                catch (Exception)
                {
                    MessageBox.Show("Uygulama yönetici olarak çalıştırılmadığından başlatılmayacaktır. ", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);    
                }
            }
        }
        private bool Yoneticiznikontrol()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public string günlükveritabanıadı;
        private void Kullanicigirisiform_Load(object sender, EventArgs e)
        {
            sifretextbox.UseSystemPasswordChar = true;
            //Yoneticizni();
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                    SqlCommand üründurum = new SqlCommand("select *from version", connection);
                    SqlDataReader üründurumusorgulama;
                    üründurumusorgulama = üründurum.ExecuteReader();
                    while (üründurumusorgulama.Read())
                    {
                        güncelversiyon = ((string)üründurumusorgulama["version"]);
                        if (Convert.ToInt16(versiyon) <= Convert.ToInt16(güncelversiyon))
                        {
                            DialogResult dialog = new DialogResult();
                            dialog = MessageBox.Show("Uygulamanızın yeni sürümünü indirmek ister misiniz?", "OS BİLİŞİM", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dialog == DialogResult.Yes)
                            {
                                MessageBox.Show(((string)üründurumusorgulama["versiyon_aciklama"]), "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Process.Start((string)üründurumusorgulama["yeni_program_indirme_linki"]);
                                Application.Exit();
                                break;
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
            }
            catch (Exception hata)
            {
                MessageBox.Show("Program başlatılmadı.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            /*if (Process.GetProcessesByName("OSBilişim").Length > 1)
            {
                MessageBox.Show("OSBilişim uygulaması çalışıyor açık olan uygulamayı kapatıp tekrar deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }*/
        }

        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.118,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        private void Kullanicigirisiform_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Sifre_goster_gizle_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sifre_goster_gizle_checkbox.Checked == true)
            {
                sifretextbox.UseSystemPasswordChar = false;
            }
            else
            {
                sifretextbox.UseSystemPasswordChar = true;
            }
        }
        private void Beni_hatirla_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            BeniHatırla();
        }

        private void Logout_label_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("OSBilişim"))
            {
                process.Kill();
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/");
        }
        
        private void Label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #region forumharaketettirme
        new

        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Kullanicigirisiform_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }

        private void Kullanicigirisiform_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Kullanicigirisiform_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }

        }
        #endregion mousedown
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
        #region sifrelemetxt
        /*  string hash = "";

          string sifrele(string text)
          {
             string path = @"OSBilisim-log.xml";
             byte[] data = Encoding.Default.GetBytes(path);
              using (MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider())
              {
                  byte[] keys = mD5.ComputeHash(Encoding.Default.GetBytes(hash));
                  using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                  {
                      Key = keys,
                      Mode = CipherMode.ECB,
                      Padding = PaddingMode.PKCS7
                  })
                  {
                      ICryptoTransform transform = tripleDES.CreateEncryptor();
                      byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                      return Convert.ToBase64String(results, 0, results.Length);
                  }
              }

            //MessageBox.Show(sifrele(path)); kullanımı
          }
          string sifrecöz(string text)
          {
              byte[] data = Convert.FromBase64String(kullaniciaditextbox.Text);
              using (MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider())
              {
                  byte[] keys = mD5.ComputeHash(Encoding.Default.GetBytes(hash));
                  using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider()
                  {
                      Key = keys,
                      Mode = CipherMode.ECB,
                      Padding = PaddingMode.PKCS7
                  })
                  {
                      ICryptoTransform transform = tripleDES.CreateDecryptor();
                      byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                      return Encoding.Default.GetString(results);
                  }
              }
             // MessageBox.Show(sifrecöz(kullaniciaditextbox.text)); kullanımı
          }*/
        #endregion
        private void Şifremiunuttumlinklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sifresıfırlamaforum.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();

        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                
                //Yazı fontumu ve çizgi çizmek için fırçamı ve kalem nesnemi oluşturdum
                Font myFont = new Font("Calibri", 20, FontStyle.Bold);
                Font tarihfont = new Font("Calibri", 9, FontStyle.Bold);
                SolidBrush sbrush = new SolidBrush(Color.Black);
                Pen myPen = new Pen(Color.Black);
                StringFormat ortala = new StringFormat();
                ortala.Alignment = StringAlignment.Center;
         
                e.Graphics.DrawRectangle(myPen, 12,5,Width,160);
                e.Graphics.DrawImage(Properties.Resources.footer, 15,15,220,150);
                e.Graphics.DrawLine(myPen, new Point(250, 165), new Point(250,5));
                e.Graphics.DrawString("ÜRÜN/HİZMET\nSİPARİŞ FORMU", myFont, sbrush ,320, 46);
                e.Graphics.DrawLine(myPen, new Point(570, 165), new Point(570, 5));
                e.Graphics.DrawString($"Oruç Reis Mah. Tekstil Kent Cad. Tekstilkent Sit.\nA 13 Blok No:63 Esenler / İSTANBUL\n+90 531 263 89 16\nwww.osbilisim.com.tr\ninfo@osbilisim.com.tr\nTarih: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} ", tarihfont,sbrush, 693, 50,ortala);
                //  e.Graphics.DrawLine(myPen, 120, 180, 750, 180);
                // e.Graphics.DrawString("SİPARİŞ FORMU", myFont, sbrush, 200, 120);


                myFont = new Font("Calibri", 12, FontStyle.Bold);
                e.Graphics.DrawString("Adet", myFont, sbrush, 140, 328);
                e.Graphics.DrawString("Ürün Adı", myFont, sbrush, 240, 328);
                e.Graphics.DrawString("Birim Fiyatı", myFont, sbrush, 440, 328);
                e.Graphics.DrawString("Fiyat", myFont, sbrush, 640, 328);
                

            }
            catch
            {

            }
        }
    }
}
