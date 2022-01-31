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

namespace TesteBancoMySQL.Forms
{
    public partial class FormAddSGBDConfig : Form
    {
        public FormAddSGBDConfig()
        {
            InitializeComponent();
        }

        private void FormAddSGBDConfig_Load(object sender, EventArgs e)
        {
            SGBDFileAcess file = SGBDFileAcess.loadChanges();
            if (file.Configured())
            {
                input_host.Text = file.HostName;
                input_database.Text = file.DatabaseName;
                input_user.Text = file.UserName;
                input_password.Text = file.Password;
            }
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            SGBDFileAcess fa = new SGBDFileAcess(input_host.Text, input_database.Text, input_user.Text, input_password.Text);
            if(fa.Configured() == false)
            {
                MessageBox.Show(
                    $"Preencha todos os campos obrigatórios!",
                    "Dados Incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            using (SGBDConnect conn = new SGBDConnect(fa.ToString()))
            {
                conn.OpenConnection();
                if(conn.State == ConnectionState.Open)
                {
                    MessageBox.Show(
                        $"Conseguimos conectar com sucesso ao seu banco de dados",
                        "Conexão com Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }
                else
                {
                    MessageBox.Show(
                        $"Não conseguimos conectar com sucesso ao seu banco de dados",
                        "Conexão Fracassada.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SGBDFileAcess fa = new SGBDFileAcess(input_host.Text, input_database.Text, input_user.Text, input_password.Text);
            if (fa.Configured() == false)
            {
                MessageBox.Show(
                    $"Preencha todos os campos obrigatórios!",
                    "Dados Incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
            using (SGBDConnect conn = new SGBDConnect(fa.ToString()))
            {
                conn.OpenConnection();
                if (conn.State == ConnectionState.Open)
                {
                    MessageBox.Show(
                        $"Conseguimos conectar com sucesso ao seu banco de dados e os dados para conexão foram salvos.",
                        "Tudo Pronto",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    fa.saveChanges();
                    return;
                }
                else
                {
                    MessageBox.Show(
                        $"Não conseguimos conectar com sucesso ao seu banco de dados",
                        "Conexão Fracassada.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    return;
                }
            }
        }
    }
}
