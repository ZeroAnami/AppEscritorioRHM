namespace AppEscritorioRHM.Controls
{
    partial class RedirecctionUrlUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RedirecctionUrlUserControl));
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelUpload = new TableLayoutPanel();
            button1 = new Button();
            labelUploadFileName = new Label();
            richTextBoxReport = new RichTextBox();
            buttonDownload = new Button();
            tableLayoutPanelRight = new TableLayoutPanel();
            flowLayoutPanelDelete = new FlowLayoutPanel();
            tableLayoutPanelImagesDelete = new TableLayoutPanel();
            checkBoxDeleteProducts = new CheckBox();
            checkBoxDeleteImages = new CheckBox();
            checkBoxRedirections = new CheckBox();
            panelProgressBarDeleteProducts = new Panel();
            pictureBoxCheckDeleteProducts = new PictureBox();
            progressBarDeleteProducts = new ProgressBar();
            panelProgressBarDeleteImages = new Panel();
            pictureBoxCheckDeleteImages = new PictureBox();
            progressBarDeleteImages = new ProgressBar();
            panelProgressBarRedirections = new Panel();
            pictureBoxCheckRedirections = new PictureBox();
            progressBarRedirections = new ProgressBar();
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonStartProgress = new Button();
            buttonCancel = new Button();
            helpProvider1 = new HelpProvider();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelUpload.SuspendLayout();
            tableLayoutPanelRight.SuspendLayout();
            flowLayoutPanelDelete.SuspendLayout();
            tableLayoutPanelImagesDelete.SuspendLayout();
            panelProgressBarDeleteProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCheckDeleteProducts).BeginInit();
            panelProgressBarDeleteImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCheckDeleteImages).BeginInit();
            panelProgressBarRedirections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCheckRedirections).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 3;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 10F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 381F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelUpload, 1, 0);
            tableLayoutPanelMain.Controls.Add(richTextBoxReport, 1, 1);
            tableLayoutPanelMain.Controls.Add(buttonDownload, 1, 2);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelRight, 2, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanelMain.Size = new Size(627, 327);
            tableLayoutPanelMain.TabIndex = 0;
            tableLayoutPanelMain.Paint += tableLayoutPanel1_Paint;
            // 
            // tableLayoutPanelUpload
            // 
            tableLayoutPanelUpload.ColumnCount = 2;
            tableLayoutPanelMain.SetColumnSpan(tableLayoutPanelUpload, 2);
            tableLayoutPanelUpload.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelUpload.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelUpload.Controls.Add(button1, 0, 0);
            tableLayoutPanelUpload.Controls.Add(labelUploadFileName, 1, 0);
            tableLayoutPanelUpload.Dock = DockStyle.Fill;
            tableLayoutPanelUpload.Location = new Point(14, 3);
            tableLayoutPanelUpload.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanelUpload.Name = "tableLayoutPanelUpload";
            tableLayoutPanelUpload.RowCount = 1;
            tableLayoutPanelUpload.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelUpload.Size = new Size(609, 59);
            tableLayoutPanelUpload.TabIndex = 2;
            tableLayoutPanelUpload.Paint += tableLayoutPanelUpload_Paint_1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Left;
            button1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(4, 16);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(74, 27);
            button1.TabIndex = 0;
            button1.Text = "CSV";
            button1.UseVisualStyleBackColor = true;
            button1.Click += buttonUpload_Click;
            // 
            // labelUploadFileName
            // 
            labelUploadFileName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            labelUploadFileName.AutoSize = true;
            labelUploadFileName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelUploadFileName.Location = new Point(86, 22);
            labelUploadFileName.Margin = new Padding(4, 0, 4, 0);
            labelUploadFileName.Name = "labelUploadFileName";
            labelUploadFileName.Size = new Size(519, 15);
            labelUploadFileName.TabIndex = 1;
            labelUploadFileName.Text = "Selecciona un archivo con los IDs de los productos";
            // 
            // richTextBoxReport
            // 
            richTextBoxReport.Dock = DockStyle.Fill;
            richTextBoxReport.Location = new Point(14, 68);
            richTextBoxReport.Margin = new Padding(4, 3, 4, 3);
            richTextBoxReport.Name = "richTextBoxReport";
            richTextBoxReport.ReadOnly = true;
            richTextBoxReport.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBoxReport.Size = new Size(373, 190);
            richTextBoxReport.TabIndex = 3;
            richTextBoxReport.Text = "";
            richTextBoxReport.TextChanged += richTextBoxReport_TextChanged;
            // 
            // buttonDownload
            // 
            buttonDownload.Anchor = AnchorStyles.Left;
            buttonDownload.Enabled = false;
            buttonDownload.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonDownload.Location = new Point(14, 280);
            buttonDownload.Margin = new Padding(4, 3, 4, 3);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(74, 27);
            buttonDownload.TabIndex = 4;
            buttonDownload.Text = "Descargar";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // tableLayoutPanelRight
            // 
            tableLayoutPanelRight.ColumnCount = 1;
            tableLayoutPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelRight.Controls.Add(flowLayoutPanelDelete, 0, 0);
            tableLayoutPanelRight.Controls.Add(tableLayoutPanel1, 0, 1);
            tableLayoutPanelRight.Dock = DockStyle.Fill;
            tableLayoutPanelRight.Location = new Point(395, 68);
            tableLayoutPanelRight.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            tableLayoutPanelRight.RowCount = 2;
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanelRight.Size = new Size(228, 190);
            tableLayoutPanelRight.TabIndex = 7;
            // 
            // flowLayoutPanelDelete
            // 
            flowLayoutPanelDelete.Controls.Add(tableLayoutPanelImagesDelete);
            flowLayoutPanelDelete.Dock = DockStyle.Fill;
            flowLayoutPanelDelete.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelDelete.Location = new Point(4, 3);
            flowLayoutPanelDelete.Margin = new Padding(4, 3, 4, 3);
            flowLayoutPanelDelete.Name = "flowLayoutPanelDelete";
            flowLayoutPanelDelete.Size = new Size(220, 134);
            flowLayoutPanelDelete.TabIndex = 2;
            // 
            // tableLayoutPanelImagesDelete
            // 
            tableLayoutPanelImagesDelete.AutoSize = true;
            tableLayoutPanelImagesDelete.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelImagesDelete.ColumnCount = 2;
            tableLayoutPanelImagesDelete.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelImagesDelete.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelImagesDelete.Controls.Add(checkBoxDeleteImages, 0, 1);
            tableLayoutPanelImagesDelete.Controls.Add(checkBoxRedirections, 0, 0);
            tableLayoutPanelImagesDelete.Controls.Add(panelProgressBarDeleteProducts, 1, 2);
            tableLayoutPanelImagesDelete.Controls.Add(panelProgressBarDeleteImages, 1, 1);
            tableLayoutPanelImagesDelete.Controls.Add(panelProgressBarRedirections, 1, 0);
            tableLayoutPanelImagesDelete.Controls.Add(checkBoxDeleteProducts, 0, 2);
            tableLayoutPanelImagesDelete.Location = new Point(4, 3);
            tableLayoutPanelImagesDelete.Margin = new Padding(4, 3, 4, 3);
            tableLayoutPanelImagesDelete.Name = "tableLayoutPanelImagesDelete";
            tableLayoutPanelImagesDelete.RowCount = 4;
            tableLayoutPanelImagesDelete.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanelImagesDelete.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanelImagesDelete.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanelImagesDelete.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanelImagesDelete.Size = new Size(213, 100);
            tableLayoutPanelImagesDelete.TabIndex = 9;
            tableLayoutPanelImagesDelete.Paint += tableLayoutPanelImagesDelete_Paint_1;
            // 
            // checkBoxDeleteProducts
            // 
            checkBoxDeleteProducts.AutoSize = true;
            checkBoxDeleteProducts.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxDeleteProducts.Location = new Point(4, 53);
            checkBoxDeleteProducts.Margin = new Padding(4, 3, 4, 3);
            checkBoxDeleteProducts.Name = "checkBoxDeleteProducts";
            checkBoxDeleteProducts.Size = new Size(117, 19);
            checkBoxDeleteProducts.TabIndex = 7;
            checkBoxDeleteProducts.Text = "Borrar productos";
            checkBoxDeleteProducts.UseVisualStyleBackColor = true;
            checkBoxDeleteProducts.CheckedChanged += checkBoxDeleteProducts_CheckedChanged;
            // 
            // checkBoxDeleteImages
            // 
            checkBoxDeleteImages.AutoSize = true;
            checkBoxDeleteImages.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxDeleteImages.Location = new Point(4, 28);
            checkBoxDeleteImages.Margin = new Padding(4, 3, 4, 3);
            checkBoxDeleteImages.Name = "checkBoxDeleteImages";
            checkBoxDeleteImages.Size = new Size(118, 19);
            checkBoxDeleteImages.TabIndex = 6;
            checkBoxDeleteImages.Text = "Borrar imágenes";
            checkBoxDeleteImages.UseVisualStyleBackColor = true;
            checkBoxDeleteImages.CheckedChanged += checkBoxDeleteImages_CheckedChanged;
            // 
            // checkBoxRedirections
            // 
            checkBoxRedirections.AutoSize = true;
            checkBoxRedirections.Checked = true;
            checkBoxRedirections.CheckState = CheckState.Checked;
            checkBoxRedirections.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxRedirections.Location = new Point(4, 3);
            checkBoxRedirections.Margin = new Padding(4, 3, 4, 3);
            checkBoxRedirections.Name = "checkBoxRedirections";
            checkBoxRedirections.Size = new Size(148, 19);
            checkBoxRedirections.TabIndex = 12;
            checkBoxRedirections.Text = "Generar redirecciones";
            checkBoxRedirections.UseVisualStyleBackColor = true;
            checkBoxRedirections.CheckedChanged += checkBoxRedirections_CheckedChanged_1;
            // 
            // panelProgressBarDeleteProducts
            // 
            panelProgressBarDeleteProducts.Controls.Add(pictureBoxCheckDeleteProducts);
            panelProgressBarDeleteProducts.Controls.Add(progressBarDeleteProducts);
            panelProgressBarDeleteProducts.Dock = DockStyle.Fill;
            panelProgressBarDeleteProducts.Location = new Point(160, 53);
            panelProgressBarDeleteProducts.Margin = new Padding(4, 3, 4, 3);
            panelProgressBarDeleteProducts.Name = "panelProgressBarDeleteProducts";
            panelProgressBarDeleteProducts.Size = new Size(49, 19);
            panelProgressBarDeleteProducts.TabIndex = 14;
            // 
            // pictureBoxCheckDeleteProducts
            // 
            pictureBoxCheckDeleteProducts.Anchor = AnchorStyles.Left;
            pictureBoxCheckDeleteProducts.ErrorImage = (Image)resources.GetObject("pictureBoxCheckDeleteProducts.ErrorImage");
            pictureBoxCheckDeleteProducts.Image = (Image)resources.GetObject("pictureBoxCheckDeleteProducts.Image");
            pictureBoxCheckDeleteProducts.Location = new Point(0, 3);
            pictureBoxCheckDeleteProducts.Margin = new Padding(4, 3, 4, 3);
            pictureBoxCheckDeleteProducts.Name = "pictureBoxCheckDeleteProducts";
            pictureBoxCheckDeleteProducts.Size = new Size(17, 16);
            pictureBoxCheckDeleteProducts.TabIndex = 19;
            pictureBoxCheckDeleteProducts.TabStop = false;
            pictureBoxCheckDeleteProducts.Visible = false;
            // 
            // progressBarDeleteProducts
            // 
            progressBarDeleteProducts.Anchor = AnchorStyles.Left;
            progressBarDeleteProducts.Location = new Point(0, 4);
            progressBarDeleteProducts.Margin = new Padding(4, 3, 4, 3);
            progressBarDeleteProducts.Name = "progressBarDeleteProducts";
            progressBarDeleteProducts.Size = new Size(45, 14);
            progressBarDeleteProducts.TabIndex = 11;
            progressBarDeleteProducts.Visible = false;
            progressBarDeleteProducts.Click += progressBarDeleteProducts_Click;
            // 
            // panelProgressBarDeleteImages
            // 
            panelProgressBarDeleteImages.AutoSize = true;
            panelProgressBarDeleteImages.Controls.Add(pictureBoxCheckDeleteImages);
            panelProgressBarDeleteImages.Controls.Add(progressBarDeleteImages);
            panelProgressBarDeleteImages.Dock = DockStyle.Fill;
            panelProgressBarDeleteImages.Location = new Point(160, 28);
            panelProgressBarDeleteImages.Margin = new Padding(4, 3, 4, 3);
            panelProgressBarDeleteImages.Name = "panelProgressBarDeleteImages";
            panelProgressBarDeleteImages.Size = new Size(49, 19);
            panelProgressBarDeleteImages.TabIndex = 15;
            // 
            // pictureBoxCheckDeleteImages
            // 
            pictureBoxCheckDeleteImages.Anchor = AnchorStyles.Left;
            pictureBoxCheckDeleteImages.ErrorImage = (Image)resources.GetObject("pictureBoxCheckDeleteImages.ErrorImage");
            pictureBoxCheckDeleteImages.Image = (Image)resources.GetObject("pictureBoxCheckDeleteImages.Image");
            pictureBoxCheckDeleteImages.Location = new Point(0, 3);
            pictureBoxCheckDeleteImages.Margin = new Padding(4, 3, 4, 3);
            pictureBoxCheckDeleteImages.Name = "pictureBoxCheckDeleteImages";
            pictureBoxCheckDeleteImages.Size = new Size(39, 16);
            pictureBoxCheckDeleteImages.TabIndex = 18;
            pictureBoxCheckDeleteImages.TabStop = false;
            pictureBoxCheckDeleteImages.Visible = false;
            // 
            // progressBarDeleteImages
            // 
            progressBarDeleteImages.Anchor = AnchorStyles.Left;
            progressBarDeleteImages.Location = new Point(0, 3);
            progressBarDeleteImages.Margin = new Padding(4, 3, 4, 3);
            progressBarDeleteImages.Name = "progressBarDeleteImages";
            progressBarDeleteImages.Size = new Size(45, 14);
            progressBarDeleteImages.TabIndex = 10;
            progressBarDeleteImages.Visible = false;
            progressBarDeleteImages.Click += progressBarDeleteImages_Click;
            // 
            // panelProgressBarRedirections
            // 
            panelProgressBarRedirections.AutoSize = true;
            panelProgressBarRedirections.Controls.Add(pictureBoxCheckRedirections);
            panelProgressBarRedirections.Controls.Add(progressBarRedirections);
            panelProgressBarRedirections.Dock = DockStyle.Fill;
            panelProgressBarRedirections.Location = new Point(160, 3);
            panelProgressBarRedirections.Margin = new Padding(4, 3, 4, 3);
            panelProgressBarRedirections.Name = "panelProgressBarRedirections";
            panelProgressBarRedirections.Size = new Size(49, 19);
            panelProgressBarRedirections.TabIndex = 16;
            // 
            // pictureBoxCheckRedirections
            // 
            pictureBoxCheckRedirections.Anchor = AnchorStyles.Left;
            pictureBoxCheckRedirections.ErrorImage = (Image)resources.GetObject("pictureBoxCheckRedirections.ErrorImage");
            pictureBoxCheckRedirections.Image = (Image)resources.GetObject("pictureBoxCheckRedirections.Image");
            pictureBoxCheckRedirections.Location = new Point(0, 3);
            pictureBoxCheckRedirections.Margin = new Padding(4, 3, 4, 3);
            pictureBoxCheckRedirections.Name = "pictureBoxCheckRedirections";
            pictureBoxCheckRedirections.Size = new Size(17, 16);
            pictureBoxCheckRedirections.TabIndex = 17;
            pictureBoxCheckRedirections.TabStop = false;
            pictureBoxCheckRedirections.Visible = false;
            // 
            // progressBarRedirections
            // 
            progressBarRedirections.Anchor = AnchorStyles.Left;
            progressBarRedirections.Location = new Point(0, 4);
            progressBarRedirections.Margin = new Padding(4, 3, 4, 3);
            progressBarRedirections.Name = "progressBarRedirections";
            progressBarRedirections.Size = new Size(45, 14);
            progressBarRedirections.TabIndex = 13;
            progressBarRedirections.Visible = false;
            progressBarRedirections.Click += progressBarRedirections_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(buttonStartProgress, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonCancel, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 143);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(222, 44);
            tableLayoutPanel1.TabIndex = 9;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint_1;
            // 
            // buttonStartProgress
            // 
            buttonStartProgress.Anchor = AnchorStyles.Left;
            buttonStartProgress.BackColor = Color.Transparent;
            buttonStartProgress.Enabled = false;
            buttonStartProgress.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonStartProgress.Location = new Point(4, 3);
            buttonStartProgress.Margin = new Padding(4, 3, 4, 3);
            buttonStartProgress.Name = "buttonStartProgress";
            buttonStartProgress.Size = new Size(94, 38);
            buttonStartProgress.TabIndex = 1;
            buttonStartProgress.Text = "Ejecutar redirecciones";
            buttonStartProgress.UseVisualStyleBackColor = false;
            buttonStartProgress.Click += buttonStartProgress_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Left;
            buttonCancel.BackColor = Color.IndianRed;
            buttonCancel.Enabled = false;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            buttonCancel.ForeColor = Color.Black;
            buttonCancel.Location = new Point(106, 8);
            buttonCancel.Margin = new Padding(4, 3, 4, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(74, 27);
            buttonCancel.TabIndex = 8;
            buttonCancel.Text = "Cancelar";
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // RedirecctionUrlUserControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanelMain);
            Font = new Font("Microsoft Sans Serif", 9F);
            Margin = new Padding(4, 3, 4, 3);
            Name = "RedirecctionUrlUserControl";
            Size = new Size(627, 327);
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelUpload.ResumeLayout(false);
            tableLayoutPanelUpload.PerformLayout();
            tableLayoutPanelRight.ResumeLayout(false);
            flowLayoutPanelDelete.ResumeLayout(false);
            flowLayoutPanelDelete.PerformLayout();
            tableLayoutPanelImagesDelete.ResumeLayout(false);
            tableLayoutPanelImagesDelete.PerformLayout();
            panelProgressBarDeleteProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxCheckDeleteProducts).EndInit();
            panelProgressBarDeleteImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxCheckDeleteImages).EndInit();
            panelProgressBarRedirections.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxCheckRedirections).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private Button buttonStartProgress;
        private TableLayoutPanel tableLayoutPanelUpload;
        private Button button1;
        private Label labelUploadFileName;
        private RichTextBox richTextBoxReport;
        private Button buttonDownload;
        private CheckBox checkBoxDeleteImages;
        private TableLayoutPanel tableLayoutPanelRight;
        private FlowLayoutPanel flowLayoutPanelDelete;
        private CheckBox checkBoxDeleteProducts;
        private TableLayoutPanel tableLayoutPanelImagesDelete;
        private ProgressBar progressBarDeleteImages;
        private ProgressBar progressBarDeleteProducts;
        private HelpProvider helpProvider1;
        private ProgressBar progressBarRedirections;
        private CheckBox checkBoxRedirections;
        private Panel panelProgressBarDeleteProducts;
        private Panel panelProgressBarDeleteImages;
        private Panel panelProgressBarRedirections;
        private PictureBox pictureBoxCheckRedirections;
        private PictureBox pictureBoxCheckDeleteProducts;
        private PictureBox pictureBoxCheckDeleteImages;
        private Button buttonCancel;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
