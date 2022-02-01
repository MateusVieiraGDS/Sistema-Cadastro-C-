using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using TesteBancoMySQL.Exceptions;
using TesteBancoMySQL.SGBD;

namespace TesteBancoMySQL.Util
{
    static class PessoaManagerDB
    {
        public static bool RemovePessoa(int pessoaID)
        {
            var fa = SGBDFileAcess.loadChanges();
            if (fa.Configured() == false) return false;

            if (
            MessageBox.Show(
                $"Tem certeza que deseja excluir este registro do banco de dados ?",
                "Confirmação de Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error
            ) == DialogResult.No)
                return false;

            using (SGBDConnect conn = new SGBDConnect(fa.ToString()))
            {
                conn.OpenConnection();
                var result = conn.ExecuteQueryPrepare("DELETE pessoa FROM pessoa WHERE pessoa.id = ?;", pessoaID);
                return true;
            }
        }

        public static bool InsertPessoa(Pessoa p)
        {
            var fa = SGBDFileAcess.loadChanges();
            if (fa.Configured() == false) return false;

            using (SGBDConnect conn = new SGBDConnect(fa.ToString()))
            {
                conn.OpenConnection();
                if (conn.State != ConnectionState.Open)
                    throw new SQLConnectionException("Não foi possivel abrir uma conexão estável com o Banco de Dados.");

                try
                {
                    var result = conn.ExecuteQueryPrepare($"SELECT id FROM pessoa WHERE email = ? OR telefone = ?", p.Email, p.Telefone);
                    if (result.RowsCount > 0)
                        throw new SQLDuplicateException("Usuário com dados duplicados [EMAIL/TELEFONE]");

                    result = conn.ExecuteQueryPrepare($"INSERT INTO pessoa (nome, email, telefone, nascimento) VALUES (?, ?, ?, ?);", p.Nome, p.Email, p.Telefone, p.Nascimento.ToString("yyyy-MM-dd"));
                    return true;
                }
                catch (MySqlException me)
                {
                    throw new SQLDataBaseQueryException(me.Message);
                }
            }
        }

        public static SGBDResult getAllPessoas()
        {
            var fa = SGBDFileAcess.loadChanges();
            if (fa.Configured() == false) return null;            

            using (SGBDConnect conn = new SGBDConnect(fa.ToString()))
            {
                conn.OpenConnection();
                var result = conn.ExecuteQuery("SELECT * FROM pessoa;");
                return result;
            }
        }
    }
}
