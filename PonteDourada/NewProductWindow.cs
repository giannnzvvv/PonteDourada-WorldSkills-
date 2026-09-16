using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
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
    public partial class NewProductWindow : Form
    {
        public NewProductWindow()
        {
            InitializeComponent();

            using (var db = new Sessao2Context())
            {
                this.comboBox1.DisplayMember = "Nome";
                this.comboBox1.ValueMember = "Id";
                this.comboBox1.DataSource = db.TiposProdutos.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (this.openFileDialog1)
            {
                this.openFileDialog1.Filter = "PNG Image (*.png)|*.png|All Files (*.*)|*.*";
                this.openFileDialog1.Title = "Selecione um imagem";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;

                    this.pictureBox1.ImageLocation += filePath;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var db = new Sessao2Context())
            {
                if (this.textBox1.Text.IsNullOrEmpty() || this.textBox2.Text.IsNullOrEmpty())
                {
                    MessageBox.Show("Nenhum campo pode ser nulo!");
                    return;
                }

                var product = new Produto();
                product.TipoId = int.Parse(comboBox1.Text);

                product.Nome = this.textBox1.Text;
                product.Descricao = this.textBox2.Text;

                product.Estoque = (int)this.numericUpDown1.Value;
                product.Valor = (double)this.numericUpDown2.Value;

                DateOnly expiration = DateOnly.FromDateTime(this.dateTimePicker1.Value);
                DateTime register = DateTime.Now;



                db.Produtos.Add(product);
            }
        }
    }
}
