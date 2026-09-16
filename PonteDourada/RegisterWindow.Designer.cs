namespace PonteDourada
{
    partial class RegisterWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            button1 = new Button();
            button2 = new Button();
            maskedTextBox2 = new MaskedTextBox();
            textBox4 = new MaskedTextBox();
            textBox2 = new TextBox();
            dateTimePicker1 = new DateTimePicker();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.DisplayMember = "Fornecedor;Cliente";
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Fornecedor", "Cliente" });
            comboBox1.Location = new Point(133, 31);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(223, 23);
            comboBox1.TabIndex = 1;
            comboBox1.ValueMember = "Fornecedor;Cliente";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(133, 70);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(223, 23);
            textBox1.TabIndex = 1;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(133, 224);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(223, 23);
            textBox5.TabIndex = 1;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(133, 262);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(223, 23);
            textBox6.TabIndex = 1;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(133, 301);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(223, 23);
            textBox7.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(89, 34);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 2;
            label1.Text = "Perfil";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(84, 73);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 2;
            label2.Text = "Nome";
            // 
            // label3
            // 
            label3.Location = new Point(54, 111);
            label3.Name = "label3";
            label3.Size = new Size(73, 40);
            label3.TabIndex = 2;
            label3.Text = "Data Nascimento";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(89, 151);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 2;
            label4.Text = "CPF";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(72, 189);
            label5.Name = "label5";
            label5.Size = new Size(51, 15);
            label5.TabIndex = 2;
            label5.Text = "Telefone";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(76, 227);
            label6.Name = "label6";
            label6.Size = new Size(47, 15);
            label6.TabIndex = 2;
            label6.Text = "Usuario";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(84, 265);
            label7.Name = "label7";
            label7.Size = new Size(39, 15);
            label7.TabIndex = 2;
            label7.Text = "Senha";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(27, 304);
            label8.Name = "label8";
            label8.Size = new Size(96, 15);
            label8.TabIndex = 2;
            label8.Text = "Confirmar Senha";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Location = new Point(183, 393);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Cancelar";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Popup;
            button2.Location = new Point(290, 393);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "Salvar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button1_Click;
            // 
            // maskedTextBox2
            // 
            maskedTextBox2.Location = new Point(133, 148);
            maskedTextBox2.Mask = "000/000/000-00";
            maskedTextBox2.Name = "maskedTextBox2";
            maskedTextBox2.Size = new Size(223, 23);
            maskedTextBox2.TabIndex = 5;
            maskedTextBox2.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(133, 186);
            textBox4.Mask = "(00) 00000-0000";
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(223, 23);
            textBox4.TabIndex = 5;
            textBox4.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(133, 111);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(223, 23);
            textBox2.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(133, 111);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(223, 23);
            dateTimePicker1.TabIndex = 7;
            // 
            // RegisterWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 453);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox2);
            Controls.Add(textBox4);
            Controls.Add(maskedTextBox2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Name = "RegisterWindow";
            Text = "Register";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private TextBox textBox1;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Button button1;
        private Button button2;
        private MaskedTextBox maskedTextBox2;
        private MaskedTextBox textBox4;
        private TextBox textBox2;
        private DateTimePicker dateTimePicker1;
    }
}