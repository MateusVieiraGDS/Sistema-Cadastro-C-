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
using TesteBancoMySQL.Forms;
using TesteBancoMySQL.SGBD;

namespace TesteBancoMySQL
{
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SGBDFileAcess sgbdAcess = SGBDFileAcess.loadChanges();
            if (sgbdAcess.Configured() == false)
            {
                MessageBox.Show(
                    $"Não foi encontrado um acesso configurado a um Banco de Dados",
                    "Configure seu Banco de Dados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            else
            {
                using (SGBDConnect conn = new SGBDConnect(sgbdAcess.ToString()))
                {
                    conn.OpenConnection();
                    if (conn.State != ConnectionState.Open)
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
                ReloadData();
            }
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            AddRegistro();
        }


        private void ReloadData() {
            var fa = SGBDFileAcess.loadChanges();
            if (fa.Configured() == false) return;

            dataGrid_reg.Columns.Clear();
            dataGrid_reg.Rows.Clear();

            using (SGBDConnect conn = new SGBDConnect(fa.ToString()))
            {
                conn.OpenConnection();
                var result = conn.ExecuteQuery("SELECT * FROM pessoa;");
                int nc = 0;
                foreach(var cinfo in result.ColumsInfos)
                {
                    dataGrid_reg.Columns.Add(cinfo.ColumName, cinfo.ColumName.ToUpper());
                    dataGrid_reg.Columns[nc].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    nc++;
                }

                foreach (var res in result)
                {
                    var values_temp = (from v in res.getValues() select v.Value.value).ToArray();
                    dataGrid_reg.Rows.Add(values_temp);
                }
            }
        }

        private void DeleteRegistro(int userID) {
            var fa = SGBDFileAcess.loadChanges();
            if (fa.Configured() == false) return;

            if(
            MessageBox.Show(
                $"Tem certeza que deseja escluir este registro do banco de dados ?",
                "Confirmação de Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error
            ) == DialogResult.No)
            return;

            using (SGBDConnect conn = new SGBDConnect(fa.ToString()))
            {
                conn.OpenConnection();
                var result = conn.ExecuteQuery("DELETE pessoa FROM pessoa WHERE pessoa.id = " + userID);
                int nc = 0;
                foreach (var cinfo in result.ColumsInfos)
                {
                    dataGrid_reg.Columns.Add(cinfo.ColumName, cinfo.ColumName.ToUpper());
                    dataGrid_reg.Columns[nc].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    nc++;
                }

                foreach (var res in result)
                {
                    var values_temp = (from v in res.getValues() select v.Value.value).ToArray();
                    dataGrid_reg.Rows.Add(values_temp);
                }
            }
            ReloadData();
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

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void adicionarConexãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddSGBDConfig newConn = new FormAddSGBDConfig();
            newConn.ShowDialog();
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (dataGrid_reg.SelectedCells.Count < 1) return;
            foreach(DataGridViewCell cell in dataGrid_reg.SelectedCells)
            {
                if(cell.OwningColumn.Name.ToLower() == "id")
                    DeleteRegistro(int.Parse(cell.Value.ToString()));
            }
            
        }
    }
}
