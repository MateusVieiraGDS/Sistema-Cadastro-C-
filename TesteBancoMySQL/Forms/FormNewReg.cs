using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteBancoMySQL.Exceptions;

namespace TesteBancoMySQL
{
    public partial class FormNewReg : Form
    {
        public FormNewReg()
        {
            InitializeComponent();
        }

        private void FormNewReg_Load(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Pessoa p = new Pessoa(input_nome.Text, input_email.Text, input_telefone.Text, input_datePickerNasc.Value);

            
        }

        private void InsertRegistro(Pessoa p) {
            
        }

    }
}
