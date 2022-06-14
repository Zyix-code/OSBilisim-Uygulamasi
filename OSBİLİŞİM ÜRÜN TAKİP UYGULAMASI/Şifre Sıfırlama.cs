using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;

namespace OSBilişim
{
    public partial class sifresıfırlamaforum : Form
    {
        public sifresıfırlamaforum()
        {
            InitializeComponent();
           
        }

        private void Logout_label_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/");
        }
        public string güvenliksorusucevabı;
        string onaykodu, eposta = "deneme@gmail.com";
        string kullanicieskisifre;
        private void Btn_giris_Click(object sender, EventArgs e)
        {
            Kullanicigirisiform kullanicigirisiform = new Kullanicigirisiform();
            if (yenisifretextbox.Text == yenisifretekrartextbox.Text)
            {
                if (connection.State == ConnectionState.Closed)
                connection.Open();
                SqlCommand komut3 = new SqlCommand("SELECT * FROM kullanicilar where k_adi ='" + kullanıcıadıtextbox.Text + "'", connection);
                SqlDataReader veriokuyucu3;
                veriokuyucu3 = komut3.ExecuteReader();
                while (veriokuyucu3.Read())
                {
                    güvenliksorusucevabı = veriokuyucu3["kullanici_güvenlik_sorusu_cevabı"].ToString();
                    kullanicieskisifre = veriokuyucu3["sifre"].ToString();
                }
                veriokuyucu3.Close();
                if (güvenliksorusutextbox.Text == güvenliksorusucevabı )
                {
                    if (onaykodu == güvenlikonaykodutextbox.Text)
                    {
                        if (kullanicieskisifre == yenisifretextbox.Text)
                        { MessageBox.Show("Girdiğiniz şifre eski şifreniz ile uyuşmaktadır. Lütfen bu şifre ile giriş sağlayınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        else
                        {
                            SqlCommand ürümdurumunugüncelle = new SqlCommand("update kullanicilar set sifre= '" + yenisifretextbox.Text + "' where k_adi = '" + kullanıcıadıtextbox.Text + "'", connection);
                            ürümdurumunugüncelle.ExecuteNonQuery();
                            MessageBox.Show("Şifreniz başarılı şekilde sıfırlanmıştır, lütfen tekrar giriş yapınız.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Onaykoduolustur();
                        }
                    }
                    else { MessageBox.Show("Girdiğiniz onay kodu yanlıştır. Lütfen düzeltiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("Girdiğiniz güvenlik sorusu cevabı yanlıştır. Lütfen düzeltiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show("Girdiğiniz şifreler birbiriyle uyuşmamaktadır. Lütfen düzeltiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.106,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        private void Sifresıfırlamaforum_Load(object sender, EventArgs e)
        {
            yenisifretekrartextbox.UseSystemPasswordChar = true;
            yenisifretextbox.UseSystemPasswordChar = true;
            Kullanicigirisiform Kullanicigirisiform = new Kullanicigirisiform();
            
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

        new
        #region forumharaket
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Sifresıfırlamaforum_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void Sifresıfırlamaforum_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Sifresıfırlamaforum_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }
     

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
        private void Sifremiunuttumlinklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                SmtpClient sc = new SmtpClient
                {
                    Port = 587,
                    Host = "delta.veriyum.net",
                    EnableSsl = true
                };
                if (kullanıcıadıtextbox.Text == "")
                {
                    MessageBox.Show("Sistem'de hangi kullanıcı adını aramalıyız? Lütfen geçerli bir kullanıcı adı giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataReader kullanıcısorgula;
                    SqlCommand kullanıcıarama = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "Select * From kullanicilar where k_adi='" + kullanıcıadıtextbox.Text + "'"
                    };

                    kullanıcısorgula = kullanıcıarama.ExecuteReader();
                    if (kullanıcısorgula.Read())
                    {
                        kullanıcısorgula.Close();

                        SqlCommand komut3 = new SqlCommand("SELECT * FROM kullanicilar where k_adi ='" + kullanıcıadıtextbox.Text + "'", connection);
                        SqlDataReader veriokuyucu3;
                        veriokuyucu3 = komut3.ExecuteReader();
                        while (veriokuyucu3.Read())
                        {
                            eposta = veriokuyucu3["kullanici_eposta"].ToString();
                            güvenliksorusucevabı = veriokuyucu3["kullanici_güvenlik_sorusu_cevabı"].ToString();
                        }
                        veriokuyucu3.Close();
                        string kime = eposta;
                        string konu = "OSBİLİŞİM - Güvenlik Sorusu";
                        sc.Credentials = new NetworkCredential("teknik@trentatek.com.tr", "H35nYH63RS");
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress("teknik@trentatek.com.tr", "OS BİLİŞİM")
                        };
                        mail.To.Add(kime);
                        mail.Subject = konu;
                        mail.IsBodyHtml = true;
                        string htmlString =
                            " <html>" +
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
                            " <p> " + kullanıcıadıtextbox.Text + " adlı kullanıcı güvenlik sorusu cevabı talebinde bulunmuştur.<br>Güvenlik sorusu cevabınız: " + güvenliksorusucevabı + " </br></p> " +
                            " <div class='altalan'></div>" +
                            " <p>" +
                            " Bu e-posta otomatik oluşturulmuştur. Lütfen cevap vermeyiniz." +
                            " </p>" +
                            " </div>" +
                            " </body>" +
                            " </html>";
                        mail.Body = htmlString;
                        sc.Send(mail);
                        MessageBox.Show("Kullanıcı adınıza ait e-mail adresini kontrol ediniz, güvenlik sorusu cevabı gönderilmiştir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else { MessageBox.Show("Böyle bir kullanıcı bulunmamaktadır, lütfen yeniden deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Güvenlik sorusu cevabı talebiniz oluşturulmadı, bir hata oluştu lütfen daha sonra tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            connection.Close();
        }

        private void Sifre_goster_gizle_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sifre_goster_gizle_checkbox.Checked == true)
            {
                yenisifretextbox.UseSystemPasswordChar = false;
                yenisifretekrartextbox.UseSystemPasswordChar = false;
            }
            else
            {
                yenisifretextbox.UseSystemPasswordChar = true;
                yenisifretekrartextbox.UseSystemPasswordChar = true;
            }
        }
        public void Onaykoduolustur()
        {
            Random random = new Random();
            int s1, s2, s3, s4;
            int h1, h2, h3;
            s1 = random.Next(1, 10);
            s2 = random.Next(10, 20);
            s3 = random.Next(20, 30);
            s4 = random.Next(30, 40);
            h1 = random.Next(65, 91);
            h2 = random.Next(65, 91);
            h3 = random.Next(65, 91);
            char k1, k2, k3;
            k1 = Convert.ToChar(h1);
            k2 = Convert.ToChar(h2);
            k3 = Convert.ToChar(h3);
            onaykodu = s1.ToString() + s2.ToString() + k1 + s3.ToString() + k2 + s4.ToString() + k3;
        }
        private void Btn_onaykodugönder_Click(object sender, EventArgs e)
        {
            try
            {
                Onaykoduolustur();
                SmtpClient sc = new SmtpClient
                {
                    Port = 587,
                    Host = "delta.veriyum.net",
                    EnableSsl = true
                };
                if (kullanıcıadıtextbox.Text == "")
                {
                    MessageBox.Show("Sistem'de hangi kullanıcı adını aramalıyız? Lütfen geçerli bir kullanıcı adı giriniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    SqlDataReader kullanıcısorgula;
                    SqlCommand kullanıcıarama = new SqlCommand
                    {
                        Connection = connection,
                        CommandText = "Select * From kullanicilar where k_adi='" + kullanıcıadıtextbox.Text + "'"
                    };

                    kullanıcısorgula = kullanıcıarama.ExecuteReader();
                    if (kullanıcısorgula.Read())
                    {
                        kullanıcısorgula.Close();
                   
                        SqlCommand komut3 = new SqlCommand("SELECT * FROM kullanicilar where k_adi ='" + kullanıcıadıtextbox.Text + "'", connection);
                        SqlDataReader veriokuyucu3;
                        veriokuyucu3 = komut3.ExecuteReader();
                        while (veriokuyucu3.Read())
                        {
                            eposta = veriokuyucu3["kullanici_eposta"].ToString();

                        }
                        veriokuyucu3.Close();
                        string kime = eposta;
                        string konu = "OSBİLİŞİM - Parola Sıfırlama";

                        sc.Credentials = new NetworkCredential("teknik@trentatek.com.tr", "H35nYH63RS");
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress("teknik@trentatek.com.tr", "OS BİLİŞİM")
                        };
                        mail.To.Add(kime);
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
                            " <p> " + kullanıcıadıtextbox.Text + " adlı kullanıcı şifre sıfırlama talebinde bulunmuştur.<br>Şifre sıfırlama onay kodunuz: " + onaykodu + " </br></p> " +
                            " <div class='altalan'></div>" +
                            " <p>" +
                            " Bu e-posta otomatik oluşturulmuştur. Lütfen cevap vermeyiniz." +
                            " </p>" +
                            " </div>" +
                            " </body>" +
                            " </html>";
                        mail.Body = htmlString;
                        sc.Send(mail);
                        MessageBox.Show("Kullanıcı adınıza ait e-mail adresini kontrol ediniz, onay kodu gönderilmiştir.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else { MessageBox.Show("Böyle bir kullanıcı bulunmamaktadır, lütfen yeniden deneyiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Şifre sıfırlama talebiniz oluşturulurken bir hata oluştu lütfen daha sonra tekrar deneyiniz.\nİnternet bağlantınızı ya da server bağlantınızı kontrol edin.\nHata kodu: " + hata.Message, "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            connection.Close();
        }
    }
}
