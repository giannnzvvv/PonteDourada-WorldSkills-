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

        public void ShowProducts(string orderType = null)
        {
            using (var db = new Sessao2Context())
            {
                foreach (var solicitacao in db.ProdutoSolicitacaos.Include(y => y.Solicitcao).Include(y => y.Produto).ThenInclude(y => y.Tipo))
                {
                    var medCard = new MedicationCard();
                    var product = solicitacao.Produto;

                    medCard.expiration = solicitacao.Solicitcao.Validade;
                    medCard.Title = product.Nome;
                    medCard.quantity = solicitacao.Quantidade;
                    medCard.imageOrWhatever = Image.FromFile(Path.Combine("C:\\Users\\antol\\Downloads\\DataFiles\\TiposProdutos", $"{solicitacao.Produto.Tipo.Nome}.png"));
                    medCard.Desc = solicitacao.Solicitcao.Descricao;
                    medCard.price += (decimal)(solicitacao.Produto.Valor * solicitacao.Quantidade ?? 0);
                    var expression = medCard.expiration.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber;


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

                    this.flowLayoutPanel1.Controls.Add(medCard);
                }

            }
        }

        private void searchProducts()
        {
            var controls = this.flowLayoutPanel1.Controls.OfType<MedicationCard>();
            foreach (var control in controls)
            {
                control.Visible = control.Title.Contains(this.textBox1.Text, StringComparison.OrdinalIgnoreCase);
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
                    controls = controls.OrderByDescending(x => x.expiration);
                    break;
                case "Oldest":
                    controls = controls.OrderBy(x => x.expiration);
                    break;
                case "More":
                    controls = controls.OrderByDescending(x => x.quantity);
                    break;
                case "Less":
                    controls = controls.OrderBy(x => x.quantity);
                    break;
                case "Closer":
                    controls = controls.OrderByDescending(x => (x.expiration.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber) <= 7);
                    break;
                case "Farther":
                    controls = controls.OrderBy(x => (x.expiration.DayNumber - DateOnly.FromDateTime(DateTime.Today).DayNumber) <= 7);
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
