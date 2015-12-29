using System; // Tipos de variáveis menos comuns
using System.Net; // Comunicação web
using System.Text; // Codificação de texto
using System.Windows.Forms; // Formulários
using Newtonsoft.Json; // JSON
using System.Collections.Generic; // Variáveis anonimas (json)
using System.Data.SQLite; // Bancos de dados

namespace InterfaceDesktop
{
    public partial class frmMain : Form
    {
        /// <summary>Componente para comunicação via internet</summary>
        WebClient Servidor = new WebClient();
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
			// Botão "Configurações"
            Form frmconfig1 = new frmConfig();
            this.Hide();
            frmconfig1.ShowDialog();
            this.Show();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
			// Lê as configurações armazenadas no banco de dados
            using (SQLiteConnection Con = new SQLiteConnection(Global.Conexao))
            {
                Con.Open();
                string sqlComando = "SELECT * FROM " + Global.TabelaConfig + " ORDER BY ID DESC LIMIT 0,1";
                using (SQLiteCommand Comando = new SQLiteCommand(sqlComando, Con))
                {
                    SQLiteDataReader Leitor = Comando.ExecuteReader();
                    if (Leitor.Read())
                    {
                        Global.Servidor = Convert.ToString(Leitor["Servidor"]);
                        Global.APIKey = Convert.ToString(Leitor["APIKEY"]);
                        Global.strP = Convert.ToString(Leitor["Node_P"]);
                        Global.strQ = Convert.ToString(Leitor["Node_Q"]);
                        Global.strS = Convert.ToString(Leitor["Node_S"]);
                        Global.strVa = Convert.ToString(Leitor["Node_Va"]);
                        Global.strVb = Convert.ToString(Leitor["Node_Vb"]);
                        Global.strVc = Convert.ToString(Leitor["Node_Vc"]);
                        Global.strIa = Convert.ToString(Leitor["Node_Ia"]);
                        Global.strIb = Convert.ToString(Leitor["Node_Ib"]);
                        Global.strIc = Convert.ToString(Leitor["Node_Ic"]);
                        Global.strNo = Convert.ToString(Leitor["Node_No"]);
                        Global.strTo = Convert.ToString(Leitor["Node_To"]);
                        Global.strTe = Convert.ToString(Leitor["Node_Te"]);
                    }
                    else
                    {
                        frmConfig Config = new frmConfig();
                        Config.ShowDialog();
						// Reinicia a rotina para [tentar] carregar as configurações
                        frmMain_Load(new object(),new EventArgs());
                        return;
                    }
                }
            }
            // Buscar índices no servidor:
            string Requisicao = Global.Servidor + "/feed/list.json?apikey=" + Global.APIKey;

            try
            {
                Requisicao = Servidor.DownloadString(Requisicao);
                txtLoad.Text = Requisicao;
                List<Feeds> Fdd = JsonConvert.DeserializeObject<List<Feeds>>(Requisicao);
                for (int kk = 0; kk < Fdd.Count; kk++)
                {
                    if (Fdd[kk].name == Global.strP) Global.striP = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strQ) Global.striQ = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strS) Global.striS = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strVa) Global.striVa = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strVb) Global.striVb = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strVc) Global.striVc = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strIa) Global.striIa = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strIb) Global.striIb = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strIc) Global.striIc = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strNo) Global.striNo = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strTo) Global.striTo = Fdd[kk].id;
                    if (Fdd[kk].name == Global.strTe) Global.striTe = Fdd[kk].id;
                }
            }
            catch (Exception Erro)
            {
				// Trocar para um alerta na barra inferior
                MessageBox.Show(Erro.Message);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
			// Relógio
            lblHora.Text = Convert.ToString(DateTime.Now);
        }

    }
}
