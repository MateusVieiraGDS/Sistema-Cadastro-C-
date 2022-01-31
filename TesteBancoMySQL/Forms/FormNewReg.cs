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

            try
            {
                InsertRegistro(p);
            }
            catch (SQLConnectionException ce)
            {
                MessageBox.Show(
                    $"Erro ao conectar ao Banco de Dados, tente novamente !\n\n[{ce.Message}]",
                    "ERRO DE CONEXÃO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (SQLDuplicateException de)
            {
                MessageBox.Show(
                    $"Este usuário já existe na base de dados. Modifique os dados e tente novamente.\n\n[{de.Message}]",
                    "Usuário Duplicado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (SQLDataBaseQueryException qe) {
                MessageBox.Show(
                    $"Houve um erro inesperado na execução da consulta.\n\n[{qe.Message}]",
                    "Erro de Consulta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void InsertRegistro(Pessoa p) {
            
        }

    }
}
