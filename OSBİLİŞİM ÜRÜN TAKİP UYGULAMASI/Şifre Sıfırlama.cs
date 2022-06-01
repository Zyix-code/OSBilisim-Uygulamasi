using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace OSBilişim
{
    public partial class sifresıfırlamaforum : Form
    {
        public sifresıfırlamaforum()
        {
            InitializeComponent();
           
        }

        private void logout_label_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mail.google.com/");
        }
        public string güvenliksorusucevabı;
        string onaykodu, eposta = "deneme@gmail.com";
        string kullanicieskisifre;
        private void btn_giris_Click(object sender, EventArgs e)
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
                        }
                    }
                    else { MessageBox.Show("Girdiğiniz onay kodu yanlıştır. Lütfen düzeltiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("Girdiğiniz güvenlik sorusu cevabı yanlıştır. Lütfen düzeltiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show("Girdiğiniz şifreler birbiriyle uyuşmamaktadır. Lütfen düzeltiniz.", "OS BİLİŞİM", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        readonly SqlConnection connection = new SqlConnection("Data Source=192.168.1.118,1433;Network Library=DBMSSOCN; Initial Catalog=OSBİLİSİM;User Id=Admin; Password=1; MultipleActiveResultSets=True;");
        private void sifresıfırlamaforum_Load(object sender, EventArgs e)
        {
            yenisifretekrartextbox.UseSystemPasswordChar = true;
            yenisifretextbox.UseSystemPasswordChar = true;
            Kullanicigirisiform Kullanicigirisiform = new Kullanicigirisiform();
        }

        new
        #region forumharaket
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void sifresıfırlamaforum_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void sifresıfırlamaforum_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void sifresıfırlamaforum_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }
     

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
            this.Cursor = Cursors.Default;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
            this.Cursor = Cursors.SizeAll;
        }
        #endregion
        private void şifremiunuttumlinklabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
                        string konu = "OSBilişim Güvenlik Sorusu";
                        string icerik = kullanıcıadıtextbox.Text + " adlı kullanıcı güvenlik sorusu cevabı talebinde bulunmuştur, güvenlik sorusunun cevabı: " + güvenliksorusucevabı;

                        sc.Credentials = new NetworkCredential("teknik@trentatek.com.tr", "H35nYH63RS");
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress("teknik@trentatek.com.tr", "OS BİLİŞİM")
                        };
                        mail.To.Add(kime);
                        mail.Subject = konu;
                        mail.IsBodyHtml = true;
                        mail.Body = icerik;
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

        private void sifre_goster_gizle_checkbox_CheckedChanged(object sender, EventArgs e)
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

        private void btn_onaykodugönder_Click(object sender, EventArgs e)
        {
            try
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
                        string konu = "OSBilişim Şifre Sıfırlama";
                        string icerik = kullanıcıadıtextbox.Text + " adlı kullanıcı şifre sıfırlama talebinde bulunmuştur, şifre sıfırlama onay kodunuz: " + onaykodu;

                        sc.Credentials = new NetworkCredential("teknik@trentatek.com.tr", "H35nYH63RS");
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress("teknik@trentatek.com.tr", "OS BİLİŞİM")
                        };
                        mail.To.Add(kime);
                        mail.Subject = konu;
                        mail.IsBodyHtml = true;
                        mail.Body = icerik;
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
