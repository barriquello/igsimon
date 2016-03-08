using System; // Alguns tipos de variáveis utilizados
using System.Windows.Forms; // Formulários
//using System.IO;
//using System.Data; // Arquivos

namespace InterfaceDesktop
{
    /// <summary>
    /// Formulário de autenticação.
    /// </summary>
    public partial class frmLogin : Form
    {
        /// <summary>
        /// Subrotina responsável por contruir o formulário.
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Subrotina responsável por verificar se a senha informada corresponde às informações armazenadas no banco de dados local.
        /// Essa subrotina oculta as entradas de texto para o caso de a senha estar correta.
        /// </summary>
        /// <returns>Retorna true para o caso de a senha informada ser correspondende às informações armazenadas ou false para o caso contrário.</returns>
        private bool verificaSenha()
        {
#if DEBUG
            // Pula a parte da senha
            //Servidor.Permissoes = 1;
            //return true;
            //Servidor.Senha = Uteis.getMD5(txtSenha.Text);
#endif
            bool retorno = false;
            //if (Servidor.Senha == Uteis.getMD5(txtSenha.Text))
            if (BancoDeDados.SenhaDoUsuario(txtUsername.Text) == Uteis.getMD5(txtSenha.Text))
            {
                Servidor.Username = txtUsername.Text;
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
        /// <summary>
        /// Evento disparado ao clicar no botão "interface Online".
        /// Essa subrotina é responsável por ocultar a tela de login e mostrar a interface online.
        /// </summary>
        /// <param name="sender">Objeto responsável pela chamada do evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
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
        /// <summary>
        /// Evento disparado ao carregar o formulário.
        /// </summary>
        /// <param name="sender">Objeto responsável pelo disparo do evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Text = Servidor.Username;
            txtSenha.Text = "";
            this.Icon =System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        /// <summary>
        /// Evento disparado ao clicar no botão "interface offline".
        /// Essa subrotina esconde a interface de autenticação e exibe a interface offline, caso os dados de autenticação estejam corretos.
        /// </summary>
        /// <param name="sender">Objeto responsável pelo disparo do evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
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
        /// <summary>
        /// Evento disparado ao clicar no botão "configurações".
        /// </summary>
        /// <param name="sender">Objeto responsável por disparar o evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
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
        /// <summary>
        /// Evento disparado ao clicar no botão "Comparações".
        /// </summary>
        /// <param name="sender">Objeto responsável por disparar o evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
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
