using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TesteBancoMySQL.Forms;
using TesteBancoMySQL.SGBD;
using TesteBancoMySQL.Util;

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
            var result = PessoaManagerDB.getAllPessoas();
            if (result == null) return;

            dataGrid_reg.Columns.Clear();
            dataGrid_reg.Rows.Clear();

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

        private void DeleteRegistro(int userID) {
            var result = PessoaManagerDB.RemovePessoa(userID);
            if (result == false) return;

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

        private void apagarConexãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (
            MessageBox.Show(
                $"Tem certeza que deseja excluir as configurações de conexão ?",
                "Confirmação de Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error
            ) == DialogResult.No)
                return;
            SGBDFileAcess.DeleteChanges();
        }

        private void repositórioGITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://github.com/MateusVieiraGDS/Sistema-Cadastro-C-");
        }

        private void creditosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCreditos fc = new FormCreditos();
            fc.ShowDialog();
        }
    }
}
