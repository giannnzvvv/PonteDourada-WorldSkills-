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
    public partial class AddProductWindow : Form
    {
        ProductCards productAsked;
        private Produto productBeingUsed;
        public int chosenQuantity;
        public AddProductWindow(ProductCards product)
        {
            InitializeComponent();
            this.productAsked = product;
        }

        private void AddProductWindow_Shown(object s, EventArgs e)
        {
            using (var db = new Sessao2Context())
            {
                var product = db.Produtos.FirstOrDefault(x => x.Nome == this.productAsked.Title);
                this.productBeingUsed = product;

                this.label1.Text = product.Nome;
                this.label2.Text = product.Validade.ToString();
                this.label3.Text = product.Valor.ToString();
                this.label4.Text = product.Estoque.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.numericUpDown1.Value <= 0)
            {
                MessageBox.Show("Quantidade inválida. Por favor, insira um valor maior que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } else if (this.numericUpDown1.Value > this.productBeingUsed.Estoque)
            {
                MessageBox.Show("Quantidade solicitada excede o estoque disponível.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.productBeingUsed.Estoque -= (int)this.numericUpDown1.Value;
                using (var db = new Sessao2Context())
                {
                    db.Produtos.Update(this.productBeingUsed);
                    db.SaveChanges();
                }
            }
            this.chosenQuantity = (int)this.numericUpDown1.Value;
            this.Close();
        }
    }
}
