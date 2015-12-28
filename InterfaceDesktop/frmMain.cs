using System; // Tipos de variáveis menos comuns
using System.Net; // Comunicação web
using System.Text; // Codificação de texto
using System.Windows.Forms; // Formulários
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SQLite;

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
            #region teste1
            // testes

            Int32 Agora = Uteis.Time2Unix(DateTime.UtcNow); //((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds));
            Int32 Antes = Uteis.Time2Unix(new DateTime(2015, 12, 21)); //((Int32)(new DateTime(2015, 12, 22).Subtract(new DateTime(1970, 1, 1)).TotalSeconds));
            //http://localhost/feed/csvexport.json?id=2&start=1450995509&end=1451081909&interval=1&timeformat=0
            // string Comando = "http://localhost/feed/csvexport.json?id=3&start=" +
            //     Antes.ToString() + "&end=" +
            //     Agora.ToString() + "&interval=1&timeformat=0" +
            string Comando = "http://localhost/feed/list.json" +
                "&apikey=00857aea31e48a2564fb02664ddfc853";//72d5d09d5ed08c6743d2c71006f3c9bd";
            //Console.WriteLine(Comando);
            try
            {
                Comando = Encoding.Default.GetString(Servidor.DownloadData(Comando));
                Comando = Comando.Replace("\n", "\r\n");
                txtLoad.Text = Comando;

                List<Feeds> Fdd =
                    JsonConvert.DeserializeObject<List<Feeds>>(Comando);
                Console.WriteLine(Fdd);
                Console.WriteLine("Nome do nó\tíndice");
                // for (int kk = 0; kk < Fdd.Count; kk++)
                //    {
                //         Console.WriteLine(Fdd[kk].name + "\t\t\t" + Fdd[kk].id);
                //     }
            }
            catch (Exception Erro)
            {
                lblMensagens.Text = Erro.Message;
            }

            #endregion

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form frmconfig1 = new frmConfig();
            this.Hide();
            frmconfig1.ShowDialog();
            this.Show();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

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
                        //                                this.Hide();
                        Config.ShowDialog();

                        frmMain_Load(new object(),new EventArgs());
                        // encerra rotina
                        return;
                        //                                this.Show
                    }
                }
            }
            // Buscar índices no servidor:
            string Requisicao = Global.Servidor + "/feed/list.json?apikey=" + Global.APIKey;

            try
            {
                Requisicao = Servidor.DownloadString(Requisicao);
                //Encoding.Default.GetString(Servidor.DownloadData(Requisicao));
                // Comando = Comando.Replace("\n", "\r\n");
                txtLoad.Text = Requisicao;
                List<Feeds> Fdd = JsonConvert.DeserializeObject<List<Feeds>>(Requisicao);
                //Console.WriteLine(Fdd);
                //Console.WriteLine("Nome do nó\tíndice");
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
                    //Console.WriteLine(Fdd[kk].name + "\t\t\t" + Fdd[kk].id);
                    //Text += Fdd[kk].name + " ";
                }
            }
            catch (Exception Erro)
            {
                MessageBox.Show(Erro.Message);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = Convert.ToString(DateTime.Now);
        }

    }
}
