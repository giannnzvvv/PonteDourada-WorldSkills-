using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PonteDourada.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PonteDourada
{
    public partial class NewSolicitation : Form
    {

        double total;
        int totalQuantity;
        double totalDiscount;
        Dictionary<ProductCards, double> selectedProducts = new Dictionary<ProductCards, double>();
        // basically, {ProdutoObject = quantity*price}
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
                foreach (var product in db.Produtos.Include(p => p.Tipo).Include(p => p.ProdutoSolicitacaos).ToList())
                {
                    var productCard = new ProductCards();

                    productCard.Title = product.Nome;
                    productCard.exp = product.Validade.ToString();
                    productCard.product = product;
                    productCard.Logo = Image.FromFile(File.Exists($"C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\{product.Id}.png") ? $"C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\{product.Id}.png" : "C:\\Users\\antol\\Downloads\\DataFiles\\Produtos\\0.png");
                    productCard.price = (decimal)product.Valor;
                    productCard.discount = 0;
                    productCard.estoque = (int)product.Estoque;
                    productCard.Type = product.Tipo.Nome;
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
            if (e.Data.GetDataPresent(typeof(ProductCards)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void flowLayoutPanel2_DragDrop(object sender, DragEventArgs e)
        {
            var productCard = (ProductCards)e.Data.GetData(typeof(ProductCards));
            
            this.flowLayoutPanel2.Controls.Add(productCard);
            if (this.selectedProducts.ContainsKey(productCard))
            {
                MessageBox.Show("Voce ja adicionou esse produto! Por favor, altere ou remova-o utilizando o botao direito do seu mouse.");
                return;
            }
            AddProductWindow addProduct = new AddProductWindow(productCard);
            addProduct.Show();
            addProduct.FormClosed += (s, e) =>
            {
                if (addProduct.chosenQuantity <= 0)
                {
                    this.flowLayoutPanel1.Controls.Add(productCard);
                    this.flowLayoutPanel2.Controls.Remove(productCard);
                    this.selectedProducts.Remove(productCard);
                    return;
                }

                productCard.ContextMenuStrip = productCard.contextMenuStrip1;

                var addedValue = (double)(productCard.price * addProduct.chosenQuantity) - productCard.discount;
                this.selectedProducts[productCard] = addProduct.chosenQuantity;
                this.totalQuantity += addProduct.chosenQuantity;
                this.totalDiscount += productCard.discount;
                this.total += addedValue;

                changeTextInLabel(this.label5, $"Desconto: R${this.totalDiscount}");
                changeTextInLabel(this.label7, $"Valor Total: R${this.total:F2}");
                changeTextInLabel(this.label4, $"Quantidade Produtos: {this.totalQuantity}");

                void menuItemClicked(object sender, ToolStripItemClickedEventArgs e)
                {
                    if (e.ClickedItem is ToolStripMenuItem)
                    {
                        if (e.ClickedItem.Text == "Excluir")
                        {
                            if (!this.selectedProducts.Remove(productCard)) return;

                            productCard.ContextMenuStrip.ItemClicked -= menuItemClicked;
                            this.selectedProducts.Remove(productCard);
                            this.flowLayoutPanel1.Controls.Add(productCard);
                            this.flowLayoutPanel2.Controls.Remove(productCard);
                            this.totalQuantity -= addProduct.chosenQuantity;
                            this.totalDiscount -= productCard.discount;
                            this.total -= addedValue;


                            changeTextInLabel(this.label5, $"Desconto: R${this.totalDiscount}");
                            changeTextInLabel(this.label7, $"Valor Total: R${this.total:F2}");
                            changeTextInLabel(this.label4, $"Quantidade Produtos: {this.totalQuantity}");
                            productCard.ContextMenuStrip = null;
                        }
                        else
                        {
                            AddProductWindow addProduct = new AddProductWindow(productCard);
                            addProduct.Show();
                            addProduct.FormClosed += (s, e) =>
                            {
                                if (addProduct.chosenQuantity <= 0)
                                {
                                    this.flowLayoutPanel1.Controls.Add(productCard);
                                    this.flowLayoutPanel2.Controls.Remove(productCard);
                                    this.selectedProducts.Remove(productCard);
                                    return;
                                }

                                productCard.ContextMenuStrip = productCard.contextMenuStrip1;

                                var addedValue = (double)(productCard.price * addProduct.chosenQuantity) - productCard.discount;
                                this.selectedProducts[productCard] = addProduct.chosenQuantity;
                                this.totalQuantity = addProduct.chosenQuantity;
                                this.totalDiscount = (this.totalDiscount - productCard.discount);
                                this.total = addedValue;

                                changeTextInLabel(this.label5, $"Desconto: R${this.totalDiscount}");
                                changeTextInLabel(this.label7, $"Valor Total: R${this.total:F2}");
                                changeTextInLabel(this.label4, $"Quantidade Produtos: {this.totalQuantity}");
                            };
                        }
                    }
                };

                productCard.ContextMenuStrip.ItemClicked += menuItemClicked;
            };

            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                using (var db = new Sessao2Context())
                {
                    var cashback = db.Cashbacks.Where(p => p.Solicitacao.Cliente.Id == Session.CurrentUser.Id)?.Sum(c => c.Valor) ?? 0;
                    var cashbackSpent = db.Solicitacaos.Where(p => p.ClienteId == Session.CurrentUser.Id).Sum(c => c.Cashback) ?? 0;
                    var cashbackAvailable = Math.Max(0, cashback - cashbackSpent);

                    changeTextInLabel(this.label6, $"Cashback: R${cashbackAvailable:F2}");

                    double finalTotal = this.total - Math.Min(this.total, cashbackAvailable);
                    changeTextInLabel(this.label7, $"Valor Total: R${finalTotal:F2}");
                }

            }
            else
            {
                changeTextInLabel(this.label6, $"Cashback: R$0,00");
                changeTextInLabel(this.label7, $"Valor Total: R${this.total:F2}");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            foreach (Control control in this.flowLayoutPanel1.Controls)
            {
                if (control is ProductCards productCard)
                {
                    string productName = productCard.Title.ToLower();
                    control.Visible = productName.Contains(this.textBox2.Text, StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        private void changeTextInLabel(Label controlito, string text)
        {
            controlito.Text = text;
        }

        private void button1_Click(object sender, EventArgs e) // it adds to database thats it
        {
            using (var db = new Sessao2Context())
            {
                var cashback = db.Cashbacks.Where(p => p.Solicitacao.Cliente.Id == Session.CurrentUser.Id)?.Sum(c => c.Valor) ?? 0;
                var cashbackSpent = db.Solicitacaos.Where(p => p.ClienteId == Session.CurrentUser.Id).Sum(c => c.Cashback) ?? 0;
                var cashbackAvailable = Math.Max(0, cashback - cashbackSpent);

                Solicitacao sos = new Solicitacao();
                sos.Cashback = this.checkBox1.Checked ? Math.Min(this.total, cashbackAvailable) : 0;
                sos.Cliente = db.Clientes.Find(Session.CurrentUser.Id);
                sos.Cashbacks = new List<Cashback>();
                sos.Cashbacks.Add(new Cashback() { Valor = this.total * 0.01 });
                sos.DataHoraCadastro = DateTime.Now;
                sos.Validade = DateOnly.FromDateTime(this.dateTimePicker1.Value);
                sos.Descricao = this.textBox1.Text;
                db.Solicitacaos.Add(sos);

                foreach (var kvp in selectedProducts)
                {
                    ProdutoSolicitacao PSAOSDFIAOFIDW = new ProdutoSolicitacao();

                    var product = kvp.Key;
                    var quantity = kvp.Value;

                    db.Produtos.Find(product.product.Id).Estoque -= (int)quantity;

                    PSAOSDFIAOFIDW.Solicitcao = sos;
                    PSAOSDFIAOFIDW.SolicitcaoId = sos.Id;
                    PSAOSDFIAOFIDW.Produto = db.Produtos.Find(product.product.Id);
                    PSAOSDFIAOFIDW.Desconto = product.discount;
                    PSAOSDFIAOFIDW.Quantidade = (int)quantity;

                    db.ProdutoSolicitacaos.Add(PSAOSDFIAOFIDW);

                }

                db.SaveChanges();

                MessageBox.Show("Solicitacao criada com sucesso!");
                this.Close();
            }
        }

        private void Medicamento_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var control in this.flowLayoutPanel1.Controls)
            {
                if (control is ProductCards)
                {
                    if (sender is Label)
                    {
                        ((ProductCards)control).Visible = ((ProductCards)control).Type == ((Label)sender).Text;
                    }
                    else if (sender is PictureBox)
                    {
                        ((ProductCards)control).Visible = ((ProductCards)control).Type == ((PictureBox)sender).Name;
                    }
                }
            }
        }

        private void label17_Click(object sender, MouseEventArgs e)
        {
            foreach (var control in this.flowLayoutPanel1.Controls)
            {
                if (control is ProductCards)
                {
                    ((ProductCards)control).Visible = true;
                }
            }
        }
    }
}
