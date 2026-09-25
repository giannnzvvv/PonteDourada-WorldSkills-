using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using PonteDourada.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PonteDourada
{
    public partial class SolicitationWindow : Form
    {
        public readonly LoginWindow loginWindow;
        public SolicitationWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            this.Shown += (s, e) => ShowProducts();
            this.textBox1.TextChanged += (s, e) => searchProducts();
            this.loginWindow = loginWindow;

        }

        public void ShowProducts()
        {
            using (var db = new Sessao2Context())
            {
                foreach (var solicitacao in db.Solicitacaos.Include(y => y.ProdutoSolicitacaos).ThenInclude(y => y.Produto).ThenInclude(y => y.Tipo))
                {
                    var medCard = new MedicationCard();
                    var lala = new ToolTip();
                    var products = solicitacao.ProdutoSolicitacaos.ToList();
                    var idk = products.GroupBy(p => p.Produto.Tipo.Nome).OrderByDescending(g => g.Count()).Take(3).Select(dsffsd => dsffsd.Key).ToList();
                    medCard.expiration = solicitacao.Validade;
                    medCard.Title = $"Solicitacao de produtos de {string.Join(", ", idk)}.";
                    medCard.quantity = solicitacao.ProdutoSolicitacaos.Sum(x => x.Quantidade);
                    medCard.imageOrWhatever = Image
                        .FromFile(Path
                        .Combine("C:\\Users\\antol\\Downloads\\DataFiles\\TiposProdutos", $"{products
                        .MaxBy(x => x.Quantidade).Produto.Tipo.Nome}.png"));

                    medCard.id = solicitacao.Id;    
                    medCard.Desc = solicitacao.Descricao;
                    medCard.price += (decimal)products.Sum(x => x.Produto.Valor * x.Quantidade);
                    medCard.cadastro = solicitacao.DataHoraCadastro;
                    medCard.productNames = products.Select(p => p.Produto.Nome).ToList(); 
                    var expression = medCard.expiration.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber;

                    lala.SetToolTip(medCard.label1, medCard.Title);

                    if (expression <= 0)
                    {
                        medCard.expired.Text = "Vencido";
                        medCard.expired.ForeColor = Color.Red;

                    }
                    else if (expression <= 7)
                    {
                        medCard.expired.Text = "Vencendo";
                        medCard.expired.ForeColor = Color.Orange;
                    }
                    else
                    {
                        medCard.expired.ForeColor = Color.Green;
                        medCard.expired.Text = "Valido";
                    }

                    if (!(expression <= 0))
                    {
                        ToolStripMenuItem Edit = new ToolStripMenuItem("Editar");
                        ToolStripMenuItem Delete = new ToolStripMenuItem("Excluir");
                        medCard.contextMenuStrip1.Items.Clear();
                        medCard.contextMenuStrip1.Items.Add(Edit);
                        medCard.contextMenuStrip1.Items.Add(new ToolStripSeparator());
                        medCard.contextMenuStrip1.Items.Add(Delete);
                        medCard.contextMenuStrip1.Refresh();
                    }
                    else
                    {
                        ToolStripMenuItem Visualize = new ToolStripMenuItem("Visualizar Detalhes");
                        medCard.contextMenuStrip1.Items.Clear();
                        medCard.contextMenuStrip1.Items.Add(Visualize);
                        medCard.contextMenuStrip1.Refresh();

                    }

                    void itemClicked(object snd, ToolStripItemClickedEventArgs erts)
                    {
                        if (erts.ClickedItem.Text == "Excluir")
                        {
                            using (var db = new Sessao2Context())
                            {
                                var resultito = MessageBox.Show("Voce realmente deseja excluir essa solicitacao?", "Aviso!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (resultito == DialogResult.Yes)
                                {
                                    var menu = snd as ContextMenuStrip;
                                    var parent = (MedicationCard)menu.SourceControl;

                                    var solicitacao = db.Solicitacaos
                                                    .Include(s => s.Cashbacks)
                                                    .Include(s => s.ProdutoSolicitacaos).ThenInclude(sdsf => sdsf.Produto)
                                                    .FirstOrDefault(s => s.Id == parent.id);
                                    foreach (var productSolicitation in solicitacao.ProdutoSolicitacaos)
                                    {
                                        productSolicitation.Produto.Estoque += productSolicitation.Quantidade;
                                    }

                                    db.Cashbacks.RemoveRange(solicitacao.Cashbacks);
                                    db.ProdutoSolicitacaos.RemoveRange(solicitacao.ProdutoSolicitacaos);
                                    db.Remove(solicitacao);
                                    db.SaveChanges();
                                    this.flowLayoutPanel1.Controls.Remove(medCard);
                                }
                            }
                        } else if (erts.ClickedItem.Text == "Editar")
                        {
                            var newSoli = new NewSolicitation(medCard.id);
                            newSoli.Show();
                            newSoli.FormClosed += (s, e) => ShowProducts();
                        } else if (erts.ClickedItem.Text == "Visualizar Detalhes")
                        {
                            var details = new Bullshitium(medCard.id);
                            details.Show();
                        }
                    }
                    medCard.contextMenuStrip1.ItemClicked += itemClicked;

                    if (flowLayoutPanel1.Controls.OfType<MedicationCard>().FirstOrDefault(x => x == medCard) == null)
                    {
                        this.flowLayoutPanel1.Controls.Add(medCard);
                    }
                }

            }
        }

        private void searchProducts()
        {
            var controls = this.flowLayoutPanel1.Controls.OfType<MedicationCard>();
            foreach (var control in controls)
            {
                control.Visible = control.productNames.Any(nome => nome.Contains(this.textBox1.Text, StringComparison.OrdinalIgnoreCase));
            }

            int index = 0;

            foreach (var control in controls.ToList())
            {
                flowLayoutPanel1.Controls.SetChildIndex(control, index++);
            }
        }

        private void orderProducts(string orderType)
        {
            var controls = this.flowLayoutPanel1.Controls.OfType<MedicationCard>();
            switch (orderType)
            {
                case "Lastest":
                    controls = controls.OrderByDescending(x => x.cadastro);
                    break;
                case "Oldest":
                    controls = controls.OrderBy(x => x.cadastro);
                    break;
                case "More":
                    controls = controls.OrderByDescending(x => x.quantity);
                    break;
                case "Less":
                    controls = controls.OrderBy(x => x.quantity);
                    break;
                case "Closer":
                    controls = controls.OrderBy(x => x.expiration);
                    break;
                case "Farther":
                    controls = controls.OrderByDescending(x => x.expiration);
                    break;
            }

            int index = 0;

            foreach (var control in controls.ToList())
            {
                flowLayoutPanel1.Controls.SetChildIndex(control, index++);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ordering = new OrderingWindow();
            ordering.Show();

            ordering.sortType += orderProducts;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var newSolicitation = new NewSolicitation();
            newSolicitation.Show();

            newSolicitation.FormClosed += (s, e) => ShowProducts();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Session.CurrentUser = null;
            Properties.Settings.Default.Remembered = -1;
            Properties.Settings.Default.Save();
            this.loginWindow.Show();
            this.Close();
        }
    }
}
