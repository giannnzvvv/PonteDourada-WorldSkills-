namespace PonteDourada
{
    partial class LoginWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
            pictureBox1 = new PictureBox();
            UserLabel = new Label();
            PassLabel = new Label();
            PassTextBox = new TextBox();
            RegisterLabel = new LinkLabel();
            loginButton = new Button();
            UserTextBox = new TextBox();
            checkBox1 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(219, 22);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(386, 121);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // UserLabel
            // 
            UserLabel.AutoSize = true;
            UserLabel.Location = new Point(166, 236);
            UserLabel.Name = "UserLabel";
            UserLabel.Size = new Size(47, 15);
            UserLabel.TabIndex = 3;
            UserLabel.Text = "Usuario";
            // 
            // PassLabel
            // 
            PassLabel.AutoSize = true;
            PassLabel.Location = new Point(166, 281);
            PassLabel.Name = "PassLabel";
            PassLabel.Size = new Size(39, 15);
            PassLabel.TabIndex = 3;
            PassLabel.Text = "Senha";
            // 
            // PassTextBox
            // 
            PassTextBox.Location = new Point(219, 278);
            PassTextBox.Name = "PassTextBox";
            PassTextBox.Size = new Size(386, 23);
            PassTextBox.TabIndex = 1;
            PassTextBox.UseSystemPasswordChar = true;
            // 
            // RegisterLabel
            // 
            RegisterLabel.AutoSize = true;
            RegisterLabel.Location = new Point(349, 394);
            RegisterLabel.Name = "RegisterLabel";
            RegisterLabel.Size = new Size(69, 15);
            RegisterLabel.TabIndex = 4;
            RegisterLabel.TabStop = true;
            RegisterLabel.Text = "Cadastre-se";
            // 
            // loginButton
            // 
            loginButton.FlatStyle = FlatStyle.Popup;
            loginButton.Location = new Point(664, 355);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(75, 23);
            loginButton.TabIndex = 5;
            loginButton.Text = "Entrar";
            loginButton.UseVisualStyleBackColor = true;
            // 
            // UserTextBox
            // 
            UserTextBox.Location = new Point(219, 233);
            UserTextBox.Name = "UserTextBox";
            UserTextBox.Size = new Size(386, 23);
            UserTextBox.TabIndex = 1;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(219, 318);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(114, 19);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "Lembrar-se mim";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // LoginWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox1);
            Controls.Add(loginButton);
            Controls.Add(RegisterLabel);
            Controls.Add(PassLabel);
            Controls.Add(UserLabel);
            Controls.Add(PassTextBox);
            Controls.Add(UserTextBox);
            Controls.Add(pictureBox1);
            Name = "LoginWindow";
            Text = "OrderingWindowDefinition";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label UserLabel;
        private Label PassLabel;
        private TextBox PassTextBox;
        private LinkLabel RegisterLabel;
        private Button loginButton;
        private TextBox UserTextBox;
        private CheckBox checkBox1;
    }
}
