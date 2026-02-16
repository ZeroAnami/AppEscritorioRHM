namespace AppEscritorioRHM
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMain = new Panel();
            tableLayoutPanelMain = new TableLayoutPanel();
            tableLayoutPanelControlButtons = new TableLayoutPanel();
            btnRedo = new Button();
            btnUndo = new Button();
            buttonConfig = new Button();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelControlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.AutoSize = true;
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(3, 3);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(628, 318);
            panelMain.TabIndex = 0;
            panelMain.Paint += panelMain_Paint;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelControlButtons, 0, 1);
            tableLayoutPanelMain.Controls.Add(panelMain, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 90F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanelMain.Size = new Size(634, 361);
            tableLayoutPanelMain.TabIndex = 1;
            tableLayoutPanelMain.Paint += tableLayoutPanelMain_Paint;
            // 
            // tableLayoutPanelControlButtons
            // 
            tableLayoutPanelControlButtons.ColumnCount = 4;
            tableLayoutPanelControlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanelControlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelControlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanelControlButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanelControlButtons.Controls.Add(btnRedo, 3, 0);
            tableLayoutPanelControlButtons.Controls.Add(btnUndo, 2, 0);
            tableLayoutPanelControlButtons.Controls.Add(buttonConfig, 0, 0);
            tableLayoutPanelControlButtons.Dock = DockStyle.Fill;
            tableLayoutPanelControlButtons.Location = new Point(3, 327);
            tableLayoutPanelControlButtons.Name = "tableLayoutPanelControlButtons";
            tableLayoutPanelControlButtons.RowCount = 1;
            tableLayoutPanelControlButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelControlButtons.Size = new Size(628, 31);
            tableLayoutPanelControlButtons.TabIndex = 1;
            tableLayoutPanelControlButtons.Paint += tableLayoutPanelControlButtons_Paint;
            // 
            // btnRedo
            // 
            btnRedo.Anchor = AnchorStyles.None;
            btnRedo.Enabled = false;
            btnRedo.Image = Properties.Resources.redo;
            btnRedo.Location = new Point(552, 3);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(72, 25);
            btnRedo.TabIndex = 2;
            btnRedo.UseVisualStyleBackColor = true;
            btnRedo.Click += btnRedo_Click;
            // 
            // btnUndo
            // 
            btnUndo.Anchor = AnchorStyles.None;
            btnUndo.Enabled = false;
            btnUndo.Font = new Font("Segoe UI Black", 9F);
            btnUndo.Image = Properties.Resources.undo;
            btnUndo.Location = new Point(472, 3);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(72, 25);
            btnUndo.TabIndex = 1;
            btnUndo.UseVisualStyleBackColor = true;
            btnUndo.Click += btnUndo_Click;
            // 
            // buttonConfig
            // 
            buttonConfig.Anchor = AnchorStyles.None;
            buttonConfig.BackgroundImage = Properties.Resources.build_circle_32;
            buttonConfig.BackgroundImageLayout = ImageLayout.Zoom;
            buttonConfig.Location = new Point(7, 3);
            buttonConfig.Name = "buttonConfig";
            buttonConfig.Size = new Size(25, 25);
            buttonConfig.TabIndex = 3;
            buttonConfig.UseVisualStyleBackColor = true;
            buttonConfig.Click += buttonConfig_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 361);
            Controls.Add(tableLayoutPanelMain);
            MinimumSize = new Size(650, 400);
            Name = "MainForm";
            Text = "Form1";
            Load += FormMain_Load;
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            tableLayoutPanelControlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private TableLayoutPanel tableLayoutPanelMain;
        private Button btnUndo;
        private TableLayoutPanel tableLayoutPanelControlButtons;
        private Button btnRedo;
        private Button buttonConfig;
    }
}
