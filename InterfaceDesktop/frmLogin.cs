using System; // Alguns tipos de variáveis utilizados
using System.Windows.Forms; // Formulários
//using System.IO;
//using System.Data; // Arquivos

namespace InterfaceDesktop
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private bool verificaSenha()
        {
#if DEBUG
            // Pula a parte da senha
            Servidor.Permissoes = 1;
            return true;
            //Servidor.Senha = Uteis.getMD5(txtSenha.Text);
#endif
            bool retorno = false;
            //if (Servidor.Senha == Uteis.getMD5(txtSenha.Text))
            if (BancoDeDados.SenhaDoUsuario(txtUsername.Text) == Uteis.getMD5(txtSenha.Text))
            {
                txtSenha.Visible = txtUsername.Visible = label2.Visible = label3.Visible = false;
                pictureBox1.Height = Height - toolStrip1.Height - 4 * pictureBox1.Top;
                retorno = true;
                Servidor.Permissoes = BancoDeDados.PermissoesDoUsuario(txtUsername.Text);
            }
            else
            {
                MessageBox.Show("Senha incorreta");
                txtSenha.Text = "";
                txtSenha.Focus();
                retorno = false;
            }
            return retorno;
        }
        private void btnOnline_Click(object sender, EventArgs e)
        {
            if (verificaSenha())
            {
                // Cria uma nova instância do formulário principal
                frmMain fmrmain = new frmMain();
                // Esconde o formulário de autenticação
                this.Hide();
                // Exibe o formulário principal e aguarda seu encerramento
                fmrmain.ShowDialog();
                fmrmain.Dispose();
                // exibe novamente o formulário de autenticação
                this.Show();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Text = Servidor.Username;
            txtSenha.Text = "";
            this.Icon =System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void btnOffline_Click(object sender, EventArgs e)
        {
            if (verificaSenha())
            {
                frmGraficos Graficos = new frmGraficos();
                this.Hide();
                Graficos.ShowDialog();
                Graficos.Dispose();
                this.Show();
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (verificaSenha())
            {
                if (Servidor.Permissoes == 1)
                {
                    frmConfig Config = new frmConfig();
                    this.Hide();
                    Config.ShowDialog();
                    Config.Dispose();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Este usuário não tem permissão para acessar a interface de configurações");
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (verificaSenha())
            {
                frmCompara Compara = new frmCompara();
                this.Hide();
                Compara.ShowDialog();
                Compara.Dispose();
                this.Show();
            }
        }
    }
}
