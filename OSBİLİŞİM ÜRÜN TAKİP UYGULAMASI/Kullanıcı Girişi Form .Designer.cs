namespace OSBilişim
{
    partial class Kullanicigirisiform
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kullanicigirisiform));
            this.btn_giris = new System.Windows.Forms.Button();
            this.kullaniciaditextbox = new System.Windows.Forms.TextBox();
            this.sifretextbox = new System.Windows.Forms.TextBox();
            this.kullaniciadi_label = new System.Windows.Forms.Label();
            this.sifre_label = new System.Windows.Forms.Label();
            this.sifre_goster_gizle_checkbox = new System.Windows.Forms.CheckBox();
            this.beni_hatirla_checkbox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.logout_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.şifremiunuttumlinklabel = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.loginpanel_hosgeldiniz_label = new System.Windows.Forms.Label();
            this.loginpanel_gelistiren_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_giris
            // 
            this.btn_giris.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            this.btn_giris.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.btn_giris, "btn_giris");
            this.btn_giris.ForeColor = System.Drawing.Color.Black;
            this.btn_giris.Name = "btn_giris";
            this.toolTip1.SetToolTip(this.btn_giris, resources.GetString("btn_giris.ToolTip"));
            this.btn_giris.UseVisualStyleBackColor = false;
            this.btn_giris.Click += new System.EventHandler(this.Btn_giris_Click);
            // 
            // kullaniciaditextbox
            // 
            this.kullaniciaditextbox.BackColor = System.Drawing.SystemColors.Window;
            this.kullaniciaditextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.kullaniciaditextbox, "kullaniciaditextbox");
            this.kullaniciaditextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.kullaniciaditextbox.Name = "kullaniciaditextbox";
            // 
            // sifretextbox
            // 
            this.sifretextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.sifretextbox, "sifretextbox");
            this.sifretextbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.sifretextbox.Name = "sifretextbox";
            // 
            // kullaniciadi_label
            // 
            resources.ApplyResources(this.kullaniciadi_label, "kullaniciadi_label");
            this.kullaniciadi_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.kullaniciadi_label.Name = "kullaniciadi_label";
            // 
            // sifre_label
            // 
            resources.ApplyResources(this.sifre_label, "sifre_label");
            this.sifre_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.sifre_label.Name = "sifre_label";
            // 
            // sifre_goster_gizle_checkbox
            // 
            resources.ApplyResources(this.sifre_goster_gizle_checkbox, "sifre_goster_gizle_checkbox");
            this.sifre_goster_gizle_checkbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sifre_goster_gizle_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.sifre_goster_gizle_checkbox.Name = "sifre_goster_gizle_checkbox";
            this.toolTip1.SetToolTip(this.sifre_goster_gizle_checkbox, resources.GetString("sifre_goster_gizle_checkbox.ToolTip"));
            this.sifre_goster_gizle_checkbox.UseVisualStyleBackColor = true;
            this.sifre_goster_gizle_checkbox.CheckedChanged += new System.EventHandler(this.Sifre_goster_gizle_checkbox_CheckedChanged);
            // 
            // beni_hatirla_checkbox
            // 
            resources.ApplyResources(this.beni_hatirla_checkbox, "beni_hatirla_checkbox");
            this.beni_hatirla_checkbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.beni_hatirla_checkbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.beni_hatirla_checkbox.Name = "beni_hatirla_checkbox";
            this.toolTip1.SetToolTip(this.beni_hatirla_checkbox, resources.GetString("beni_hatirla_checkbox.ToolTip"));
            this.beni_hatirla_checkbox.UseVisualStyleBackColor = true;
            this.beni_hatirla_checkbox.CheckedChanged += new System.EventHandler(this.Beni_hatirla_checkbox_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // logout_label
            // 
            resources.ApplyResources(this.logout_label, "logout_label");
            this.logout_label.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logout_label.Name = "logout_label";
            this.toolTip1.SetToolTip(this.logout_label, resources.GetString("logout_label.ToolTip"));
            this.logout_label.Click += new System.EventHandler(this.Logout_label_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            this.label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // şifremiunuttumlinklabel
            // 
            resources.ApplyResources(this.şifremiunuttumlinklabel, "şifremiunuttumlinklabel");
            this.şifremiunuttumlinklabel.Name = "şifremiunuttumlinklabel";
            this.şifremiunuttumlinklabel.TabStop = true;
            this.toolTip1.SetToolTip(this.şifremiunuttumlinklabel, resources.GetString("şifremiunuttumlinklabel.ToolTip"));
            this.şifremiunuttumlinklabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Şifremiunuttumlinklabel_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.panel2.Controls.Add(this.loginpanel_hosgeldiniz_label);
            this.panel2.Controls.Add(this.loginpanel_gelistiren_label);
            this.panel2.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseDown);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseMove);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseUp);
            // 
            // loginpanel_hosgeldiniz_label
            // 
            resources.ApplyResources(this.loginpanel_hosgeldiniz_label, "loginpanel_hosgeldiniz_label");
            this.loginpanel_hosgeldiniz_label.ForeColor = System.Drawing.Color.White;
            this.loginpanel_hosgeldiniz_label.Name = "loginpanel_hosgeldiniz_label";
            // 
            // loginpanel_gelistiren_label
            // 
            resources.ApplyResources(this.loginpanel_gelistiren_label, "loginpanel_gelistiren_label");
            this.loginpanel_gelistiren_label.ForeColor = System.Drawing.Color.White;
            this.loginpanel_gelistiren_label.Name = "loginpanel_gelistiren_label";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(195)))), ((int)(((byte)(215)))));
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(27)))));
            this.label2.Name = "label2";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabel1.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(117)))), ((int)(((byte)(189)))));
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.TabStop = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // Kullanicigirisiform
            // 
            this.AcceptButton = this.btn_giris;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.şifremiunuttumlinklabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logout_label);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.beni_hatirla_checkbox);
            this.Controls.Add(this.sifre_goster_gizle_checkbox);
            this.Controls.Add(this.sifre_label);
            this.Controls.Add(this.kullaniciadi_label);
            this.Controls.Add(this.sifretextbox);
            this.Controls.Add(this.kullaniciaditextbox);
            this.Controls.Add(this.btn_giris);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Kullanicigirisiform";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Kullanicigirisiform_FormClosed);
            this.Load += new System.EventHandler(this.Kullanicigirisiform_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Kullanicigirisiform_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Kullanicigirisiform_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Kullanicigirisiform_MouseUp);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox kullaniciaditextbox;
        private System.Windows.Forms.TextBox sifretextbox;
        private System.Windows.Forms.Label kullaniciadi_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label sifre_label;
        private System.Windows.Forms.CheckBox sifre_goster_gizle_checkbox;
        private System.Windows.Forms.CheckBox beni_hatirla_checkbox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label loginpanel_gelistiren_label;
        private System.Windows.Forms.Label loginpanel_hosgeldiniz_label;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label logout_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_giris;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel şifremiunuttumlinklabel;
    }
}

