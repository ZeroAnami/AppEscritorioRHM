using AppEscritorioRHM.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppEscritorioRHM.Forms
{
    public partial class FormImageSelector : Form
    {
        private readonly List<ImageProduct> _images;
        private readonly List<CheckBox> _checkBoxes = new();
        private readonly ToolTip _toolTip = new();
        private FlowLayoutPanel _flowPanel;

        public List<ImageProduct> SelectedImages { get; private set; } = new();

        public FormImageSelector(List<ImageProduct> images)
        {
            _images = images;
            InitializeComponent();
            CreateCustomLayout();
        }

        private void CreateCustomLayout()
        {
            this.StartPosition = FormStartPosition.CenterParent;

            // Panel superior: Acciones
            var topPanel = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.FromArgb(240, 240, 240) };
            var btnAll = new Button { Text = "Marcar Todo", Left = 12, Top = 12, Width = 100 };
            var btnNone = new Button { Text = "Desmarcar Todo", Left = 118, Top = 12, Width = 110 };

            btnAll.Click += (s, e) => ToggleCheckboxes(true);
            btnNone.Click += (s, e) => ToggleCheckboxes(false);
            topPanel.Controls.AddRange([btnAll, btnNone]);

            // Panel inferior: Confirmación
            var bottomPanel = new Panel { Dock = DockStyle.Bottom, Height = 60 };
            var btnOk = new Button { Text = "Eliminar Seleccionadas", DialogResult = DialogResult.OK, Dock = DockStyle.Right, Width = 150, Margin = new Padding(10) };
            var btnCancel = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Dock = DockStyle.Right, Width = 100 };

            btnOk.Click += (s, e) => SelectedImages = _checkBoxes.Where(c => c.Checked).Select(c => (ImageProduct)c.Tag!).ToList();
            bottomPanel.Padding = new Padding(10);
            bottomPanel.Controls.AddRange([btnOk, new Label { Dock = DockStyle.Right, Width = 10 }, btnCancel]);

            // Contenedor principal de imágenes
            _flowPanel = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.White };

            this.Controls.Add(_flowPanel);
            this.Controls.Add(topPanel);
            this.Controls.Add(bottomPanel);

            this.Load += async (s, e) => await LoadThumbnailsAsync();
        }

        private async Task LoadThumbnailsAsync()
        {
            foreach (var img in _images)
            {
                var container = new TableLayoutPanel { Size = new Size(140, 200), RowCount = 2, Margin = new Padding(10) };
                container.RowStyles.Add(new RowStyle(SizeType.Absolute, 130));
                container.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                var pb = new PictureBox { SizeMode = PictureBoxSizeMode.Zoom, Dock = DockStyle.Fill, BackColor = Color.GhostWhite };
                var cb = new CheckBox
                {
                    Text = img.Name,
                    Dock = DockStyle.Fill,
                    Tag = img,
                    Checked = true,
                    // Coloca el recuadro de verificación arriba a la izquierda
                    CheckAlign = ContentAlignment.TopLeft,
                    // Alinea el texto también arriba a la izquierda para que coincidan
                    TextAlign = ContentAlignment.TopLeft,
                    Padding = new Padding(0, 5, 0, 0) // Un pequeño margen superior para estética
                };

                _toolTip.SetToolTip(cb, img.Name);

                container.Controls.Add(pb, 0, 0);
                container.Controls.Add(cb, 0, 1);
                _flowPanel.Controls.Add(container);
                _checkBoxes.Add(cb);

                _ = Task.Run(() => DownloadImage(img.Src, pb));
            }
        }

        private void DownloadImage(string url, PictureBox pb)
        {
            try
            {
                using var client = new System.Net.Http.HttpClient();
                var data = client.GetByteArrayAsync(url).Result;
                using var ms = new System.IO.MemoryStream(data);
                var image = Image.FromStream(ms);
                pb.Invoke(() => pb.Image = image);
            }
            catch { /* Silenciar errores de red para miniaturas individuales */ }
        }

        private void ToggleCheckboxes(bool state) => _checkBoxes.ForEach(c => c.Checked = state);
    }
}
