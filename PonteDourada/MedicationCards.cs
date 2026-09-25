using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace PonteDourada
{
    public partial class MedicationCard : UserControl
    {

        public MedicationCard()
        {
            InitializeComponent();
            this.label2.MouseHover += (a, b) => this.mouseHover();

        }
        private void mouseHover()
        {
            ToolTip namea = new ToolTip();
            Label asdlkasdlkad = new Label();
            namea.ToolTipTitle = "Description";
            namea.SetToolTip(asdlkasdlkad, this.Desc);
            namea.InitialDelay = 200;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Title
        {
            get => this.label1.Text;
            set => this.label1.Text = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Desc
        {
            get => this.label2.Text;
            set => this.label2.Text = value;
        }

        [Browsable(true)]

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime cadastro { get; set; }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image Logo
        {
            get => this.pictureBox1.Image;
            set => this.pictureBox1.Image = value;
        }
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public decimal price
        {
            get => decimal.TryParse(this.label4.Text, out decimal result) ? result : 0m;
            set => this.label4.Text = $"R${value.ToString()}";
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateOnly expiration
        {
            get => DateOnly.FromDateTime(DateTime.TryParse(this.label3.Text, CultureInfo.CurrentCulture, out DateTime result) ? result : new DateTime());
            set => this.label3.Text = value.ToString();
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Label expired
        {
            get => this.label5;
            set => label5 = value;
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? quantity { get; set; }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image imageOrWhatever { get => this.pictureBox1.Image; set => this.pictureBox1.Image = value;  }
    }
}
