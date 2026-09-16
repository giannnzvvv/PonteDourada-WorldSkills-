using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PonteDourada
{
    public partial class OrderingWindow : Form
    {
        public Action<string>? sortType;
        public OrderingWindow()
        {
            InitializeComponent();
            this.flowLayoutPanel1.Controls.Add(button1);
            this.flowLayoutPanel1.Controls.Add(button2);
            this.flowLayoutPanel1.Controls.Add(button3);
            this.flowLayoutPanel1.Controls.Add(button4);
            this.flowLayoutPanel1.Controls.Add(button5);
            this.flowLayoutPanel1.Controls.Add(button6);

            this.button1.Click += (s, e) =>
            {
                this.Close();
                sortType?.Invoke("Lastest");
            };
            this.button2.Click += (s, e) => {
                this.Close();
                sortType?.Invoke("Oldest");
            };
            this.button3.Click += (s, e) => {
                this.Close();
                sortType?.Invoke("More");
            };
            this.button4.Click += (s, e) => {
                this.Close();
                sortType?.Invoke("Less");
            };
            this.button5.Click += (s, e) => {
                this.Close();
                sortType?.Invoke("Closer");
            };
            this.button6.Click += (s, e) => {
                this.Close();
                sortType?.Invoke("Farther");
            };

        }
    }
}
