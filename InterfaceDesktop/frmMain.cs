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
            string strInformacoes = GetCSV(Global.strComandoCSV, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), Global.striIa);
            txtLoad.Text = strInformacoes;
            if (strInformacoes.Length > 10)
            {
                List<RegistroCSV> Registros =
                CSV2Matriz(strInformacoes);
                foreach (RegistroCSV Reg in Registros)
                {
                    textBox1.Text += Reg.time().ToString() + " , " + Reg.valor().ToString() + "\n";
                }
            }
            //picStatus.Image = InterfaceDesktop.Properties.Resources.Vermelho;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Botão "Configurações"
            Form frmconfig1 = new frmConfig();
            this.Hide();
            frmconfig1.ShowDialog();
            frmMain_Load(new object(), new EventArgs());
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
                        frmMain_Load(new object(), new EventArgs());
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
                List<Feed> Fdd = JsonConvert.DeserializeObject<List<Feed>>(Requisicao);
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
            // Verifica se alguma variável não está associada a um índice
            if ((Global.striP == "") | (Global.striQ == "") | (Global.striS == "") | (Global.striVa == "") | (Global.striVb == "") | (Global.striVc == "") | (Global.striIa == "") | (Global.striIb == "") | (Global.striIc == "") | (Global.striNo == "") | (Global.striTo == "") | (Global.striTe == ""))
            {
                MessageBox.Show("Verifique os nomes das variáveis");
                frmConfig Config = new frmConfig();
                Config.ShowDialog();
                frmMain_Load(sender, e);
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Relógio
            lblHora.Text = Convert.ToString(DateTime.Now);
        }

        // Atualiza os gráficos
        private void tmrGraficos_Tick(object sender, EventArgs e)
        {
            string strResquisicao = Global.Servidor + "";

        }

        // Esta rotina deve ser utilizada considerando um intervalo de tempo limitado
        /// <summary>Retorna um arquivo CSV</summary>
        /// <param name="Comando">Comando para o servidor</param>
        /// <param name="Inicio">Tempo inicial</param>
        /// <param name="Fim">Tempo final</param>
        /// <param name="ID">ID do feed</param>
        /// <returns>String CSV</returns>
        private string GetCSV(string Comando, DateTime Inicio, DateTime Fim, string ID)
        {
            string strComando = Global.Servidor + Comando + ID +
                "&start=" + Uteis.Time2Unix(Inicio) +
                "&end=" + Uteis.Time2Unix(Fim) +
                "&apikey=" + Global.APIKey + "&interval=30&timeformat=0";
            Clipboard.SetText(strComando);
            WebClient Web = new WebClient();
            return Web.DownloadString(strComando);
        }

        private List<RegistroCSV> CSV2Matriz(string strCSV)
        {
            List<RegistroCSV> Registros = new List<RegistroCSV>();

            int inicio = 0;
            int jj = 0; // Contador para percorrer a string
            // Loop para percorrer toda a sequência de texto
            // Tempo \t Valor
            // 1234  \t 14.9
            // Pular a linha de cabeçalho
            if (Global.CabecalhoCSV)
            {
                do
                {
                    jj++;
                } while ((!(strCSV[jj] == '\n')) & (jj < strCSV.Length - 1));
            }

            while (jj < strCSV.Length - 1)
            {
                RegistroCSV Registro = new RegistroCSV();
                inicio = ++jj;
                while ((!(strCSV[jj] == Global.SeparadorCSV)) & (jj < strCSV.Length - 1))
                {
                    jj++;
                }
                Registro.Time = strCSV.Substring(inicio, jj - inicio);
                inicio = ++jj;
                while ((!(strCSV[jj] == '\n')) & (jj < strCSV.Length - 1))
                {
                    jj++;
                }
                Registro.Valor = strCSV.Substring(inicio, jj - inicio).Replace('.', ',');
                Registros.Add(Registro);
            }
            return Registros;
        }
    }
}
