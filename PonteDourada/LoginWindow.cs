using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PonteDourada.Data;
using PonteDourada.Properties;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PonteDourada
{
    public partial class LoginWindow : Form
    {
        public LoginWindow()
        {
            InitializeComponent();
            this.loginButton.Click += async (sender, eventArgs) => await login(this.UserTextBox.Text, this.PassTextBox.Text);
            this.RegisterLabel.Click += (s, e) =>
            {
                var result = MessageBox.Show("Gostaria de criar uma nova conta?", null, buttons: MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    var OpenedRegisterWindow = new RegisterWindow();
                    OpenedRegisterWindow.Show();

                }
            };
            this.Shown += (s, e) =>
            {
                using (var db = new Sessao2Context())
                {
                    if (Settings.Default.Remembered != -1)
                    {
                        var usering = db.Usuarios.FirstOrDefault(x => x.Id == Settings.Default.Remembered);
                        {
                            if (usering.IdNavigation?.Fornecedor == null)
                            {
                                var request = new SolicitationWindow(this);
                                request.Show();
                                this.Hide();
                            }
                            else
                            {
                                var productWindow = new ProductWindow(this);
                                productWindow.Show();
                                this.Hide();
                            }
                        }
                    }
                }
            };
        }


            
           

        private async Task login(string username, string password)
        {
            if (this.UserTextBox.Text.Replace(" ", string.Empty) == string.Empty || this.PassTextBox.Text.Replace(" ", string.Empty) == string.Empty)
            {
                MessageBox.Show("Campos nao podem ser nulos.");
                return;
            }
            using (var db = new Sessao2Context())
            {

                var user = await db.Usuarios.FirstOrDefaultAsync(u => u.Login == $"{this.UserTextBox.Text}");
                if (user == null)
                {
                    MessageBox.Show("Usuario ou senha incorretos.");
                    return;
                }

                if (user.SenhaHash == this.PassTextBox.Text)
                {
                    Session.CurrentUser = user;

                    if (this.checkBox1.Checked)
                    {
                        Settings.Default.Remembered = user.Id;
                        Settings.Default.Save();
                    }

                    var result = MessageBox.Show("Logado.");
                    if (result == DialogResult.OK || result == DialogResult.None)
                    {
                        if (user.IdNavigation?.Fornecedor == null)
                        {
                            var request = new SolicitationWindow(this);
                            request.Show();
                            this.Hide();
                        }
                        else
                        {
                            var productWindow = new ProductWindow(this);
                            productWindow.Show();
                            this.Hide();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Usuario ou senha incorretos.");
                }
            }
        }

    }
}
