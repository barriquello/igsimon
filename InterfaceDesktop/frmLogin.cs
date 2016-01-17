using System; // Alguns tipos de variáveis utilizados
using System.Windows.Forms; // Formulários
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
            if (Servidor.Senha == Uteis.getMD5(txtSenha.Text))
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
            else
            {
                MessageBox.Show("Senha incorreta");
                txtSenha.Text = "";
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Text = Servidor.Username;
            txtSenha.Text = "";
        }
    }
}
