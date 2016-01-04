using System; // tipos de variáveis utilizada
using System.Windows.Forms; // Formulários
using System.Data.SQLite; // Banco de dados SQLite
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
            ComandoSQL.INSERT("INSERT INTO " + Global.TabelaConfig +
                " (Servidor, Username, " +
                "Node_P, Node_Q, Node_S, " +
                "Node_Va, Node_Vb, Node_Vc, " +
                "Node_Ia, Node_Ib, Node_Ic, " +
                "Node_No, Node_To, Node_Te, " +
                "APIKEY) VALUES ('" +
                txtServidor.Text + "', '" + Global.Username + "', '" +
                txtP.Text + "', '" + txtQ.Text + "', '" + txtS.Text + "', '" +
                txtVa.Text + "', '" + txtVb.Text + "', '" + txtVc.Text + "', '" +
                txtIa.Text + "', '" + txtIb.Text + "', '" + txtIc.Text + "', '" +
                txtNo.Text + "', '" + txtTo.Text + "', '" + txtTe.Text + "', '" +
                txtAPIKEY.Text + "')");
            Global.restart = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            if (Global.ConfigObriatoria)
            {
                // Encerra o programa
                Application.Exit();
            }
            else
            {
                // Encerra o formulário
                Global.restart = false;
                this.Close();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            bool erro = false;
            // Verifica a senha (ou se existe algum)
            if ((Uteis.getMD5(txtSenhaAntiga.Text) == Global.Senha) | !Global.tabPage1)
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
                            if (txtNome.Text == Global.Username)//Se for troca de senha
                            {
                                if (Global.Senha == Uteis.getMD5(txtSenhaAntiga.Text))
                                {
                                    // Adicionar novo registro
                                    // Comando para trocar senha (adicionar um novo usuário com o mesmo nome)
                                    // Verifica se as senhas são idênticas
                                    if (txtSenha.Text == txtSenha2.Text)
                                    {
                                        Global.Senha = Uteis.getMD5(txtSenha.Text);
                                        Global.Username = txtNome.Text;
                                        ComandoSQL.INSERT("INSERT INTO " + Global.TabelaUsers + " (Username, Senha) VALUES ('" + txtNome.Text + "' ,'" + Global.Senha + "');");
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
                                bool Existe = false;
                                // Verifica se existe o usuário
                                string strComando = "SELECT * FROM " + Global.TabelaUsers +
                                    " WHERE Username ='" + txtNome.Text + "' LIMIT 0,1";
                                using (SQLiteConnection Con = new SQLiteConnection(Global.Conexao))
                                {
                                    Con.Open();
                                    using (SQLiteCommand Comando = new SQLiteCommand(strComando, Con))
                                    {
                                        SQLiteDataReader Leitor = Comando.ExecuteReader();
                                        if (Leitor.Read())
                                            Existe = true;
                                    }
                                }
                                if (Existe)
                                {
                                    MessageBox.Show("Não é possível modificar a senha de outro usuário");
                                }
                                else
                                {
                                    // Criar um novo usuário
                                    // Adicionar novo registro
                                    // Comando para trocar senha (adicionar um novo usuário com o mesmo nome)
                                    if (txtSenha.Text == txtSenha2.Text)
                                    {
                                        ComandoSQL.INSERT("INSERT INTO " + Global.TabelaUsers + " (Username, Senha) VALUES ('" + txtNome.Text + "' ,'" + Uteis.getMD5(txtSenha.Text) + "');");
                                        Global.tabPage1 = true;
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("As senhas não são idênticas");
                                    }

                                }
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
            txtNome.Text = Global.Username;
            if (!Global.tabPage1)
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
                btnCancelar.Enabled = false;
                txtNome.Select();
                this.ControlBox = false;
                AcceptButton = btnAddUser;
            }
            else
            {
                // Nome de usuário igual ao do usuário atual para troca de senha
                txtNome.Text = Global.Username;
                // Carrega as configurações
                string sqlComando = "SELECT * FROM " + Global.TabelaConfig + " ORDER BY ID DESC LIMIT 0,1";
                using (SQLiteConnection Con = new SQLiteConnection(Global.Conexao))
                {
                    Con.Open();
                    using (SQLiteCommand Comando = new SQLiteCommand(sqlComando, Con))
                    {
                        SQLiteDataReader Leitor = Comando.ExecuteReader();
                        if (Leitor.Read())
                        {
                            txtServidor.Text = Convert.ToString(Leitor["Servidor"]);
                            txtAPIKEY.Text = Convert.ToString(Leitor["APIKEY"]);
                            txtP.Text = Convert.ToString(Leitor["Node_P"]);
                            txtQ.Text = Convert.ToString(Leitor["Node_Q"]);
                            txtS.Text = Convert.ToString(Leitor["Node_S"]);
                            txtVa.Text = Convert.ToString(Leitor["Node_Va"]);
                            txtVb.Text = Convert.ToString(Leitor["Node_Vb"]);
                            txtVc.Text = Convert.ToString(Leitor["Node_Vc"]);
                            txtIa.Text = Convert.ToString(Leitor["Node_Ia"]);
                            txtIb.Text = Convert.ToString(Leitor["Node_Ib"]);
                            txtIc.Text = Convert.ToString(Leitor["Node_Ic"]);
                            txtNo.Text = Convert.ToString(Leitor["Node_No"]);
                            txtTo.Text = Convert.ToString(Leitor["Node_To"]);
                            txtTe.Text = Convert.ToString(Leitor["Node_Te"]);
                        }
                        else
                        {
                            //configurações padrão
                            txtAPIKEY.Text = "";
                            txtServidor.Text = "http://";
                            txtP.Text = "P";
                            txtQ.Text = "Q";
                            txtS.Text = "S";
                            txtVa.Text = "Va";
                            txtVb.Text = "Vb";
                            txtVc.Text = "Vc";
                            txtIa.Text = "Ia";
                            txtIb.Text = "Ib";
                            txtIc.Text = "Ic";
                            txtNo.Text = "No";
                            txtTo.Text = "To";
                            txtTe.Text = "Te";
                            btnCancelar.Enabled = false;

                        }
                    }
                }
            }
        }
    }
}
