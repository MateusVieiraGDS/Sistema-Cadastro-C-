using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteBancoMySQL.SGBD;

namespace TesteBancoMySQL
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();         
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            
        }


        private void ReloadData() {
            
        }

        private void DeleteRegistro(Pessoa p) {
        
        }
        private void AddRegistro() {
            FormNewReg newReg = new FormNewReg();
            if (newReg.ShowDialog() == DialogResult.OK)
                ReloadData();
        }

        private void StopSGBDConnection(object sender, FormClosingEventArgs e)
        {
            //SGBDConnect.CloseInstance();
        }
    }
}
