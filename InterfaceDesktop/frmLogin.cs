using System; // Alguns tipos de variáveis utilizados
using System.Windows.Forms; // Formulários
using System.Data.SQLite; // Bancos de dados SQLite
using System.IO;
using System.Data; // Arquivos

namespace InterfaceDesktop
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Global.Username = txtUsername.Text;
            Global.Senha = Uteis.getMD5(txtSenha.Text);
            // Limpa os campos "Senha" para o caso de a senha estar incorreta
            txtSenha.Text = "";
			// Procura pelo nome de usuário no banco de dados
            string strComandoSQL = "SELECT * FROM " + Global.TabelaUsers +
                " WHERE Username='" + txtUsername.Text + "' ORDER BY ID DESC LIMIT 0,1";

            bool Erro = false;
            using (SQLiteConnection Con = new SQLiteConnection(Global.Conexao))
            {
                Con.Open();
                using (SQLiteCommand Comando = new SQLiteCommand(strComandoSQL, Con))
                {
                    SQLiteDataReader Leitor = Comando.ExecuteReader();
                    
                    if (Leitor.Read())
                    {
                        if (Convert.ToString(Leitor["Senha"]) == Global.Senha)
                        {
                            //Login Permitido Erro = false;
                        }
                        else
                        {
                            // Senha incorreta
                            Erro = true;
                        }
                    }
                    else
                    {
                        Erro = true;
                    }
                }
            }
            if (Erro)
                MessageBox.Show("Login incorreto");
            else
            {

                // Cria uma nova instância do formulário principal
                Form fmrmain = new frmMain();
                // Esconde o formulário de autenticação
                this.Hide();
                // Exibe o formulário principal e aguarda seu encerramento
                fmrmain.ShowDialog();
                // exibe novamente o formulário de autenticação
                //this.Show();
                // Encerra o programa
                this.Show();
            }
        }



        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Cria o banco de dados e as tabelas se não existirem
            ComandoSQL.CriarBancoDeDados();
            // Verifica se existe um usuário cadastrado
            string comandoSQL = ("SELECT * FROM " + Global.TabelaUsers);// + " ORDER BY ID DESC LIMIT 0,1");
            using (SQLiteConnection Con = new SQLiteConnection(Global.Conexao))
            {
                Con.Open();
                using (SQLiteCommand Comando = new SQLiteCommand(comandoSQL, Con))
                {

                    if (!Comando.ExecuteReader().Read())
                    {
                        // Criar um novo usuário
                        Global.tabPage1 = false;
                        while (!Global.tabPage1)
                        {
                            frmConfig Config = new frmConfig();
                            this.Hide();
                            Config.ShowDialog();
                        }
                        this.Show();
                    }
                }
            }

            // Elimina o login
            frmMain frmmain = new frmMain();
            frmmain.ShowDialog();
            this.Close();
        }
    }
}
