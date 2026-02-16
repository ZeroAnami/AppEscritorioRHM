namespace AppEscritorioRHM.UI.Forms
{
    partial class FormConfig
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            splitContainerMain = new SplitContainer();
            lstStores = new ListBox();
            btnAddStore = new Button();
            pnlSidebarHeader = new Panel();
            lblSidebarTitle = new Label();
            pnlRightContent = new Panel();
            grpApiDetails = new GroupBox();
            btnVerify = new Button();
            chkShowSecret = new CheckBox();
            txtConsumerSecret = new TextBox();
            lblSecret = new Label();
            txtConsumerKey = new TextBox();
            lblKey = new Label();
            pnlApiSelector = new FlowLayoutPanel();
            lblApiSelectorTitle = new Label();
            grpStoreInfo = new GroupBox();
            btnDeleteStore = new Button();
            cmbPlatform = new ComboBox();
            lblPlatform = new Label();
            txtStoreUrl = new TextBox();
            lblUrl = new Label();
            txtStoreName = new TextBox();
            lblName = new Label();
            pnlFooter = new Panel();
            btnSave = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            pnlSidebarHeader.SuspendLayout();
            pnlRightContent.SuspendLayout();
            grpApiDetails.SuspendLayout();
            grpStoreInfo.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.FixedPanel = FixedPanel.Panel1;
            splitContainerMain.Location = new Point(0, 0);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.BackColor = Color.WhiteSmoke;
            splitContainerMain.Panel1.Controls.Add(lstStores);
            splitContainerMain.Panel1.Controls.Add(btnAddStore);
            splitContainerMain.Panel1.Controls.Add(pnlSidebarHeader);
            splitContainerMain.Panel1.Padding = new Padding(10);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.BackColor = Color.White;
            splitContainerMain.Panel2.Controls.Add(pnlRightContent);
            splitContainerMain.Panel2.Controls.Add(pnlFooter);
            splitContainerMain.Size = new Size(800, 500);
            splitContainerMain.SplitterDistance = 250;
            splitContainerMain.TabIndex = 0;
            // 
            // lstStores
            // 
            lstStores.BorderStyle = BorderStyle.FixedSingle;
            lstStores.Dock = DockStyle.Fill;
            lstStores.Font = new Font("Segoe UI", 9F);
            lstStores.FormattingEnabled = true;
            lstStores.Location = new Point(10, 50);
            lstStores.Name = "lstStores";
            lstStores.Size = new Size(230, 410);
            lstStores.TabIndex = 1;
            lstStores.SelectedIndexChanged += lstStores_SelectedIndexChanged;
            // 
            // btnAddStore
            // 
            btnAddStore.BackColor = Color.White;
            btnAddStore.Dock = DockStyle.Bottom;
            btnAddStore.FlatStyle = FlatStyle.Flat;
            btnAddStore.Font = new Font("Segoe UI", 9F);
            btnAddStore.Location = new Point(10, 460);
            btnAddStore.Name = "btnAddStore";
            btnAddStore.Size = new Size(230, 30);
            btnAddStore.TabIndex = 2;
            btnAddStore.Text = "[+] Agregar Tienda";
            btnAddStore.UseVisualStyleBackColor = false;
            btnAddStore.Click += btnAddStore_Click;
            // 
            // pnlSidebarHeader
            // 
            pnlSidebarHeader.Controls.Add(lblSidebarTitle);
            pnlSidebarHeader.Dock = DockStyle.Top;
            pnlSidebarHeader.Location = new Point(10, 10);
            pnlSidebarHeader.Name = "pnlSidebarHeader";
            pnlSidebarHeader.Size = new Size(230, 40);
            pnlSidebarHeader.TabIndex = 0;
            // 
            // lblSidebarTitle
            // 
            lblSidebarTitle.AutoSize = true;
            lblSidebarTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSidebarTitle.Location = new Point(0, 5);
            lblSidebarTitle.Name = "lblSidebarTitle";
            lblSidebarTitle.Size = new Size(100, 21);
            lblSidebarTitle.TabIndex = 0;
            lblSidebarTitle.Text = "Mis Tiendas";
            // 
            // pnlRightContent
            // 
            pnlRightContent.Controls.Add(grpApiDetails);
            pnlRightContent.Controls.Add(pnlApiSelector);
            pnlRightContent.Controls.Add(lblApiSelectorTitle);
            pnlRightContent.Controls.Add(grpStoreInfo);
            pnlRightContent.Dock = DockStyle.Fill;
            pnlRightContent.Location = new Point(0, 0);
            pnlRightContent.Name = "pnlRightContent";
            pnlRightContent.Padding = new Padding(20);
            pnlRightContent.Size = new Size(546, 450);
            pnlRightContent.TabIndex = 0;
            // 
            // grpApiDetails
            // 
            grpApiDetails.Controls.Add(btnVerify);
            grpApiDetails.Controls.Add(chkShowSecret);
            grpApiDetails.Controls.Add(txtConsumerSecret);
            grpApiDetails.Controls.Add(lblSecret);
            grpApiDetails.Controls.Add(txtConsumerKey);
            grpApiDetails.Controls.Add(lblKey);
            grpApiDetails.Dock = DockStyle.Fill;
            grpApiDetails.Font = new Font("Segoe UI", 9F);
            grpApiDetails.Location = new Point(20, 230);
            grpApiDetails.Name = "grpApiDetails";
            grpApiDetails.Size = new Size(506, 200);
            grpApiDetails.TabIndex = 2;
            grpApiDetails.TabStop = false;
            grpApiDetails.Text = "Credenciales de API";
            // 
            // btnVerify
            // 
            btnVerify.BackColor = Color.FromArgb(0, 120, 215);
            btnVerify.FlatStyle = FlatStyle.Flat;
            btnVerify.ForeColor = Color.White;
            btnVerify.Location = new Point(20, 155);
            btnVerify.Name = "btnVerify";
            btnVerify.Size = new Size(400, 30);
            btnVerify.TabIndex = 2;
            btnVerify.Text = "Verificar Conexión";
            btnVerify.UseVisualStyleBackColor = false;
            btnVerify.Click += btnVerify_Click;
            // 
            // chkShowSecret
            // 
            chkShowSecret.AutoSize = true;
            chkShowSecret.Location = new Point(426, 117);
            chkShowSecret.Name = "chkShowSecret";
            chkShowSecret.Size = new Size(42, 19);
            chkShowSecret.TabIndex = 3;
            chkShowSecret.Text = "Ver";
            chkShowSecret.UseVisualStyleBackColor = true;
            chkShowSecret.CheckedChanged += chkShowSecret_CheckedChanged;
            // 
            // txtConsumerSecret
            // 
            txtConsumerSecret.Location = new Point(20, 115);
            txtConsumerSecret.Name = "txtConsumerSecret";
            txtConsumerSecret.Size = new Size(400, 23);
            txtConsumerSecret.TabIndex = 1;
            txtConsumerSecret.UseSystemPasswordChar = true;
            // 
            // lblSecret
            // 
            lblSecret.AutoSize = true;
            lblSecret.Location = new Point(20, 95);
            lblSecret.Name = "lblSecret";
            lblSecret.Size = new Size(180, 15);
            lblSecret.TabIndex = 4;
            lblSecret.Text = "Clave secreta (Consumer Secret):";
            // 
            // txtConsumerKey
            // 
            txtConsumerKey.Location = new Point(20, 60);
            txtConsumerKey.Name = "txtConsumerKey";
            txtConsumerKey.Size = new Size(400, 23);
            txtConsumerKey.TabIndex = 0;
            // 
            // lblKey
            // 
            lblKey.AutoSize = true;
            lblKey.Location = new Point(20, 40);
            lblKey.Name = "lblKey";
            lblKey.Size = new Size(181, 15);
            lblKey.TabIndex = 5;
            lblKey.Text = "Clave de cliente (Consumer Key):";
            // 
            // pnlApiSelector
            // 
            pnlApiSelector.AutoSize = true;
            pnlApiSelector.Dock = DockStyle.Top;
            pnlApiSelector.Location = new Point(20, 190);
            pnlApiSelector.MinimumSize = new Size(0, 40);
            pnlApiSelector.Name = "pnlApiSelector";
            pnlApiSelector.Size = new Size(506, 40);
            pnlApiSelector.TabIndex = 1;
            // 
            // lblApiSelectorTitle
            // 
            lblApiSelectorTitle.AutoSize = true;
            lblApiSelectorTitle.Dock = DockStyle.Top;
            lblApiSelectorTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblApiSelectorTitle.Location = new Point(20, 160);
            lblApiSelectorTitle.Name = "lblApiSelectorTitle";
            lblApiSelectorTitle.Padding = new Padding(0, 10, 0, 5);
            lblApiSelectorTitle.Size = new Size(159, 30);
            lblApiSelectorTitle.TabIndex = 3;
            lblApiSelectorTitle.Text = "Seleccionar Conexión (API):";
            // 
            // grpStoreInfo
            // 
            grpStoreInfo.Controls.Add(btnDeleteStore);
            grpStoreInfo.Controls.Add(cmbPlatform);
            grpStoreInfo.Controls.Add(lblPlatform);
            grpStoreInfo.Controls.Add(txtStoreUrl);
            grpStoreInfo.Controls.Add(lblUrl);
            grpStoreInfo.Controls.Add(txtStoreName);
            grpStoreInfo.Controls.Add(lblName);
            grpStoreInfo.Dock = DockStyle.Top;
            grpStoreInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpStoreInfo.Location = new Point(20, 20);
            grpStoreInfo.Name = "grpStoreInfo";
            grpStoreInfo.Size = new Size(506, 140);
            grpStoreInfo.TabIndex = 0;
            grpStoreInfo.TabStop = false;
            grpStoreInfo.Text = "Configuración de Tienda";
            // 
            // btnDeleteStore
            // 
            btnDeleteStore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteStore.BackColor = Color.IndianRed;
            btnDeleteStore.FlatStyle = FlatStyle.Flat;
            btnDeleteStore.Font = new Font("Segoe UI", 8F);
            btnDeleteStore.ForeColor = Color.White;
            btnDeleteStore.Location = new Point(450, 20);
            btnDeleteStore.Name = "btnDeleteStore";
            btnDeleteStore.Size = new Size(50, 25);
            btnDeleteStore.TabIndex = 3;
            btnDeleteStore.Text = "Borrar";
            btnDeleteStore.UseVisualStyleBackColor = false;
            btnDeleteStore.Click += btnDeleteStore_Click;
            // 
            // cmbPlatform
            // 
            cmbPlatform.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlatform.Font = new Font("Segoe UI", 9F);
            cmbPlatform.FormattingEnabled = true;
            cmbPlatform.Location = new Point(240, 50);
            cmbPlatform.Name = "cmbPlatform";
            cmbPlatform.Size = new Size(200, 23);
            cmbPlatform.TabIndex = 1;
            cmbPlatform.SelectedIndexChanged += cmbPlatform_SelectedIndexChanged;
            // 
            // lblPlatform
            // 
            lblPlatform.AutoSize = true;
            lblPlatform.Font = new Font("Segoe UI", 9F);
            lblPlatform.Location = new Point(240, 30);
            lblPlatform.Name = "lblPlatform";
            lblPlatform.Size = new Size(68, 15);
            lblPlatform.TabIndex = 4;
            lblPlatform.Text = "Plataforma:";
            // 
            // txtStoreUrl
            // 
            txtStoreUrl.Font = new Font("Segoe UI", 9F);
            txtStoreUrl.Location = new Point(20, 100);
            txtStoreUrl.Name = "txtStoreUrl";
            txtStoreUrl.Size = new Size(420, 23);
            txtStoreUrl.TabIndex = 2;
            // 
            // lblUrl
            // 
            lblUrl.AutoSize = true;
            lblUrl.Font = new Font("Segoe UI", 9F);
            lblUrl.Location = new Point(20, 80);
            lblUrl.Name = "lblUrl";
            lblUrl.Size = new Size(95, 15);
            lblUrl.TabIndex = 5;
            lblUrl.Text = "URL de la tienda:";
            // 
            // txtStoreName
            // 
            txtStoreName.Font = new Font("Segoe UI", 9F);
            txtStoreName.Location = new Point(20, 50);
            txtStoreName.Name = "txtStoreName";
            txtStoreName.Size = new Size(200, 23);
            txtStoreName.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F);
            lblName.Location = new Point(20, 30);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 15);
            lblName.TabIndex = 6;
            lblName.Text = "Nombre:";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.WhiteSmoke;
            pnlFooter.Controls.Add(btnSave);
            pnlFooter.Controls.Add(btnClose);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 450);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(546, 50);
            pnlFooter.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.BackColor = Color.SeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(326, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 30);
            btnSave.TabIndex = 0;
            btnSave.Text = "Guardar Todo";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(456, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 30);
            btnClose.TabIndex = 1;
            btnClose.Text = "Cerrar";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // FormConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Controls.Add(splitContainerMain);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(816, 539);
            Name = "FormConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Configuración - Gestor de Ecommerce";
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            pnlSidebarHeader.ResumeLayout(false);
            pnlSidebarHeader.PerformLayout();
            pnlRightContent.ResumeLayout(false);
            pnlRightContent.PerformLayout();
            grpApiDetails.ResumeLayout(false);
            grpApiDetails.PerformLayout();
            grpStoreInfo.ResumeLayout(false);
            grpStoreInfo.PerformLayout();
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ListBox lstStores;
        private System.Windows.Forms.Button btnAddStore;
        private System.Windows.Forms.Panel pnlSidebarHeader;
        private System.Windows.Forms.Label lblSidebarTitle;
        private System.Windows.Forms.Panel pnlRightContent;
        private System.Windows.Forms.GroupBox grpStoreInfo;
        private System.Windows.Forms.TextBox txtStoreName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ComboBox cmbPlatform;
        private System.Windows.Forms.Label lblPlatform;
        private System.Windows.Forms.TextBox txtStoreUrl;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnDeleteStore;
        private System.Windows.Forms.FlowLayoutPanel pnlApiSelector;
        private System.Windows.Forms.Label lblApiSelectorTitle;
        private System.Windows.Forms.GroupBox grpApiDetails;
        private System.Windows.Forms.TextBox txtConsumerKey;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtConsumerSecret;
        private System.Windows.Forms.Label lblSecret;
        private System.Windows.Forms.CheckBox chkShowSecret;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}