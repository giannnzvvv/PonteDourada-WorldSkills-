using PonteDourada.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PonteDourada
{
    public partial class ProductWindow : Form
    {
        private readonly LoginWindow loginWindow;
        public ProductWindow(LoginWindow loginWindow)
        {
            InitializeComponent();

            using (var db = new Sessao2Context())
            {
                var datas = db.Produtos.Select(p => new
                {
                    Nome = p.Nome,
                    Tipo = p.Tipo.Nome,
                    Validade = p.Validade,
                    Cadastro = p.DataHoraCadastro
                }).ToList();

                this.dataGridView1.DataSource = datas;
            }

            this.loginWindow = loginWindow;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "Validade")
            {
                if (e.Value != null && DateTime.TryParse(e.Value.ToString(), out DateTime result))
                {
                    int remaining = (result.Date - DateTime.Today).Days;
                    var current = this.dataGridView1.Rows[e.RowIndex];

                    if (remaining <= 0)
                    {
                        current.DefaultCellStyle.BackColor = Color.FromArgb(255, 90, 90);
                        current.DefaultCellStyle.ForeColor = Color.White;
                        current.DefaultCellStyle.SelectionBackColor = Color.FromArgb(150, 50, 50);
                        current.DefaultCellStyle.SelectionForeColor = Color.White;

                    }
                    else if (remaining <= 7)
                    {
                        current.DefaultCellStyle.BackColor = Color.FromArgb(130, 255, 255);
                        current.DefaultCellStyle.ForeColor = Color.White;
                        current.DefaultCellStyle.SelectionBackColor = Color.FromArgb(110, 255, 255);
                        current.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nothing yet");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Session.CurrentUser = null;
            Properties.Settings.Default.Remembered = -1;
            Properties.Settings.Default.Save();
            this.loginWindow.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewProductWindow newProduct = new NewProductWindow();
            newProduct.Show();
        }
    }
}
