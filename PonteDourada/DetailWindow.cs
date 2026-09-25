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
    public partial class DetailWindow : Form
    {
        private Dictionary<string, bool> lastSortedAscending = new Dictionary<string, bool>();
        private List<ProductCards> data = new List<ProductCards>();
        public DetailWindow(Dictionary<ProductCards, int> selectedProducts)
        {
            InitializeComponent();
            foreach (KeyValuePair<ProductCards, int> lala in selectedProducts)
            {
                lala.Key.quantity = lala.Value;
                lala.Key.subtotal = (double)lala.Key.price * lala.Key.quantity;
                this.data.Add(lala.Key);
            }
            this.dataGridView1.AutoGenerateColumns = false;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "title",
                DataPropertyName = "title",
                HeaderText = "Nome",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "exp",
                DataPropertyName = "exp",
                HeaderText = "Validade",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy"
                }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "quantity",
                DataPropertyName = "quantity",
                HeaderText = "Quantidade",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "price",
                DataPropertyName = "price",
                HeaderText = "Preço",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "F2"
                }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "discount",
                DataPropertyName = "discount",
                HeaderText = "Desconto",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "F2"
                }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fornecedor",
                DataPropertyName = "fornecedor",
                HeaderText = "Fornecedor",
                SortMode = DataGridViewColumnSortMode.Programmatic
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "subtotal",
                DataPropertyName = "subtotal",
                HeaderText = "Subtotal",
                SortMode = DataGridViewColumnSortMode.Programmatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "F2"
                }
            });

            this.dataGridView1.DataSource = this.data;

            foreach (DataGridViewColumn column in this.dataGridView1.Columns)
            {
                lastSortedAscending[column.Name] = true;
            }

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

        
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var column = this.dataGridView1.Columns[e.ColumnIndex];
            var newData = column.Name switch
            {
                "title" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.Name).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.Name).ToList(),
                "price" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.price).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.price).ToList(),
                "exp" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.exp).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.exp).ToList(),
                "quantity" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.quantity).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.quantity).ToList(),
                "discount" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.discount).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.discount).ToList(),
                "fornecedor" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.fornecedor).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.fornecedor).ToList(),
                "subtotal" => lastSortedAscending[column.Name]
                    ? this.dataGridView1.DataSource = this.data.OrderBy(d => d.subtotal).ToList()
                    : this.dataGridView1.DataSource = this.data.OrderByDescending(d => d.subtotal).ToList(),
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

