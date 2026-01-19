namespace AppEscritorioRHM
{
    partial class FormConfig
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
            components = new System.ComponentModel.Container();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelForm = new TableLayoutPanel();
            textBoxTokenSecretWP = new TextBox();
            textBoxTokenPublicWP = new TextBox();
            textBoxTokenSecretWC = new TextBox();
            textBoxTokenPublicWC = new TextBox();
            labelDomain = new Label();
            labelTokenPublicWC = new Label();
            labelTokenSecretWC = new Label();
            labelTokenPublicWP = new Label();
            labelTokenSecretWP = new Label();
            textBoxDomain = new TextBox();
            tableLayoutPanelCheck = new TableLayoutPanel();
            buttonCheck = new Button();
            progressBarLoading = new ProgressBar();
            errorProvider1 = new ErrorProvider(components);
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelForm.SuspendLayout();
            tableLayoutPanelCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 3;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 550F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelForm, 1, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 252F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanelMain.Size = new Size(634, 361);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelForm
            // 
            tableLayoutPanelForm.ColumnCount = 2;
            tableLayoutPanelForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.31579F));
            tableLayoutPanelForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.68421F));
            tableLayoutPanelForm.Controls.Add(textBoxTokenSecretWP, 1, 4);
            tableLayoutPanelForm.Controls.Add(textBoxTokenPublicWP, 1, 3);
            tableLayoutPanelForm.Controls.Add(textBoxTokenSecretWC, 1, 2);
            tableLayoutPanelForm.Controls.Add(textBoxTokenPublicWC, 1, 1);
            tableLayoutPanelForm.Controls.Add(labelDomain, 0, 0);
            tableLayoutPanelForm.Controls.Add(labelTokenPublicWC, 0, 1);
            tableLayoutPanelForm.Controls.Add(labelTokenSecretWC, 0, 2);
            tableLayoutPanelForm.Controls.Add(labelTokenPublicWP, 0, 3);
            tableLayoutPanelForm.Controls.Add(labelTokenSecretWP, 0, 4);
            tableLayoutPanelForm.Controls.Add(textBoxDomain, 1, 0);
            tableLayoutPanelForm.Controls.Add(tableLayoutPanelCheck, 0, 5);
            tableLayoutPanelForm.Dock = DockStyle.Fill;
            tableLayoutPanelForm.Location = new Point(45, 57);
            tableLayoutPanelForm.Name = "tableLayoutPanelForm";
            tableLayoutPanelForm.RowCount = 6;
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelForm.Size = new Size(544, 246);
            tableLayoutPanelForm.TabIndex = 0;
            tableLayoutPanelForm.Paint += tableLayoutPanel1_Paint;
            // 
            // textBoxTokenSecretWP
            // 
            textBoxTokenSecretWP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxTokenSecretWP.Location = new Point(153, 164);
            textBoxTokenSecretWP.Margin = new Padding(10, 3, 20, 3);
            textBoxTokenSecretWP.Name = "textBoxTokenSecretWP";
            textBoxTokenSecretWP.Size = new Size(371, 23);
            textBoxTokenSecretWP.TabIndex = 9;
            textBoxTokenSecretWP.TextChanged += textBoxTokenSecretWP_TextChanged;
            // 
            // textBoxTokenPublicWP
            // 
            textBoxTokenPublicWP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxTokenPublicWP.Location = new Point(153, 125);
            textBoxTokenPublicWP.Margin = new Padding(10, 3, 20, 3);
            textBoxTokenPublicWP.Name = "textBoxTokenPublicWP";
            textBoxTokenPublicWP.Size = new Size(371, 23);
            textBoxTokenPublicWP.TabIndex = 8;
            textBoxTokenPublicWP.TextChanged += textBoxTokenPublicWP_TextChanged;
            // 
            // textBoxTokenSecretWC
            // 
            textBoxTokenSecretWC.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxTokenSecretWC.Location = new Point(153, 86);
            textBoxTokenSecretWC.Margin = new Padding(10, 3, 20, 3);
            textBoxTokenSecretWC.Name = "textBoxTokenSecretWC";
            textBoxTokenSecretWC.Size = new Size(371, 23);
            textBoxTokenSecretWC.TabIndex = 7;
            textBoxTokenSecretWC.TextChanged += textBoxTokenSecretWC_TextChanged;
            // 
            // textBoxTokenPublicWC
            // 
            textBoxTokenPublicWC.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxTokenPublicWC.Location = new Point(153, 47);
            textBoxTokenPublicWC.Margin = new Padding(10, 3, 20, 3);
            textBoxTokenPublicWC.Name = "textBoxTokenPublicWC";
            textBoxTokenPublicWC.Size = new Size(371, 23);
            textBoxTokenPublicWC.TabIndex = 6;
            textBoxTokenPublicWC.TextChanged += textBoxTokenPublicWC_TextChanged;
            // 
            // labelDomain
            // 
            labelDomain.Anchor = AnchorStyles.None;
            labelDomain.AutoSize = true;
            labelDomain.Location = new Point(45, 12);
            labelDomain.Name = "labelDomain";
            labelDomain.Size = new Size(53, 15);
            labelDomain.TabIndex = 0;
            labelDomain.Text = "Dominio";
            // 
            // labelTokenPublicWC
            // 
            labelTokenPublicWC.Anchor = AnchorStyles.None;
            labelTokenPublicWC.AutoSize = true;
            labelTokenPublicWC.Location = new Point(19, 51);
            labelTokenPublicWC.Name = "labelTokenPublicWC";
            labelTokenPublicWC.Size = new Size(104, 15);
            labelTokenPublicWC.TabIndex = 1;
            labelTokenPublicWC.Text = "Token público WC";
            // 
            // labelTokenSecretWC
            // 
            labelTokenSecretWC.Anchor = AnchorStyles.None;
            labelTokenSecretWC.AutoSize = true;
            labelTokenSecretWC.Location = new Point(19, 90);
            labelTokenSecretWC.Name = "labelTokenSecretWC";
            labelTokenSecretWC.Size = new Size(104, 15);
            labelTokenSecretWC.TabIndex = 2;
            labelTokenSecretWC.Text = "Token privado WC";
            labelTokenSecretWC.Click += labelTokenSecretWC_Click;
            // 
            // labelTokenPublicWP
            // 
            labelTokenPublicWP.Anchor = AnchorStyles.None;
            labelTokenPublicWP.AutoSize = true;
            labelTokenPublicWP.Location = new Point(20, 129);
            labelTokenPublicWP.Name = "labelTokenPublicWP";
            labelTokenPublicWP.Size = new Size(103, 15);
            labelTokenPublicWP.TabIndex = 3;
            labelTokenPublicWP.Text = "Token público WP";
            // 
            // labelTokenSecretWP
            // 
            labelTokenSecretWP.Anchor = AnchorStyles.None;
            labelTokenSecretWP.AutoSize = true;
            labelTokenSecretWP.Location = new Point(20, 168);
            labelTokenSecretWP.Name = "labelTokenSecretWP";
            labelTokenSecretWP.Size = new Size(103, 15);
            labelTokenSecretWP.TabIndex = 4;
            labelTokenSecretWP.Text = "Token privado WP";
            labelTokenSecretWP.Click += labelTokenSecretWP_Click;
            // 
            // textBoxDomain
            // 
            textBoxDomain.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            textBoxDomain.Location = new Point(153, 8);
            textBoxDomain.Margin = new Padding(10, 3, 20, 3);
            textBoxDomain.Name = "textBoxDomain";
            textBoxDomain.Size = new Size(371, 23);
            textBoxDomain.TabIndex = 5;
            textBoxDomain.TextChanged += textBoxDomain_TextChanged;
            // 
            // tableLayoutPanelCheck
            // 
            tableLayoutPanelCheck.ColumnCount = 3;
            tableLayoutPanelForm.SetColumnSpan(tableLayoutPanelCheck, 2);
            tableLayoutPanelCheck.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelCheck.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanelCheck.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelCheck.Controls.Add(buttonCheck, 1, 0);
            tableLayoutPanelCheck.Controls.Add(progressBarLoading, 2, 0);
            tableLayoutPanelCheck.Dock = DockStyle.Fill;
            tableLayoutPanelCheck.Location = new Point(3, 198);
            tableLayoutPanelCheck.Name = "tableLayoutPanelCheck";
            tableLayoutPanelCheck.RowCount = 1;
            tableLayoutPanelCheck.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelCheck.Size = new Size(538, 45);
            tableLayoutPanelCheck.TabIndex = 3;
            // 
            // buttonCheck
            // 
            buttonCheck.Anchor = AnchorStyles.None;
            buttonCheck.Enabled = false;
            buttonCheck.Location = new Point(224, 6);
            buttonCheck.Name = "buttonCheck";
            buttonCheck.Size = new Size(89, 32);
            buttonCheck.TabIndex = 10;
            buttonCheck.Text = "Comprobar";
            buttonCheck.UseVisualStyleBackColor = true;
            buttonCheck.Click += buttonCheck_Click;
            // 
            // progressBarLoading
            // 
            progressBarLoading.Anchor = AnchorStyles.Left;
            progressBarLoading.Location = new Point(332, 14);
            progressBarLoading.Name = "progressBarLoading";
            progressBarLoading.Size = new Size(65, 17);
            progressBarLoading.Style = ProgressBarStyle.Marquee;
            progressBarLoading.TabIndex = 2;
            progressBarLoading.Visible = false;
            progressBarLoading.Click += progressBar1_Click;
            // 
            // errorProvider1
            // 
            errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider1.ContainerControl = this;
            // 
            // FormConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 361);
            Controls.Add(tableLayoutPanelMain);
            MinimumSize = new Size(650, 400);
            Name = "FormConfig";
            Text = "Configuración";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelForm.ResumeLayout(false);
            tableLayoutPanelForm.PerformLayout();
            tableLayoutPanelCheck.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)errorProvider1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TableLayoutPanel tableLayoutPanelForm;
        private Label labelDomain;
        private TextBox textBoxTokenSecretWP;
        private TextBox textBoxTokenPublicWP;
        private TextBox textBoxTokenSecretWC;
        private TextBox textBoxTokenPublicWC;
        private Label labelTokenPublicWC;
        private Label labelTokenSecretWC;
        private Label labelTokenPublicWP;
        private Label labelTokenSecretWP;
        private TextBox textBoxDomain;
        private Button buttonCheck;
        private ErrorProvider errorProvider1;
        private ProgressBar progressBarLoading;
        private TableLayoutPanel tableLayoutPanelCheck;
    }
}