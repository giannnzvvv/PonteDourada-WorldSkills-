using Microsoft.EntityFrameworkCore;
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
        decimal total;
        int totalQuantity;

        List<ProductCards> selectedProducts = new List<ProductCards>();
        Dictionary<ProductCards, int> selectedProductsData = new Dictionary<ProductCards, int>();
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
                foreach (var product in db.Produtos.Include(p => p.ProdutoSolicitacaos))
                {
                    var productCard = new ProductCards();

                    productCard.Title = product.Nome;
                    productCard.exp = product.Validade.ToString();
                    productCard.discount = 0;
                    productCard.ContextMenuStrip = productCard.contextMenuStrip1;
                    productCard.Logo = Image.FromFile(File.Exists($"C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\{product.Id}.png") ? $"C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\{product.Id}.png" : "C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\0.png");
                    productCard.price = (decimal)product.Valor;

                    this.flowLayoutPanel1.Controls.Add(productCard);
                }

                foreach (Control control in this.flowLayoutPanel1.Controls)
                {
                    if (control is ProductCards productCard && this.flowLayoutPanel1.Controls.Find(productCard.Name, false).Length > 0)
                    {
                        productCard.MouseDown += (s, e) =>
                        {
                            if (e.Button == MouseButtons.Left)
                            {
                                DoDragDrop(productCard, DragDropEffects.Move);
                            }
                        };

                        productCard.contextMenuStrip1.Items[1].Click += (s, e) =>
                        {
                            var editProductWindow = new ProductInfo();
                            editProductWindow.label1.Text = $"{productCard.Title}";
                            editProductWindow.label2.Text = $"R${productCard.price:F2} por cada unidade.";
                            editProductWindow.label3.Text = $"Fornecido por {this.comboBox1.SelectedItem}.";
                            editProductWindow.label4.Text = $"{this.selectedProductsData[productCard]} unidade(s).";
                            editProductWindow.label6.Text = $"Desconto: {productCard.discount}";
                            editProductWindow.label7.Text = $"Validade: {productCard.exp}";
                            editProductWindow.label5.Text = $"Subtotal: R${this.selectedProductsData[productCard] * productCard.price:F2}";

                            editProductWindow.Show();
                        };
                    }

                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flowLayoutPanel2_DragEnter(object sender, DragEventArgs e)
        {
            var productCard = (ProductCards)e.Data.GetData(typeof(ProductCards));
            if (e.Data.GetDataPresent(typeof(ProductCards)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else if (this.selectedProducts.Contains((ProductCards)e.Data.GetData(typeof(ProductCards))))
            {
                e.Effect = DragDropEffects.None;
            } else
            {
                e.Effect = DragDropEffects.None;
            }
        }   

        private void flowLayoutPanel2_DragDrop(object sender, DragEventArgs e)
        {
            if (this.selectedProducts.Contains((ProductCards)e.Data.GetData(typeof(ProductCards))))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            if (e.Data.GetDataPresent(typeof(ProductCards)))
            {
                e.Effect = DragDropEffects.Move;
                
                this.flowLayoutPanel2.Controls.Add((ProductCards)e.Data.GetData(typeof(ProductCards)));
                this.selectedProducts.Add((ProductCards)e.Data.GetData(typeof(ProductCards)));
                this.flowLayoutPanel1.Controls.Remove((ProductCards)e.Data.GetData(typeof(ProductCards)));

                var product = (ProductCards)e.Data.GetData(typeof(ProductCards));
                var addProductWindow = new AddProductWindow(product);
                addProductWindow.Show();
                addProductWindow.FormClosed += (s, args) =>
                {
                    if (addProductWindow.chosenQuantity <= 0)
                    {
                        this.flowLayoutPanel1.Controls.Add((ProductCards)e.Data.GetData(typeof(ProductCards)));
                        this.flowLayoutPanel2.Controls.Remove((ProductCards)e.Data.GetData(typeof(ProductCards)));
                        this.selectedProducts.Remove((ProductCards)e.Data.GetData(typeof(ProductCards)));
                        return;
                    }

                    this.label4.Text = $"Quantidade Produtos: {totalQuantity}";
                    this.total += (decimal)addProductWindow.chosenQuantity * product.price;
                    this.selectedProductsData[product] = addProductWindow.chosenQuantity;
                    this.label7.Text = $"Valor Total: R${this.total:F2}";

                    
                };
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
