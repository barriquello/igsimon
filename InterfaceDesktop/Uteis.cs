using System;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
namespace InterfaceDesktop
{
    class Uteis
    {
        /// <summary>
        /// Algoritmo MD5
        /// </summary>
        /// <param name="Input">Texto a ser criptografado</param>
        /// <returns>Texto criptografado</returns>
        public static string getMD5(string Input)
        {
            MD5 md5 = MD5.Create();
            byte[] InputBytes = Encoding.ASCII.GetBytes(Input);
            byte[] md5Hash = md5.ComputeHash(InputBytes);
            StringBuilder SB = new System.Text.StringBuilder();
            for (int ii = 0; ii < md5Hash.Length; ii++)
                SB.Append(md5Hash[md5Hash.Length - ii - 1].ToString("X2"));
            return SB.ToString();
        }

        public static Int32 Time2Unix(DateTime Horario)
        {
            return (Int32)Horario.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public static DateTime Unix2time(Int32 Unix)
        {
            return new DateTime(1970, 1, 1).AddSeconds(Unix);
        }

    }

    class ComandoSQL
    {
 /*       public static SQLiteDataReader SELECT(string strSQL)
        {
            SQLiteConnection Con = new SQLiteConnection(Global.Conexao);
            Con.Open();
            return new SQLiteCommand(strSQL, Con).ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }//*/

        public static void INSERT(string strSQL)
        {
            using (SQLiteConnection Con = new SQLiteConnection(Global.Conexao))
            {
                Con.Open();
                using (
                SQLiteCommand SQLiteComando = new SQLiteCommand(strSQL, Con))
                {
                    SQLiteComando.ExecuteNonQuery();
                    Con.Close();
                }
            }
        }

        /// <summary>Criar uma tabela no banco de dados</summary>
        public static void CriarBancoDeDados()
        {
            // Verifica se o arquivo db.db existe

            string strConexao = Global.Conexao; //Properties.Settings.Default.ConnectionString;

            SQLiteConnection sqlConexao = new SQLiteConnection(strConexao);
            if (!(new FileInfo(Path.Combine(Application.StartupPath, Global.ArquivoDB)).Exists))
                SQLiteConnection.CreateFile(Path.Combine(Application.StartupPath, Global.ArquivoDB));
            sqlConexao.Open();
            SQLiteCommand SqlComando = sqlConexao.CreateCommand();
            // Cria a tabela de usuários
            SqlComando.CommandText = Global.strCriarTabelaUsers;
            SqlComando.ExecuteNonQuery();
            // Cria a tabela de configuração
            SqlComando.CommandText = Global.strCriarTabelaConfig;
            SqlComando.ExecuteNonQuery();
            // Cria a tabela de dados
            SqlComando.CommandText = Global.strCriarTabelaDados;
            SqlComando.ExecuteNonQuery();
            sqlConexao.Close();
        }
    }


}
