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
    public partial class NewSolicitation : Form
    {
        public NewSolicitation()
        {
            InitializeComponent();

            using (var db = new Sessao2Context())
            {
                this.comboBox1.DataSource = db.Pessoas
                    .Where(x => x.Fornecedor != null)
                    .Select(x => x.Nome)
                    .ToList();


            }
        }


    }
}
