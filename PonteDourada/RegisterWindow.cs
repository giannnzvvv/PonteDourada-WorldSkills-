using Microsoft.IdentityModel.Tokens;
using PonteDourada;
using PonteDourada.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PonteDourada
{
    public partial class RegisterWindow : Form
    {
        public RegisterWindow()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 1;
            this.button1.Click += (s, e) => this.Close();
            this.comboBox1.SelectedIndexChanged += (s, e) => comboBox1Changed();
            comboBox1Changed();
        }

        private void comboBox1Changed()
        {
            if (this.comboBox1.Text == "Fornecedor")
            {
                this.textBox2.Visible = true;
                this.dateTimePicker1.Visible = false;
                this.maskedTextBox2.Mask = @"00\.000\.000\/0000-00";
                this.label3.Text = "Razao Social";
                this.textBox2.Text = string.Empty;
                this.label4.Text = "CNPJ";
            }
            else if (this.comboBox1.Text == string.Empty)
            {
                this.comboBox1.SelectedIndex = 1;
            }
            else
            {
                this.label3.Text = "Data Nascimento";
                this.textBox2.Text = "1";
                this.textBox2.Visible = false;
                this.dateTimePicker1.Visible = true;
                this.maskedTextBox2.Mask = "000/000/000-00";
                this.label4.Text = "CPF";

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            bool result = realizeFieldChecks();

            if (result)
            {
                createUserOnDatabase();
            }

        }

        private bool realizeFieldChecks()
        {
            if (this.comboBox1.Text == "Fornecedor")
            {
                if (this.maskedTextBox2.Text.Length is not 14)
                {
                    MessageBox.Show("Campo de CNPJ Invalido.");
                    return false;
                }

            }
            else
            {
                if (this.maskedTextBox2.Text.Length is not 11)
                {
                    MessageBox.Show("Campo de CPF invalido.");
                    return false;
                }
                if (!(this.dateTimePicker1.Value.AddYears(18) <= DateTime.Today))
                {
                    MessageBox.Show("Voce tem menos de 18 anos.");
                    return false;
                }

                if (!(this.dateTimePicker1.Value.AddYears(130) > DateTime.Today) || !((this.dateTimePicker1.Value - DateTime.Today).Days < 3285))
                {
                    MessageBox.Show("Por favor, escolha uma data de nascimento valida.");
                    return false;
                }
            }
            


            bool AnyNull = false;

            for (int i = 1; i <= 8; i++)
            {
                string name = "textBox" + i;

                Control[] foundControls = this.Controls.Find(name, true);


                if (foundControls.Length > 0)
                {

                    if (string.IsNullOrWhiteSpace(foundControls[0].Text))
                    {
                        AnyNull = true;
                    }
                }
            }

            if (AnyNull) { MessageBox.Show("Nenhum campo pode ser nulo."); return false; }

            if (this.textBox6.Text != this.textBox7.Text)
            {
                MessageBox.Show("Confirmacao de senha incorreta, os campos nao sao iguais.");
                return false;
            }

            return true;
        }
        private void createUserOnDatabase()
        {
            using (var db = new Sessao2Context())
            {
                string type = this.comboBox1.Text;
                string name = this.textBox1.Text;
                string username = this.textBox5.Text;
                string password = this.textBox6.Text;
                string telephoneNumber = this.textBox4.Text;
                string cpf_cnpj = this.maskedTextBox2.Text;

                var pessoa = new Pessoa();
                pessoa.Nome = name;
                pessoa.Telefone = telephoneNumber;
                db.Pessoas.Add(pessoa);

                var usuario = new Usuario();
                usuario.SenhaHash = password;
                usuario.Login = $"{username}@email.com";
                usuario.IdNavigation = pessoa;
                db.Usuarios.Add(usuario);

                if (type == "Fornecedor") { 
                    var fornecedor = new Fornecedor();

                    fornecedor.Cnpj = cpf_cnpj;
                    fornecedor.RazaoSocial = this.textBox2.Text;
                    fornecedor.IdNavigation = pessoa;
                    db.Fornecedors.Add(fornecedor);
                } else
                {
                    var cliente = new Cliente();
                    cliente.IdNavigation = pessoa;
                    cliente.Cpf = cpf_cnpj;
                    cliente.DataNascimento = DateOnly.FromDateTime(this.dateTimePicker1.Value);
                    db.Clientes.Add(cliente);
                }


                var message = MessageBox.Show("Usuario criado com sucesso!");

                db.SaveChanges();

                if (message == DialogResult.OK)
                {
                    this.Close();
                }
            }
        }
    }
}
