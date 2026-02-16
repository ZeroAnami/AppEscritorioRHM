namespace AppEscritorioRHM.Controls.Config
{
    partial class TokenConfigUserControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            panelMain = new Panel();
            tableLayoutPanelForm = new TableLayoutPanel();
            labelDomain = new Label();
            textBoxDomain = new TextBox();
            tableLayoutPanelCheck = new TableLayoutPanel();
            buttonCheck = new Button();
            progressBarLoading = new ProgressBar();
            panelMain.SuspendLayout();
            tableLayoutPanelForm.SuspendLayout();
            tableLayoutPanelCheck.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(tableLayoutPanelForm);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(544, 275);
            panelMain.TabIndex = 0;
            // 
            // tableLayoutPanelForm
            // 
            tableLayoutPanelForm.AutoSize = true;
            tableLayoutPanelForm.ColumnCount = 2;
            tableLayoutPanelForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.31579F));
            tableLayoutPanelForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.68421F));
            tableLayoutPanelForm.Controls.Add(labelDomain, 0, 0);
            tableLayoutPanelForm.Controls.Add(textBoxDomain, 1, 0);
            tableLayoutPanelForm.Controls.Add(tableLayoutPanelCheck, 0, 1);
            tableLayoutPanelForm.Dock = DockStyle.Fill;
            tableLayoutPanelForm.Location = new Point(0, 0);
            tableLayoutPanelForm.MinimumSize = new Size(544, 195);
            tableLayoutPanelForm.Name = "tableLayoutPanelForm";
            tableLayoutPanelForm.RowCount = 3;
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelForm.Size = new Size(544, 275);
            tableLayoutPanelForm.TabIndex = 2;
            tableLayoutPanelForm.Paint += tableLayoutPanelForm_Paint;
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
            // textBoxDomain
            // 
            textBoxDomain.Anchor = AnchorStyles.Left;
            textBoxDomain.Location = new Point(153, 8);
            textBoxDomain.Margin = new Padding(10, 3, 20, 3);
            textBoxDomain.Name = "textBoxDomain";
            textBoxDomain.Size = new Size(371, 23);
            textBoxDomain.TabIndex = 5;
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
            tableLayoutPanelCheck.Location = new Point(3, 43);
            tableLayoutPanelCheck.Name = "tableLayoutPanelCheck";
            tableLayoutPanelCheck.RowCount = 1;
            tableLayoutPanelCheck.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelCheck.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelCheck.Size = new Size(538, 44);
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
            progressBarLoading.Location = new Point(332, 13);
            progressBarLoading.Name = "progressBarLoading";
            progressBarLoading.Size = new Size(65, 17);
            progressBarLoading.Style = ProgressBarStyle.Marquee;
            progressBarLoading.TabIndex = 2;
            progressBarLoading.Visible = false;
            // 
            // TokenConfigUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMain);
            Name = "TokenConfigUserControl";
            Size = new Size(544, 275);
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            tableLayoutPanelForm.ResumeLayout(false);
            tableLayoutPanelForm.PerformLayout();
            tableLayoutPanelCheck.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private TableLayoutPanel tableLayoutPanelForm;
        private Label labelDomain;
        private TextBox textBoxDomain;
        private TableLayoutPanel tableLayoutPanelCheck;
        private Button buttonCheck;
        private ProgressBar progressBarLoading;
    }
}
