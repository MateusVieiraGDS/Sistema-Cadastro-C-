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
using TesteBancoMySQL.Exceptions;
using TesteBancoMySQL.SGBD;

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

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if(input_nome.Text == "" || input_email.Text == "" || input_telefone.Text == "")
            {
                MessageBox.Show(
                            $"Preencha todos os campos para continuar.",
                            "Dados Incompletos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                return;
            }
            Pessoa p = new Pessoa(input_nome.Text, input_email.Text, input_telefone.Text, input_datePickerNasc.Value);

            try
            {
                InsertRegistro(p);
                this.DialogResult = DialogResult.OK;
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
            var fa = SGBDFileAcess.loadChanges();
            if (fa.Configured() == false) return;

            using (SGBDConnect conn = new SGBDConnect(fa.ToString()))
            {
                conn.OpenConnection();
                if (conn.State != ConnectionState.Open)
                    throw new SQLConnectionException("Não foi possivel abrir uma conexão estável com o Banco de Dados.");

                try
                {
                    var result = conn.ExecuteQuery($"SELECT id FROM pessoa WHERE email = '{p.Email}' OR telefone = '{p.Telefone}'");
                    if (result.RowsCount > 0)
                        throw new SQLDuplicateException("Usuário com dados duplicados [EMAIL/TELEFONE]");

                    result = conn.ExecuteQuery($"INSERT INTO pessoa (nome, email, telefone, nascimento) VALUES ('{p.Nome}', '{p.Email}', '{p.Telefone}', '{p.Nascimento.ToString("yyyy-MM-dd")}');");                    
                }
                catch (MySqlException me)
                {
                    throw new SQLDataBaseQueryException(me.Message);
                }
            }
        }
    }
}
