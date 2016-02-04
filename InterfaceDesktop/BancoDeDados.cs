using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
namespace InterfaceDesktop
{
    class BancoDeDados
    {
        public static string ComandoCriarTabelas = "CREATE TABLE IF NOT EXISTS [Usuarios] ([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [Usuario] TEXT NULL, [Senha] TEXT NULL, [Permissao] INTEGER NULL)";
        public static string ComandoInserirUsuario = "INSERT INTO Usuarios (Usuario, Senha, Permissao) VALUES (@Usuario, @Senha, @Permissao)";
        public static string ComandoSelect = "";
        public static string ConnectionString = "Data Source=|DataDirectory|\\Usuarios.db;Pooling=True;Synchronous=Off;journal mode=Wal";

        public static void CriarTabela()
        {
            // Banco de dados: ID, Usuário, Senha, Permissoes
            if (!(new FileInfo(Path.Combine(Application.StartupPath, "Usuarios.db")).Exists))
            {
                SQLiteConnection sqlConexao = new SQLiteConnection(ConnectionString);
                SQLiteConnection.CreateFile(Path.Combine(Application.StartupPath, "Usuarios.db"));
                sqlConexao.Open();
                SQLiteCommand SqlComando = sqlConexao.CreateCommand();
                SqlComando.CommandText = ComandoCriarTabelas;
                SqlComando.ExecuteNonQuery();
                sqlConexao.Close();
            }
            else
            {
                using (SQLiteConnection Con = new SQLiteConnection(ConnectionString))
                {
                    try
                    {
                        Con.Open();
                        using (SQLiteCommand Comando = new SQLiteCommand(ComandoCriarTabelas, Con))
                            Comando.ExecuteNonQuery();
                        Con.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao criar o banco de dados, o programa será encerrado agora");
                        Environment.Exit(-1);
                    }
                }
            }
        }

        public static void InserirUsuario(string Usuario, string Senha, int Permissao)
        {
            SQLiteConnection Conexao = new SQLiteConnection(ConnectionString);
            using (SQLiteCommand Comando = new SQLiteCommand(ComandoInserirUsuario, Conexao))
            {
                Conexao.Open();
                Comando.Parameters.AddWithValue("Usuario", Usuario);
                Comando.Parameters.AddWithValue("Senha", Senha);
                Comando.Parameters.AddWithValue("Permissao", Permissao);
                Comando.ExecuteNonQuery();
                Conexao.Close();
            }
        }

        public static string[] ListaDeUsuarios()
        {
            List<string> Usuarios = new List<string>();
            SQLiteConnection Conexao = new SQLiteConnection(ConnectionString);
            using (SQLiteCommand Comando = new SQLiteCommand(ComandoSelect, Conexao))
            {
                Conexao.Open();
                SQLiteDataReader Reader = Comando.ExecuteReader();
                while (Reader.Read())
                {
                    Usuarios.Add(Reader["Usuario"].ToString());
                }

                Conexao.Close();
            }
            return Usuarios.ToArray();
        }
        public string SenhaDoUsuario(string Usuario)
        {

            return "";
        }
        public int PermissoesDoUsuario(string Usuario)
        {

            return 0;
        }
    }
}
