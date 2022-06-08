namespace OSBilişim
{
    partial class Ürüneklemedüzenlemeform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ürüneklemedüzenlemeform));
            this.notebook_ürün_kodlari_listbox = new System.Windows.Forms.ListBox();
            this.notebook_ürünler_listbox = new System.Windows.Forms.ListBox();
            this.ürün_adi_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.yeni_ürün_ekle_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.notebook_ürün_seri_no_listbox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ana_menü_btn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.secili_ürünü_sil_btn = new System.Windows.Forms.Button();
            this.secili_ürünün_kod_seri_no_sil_btn = new System.Windows.Forms.Button();
            this.windows_kücültme_label = new System.Windows.Forms.Label();
            this.logout_label = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ürün_stok_kodu_textbox = new System.Windows.Forms.TextBox();
            this.ürün_adi_textbox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ürün_seri_no_textbox = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.loginpanel_gelistiren_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notebook_ürün_kodlari_listbox
            // 
            this.notebook_ürün_kodlari_listbox.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.notebook_ürün_kodlari_listbox.FormattingEnabled = true;
            this.notebook_ürün_kodlari_listbox.ItemHeight = 16;
            this.notebook_ürün_kodlari_listbox.Location = new System.Drawing.Point(642, 97);
            this.notebook_ürün_kodlari_listbox.Name = "notebook_ürün_kodlari_listbox";
            this.notebook_ürün_kodlari_listbox.Size = new System.Drawing.Size(191, 404);
            this.notebook_ürün_kodlari_listbox.Sorted = true;
            this.notebook_ürün_kodlari_listbox.TabIndex = 0;
            this.notebook_ürün_kodlari_listbox.SelectedIndexChanged += new System.EventHandler(this.Notebook_ürün_kodlari_listbox_SelectedIndexChanged);
            // 
            // notebook_ürünler_listbox
            // 
            this.notebook_ürünler_listbox.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.notebook_ürünler_listbox.FormattingEnabled = true;
            this.notebook_ürünler_listbox.ItemHeight = 16;
            this.notebook_ürünler_listbox.Location = new System.Drawing.Point(445, 97);
            this.notebook_ürünler_listbox.Name = "notebook_ürünler_listbox";
            this.notebook_ürünler_listbox.Size = new System.Drawing.Size(191, 404);
            this.notebook_ürünler_listbox.Sorted = true;
            this.notebook_ürünler_listbox.TabIndex = 1;
            this.notebook_ürünler_listbox.SelectedIndexChanged += new System.EventHandler(this.Notebook_ürünler_listbox_SelectedIndexChanged);
            // 
            // ürün_adi_label
            // 
            this.ürün_adi_label.AutoSize = true;
            this.ürün_adi_label.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.ürün_adi_label.ForeColor = System.Drawing.SystemColors.Window;
            this.ürün_adi_label.Location = new System.Drawing.Point(50, 283);
            this.ürün_adi_label.Name = "ürün_adi_label";
            this.ürün_adi_label.Size = new System.Drawing.Size(75, 19);
            this.ürün_adi_label.TabIndex = 4;
            this.ürün_adi_label.Text = "Ürün adı: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(8, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Ürün stok kodu: ";
            // 
            // yeni_ürün_ekle_btn
            // 
            this.yeni_ürün_ekle_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.yeni_ürün_ekle_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yeni_ürün_ekle_btn.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.yeni_ürün_ekle_btn.ForeColor = System.Drawing.SystemColors.Window;
            this.yeni_ürün_ekle_btn.Location = new System.Drawing.Point(106, 374);
            this.yeni_ürün_ekle_btn.Name = "yeni_ürün_ekle_btn";
            this.yeni_ürün_ekle_btn.Size = new System.Drawing.Size(101, 32);
            this.yeni_ürün_ekle_btn.TabIndex = 6;
            this.yeni_ürün_ekle_btn.Text = "Yeni ürün ekle";
            this.toolTip1.SetToolTip(this.yeni_ürün_ekle_btn, "Daha önce sisteme kayıt edilmemiş yeni ürün eklemenizi sağlar.");
            this.yeni_ürün_ekle_btn.UseVisualStyleBackColor = false;
            this.yeni_ürün_ekle_btn.Click += new System.EventHandler(this.Yeni_ürün_ekle_btn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label3.Location = new System.Drawing.Point(682, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "ÜRÜN KODLARI";
            // 
            // notebook_ürün_seri_no_listbox
            // 
            this.notebook_ürün_seri_no_listbox.Font = new System.Drawing.Font("Century Gothic", 8.5F);
            this.notebook_ürün_seri_no_listbox.FormattingEnabled = true;
            this.notebook_ürün_seri_no_listbox.ItemHeight = 16;
            this.notebook_ürün_seri_no_listbox.Location = new System.Drawing.Point(839, 97);
            this.notebook_ürün_seri_no_listbox.Name = "notebook_ürün_seri_no_listbox";
            this.notebook_ürün_seri_no_listbox.Size = new System.Drawing.Size(191, 404);
            this.notebook_ürün_seri_no_listbox.Sorted = true;
            this.notebook_ürün_seri_no_listbox.TabIndex = 9;
            this.notebook_ürün_seri_no_listbox.SelectedIndexChanged += new System.EventHandler(this.Notebook_ürün_seri_no_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label4.Location = new System.Drawing.Point(901, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "SERİ NO";
            // 
            // ana_menü_btn
            // 
            this.ana_menü_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.ana_menü_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ana_menü_btn.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.ana_menü_btn.ForeColor = System.Drawing.SystemColors.Window;
            this.ana_menü_btn.Location = new System.Drawing.Point(12, 610);
            this.ana_menü_btn.Name = "ana_menü_btn";
            this.ana_menü_btn.Size = new System.Drawing.Size(121, 32);
            this.ana_menü_btn.TabIndex = 11;
            this.ana_menü_btn.Text = "Ana menüye dön";
            this.toolTip1.SetToolTip(this.ana_menü_btn, "Ana menüye dönmenizi sağlar.");
            this.ana_menü_btn.UseVisualStyleBackColor = false;
            this.ana_menü_btn.Click += new System.EventHandler(this.Ana_menü_btn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label5.ForeColor = System.Drawing.SystemColors.Window;
            this.label5.Location = new System.Drawing.Point(30, 340);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 19);
            this.label5.TabIndex = 14;
            this.label5.Text = "Ürün seri no: ";
            // 
            // secili_ürünü_sil_btn
            // 
            this.secili_ürünü_sil_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.secili_ürünü_sil_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.secili_ürünü_sil_btn.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.secili_ürünü_sil_btn.ForeColor = System.Drawing.SystemColors.Window;
            this.secili_ürünü_sil_btn.Location = new System.Drawing.Point(106, 412);
            this.secili_ürünü_sil_btn.Name = "secili_ürünü_sil_btn";
            this.secili_ürünü_sil_btn.Size = new System.Drawing.Size(101, 32);
            this.secili_ürünü_sil_btn.TabIndex = 15;
            this.secili_ürünü_sil_btn.Text = "Seçili ürünü sil";
            this.toolTip1.SetToolTip(this.secili_ürünü_sil_btn, "Seçtiğiniz herhangi bir notebook ürününü silmenizi sağlar, dikkat edin sildiğiniz" +
        " ürünün seri numaraları ve ürün kodlarını da otamatik olarak silinmektedir.");
            this.secili_ürünü_sil_btn.UseVisualStyleBackColor = false;
            this.secili_ürünü_sil_btn.Click += new System.EventHandler(this.Secili_ürünü_sil_btn_Click);
            // 
            // secili_ürünün_kod_seri_no_sil_btn
            // 
            this.secili_ürünün_kod_seri_no_sil_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.secili_ürünün_kod_seri_no_sil_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.secili_ürünün_kod_seri_no_sil_btn.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.secili_ürünün_kod_seri_no_sil_btn.ForeColor = System.Drawing.SystemColors.Window;
            this.secili_ürünün_kod_seri_no_sil_btn.Location = new System.Drawing.Point(213, 374);
            this.secili_ürünün_kod_seri_no_sil_btn.Name = "secili_ürünün_kod_seri_no_sil_btn";
            this.secili_ürünün_kod_seri_no_sil_btn.Size = new System.Drawing.Size(129, 70);
            this.secili_ürünün_kod_seri_no_sil_btn.TabIndex = 16;
            this.secili_ürünün_kod_seri_no_sil_btn.Text = "Seçili ürün kodu ve seri numarası sil";
            this.toolTip1.SetToolTip(this.secili_ürünün_kod_seri_no_sil_btn, "Seçtiğiniz ürünün sadece seri numarasını veya ürün kodunu silmenizi sağlar.");
            this.secili_ürünün_kod_seri_no_sil_btn.UseVisualStyleBackColor = false;
            this.secili_ürünün_kod_seri_no_sil_btn.Click += new System.EventHandler(this.Secili_ürünün_kod_seri_no_sil_btn_Click);
            // 
            // windows_kücültme_label
            // 
            this.windows_kücültme_label.AutoSize = true;
            this.windows_kücültme_label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.windows_kücültme_label.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.windows_kücültme_label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.windows_kücültme_label.Location = new System.Drawing.Point(1044, 7);
            this.windows_kücültme_label.Name = "windows_kücültme_label";
            this.windows_kücültme_label.Size = new System.Drawing.Size(22, 25);
            this.windows_kücültme_label.TabIndex = 22;
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
            this.logout_label.Location = new System.Drawing.Point(1064, 8);
            this.logout_label.Name = "logout_label";
            this.logout_label.Size = new System.Drawing.Size(28, 25);
            this.logout_label.TabIndex = 21;
            this.logout_label.Text = "X";
            this.toolTip1.SetToolTip(this.logout_label, "Uygulamayı kapatmanızı sağlar.");
            this.logout_label.Click += new System.EventHandler(this.Logout_label_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.panel3.Location = new System.Drawing.Point(131, 330);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 1);
            this.panel3.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.panel1.Location = new System.Drawing.Point(131, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 1);
            this.panel1.TabIndex = 29;
            // 
            // ürün_stok_kodu_textbox
            // 
            this.ürün_stok_kodu_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.ürün_stok_kodu_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ürün_stok_kodu_textbox.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.ürün_stok_kodu_textbox.ForeColor = System.Drawing.SystemColors.Window;
            this.ürün_stok_kodu_textbox.Location = new System.Drawing.Point(131, 311);
            this.ürün_stok_kodu_textbox.Name = "ürün_stok_kodu_textbox";
            this.ürün_stok_kodu_textbox.Size = new System.Drawing.Size(200, 18);
            this.ürün_stok_kodu_textbox.TabIndex = 24;
            // 
            // ürün_adi_textbox
            // 
            this.ürün_adi_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.ürün_adi_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ürün_adi_textbox.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.ürün_adi_textbox.ForeColor = System.Drawing.SystemColors.Window;
            this.ürün_adi_textbox.Location = new System.Drawing.Point(131, 282);
            this.ürün_adi_textbox.Name = "ürün_adi_textbox";
            this.ürün_adi_textbox.Size = new System.Drawing.Size(200, 18);
            this.ürün_adi_textbox.TabIndex = 23;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.panel2.Location = new System.Drawing.Point(131, 358);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 1);
            this.panel2.TabIndex = 32;
            // 
            // ürün_seri_no_textbox
            // 
            this.ürün_seri_no_textbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.ürün_seri_no_textbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ürün_seri_no_textbox.Font = new System.Drawing.Font("Century Gothic", 11F);
            this.ürün_seri_no_textbox.ForeColor = System.Drawing.SystemColors.Window;
            this.ürün_seri_no_textbox.Location = new System.Drawing.Point(131, 339);
            this.ürün_seri_no_textbox.Name = "ürün_seri_no_textbox";
            this.ürün_seri_no_textbox.Size = new System.Drawing.Size(200, 18);
            this.ürün_seri_no_textbox.TabIndex = 31;
            this.ürün_seri_no_textbox.TextChanged += new System.EventHandler(this.ürün_seri_no_textbox_TextChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.panel4.Controls.Add(this.loginpanel_gelistiren_label);
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Controls.Add(this.secili_ürünün_kod_seri_no_sil_btn);
            this.panel4.Controls.Add(this.ürün_seri_no_textbox);
            this.panel4.Controls.Add(this.secili_ürünü_sil_btn);
            this.panel4.Controls.Add(this.ürün_adi_label);
            this.panel4.Controls.Add(this.ana_menü_btn);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.yeni_ürün_ekle_btn);
            this.panel4.Controls.Add(this.ürün_stok_kodu_textbox);
            this.panel4.Controls.Add(this.ürün_adi_textbox);
            this.panel4.Location = new System.Drawing.Point(-1, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(376, 689);
            this.panel4.TabIndex = 33;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel4_MouseDown);
            this.panel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel4_MouseMove);
            this.panel4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel4_MouseUp);
            // 
            // loginpanel_gelistiren_label
            // 
            this.loginpanel_gelistiren_label.AutoSize = true;
            this.loginpanel_gelistiren_label.Font = new System.Drawing.Font("Century Gothic", 7F);
            this.loginpanel_gelistiren_label.ForeColor = System.Drawing.Color.White;
            this.loginpanel_gelistiren_label.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.loginpanel_gelistiren_label.Location = new System.Drawing.Point(266, 615);
            this.loginpanel_gelistiren_label.Name = "loginpanel_gelistiren_label";
            this.loginpanel_gelistiren_label.Size = new System.Drawing.Size(107, 30);
            this.loginpanel_gelistiren_label.TabIndex = 7;
            this.loginpanel_gelistiren_label.Text = "        Selçuk Şahin \r\nTarafından geliştirildi\r\n";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBox1.Location = new System.Drawing.Point(42, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(282, 207);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 10F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.label7.Location = new System.Drawing.Point(502, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 19);
            this.label7.TabIndex = 34;
            this.label7.Text = "ÜRÜN ADI";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(117)))), ((int)(((byte)(189)))));
            this.linkLabel1.Location = new System.Drawing.Point(567, 629);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(139, 13);
            this.linkLabel1.TabIndex = 36;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "selcuksahin158@gmail.com";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(381, 597);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 48);
            this.label2.TabIndex = 35;
            this.label2.Text = "Bu uygulamaya ilgili \r\nherhangi bir sorun, şikayet ve öneri için\r\nbir -eposta mes" +
    "aji gönderebilirsiniz\r\n";
            // 
            // Ürüneklemedüzenlemeform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1099, 655);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.windows_kücültme_label);
            this.Controls.Add(this.logout_label);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.notebook_ürün_seri_no_listbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.notebook_ürünler_listbox);
            this.Controls.Add(this.notebook_ürün_kodlari_listbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ürüneklemedüzenlemeform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ürün_Ekle_ve_Düzenleme_FormClosed);
            this.Load += new System.EventHandler(this.Ürün_Ekle_ve_Düzenleme_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Ürün_Ekle_ve_Düzenleme_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Ürün_Ekle_ve_Düzenleme_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Ürün_Ekle_ve_Düzenleme_MouseUp);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox notebook_ürün_kodlari_listbox;
        private System.Windows.Forms.ListBox notebook_ürünler_listbox;
        private System.Windows.Forms.Label ürün_adi_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button yeni_ürün_ekle_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox notebook_ürün_seri_no_listbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ana_menü_btn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button secili_ürünü_sil_btn;
        private System.Windows.Forms.Button secili_ürünün_kod_seri_no_sil_btn;
        private System.Windows.Forms.Label windows_kücültme_label;
        private System.Windows.Forms.Label logout_label;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox ürün_stok_kodu_textbox;
        private System.Windows.Forms.TextBox ürün_adi_textbox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox ürün_seri_no_textbox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label loginpanel_gelistiren_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
    }
}