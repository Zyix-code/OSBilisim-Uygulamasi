namespace OSBilişim
{
    partial class Siparisolusturmaform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Siparisolusturmaform));
            this.ürünadi = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ürünserino_checklistbox = new System.Windows.Forms.CheckedListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.aciklama_textbox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ürün_hazirlik_durumu_textbox = new System.Windows.Forms.TextBox();
            this.ürünstokkodu = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ürünadetitextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ürünün_satıldığı_firma = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.satış_yapılan_firma = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.alicisoyaditextboxt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.aliciaditextbox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.malzeme_ekle_btn = new System.Windows.Forms.Button();
            this.siparis_gonder = new System.Windows.Forms.Button();
            this.kullanilacak_malzeme_adeti_textbox = new System.Windows.Forms.TextBox();
            this.liste_temizle = new System.Windows.Forms.Button();
            this.kullanilacak_malzemeler_listbox = new System.Windows.Forms.ListBox();
            this.kullanilacak_malzemeler_listesi = new System.Windows.Forms.ListBox();
            this.silbtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ana_menü_btn = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.loginpanel_hosgeldiniz_label = new System.Windows.Forms.Label();
            this.loginpanel_gelistiren_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.windows_kücültme_label = new System.Windows.Forms.Label();
            this.logout_label = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ürünadi
            // 
            this.ürünadi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ürünadi.FormattingEnabled = true;
            this.ürünadi.Location = new System.Drawing.Point(113, 20);
            this.ürünadi.Name = "ürünadi";
            this.ürünadi.Size = new System.Drawing.Size(180, 24);
            this.ürünadi.TabIndex = 1;
            this.ürünadi.SelectedIndexChanged += new System.EventHandler(this.Ürunadi_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ürünserino_checklistbox);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.aciklama_textbox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.ürün_hazirlik_durumu_textbox);
            this.groupBox1.Controls.Add(this.ürünstokkodu);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ürünadetitextbox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ürünadi);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.groupBox1.Location = new System.Drawing.Point(396, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 360);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ÜRÜN BİLGİLERİ";
            // 
            // ürünserino_checklistbox
            // 
            this.ürünserino_checklistbox.FormattingEnabled = true;
            this.ürünserino_checklistbox.Location = new System.Drawing.Point(113, 80);
            this.ürünserino_checklistbox.Name = "ürünserino_checklistbox";
            this.ürünserino_checklistbox.Size = new System.Drawing.Size(180, 52);
            this.ürünserino_checklistbox.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "Ürün seri no: ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 195);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "Ürün açıklaması: ";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aciklama_textbox
            // 
            this.aciklama_textbox.Location = new System.Drawing.Point(113, 192);
            this.aciklama_textbox.Multiline = true;
            this.aciklama_textbox.Name = "aciklama_textbox";
            this.aciklama_textbox.Size = new System.Drawing.Size(180, 154);
            this.aciklama_textbox.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 16);
            this.label9.TabIndex = 8;
            this.label9.Text = "Ürün durumu: ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ürün_hazirlik_durumu_textbox
            // 
            this.ürün_hazirlik_durumu_textbox.Location = new System.Drawing.Point(113, 165);
            this.ürün_hazirlik_durumu_textbox.Name = "ürün_hazirlik_durumu_textbox";
            this.ürün_hazirlik_durumu_textbox.ReadOnly = true;
            this.ürün_hazirlik_durumu_textbox.Size = new System.Drawing.Size(180, 21);
            this.ürün_hazirlik_durumu_textbox.TabIndex = 7;
            this.ürün_hazirlik_durumu_textbox.Text = "Sipariş Beklemede";
            // 
            // ürünstokkodu
            // 
            this.ürünstokkodu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ürünstokkodu.FormattingEnabled = true;
            this.ürünstokkodu.Location = new System.Drawing.Point(113, 50);
            this.ürünstokkodu.Name = "ürünstokkodu";
            this.ürünstokkodu.Size = new System.Drawing.Size(180, 24);
            this.ürünstokkodu.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ürün adeti: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ürünadetitextbox
            // 
            this.ürünadetitextbox.Location = new System.Drawing.Point(113, 138);
            this.ürünadetitextbox.Name = "ürünadetitextbox";
            this.ürünadetitextbox.Size = new System.Drawing.Size(180, 21);
            this.ürünadetitextbox.TabIndex = 4;
            this.ürünadetitextbox.Text = "0";
            this.ürünadetitextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ürünadetitextbox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ürün stok kodu: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ürün adı: ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox2.Controls.Add(this.ürünün_satıldığı_firma);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.satış_yapılan_firma);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.alicisoyaditextboxt);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.aliciaditextbox);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.groupBox2.Location = new System.Drawing.Point(744, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 144);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ALICI VE FİRMA BİLGİLERİ";
            // 
            // ürünün_satıldığı_firma
            // 
            this.ürünün_satıldığı_firma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ürünün_satıldığı_firma.FormattingEnabled = true;
            this.ürünün_satıldığı_firma.Items.AddRange(new object[] {
            "Hepsiburada",
            "Gittigidiyor",
            "Trendyol",
            "N11",
            "Diğer"});
            this.ürünün_satıldığı_firma.Location = new System.Drawing.Point(158, 106);
            this.ürünün_satıldığı_firma.Name = "ürünün_satıldığı_firma";
            this.ürünün_satıldığı_firma.Size = new System.Drawing.Size(162, 24);
            this.ürünün_satıldığı_firma.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 109);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 16);
            this.label8.TabIndex = 13;
            this.label8.Text = "Ürünün satıldığı firma: ";
            // 
            // satış_yapılan_firma
            // 
            this.satış_yapılan_firma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.satış_yapılan_firma.FormattingEnabled = true;
            this.satış_yapılan_firma.Items.AddRange(new object[] {
            "OS BİLİŞİM",
            "TRENTA TEKNOLOJİ"});
            this.satış_yapılan_firma.Location = new System.Drawing.Point(158, 76);
            this.satış_yapılan_firma.Name = "satış_yapılan_firma";
            this.satış_yapılan_firma.Size = new System.Drawing.Size(162, 24);
            this.satış_yapılan_firma.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Satış yapılan firma: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Teslim alacak kişi soyadı: ";
            // 
            // alicisoyaditextboxt
            // 
            this.alicisoyaditextboxt.Location = new System.Drawing.Point(158, 49);
            this.alicisoyaditextboxt.Name = "alicisoyaditextboxt";
            this.alicisoyaditextboxt.Size = new System.Drawing.Size(162, 21);
            this.alicisoyaditextboxt.TabIndex = 8;
            this.alicisoyaditextboxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Alicisoyaditextboxt_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Teslim alacak kişi adı: ";
            // 
            // aliciaditextbox
            // 
            this.aliciaditextbox.Location = new System.Drawing.Point(158, 22);
            this.aliciaditextbox.Name = "aliciaditextbox";
            this.aliciaditextbox.Size = new System.Drawing.Size(162, 21);
            this.aliciaditextbox.TabIndex = 6;
            this.aliciaditextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Aliciaditextbox_KeyPress);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.malzeme_ekle_btn);
            this.groupBox3.Controls.Add(this.siparis_gonder);
            this.groupBox3.Controls.Add(this.kullanilacak_malzeme_adeti_textbox);
            this.groupBox3.Controls.Add(this.liste_temizle);
            this.groupBox3.Controls.Add(this.kullanilacak_malzemeler_listbox);
            this.groupBox3.Controls.Add(this.kullanilacak_malzemeler_listesi);
            this.groupBox3.Controls.Add(this.silbtn);
            this.groupBox3.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.groupBox3.Location = new System.Drawing.Point(396, 393);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(696, 277);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "KULLANILACAK MALZEMELER";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(40, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Ürün adeti: ";
            // 
            // malzeme_ekle_btn
            // 
            this.malzeme_ekle_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.malzeme_ekle_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.malzeme_ekle_btn.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.malzeme_ekle_btn.ForeColor = System.Drawing.Color.Black;
            this.malzeme_ekle_btn.Location = new System.Drawing.Point(147, 226);
            this.malzeme_ekle_btn.Name = "malzeme_ekle_btn";
            this.malzeme_ekle_btn.Size = new System.Drawing.Size(121, 32);
            this.malzeme_ekle_btn.TabIndex = 1;
            this.malzeme_ekle_btn.Text = "Malzeme seç";
            this.toolTip1.SetToolTip(this.malzeme_ekle_btn, "Yukardaki malzemeleri sağ taraftaki gönderilecek malzemeler bölümüne eklemenizi s" +
        "ağlar.");
            this.malzeme_ekle_btn.UseVisualStyleBackColor = false;
            this.malzeme_ekle_btn.Click += new System.EventHandler(this.Malzeme_ekle_btn_Click);
            // 
            // siparis_gonder
            // 
            this.siparis_gonder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.siparis_gonder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.siparis_gonder.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.siparis_gonder.ForeColor = System.Drawing.Color.Black;
            this.siparis_gonder.Location = new System.Drawing.Point(446, 236);
            this.siparis_gonder.Name = "siparis_gonder";
            this.siparis_gonder.Size = new System.Drawing.Size(121, 32);
            this.siparis_gonder.TabIndex = 4;
            this.siparis_gonder.Text = "Sipariş oluştur";
            this.toolTip1.SetToolTip(this.siparis_gonder, "Siparişi tamamladıktan sonra oluşturmanızı sağlar.");
            this.siparis_gonder.UseVisualStyleBackColor = false;
            this.siparis_gonder.Click += new System.EventHandler(this.Siparis_gonder_Click);
            // 
            // kullanilacak_malzeme_adeti_textbox
            // 
            this.kullanilacak_malzeme_adeti_textbox.Location = new System.Drawing.Point(113, 200);
            this.kullanilacak_malzeme_adeti_textbox.Name = "kullanilacak_malzeme_adeti_textbox";
            this.kullanilacak_malzeme_adeti_textbox.Size = new System.Drawing.Size(180, 21);
            this.kullanilacak_malzeme_adeti_textbox.TabIndex = 7;
            this.kullanilacak_malzeme_adeti_textbox.Text = "0";
            this.kullanilacak_malzeme_adeti_textbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Kullanilacak_malzeme_adeti_textbox_KeyPress);
            // 
            // liste_temizle
            // 
            this.liste_temizle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.liste_temizle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.liste_temizle.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.liste_temizle.ForeColor = System.Drawing.Color.Black;
            this.liste_temizle.Location = new System.Drawing.Point(504, 198);
            this.liste_temizle.Name = "liste_temizle";
            this.liste_temizle.Size = new System.Drawing.Size(121, 32);
            this.liste_temizle.TabIndex = 3;
            this.liste_temizle.Text = "Listeyi temizle";
            this.toolTip1.SetToolTip(this.liste_temizle, "Bütün listeyi temizlemenizi sağlar.");
            this.liste_temizle.UseVisualStyleBackColor = false;
            this.liste_temizle.Click += new System.EventHandler(this.Liste_temizle_Click);
            // 
            // kullanilacak_malzemeler_listbox
            // 
            this.kullanilacak_malzemeler_listbox.FormattingEnabled = true;
            this.kullanilacak_malzemeler_listbox.ItemHeight = 16;
            this.kullanilacak_malzemeler_listbox.Location = new System.Drawing.Point(17, 19);
            this.kullanilacak_malzemeler_listbox.Name = "kullanilacak_malzemeler_listbox";
            this.kullanilacak_malzemeler_listbox.Size = new System.Drawing.Size(314, 164);
            this.kullanilacak_malzemeler_listbox.TabIndex = 0;
            // 
            // kullanilacak_malzemeler_listesi
            // 
            this.kullanilacak_malzemeler_listesi.FormattingEnabled = true;
            this.kullanilacak_malzemeler_listesi.ItemHeight = 16;
            this.kullanilacak_malzemeler_listesi.Location = new System.Drawing.Point(337, 19);
            this.kullanilacak_malzemeler_listesi.Name = "kullanilacak_malzemeler_listesi";
            this.kullanilacak_malzemeler_listesi.Size = new System.Drawing.Size(315, 164);
            this.kullanilacak_malzemeler_listesi.Sorted = true;
            this.kullanilacak_malzemeler_listesi.TabIndex = 1;
            // 
            // silbtn
            // 
            this.silbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.silbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.silbtn.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.silbtn.ForeColor = System.Drawing.Color.Black;
            this.silbtn.Location = new System.Drawing.Point(377, 198);
            this.silbtn.Name = "silbtn";
            this.silbtn.Size = new System.Drawing.Size(121, 32);
            this.silbtn.TabIndex = 2;
            this.silbtn.Text = "Listedeki ürünü sil";
            this.toolTip1.SetToolTip(this.silbtn, "Seçtiğiniz bir ürünü listeden silmenizi sağlar.");
            this.silbtn.UseVisualStyleBackColor = false;
            this.silbtn.Click += new System.EventHandler(this.Silbtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.panel2.Controls.Add(this.ana_menü_btn);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.loginpanel_hosgeldiniz_label);
            this.panel2.Controls.Add(this.loginpanel_gelistiren_label);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(-2, -4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(377, 703);
            this.panel2.TabIndex = 14;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseMove);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseUp);
            // 
            // ana_menü_btn
            // 
            this.ana_menü_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.ana_menü_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ana_menü_btn.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.ana_menü_btn.ForeColor = System.Drawing.SystemColors.Window;
            this.ana_menü_btn.Location = new System.Drawing.Point(14, 641);
            this.ana_menü_btn.Name = "ana_menü_btn";
            this.ana_menü_btn.Size = new System.Drawing.Size(121, 32);
            this.ana_menü_btn.TabIndex = 12;
            this.ana_menü_btn.Text = "Ana menüye dön";
            this.toolTip1.SetToolTip(this.ana_menü_btn, "Ana menüye dönmenizi sağlar.");
            this.ana_menü_btn.UseVisualStyleBackColor = false;
            this.ana_menü_btn.Click += new System.EventHandler(this.Ana_menü_btn_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 16F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(81, 333);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(229, 25);
            this.label12.TabIndex = 10;
            this.label12.Text = "SİPARİŞ OLUŞTURMA";
            // 
            // loginpanel_hosgeldiniz_label
            // 
            this.loginpanel_hosgeldiniz_label.AutoSize = true;
            this.loginpanel_hosgeldiniz_label.Font = new System.Drawing.Font("Century Gothic", 16F);
            this.loginpanel_hosgeldiniz_label.ForeColor = System.Drawing.Color.White;
            this.loginpanel_hosgeldiniz_label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.loginpanel_hosgeldiniz_label.Location = new System.Drawing.Point(128, 303);
            this.loginpanel_hosgeldiniz_label.Name = "loginpanel_hosgeldiniz_label";
            this.loginpanel_hosgeldiniz_label.Size = new System.Drawing.Size(123, 25);
            this.loginpanel_hosgeldiniz_label.TabIndex = 9;
            this.loginpanel_hosgeldiniz_label.Text = "OS BİLİŞİM\r\n";
            // 
            // loginpanel_gelistiren_label
            // 
            this.loginpanel_gelistiren_label.AutoSize = true;
            this.loginpanel_gelistiren_label.Font = new System.Drawing.Font("Century Gothic", 7F);
            this.loginpanel_gelistiren_label.ForeColor = System.Drawing.Color.White;
            this.loginpanel_gelistiren_label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.loginpanel_gelistiren_label.Location = new System.Drawing.Point(267, 646);
            this.loginpanel_gelistiren_label.Name = "loginpanel_gelistiren_label";
            this.loginpanel_gelistiren_label.Size = new System.Drawing.Size(107, 30);
            this.loginpanel_gelistiren_label.TabIndex = 7;
            this.loginpanel_gelistiren_label.Text = "        Selçuk Şahin \r\nTarafından geliştirildi\r\n";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(47, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(282, 207);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // windows_kücültme_label
            // 
            this.windows_kücültme_label.AutoSize = true;
            this.windows_kücültme_label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windows_kücültme_label.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.windows_kücültme_label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.windows_kücültme_label.Location = new System.Drawing.Point(1104, 7);
            this.windows_kücültme_label.Name = "windows_kücültme_label";
            this.windows_kücültme_label.Size = new System.Drawing.Size(22, 25);
            this.windows_kücültme_label.TabIndex = 24;
            this.windows_kücültme_label.Text = "-";
            this.toolTip1.SetToolTip(this.windows_kücültme_label, "Uygulamayı küçültmenizi sağlar.");
            this.windows_kücültme_label.Click += new System.EventHandler(this.Windows_kücültme_label_Click);
            // 
            // logout_label
            // 
            this.logout_label.AutoSize = true;
            this.logout_label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logout_label.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.logout_label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.logout_label.Location = new System.Drawing.Point(1124, 8);
            this.logout_label.Name = "logout_label";
            this.logout_label.Size = new System.Drawing.Size(28, 25);
            this.logout_label.TabIndex = 23;
            this.logout_label.Text = "X";
            this.toolTip1.SetToolTip(this.logout_label, "Uygulamayı kapatmanızı sağlar.");
            this.logout_label.Click += new System.EventHandler(this.Logout_label_Click);
            // 
            // Siparisolusturmaform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1159, 681);
            this.Controls.Add(this.windows_kücültme_label);
            this.Controls.Add(this.logout_label);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Siparisolusturmaform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sipariş Oluşturma Forum";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Siparisolusturmaform_FormClosed);
            this.Load += new System.EventHandler(this.Siparisolusturmaform_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Siparisolusturmaform_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Siparisolusturmaform_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Siparisolusturmaform_MouseUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox ürünadi;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ürünadetitextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox aliciaditextbox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox kullanilacak_malzemeler_listbox;
        private System.Windows.Forms.ComboBox ürünstokkodu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox alicisoyaditextboxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button malzeme_ekle_btn;
        private System.Windows.Forms.TextBox kullanilacak_malzeme_adeti_textbox;
        private System.Windows.Forms.ComboBox ürünün_satıldığı_firma;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox satış_yapılan_firma;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox ürün_hazirlik_durumu_textbox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox aciklama_textbox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label loginpanel_gelistiren_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button siparis_gonder;
        private System.Windows.Forms.Button liste_temizle;
        private System.Windows.Forms.Button silbtn;
        private System.Windows.Forms.ListBox kullanilacak_malzemeler_listesi;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label loginpanel_hosgeldiniz_label;
        private System.Windows.Forms.Button ana_menü_btn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label windows_kücültme_label;
        private System.Windows.Forms.Label logout_label;
        private System.Windows.Forms.CheckedListBox ürünserino_checklistbox;
    }
}