using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
namespace InterfaceDesktop
{
    // Classe que provê recursos para o acesso ao banco de dados local (sqlite).
    class BancoDeDados
    {
        /// <summary>Comando SQL para criar a tabela de usuários no banco de dados local.</summary> 
        public static string ComandoCriarTabelas = "CREATE TABLE IF NOT EXISTS [Usuarios] ([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, [Usuario] TEXT NULL, [Senha] TEXT NULL, [Permissao] INTEGER NULL)";
        /// <summary>Comando SQL para inserir ou atualizar a senha de um usuário no banco de dados local.</summary>
        public static string ComandoInserirUsuario = "INSERT INTO Usuarios (Usuario, Senha, Permissao) VALUES (@Usuario, @Senha, @Permissao)";
        /// <summary>Comando SQL para listar os nomes de usuário presentes no banco de dados local.</summary>
        public static string ComandoSelect = "SELECT * FROM Usuarios";
        /// <summary>Comando SQL para buscar a senha (criptografida) e acesso do usuário.</summary>
        public static string ComandoSenha = "SELECT * FROM Usuarios WHERE Usuario='{0}' ORDER BY Usuario DESC LIMIT 0,1";
        /// <summary>String de conexão com o banco de dados local.</summary>
        public static string ConnectionString = "Data Source=|DataDirectory|\\Usuarios.db;Pooling=True;Synchronous=Off;journal mode=Wal";

        /// <summary>
        /// Subrotina responsável por criar a tabela de usuários no banco de dados local.
        /// Caso necessário, essa subrotina cria o arquivo de banco de dados.
        /// </summary>
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
                        // Caso a pasta do programa não seja gravável ou o usuário não tenha permissões de gravação
                        MessageBox.Show("Erro ao criar o banco de dados, o programa será encerrado agora");
                        Environment.Exit(-1);
                    }
                }
            }
        }
        /// <summary>Subrotina responsável pela inserção de um novo usuário no banco de dados.</summary>
        /// <param name="Usuario">Nome do usuário.</param>
        /// <param name="Senha">Senha criptografada.</param>
        /// <param name="Permissao">Permissão de acesso ao painel de configurações.</param>
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
        /// <summary>Função para retornar a lista de usuários.</summary>
        /// <returns>Retorna uma matriz de strings contendo a lista de usuários presente no banco de dados.</returns>
        public static string[] ListaDeUsuarios()
        {
            List<string> Usuarios = new List<string>();
            using (SQLiteConnection Conexao = new SQLiteConnection(ConnectionString))
            {
                Conexao.Open();
                using (SQLiteCommand Comando = new SQLiteCommand(ComandoSelect, Conexao))
                {
                    SQLiteDataReader Reader = Comando.ExecuteReader();
                    while (Reader.Read())
                    {
                        Usuarios.Add(Reader["Usuario"].ToString());
                    }

                    Conexao.Close();
                }
            }
            return Usuarios.ToArray();
        }

        /// <summary>Função para retornar a senha criptografada de um usuário</summary>
        /// <param name="Usuario">Nome do usuário</param>
        /// <returns>Senha criptografada</returns>
        public static string SenhaDoUsuario(string Usuario)
        {
            string Senha = "";
            using (SQLiteConnection Conexao = new SQLiteConnection(ConnectionString))
            {
                Conexao.Open();
                using (SQLiteCommand Comando = new SQLiteCommand(string.Format(ComandoSenha, Usuario), Conexao))
                {
                    SQLiteDataReader Reader = Comando.ExecuteReader();
                    while (Reader.Read())
                    {
                        Senha = Reader["Senha"].ToString();
                    }

                    Conexao.Close();
                }
            }
            return Senha;
        }

        /// <summary>Função para retornar as permissões do usuário a partir do banco de dados local.</summary>
        /// <param name="Usuario">Nome do usuário.</param>
        /// <returns>1 = permissão para acesar o painel de configurações; 0 = sem permissão para acessar o painel de configurações.</returns>
        public static int PermissoesDoUsuario(string Usuario)
        {
            int Permissao = 0;
            using (SQLiteConnection Conexao = new SQLiteConnection(ConnectionString))
            {
                Conexao.Open();
                using (SQLiteCommand Comando = new SQLiteCommand(string.Format(ComandoSenha, Usuario), Conexao))
                {
                    SQLiteDataReader Reader = Comando.ExecuteReader();
                    while (Reader.Read())
                    {
                        Permissao = Convert.ToInt32(Reader["Permissao"]);
                    }

                    Conexao.Close();
                }
            }
            return Permissao;
        }
    }
}
