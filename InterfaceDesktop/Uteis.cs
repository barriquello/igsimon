using System;
using System.Data.SQLite; // Banco de dados
using System.IO; // Sistema de arquivos
using System.Security.Cryptography; // sistema de criptografia
using System.Text; //Codificação de texto
using System.Windows.Forms; //Formulários e controles
namespace InterfaceDesktop
{
    class Uteis
    {
        /// <summary>Algoritmo MD5</summary>
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
		/// <summary>Converte Horário em horário Unix</summary>
        public static Int32 Time2Unix(DateTime Horario)
        {
            return (Int32)Horario.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
		/// <summary>Converte horário Unix em horário</summary>
        public static DateTime Unix2time(Int32 Unix)
        {
            return new DateTime(1970, 1, 1).AddSeconds(Unix);
        }

    }
	// Classe para manipular o banco de dados SQLite
    class ComandoSQL
    {
		// Comnado SQL INSERT
        public static void INSERT(string strSQL)
        {
			// Abre uma conexão com o banco de dados
            using (SQLiteConnection Con = new SQLiteConnection(Global.Conexao))
            {
                Con.Open();
				//Cria um comando virtual 
                using (SQLiteCommand SQLiteComando = new SQLiteCommand(strSQL, Con))
                {
					// Executa o comando SQL espeficidado
                    SQLiteComando.ExecuteNonQuery();
                }
				// Encerra a conexão com o banco de dados
				Con.Close();
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
