using System; // tipos de variáveis utilizada
using System.Collections.Generic; // Formulários
using System.Net;
using System.Windows.Forms;
namespace InterfaceDesktop
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Salva as configurações
            Variaveis.fEP.NomeFeed =
                Properties.Settings.Default.sEP = txtEP.Text;
            Variaveis.fEQ.NomeFeed =
                Properties.Settings.Default.sEQ = txtEQ.Text;
            Variaveis.fES.NomeFeed =
                Properties.Settings.Default.sES = txtES.Text;
            Variaveis.fFatorPotencia.NomeFeed =
                Properties.Settings.Default.sFP = txtFatorPotencia.Text;
            Variaveis.fFreq.NomeFeed =
                Properties.Settings.Default.sFreq = txtFreq.Text;
            Variaveis.fIa.NomeFeed =
                Properties.Settings.Default.sIa = txtIa.Text;
            Variaveis.fIb.NomeFeed =
                Properties.Settings.Default.sIb = txtIb.Text;
            Variaveis.fIc.NomeFeed =
                Properties.Settings.Default.sIc = txtIc.Text;
            Variaveis.fIMa.NomeFeed =
                Properties.Settings.Default.sIMa = txtIMa.Text;
            Variaveis.fIMb.NomeFeed =
                Properties.Settings.Default.sIMb = txtIMb.Text;
            Variaveis.fIMc.NomeFeed =
                Properties.Settings.Default.sIMc = txtIMc.Text;
            Variaveis.fIPa.NomeFeed =
                Properties.Settings.Default.sIPa = txtIPa.Text;
            Variaveis.fIPb.NomeFeed =
                Properties.Settings.Default.sIPb = txtIPb.Text;
            Variaveis.fIPc.NomeFeed =
                Properties.Settings.Default.sIPc = txtIPc.Text;
            Variaveis.fNivelOleo.NomeFeed =
                Properties.Settings.Default.sNivelOleo = txtNo.Text;
            Variaveis.fP.NomeFeed =
                Properties.Settings.Default.sP = txtP.Text;
            Variaveis.fPM.NomeFeed =
                Properties.Settings.Default.sPM = txtPM.Text;
            Variaveis.fPP.NomeFeed =
                Properties.Settings.Default.sPP = txtPP.Text;
            Variaveis.fQ.NomeFeed =
                Properties.Settings.Default.sQ = txtQ.Text;
            Variaveis.fQM.NomeFeed =
                Properties.Settings.Default.sQM = txtQM.Text;
            Variaveis.fQP.NomeFeed =
                Properties.Settings.Default.sQP = txtQP.Text;
            Variaveis.fS.NomeFeed =
                Properties.Settings.Default.sS = txtS.Text;
            Variaveis.fSM.NomeFeed =
                Properties.Settings.Default.sSM = txtSM.Text;
            Variaveis.fSP.NomeFeed =
                Properties.Settings.Default.sSP = txtSP.Text;
            Variaveis.fTEnrolamento.NomeFeed =
                Properties.Settings.Default.sTEntolamento = txtTe.Text;
            Variaveis.fTOleo.NomeFeed =
                Properties.Settings.Default.sTOleo = txtTo.Text;
            Variaveis.fVab.NomeFeed =
                Properties.Settings.Default.sVab = txtVab.Text;
            Variaveis.fValvulaPressao.NomeFeed =
                Properties.Settings.Default.sValvulaPressao = txtValvulaPressao.Text;
            Variaveis.fVan.NomeFeed =
                Properties.Settings.Default.sVan = txtVan.Text;
            Variaveis.fVbc.NomeFeed =
                Properties.Settings.Default.sVbc = txtVbc.Text;
            Variaveis.fVbn.NomeFeed =
                Properties.Settings.Default.sVbn = txtVbn.Text;
            Variaveis.fVca.NomeFeed =
                Properties.Settings.Default.sVca = txtVca.Text;
            Variaveis.fVcn.NomeFeed =
                Properties.Settings.Default.sVcn = txtVcn.Text;
            Servidor.APIKey =
                Properties.Settings.Default.APIKEY = txtAPIKEY.Text;
            Servidor.Server =
                Properties.Settings.Default.Servidor = txtServidor.Text;

            Properties.Settings.Default.Save();
            Global.boolReiniciar = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            if (Global.boolConfigObriatoria)
            {
                // Encerra o programa
                this.Close();
            }
            else
            {
                // Encerra o formulário
                Global.boolReiniciar = false;
                this.Close();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            bool erro = false;
            // Verifica a senha (ou se existe algum)
            if ((Uteis.getMD5(txtSenhaAntiga.Text) == Servidor.Senha) | Servidor.Senha == "")
            {
                // Verifica o comprimento do nome de usuário
                if (txtNome.Text.Length >= 4)
                {
                    // Verifica o comprimento da senha
                    if (txtSenha.Text.Length >= 4)
                    {
                        // Verificar se o nome de usuário contém caracteres especiais (não letras e números)
                        char[] username = txtNome.Text.ToCharArray();
                        for (int jj = 0; jj < username.Length; jj++)
                        {
                            char letra = txtNome.Text[jj];
                            // caracteres: [a-z,A-Z,0-9]
                            erro |= (letra < 'a' | letra > 'z') & (letra < '0' | letra > '9') & (letra < 'A' | letra > 'Z');
                        }
                        if (!erro)
                        {
                            // Gravar informações no banco de dados

                            if ((Servidor.Senha == Uteis.getMD5(txtSenhaAntiga.Text)) | (Servidor.Senha == ""))
                            {
                                // Adicionar novo registro
                                // Comando para trocar senha (adicionar um novo usuário com o mesmo nome)
                                // Verifica se as senhas são idênticas
                                if (txtSenha.Text == txtSenha2.Text)
                                {
                                    Servidor.Senha = Uteis.getMD5(txtSenha.Text);
                                    Servidor.Username = txtNome.Text;
                                    Properties.Settings.Default.Usuario = txtNome.Text;
                                    Properties.Settings.Default.Senha = Servidor.Senha;
                                    Properties.Settings.Default.Save();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("As senhas não são idênticas");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Senha incorreta");
                            }

                        }
                        else
                        {
                            MessageBox.Show("São permitidos apenas letras (a-z, A-Z) e números (0-9) para nome de usuário");
                        }
                    }
                    else
                    {
                        MessageBox.Show("A senha deve conter pelo menos 4 caracteres");
                    }
                }
                else
                {
                    MessageBox.Show("O nome de usuário deve conter 4 caracteres ou mais");
                }
            }
            else
            {
                MessageBox.Show("Senha incorreta");
            }
        }

        private void frmConfig_Load(object sender, EventArgs e)
        {
            this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            txtNome.Text = Servidor.Username;
            if (!Global.boolNovoUsuario)
            {
                // Rearanja os controles, ocultando as configurações não relevantes 
                tabControl1.TabPages.Remove(tabPage1);
                txtSenhaAntiga.Visible = lblSA.Visible = false;
                lblSenha2.Top = lblSenha.Top;
                lblSenha.Top = lblNome.Top;
                lblNome.Top = lblSA.Top;
                txtSenha2.Top = txtSenha.Top;
                txtSenha.Top = txtNome.Top;
                txtNome.Top = txtSenhaAntiga.Top;
                btnOK.Enabled = false;
                txtNome.Select();
                this.ControlBox = false;
                btnAddUser.Text = "Criar usuário";
                AcceptButton = btnAddUser;
                chkAdmin.Checked = true;
                chkAdmin.Enabled = false;
            }
            else
            {
                this.Top = 0;
                this.Height = Screen.PrimaryScreen.WorkingArea.Height - 10;
                // Carrega as configurações para o formulário

                txtEP.Text = Properties.Settings.Default.sEP;
                txtEQ.Text = Properties.Settings.Default.sEQ;
                txtES.Text = Properties.Settings.Default.sES;
                txtFatorPotencia.Text = Properties.Settings.Default.sFP;
                txtFreq.Text = Properties.Settings.Default.sFreq;
                txtIa.Text = Properties.Settings.Default.sIa;
                txtIb.Text = Properties.Settings.Default.sIb;
                txtIc.Text = Properties.Settings.Default.sIc;
                txtIMa.Text = Properties.Settings.Default.sIMa;
                txtIMb.Text = Properties.Settings.Default.sIMb;
                txtIMc.Text = Properties.Settings.Default.sIMc;
                txtIPa.Text = Properties.Settings.Default.sIPa;
                txtIPb.Text = Properties.Settings.Default.sIPb;
                txtIPc.Text = Properties.Settings.Default.sIPc;
                txtNo.Text = Properties.Settings.Default.sNivelOleo;
                txtP.Text = Properties.Settings.Default.sP;
                txtPM.Text = Properties.Settings.Default.sPM;
                txtPP.Text = Properties.Settings.Default.sPP;
                txtQ.Text = Properties.Settings.Default.sQ;
                txtQM.Text = Properties.Settings.Default.sQM;
                txtQP.Text = Properties.Settings.Default.sQP;
                txtS.Text = Properties.Settings.Default.sS;
                txtSM.Text = Properties.Settings.Default.sSM;
                txtSP.Text = Properties.Settings.Default.sSP;
                txtTe.Text = Properties.Settings.Default.sTEntolamento;
                txtTo.Text = Properties.Settings.Default.sTOleo;
                txtVab.Text = Properties.Settings.Default.sVab;
                txtValvulaPressao.Text = Properties.Settings.Default.sValvulaPressao;
                txtVan.Text = Properties.Settings.Default.sVan;
                txtVbc.Text = Properties.Settings.Default.sVbc;
                txtVbn.Text = Properties.Settings.Default.sVbn;
                txtVca.Text = Properties.Settings.Default.sVca;
                txtVcn.Text = Properties.Settings.Default.sVcn;
                txtAPIKEY.Text = Properties.Settings.Default.APIKEY;
                txtServidor.Text = Properties.Settings.Default.Servidor;

                this.Show();
            }
        }

        private void btnServidor_Click(object sender, EventArgs e)
        {
            // Carrega a lista de variáveis disponível no servidor web
            if ((txtServidor.Text != "") & (txtAPIKEY.Text != ""))
            {
                WebClient ServidorWeb = new WebClient();
                string Requisicao = txtServidor.Text + ComandosCSV.strComandoFeedList + txtAPIKEY.Text;
                try
                {
                    Requisicao = ServidorWeb.DownloadString(Requisicao);
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro na requisição:\n"+erro.Message);
                    return;
                }
                List<Feed> Fdd = json.json2Feed(Requisicao);
                string[] strVariaveis = new string[Fdd.Count];
                for (int kk = 0; kk < Fdd.Count; kk++)
                {
                    strVariaveis[kk] = Fdd[kk].Nome;
                }
                for (int kk = 0; kk < tabPage1.Controls.Count; kk++)
                {
                    if (tabPage1.Controls[kk] is ComboBox)
                    {
                        ((ComboBox)tabPage1.Controls[kk]).Items.Clear();
                        ((ComboBox)tabPage1.Controls[kk]).Items.AddRange(strVariaveis);
                    }
                }
            }
        }
    }
}
