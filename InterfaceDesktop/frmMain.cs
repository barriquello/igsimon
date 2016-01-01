using System; // Tipos de variáveis menos comuns
using System.Net; // Comunicação web
using System.Text; // Codificação de texto
using System.Windows.Forms; // Formulários
using Newtonsoft.Json; // JSON
using System.Collections.Generic; // Variáveis anonimas (json)
using System.Data.SQLite; // Bancos de dados
using System.Windows.Forms.DataVisualization.Charting; //Gráficos

namespace InterfaceDesktop
{
    public partial class frmMain : Form
    {
        /// <summary>Componente para comunicação via internet</summary>
        WebClient Servidor = new WebClient();
        /// <summary>Data do registro mais recente no servidor</summary>
        DateTime tUltimaAtualizacao;

        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Pegar o "time" mais recente:
            string strTime = GetCSV("/feed/get.json?field=time&id=", DateTime.Now, DateTime.Now, Global.striP).Replace("\"", "");
            tUltimaAtualizacao = Uteis.Unix2time(Convert.ToInt32(strTime)); // Horário mais recente armazenado no servidor
            lblMensagens.Text = "Último registro: " + tUltimaAtualizacao.ToString();
            chartTemperatura.Series.Clear();
            chartTemperatura.ChartAreas.Clear();
            chartTemperatura.ChartAreas.Add("P");
            chartTemperatura.ChartAreas.Add("V");
            chartTemperatura.ChartAreas.Add("I");
            chartTemperatura.ChartAreas.Add("T");
            //habilita o zoom
            chartTemperatura.ChartAreas["T"].CursorX.IsUserSelectionEnabled =
                chartTemperatura.ChartAreas["I"].CursorX.IsUserSelectionEnabled =
                chartTemperatura.ChartAreas["V"].CursorX.IsUserSelectionEnabled =
                chartTemperatura.ChartAreas["P"].CursorX.IsUserSelectionEnabled = true;
            chartTemperatura.ChartAreas["T"].CursorX.Interval =
                chartTemperatura.ChartAreas["I"].CursorX.Interval =
                chartTemperatura.ChartAreas["V"].CursorX.Interval =
                chartTemperatura.ChartAreas["P"].CursorX.Interval = 0.05;
            // Desabilita a escala no eixo X para quase todos os gráficos
            chartTemperatura.ChartAreas["P"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["V"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["I"].AxisX.LabelStyle.Enabled = false;
            // Posiciona as várias chartáreas:
            int intTamanhoLegenda = 100;
            float fLargura = 100f * (chartTemperatura.Width - intTamanhoLegenda) / (chartTemperatura.Width * 1f);
            chartTemperatura.ChartAreas["P"].Position.FromRectangleF(new System.Drawing.RectangleF(0, 0, fLargura, 25));
            chartTemperatura.ChartAreas["V"].Position.FromRectangleF(new System.Drawing.RectangleF(0, 25, fLargura, 25));
            chartTemperatura.ChartAreas["I"].Position.FromRectangleF(new System.Drawing.RectangleF(0, 50, fLargura, 25));
            chartTemperatura.ChartAreas["T"].Position.FromRectangleF(new System.Drawing.RectangleF(0, 75, fLargura, 25));
            // Alinhamento das chartareas
            chartTemperatura.ChartAreas["P"].AlignWithChartArea =
                chartTemperatura.ChartAreas["V"].AlignWithChartArea =
                chartTemperatura.ChartAreas["I"].AlignWithChartArea = "T";

            string[] strVariaveis = Global.strTodas();
            string[] Indices = Global.striTodas();
            for (int kk = 0; kk < strVariaveis.Length; kk++)
            {
                Series srTeste = new Series(strVariaveis[kk]);
                srTeste.ChartType = SeriesChartType.StepLine;
                srTeste.XValueType = ChartValueType.Auto;

                string strInformacoes = GetCSV(Global.strComandoCSV, tUltimaAtualizacao.AddDays(-1), tUltimaAtualizacao, Indices[kk]);
                //txtLoad.Text = strInformacoes;
                if (strInformacoes.Length > 10)
                {
                    List<RegistroCSV> Registros =
                    CSV2Matriz(strInformacoes);
                    foreach (RegistroCSV Reg in Registros)
                    {
                        srTeste.Points.AddXY(Reg.time(), Reg.valor());
                    }
                    srTeste.ChartArea = Global.strCategoria[kk];
                    chartTemperatura.Series.Add(srTeste);
                }
            }
            foreach (Series Sr in chartTemperatura.Series)
            {
                Sr.XValueType = ChartValueType.Time;
            }
            //picStatus.Image = InterfaceDesktop.Properties.Resources.Vermelho;
        }

        private void btnConfig_Click(object sender, EventArgs e)
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

        private void timerRelogio_Tick(object sender, EventArgs e)
        {
            // Relógio
            lblHora.Text = Convert.ToString(DateTime.Now);
        }

        // Atualiza os gráficos
        private void tmrGraficos_Tick(object sender, EventArgs e)
        {
            // Rotina para buscar novas informações no servidor e exibir na tela
            if (tmrGraficos.Interval != 5000)
                button1_Click(new object(), new EventArgs());

            tmrGraficos.Interval = 5000; // 15 segundos
            // Atualiza os gráficos:


            //tmrGraficos.Enabled = false; return;
            string strTime = GetCSV("/feed/get.json?field=time&id=", DateTime.Now, DateTime.Now, Global.striP).Replace("\"", "");
            DateTime NovaAtualizaco = Uteis.Unix2time(Convert.ToInt32(strTime));
            // Verifica atualizacao
            if (!(NovaAtualizaco == tUltimaAtualizacao))
            {
                string strP = "";
                string strQ = "";
                string strS = "";
                string strVa = "";
                string strVb = "";
                string strVc = "";
                string strIa = "";
                string strIb = "";
                string strIc = "";
                string strNo = "";
                string strTo = "";
                string strTe = "";
                // Atualiza as informações
                try
                {
                    tUltimaAtualizacao = NovaAtualizaco;
                    strP = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striP).Replace("\"", "").Replace(".", ",");
                    strQ = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striQ).Replace("\"", "").Replace(".", ",");
                    strS = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striS).Replace("\"", "").Replace(".", ",");
                    strVa = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striVa).Replace("\"", "").Replace(".", ",");
                    strVb = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striVb).Replace("\"", "").Replace(".", ",");
                    strVc = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striVc).Replace("\"", "").Replace(".", ",");
                    strIa = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striIa).Replace("\"", "").Replace(".", ",");
                    strIb = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striIb).Replace("\"", "").Replace(".", ",");
                    strIc = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striIc).Replace("\"", "").Replace(".", ",");
                    strNo = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striNo).Replace("\"", "").Replace(".", ",");
                    strTo = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striTo).Replace("\"", "").Replace(".", ",");
                    strTe = GetCSV("/feed/get.json?field=value&id=", DateTime.Now, DateTime.Now, Global.striTe).Replace("\"", "").Replace(".", ",");
                }
                catch { }
                // Exibe na tela
                lblP.Text = strP;
                lblQ.Text = strQ;
                lblS.Text = strS;
                lblVa.Text = strVa;
                lblVb.Text = strVb;
                lblVc.Text = strVc;
                lblIa.Text = strIa;
                lblIb.Text = strIb;
                lblIc.Text = strIc;
                lblNo.Text = strNo;
                lblTo.Text = strTo;
                lblTe.Text = strTe;

                // Atualiza os gráficos:
                NovoPontoNoGrafico(Global.strP, strP);
                NovoPontoNoGrafico(Global.strQ, strQ);
                NovoPontoNoGrafico(Global.strS, strS);

                NovoPontoNoGrafico(Global.strVa, strVa);
                NovoPontoNoGrafico(Global.strVb, strVb);
                NovoPontoNoGrafico(Global.strVc, strVc);

                NovoPontoNoGrafico(Global.strIa, strIa);
                NovoPontoNoGrafico(Global.strIb, strIb);
                NovoPontoNoGrafico(Global.strIc, strIc);

                NovoPontoNoGrafico(Global.strNo, strNo);
                NovoPontoNoGrafico(Global.strTo, strTo);
                NovoPontoNoGrafico(Global.strTe, strTe);
                chartTemperatura.Refresh();
            }
        }

        private void NovoPontoNoGrafico(string Grafico, string Ponto)
        {
            chartTemperatura.Series[Grafico].Points.AddXY(tUltimaAtualizacao, Convert.ToDouble(Ponto));
            chartTemperatura.Series[Grafico].Points.RemoveAt(0);
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
            //    Clipboard.SetText(strComando);
            WebClient Web = new WebClient();
            return Web.DownloadString(strComando);
        }

        private List<RegistroCSV> CSV2Matriz(string strCSV)
        {
            char TerminadorDeLinha = '\n'; // \n (O CSV gerado pela página tem como terminador '\n' mas em outros casos pode ser '\r' ou '\r\n')
            if (strCSV.Length < 2) return new List<RegistroCSV>();
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
                    jj++; // pula o caractere
                } while ((!(strCSV[jj] == TerminadorDeLinha)) & (jj < strCSV.Length - 1));
            }

            while (jj < strCSV.Length - 1)
            {
                // Primeiro campo
                RegistroCSV Registro = new RegistroCSV();
                inicio = ++jj;
                while ((!(strCSV[jj] == Global.SeparadorCSV)) & (jj < strCSV.Length - 1))
                {
                    jj++;
                }
                Registro.Time = strCSV.Substring(inicio, jj - inicio);
                inicio = ++jj;
                while ((!(strCSV[jj] == TerminadorDeLinha)) & (jj < strCSV.Length - 1))
                {
                    jj++;
                }
                Registro.Valor = strCSV.Substring(inicio, jj - inicio).Replace('.', ',');
                Registros.Add(Registro);
            }
            return Registros;
        }

        private void chkPotencia_CheckedChanged(object sender, EventArgs e)
        {
            chkP.Checked = chkQ.Checked = chkS.Checked = chkPotencia.Checked;
        }

        private void chkTensao_CheckedChanged(object sender, EventArgs e)
        {
            chkVa.Checked = chkVb.Checked = chkVc.Checked = chkTensao.Checked;
        }

        private void chkCorrente_CheckedChanged(object sender, EventArgs e)
        {
            chkIa.Checked = chkIb.Checked = chkIc.Checked = chkCorrente.Checked;
        }

        private void chkETC_CheckedChanged(object sender, EventArgs e)
        {
            chkNo.Checked = chkTo.Checked = chkTe.Checked = chkETC.Checked;
        }

        private void btnGraficos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("falta essa parte ainda...");
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("falta essa parte ainda...");
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("falta essa parte ainda...");
        }
    }
}
