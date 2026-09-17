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
    public partial class ProductWindow : Form
    {
        private readonly LoginWindow loginWindow;

        private Dictionary<string, bool> lastSortedAscending = new Dictionary<string, bool>();
        private DataGridViewCell selected;

        private List<DataGridRowForMyOwnProjectWhatever> data;
        public ProductWindow(LoginWindow loginWindow)
        {
            InitializeComponent();

            using (var db = new Sessao2Context())
            {
                var datas = db.Produtos.Select(p => new DataGridRowForMyOwnProjectWhatever
                {
                    Nome = p.Nome,
                    Tipo = p.Tipo.Nome,
                    Validade = p.Validade,
                    Cadastro = p.DataHoraCadastro
                }).ToList();

                this.data = datas;
                this.dataGridView1.DataSource = datas;

                this.dataGridView1.Columns["Nome"].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.dataGridView1.Columns["Tipo"].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.dataGridView1.Columns["Validade"].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.dataGridView1.Columns["Cadastro"].SortMode = DataGridViewColumnSortMode.Programmatic;

                foreach (DataGridViewColumn column in this.dataGridView1.Columns)
                {
                    lastSortedAscending[column.Name] = true;
                }
            }

            this.loginWindow = loginWindow;

            // debug for datagridview1 columns or wtv
            //this.Shown += (s, e) =>
            //{
            //    foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            //    {
            //        MessageBox.Show(column.SortMode.ToString());
            //    }
            //};
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
            // yes i know im a dumbass and i couldve just added editProductInDataBase() as the click event function but no im against good code.
            editProductInDataBase();
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

            newProduct.FormClosing += (s, args) =>
            {
                using (var db = new Sessao2Context())
                {
                    var datas = db.Produtos.Select(p => new DataGridRowForMyOwnProjectWhatever
                    {
                        Nome = p.Nome,
                        Tipo = p.Tipo.Nome,
                        Validade = p.Validade,
                        Cadastro = p.DataHoraCadastro
                    }).ToList();
                    this.dataGridView1.DataSource = datas;
                }
            };
        }

        private void editProductInDataBase()
        {
            if (this.dataGridView1.SelectedCells[0].OwningRow == null)
            {
                MessageBox.Show("Nenhum produto selecionado, por favor selecione um produto na lista");
            } else
            {
                var selected = this.dataGridView1.SelectedCells[0].OwningRow;

                NewProductWindow newProduct = new NewProductWindow();

                newProduct.Show();
                newProduct.IsEditing = true;

                using (var db = new Sessao2Context())
                {
                    var product = db.Produtos.Include(x => x.Fornecedor)
                        .FirstOrDefault(p => p.Nome == selected.Cells["Nome"].Value.ToString());
                    newProduct.ChosenProduct = product;

                    if (product != null)
                    {

                        var info = new InformationClassForMyOwnProjectIgWtv
                        {
                            N_ame = product.Nome,
                            Type = product.TipoId,
                            BasicallyOwnerId = product.FornecedorId,
                            Desc = product.Descricao,
                            Exp = product.Validade,
                            Price = product.Valor,
                            Quantity = product.Estoque
                        };

                        newProduct.setCells(info);
                    } else
                    {
                        MessageBox.Show("Produto não encontrado no banco de dados, por favor tente novamente");
                    }
                }

                newProduct.FormClosed += (s, e) =>
                {
                    using (var db = new Sessao2Context())
                    {
                        var datas = db.Produtos.Select(p => new DataGridRowForMyOwnProjectWhatever
                        {
                            Nome = p.Nome,
                            Tipo = p.Tipo.Nome,
                            Validade = p.Validade,
                            Cadastro = p.DataHoraCadastro
                        }).ToList();
                        this.dataGridView1.DataSource = datas;
                    }
                };
            }

        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var column = this.dataGridView1.Columns[e.ColumnIndex];
            var newData = column.Name switch
            {
                "Nome" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.Nome).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.Nome).ToList(),
                "Tipo" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.Tipo).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.Tipo).ToList(),
                "Validade" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.Validade).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.Validade).ToList(),
                "Cadastro" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.Cadastro).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.Cadastro).ToList(),
                _ => this.data,
            };

            this.dataGridView1.DataSource = newData;
            column = this.dataGridView1.Columns[e.ColumnIndex];
            this.lastSortedAscending[column.Name] = !this.lastSortedAscending[column.Name];
            column.SortMode = DataGridViewColumnSortMode.Programmatic;
            column.HeaderCell.SortGlyphDirection = this.lastSortedAscending[column.Name] ? SortOrder.Ascending : SortOrder.Descending;
        }
    }
}

