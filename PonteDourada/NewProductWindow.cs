using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using PonteDourada.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PonteDourada
{
    public partial class NewProductWindow : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEditing { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Produto ChosenProduct { get; set; }

        public NewProductWindow()
        {
            InitializeComponent();

            using (var db = new Sessao2Context())
            {
                this.comboBox1.DisplayMember = "Nome";
                this.comboBox1.ValueMember = "Id";
                this.comboBox1.DataSource = db.TiposProdutos.ToList();
                this.comboBox1.SelectedValue = 1;
            }
            this.IsEditing = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (this.openFileDialog1)
            {
                this.openFileDialog1.Filter = "PNG Image (*.png)|*.png|All Files (*.*)|*.*";
                this.openFileDialog1.Title = "Selecione uma imagem";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;

                    if (filePath != string.Empty && filePath != null)
                    {
                        if (File.Exists(filePath))
                        {
                            this.pictureBox1.ImageLocation += filePath;
                            File.Copy(filePath, $"{AppContext.BaseDirectory}\\DataFiles\\Produtos\\{this.ChosenProduct.Id}.png", true);
                        }
                    }
                }
            }
        }

        public void setCells(InformationClassForMyOwnProjectIgWtv info)
        {
            var nome = info.N_ame;
            this.textBox1.Text = nome;
            var descricao = info.Desc;
            this.textBox2.Text = descricao;
            var validade = info.Exp;
            this.dateTimePicker1.Value = validade.ToDateTime(TimeOnly.MinValue);
            var tipo = info.Type;
            this.comboBox1.SelectedValue = tipo;
            var quantidade = info.Quantity;
            this.numericUpDown2.Value = (decimal)quantidade;
            var valor = info.Price;
            this.numericUpDown2.Value = (decimal)valor;
      
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

                if (this.IsEditing) {
                    var j = db.Produtos.FirstOrDefault(p => p.Id == this.ChosenProduct.Id);

                    j.TipoId = (int)comboBox1.SelectedValue;

                    j.Nome = this.textBox1.Text;
                    j.Descricao = this.textBox2.Text;

                    j.Estoque = (int)this.numericUpDown1.Value;
                    j.Valor = (double)this.numericUpDown2.Value;

                    j.Validade = DateOnly.FromDateTime(this.dateTimePicker1.Value);
                    j.DataHoraCadastro = DateTime.Now;

                    j.Fornecedor = db.Fornecedors.FirstOrDefault(f => f.Id == Session.CurrentUser.Id);
                    j.FornecedorId = Session.CurrentUser.Id;

                    MessageBox.Show("Produto editado com sucesso!");
                    db.SaveChanges();
                    this.Close();
                    return;
                }

                var product = new Produto();
                product.TipoId = (int)comboBox1.SelectedValue;

                product.Nome = this.textBox1.Text;
                product.Descricao = this.textBox2.Text; 

                product.Estoque = (int)this.numericUpDown1.Value;
                product.Valor = (double)this.numericUpDown2.Value;

                product.Validade = DateOnly.FromDateTime(this.dateTimePicker1.Value);
                product.DataHoraCadastro = DateTime.Now;

                product.Fornecedor = db.Fornecedors.FirstOrDefault(f => f.Id == Session.CurrentUser.Id);
                product.FornecedorId = Session.CurrentUser.Id;


                db.Produtos.Add(product);
                db.SaveChanges();

                MessageBox.Show("Produto cadastrado com sucesso!");
                this.Close();
            }
        }
    }
}
