namespace PonteDourada
{
    partial class OrderingWindow
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button5 = new Button();
            button6 = new Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(25, 86);
            button1.Name = "button1";
            button1.Size = new Size(318, 48);
            button1.TabIndex = 0;
            button1.Text = "Mais Recente";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(25, 86);
            button2.Name = "button2";
            button2.Size = new Size(318, 48);
            button2.TabIndex = 0;
            button2.Text = "Mais Antiga";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(25, 86);
            button3.Name = "button3";
            button3.Size = new Size(318, 48);
            button3.TabIndex = 0;
            button3.Text = "Mais Produtos";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(25, 86);
            button4.Name = "button4";
            button4.Size = new Size(318, 48);
            button4.TabIndex = 0;
            button4.Text = "Menos Produtos";
            button4.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(button5);
            flowLayoutPanel1.Controls.Add(button6);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(12, 18);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(354, 232);
            flowLayoutPanel1.TabIndex = 1;
            flowLayoutPanel1.WrapContents = false;
            // 
            // button5
            // 
            button5.Location = new Point(3, 3);
            button5.Name = "button5";
            button5.Size = new Size(318, 48);
            button5.TabIndex = 0;
            button5.Text = "Mais perto do vencimento";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(3, 57);
            button6.Name = "button6";
            button6.Size = new Size(318, 48);
            button6.TabIndex = 0;
            button6.Text = "Mais distante do vencimento";
            button6.UseVisualStyleBackColor = true;
            // 
            // OrderingWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 270);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "OrderingWindow";
            Text = "Form1";
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button5;
        private Button button6;
    }
}