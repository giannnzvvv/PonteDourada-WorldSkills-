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
    public partial class NewSolicitation : Form
    {
        public NewSolicitation()
        {
            InitializeComponent();

            using (var db = new Sessao2Context())
            {
                this.comboBox1.DataSource = db.Pessoas
                    .Where(x => x.Fornecedor != null)
                    .Select(x => x.Nome)
                    .ToList();



            }

            this.Shown += (s, e) => showProductsInThisBullshit();
        }

        private void showProductsInThisBullshit()
        {
            using (var db = new Sessao2Context())
            {
                foreach (var product in db.Produtos)
                {
                    var productCard = new ProductCards();

                    productCard.Title = product.Nome;
                    productCard.Logo = Image.FromFile(File.Exists($"C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\{product.Id}.png") ? $"C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\{product.Id}.png" : "C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\0.png");
                    productCard.price = (decimal)product.Valor;

                    this.flowLayoutPanel1.Controls.Add(productCard);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
