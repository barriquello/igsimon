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
        // Banco de dados temporário
        //SQLiteConnection SQLDBTemp;
        // Janela de tempo
        //UInt32 JanelaDeTempo = Uteis.Time2Unix(new DateTime(1970, 1, 1).AddDays(1));

        List<RegistroDB> Registros = new List<RegistroDB>();

        public frmMain()
        {
            InitializeComponent();
        }

        // Cria a ESTRUTURA do gráfico
        private void GerarGrafico()
        {
            // Limpa o gráfico (tudo nele)
            chartTemperatura.Series.Clear();
            chartTemperatura.ChartAreas.Clear();
            chartTemperatura.Legends.Clear();
            // Inserir chartareas
            chartTemperatura.ChartAreas.Add("P");
            chartTemperatura.ChartAreas.Add("V");
            chartTemperatura.ChartAreas.Add("I");
            chartTemperatura.ChartAreas.Add("N");
            chartTemperatura.ChartAreas.Add("T");
            // Adiciona legendas
            for (int jj = 0; jj < chartTemperatura.ChartAreas.Count; jj++)
            {
                // por consequência do chartTemperatura.Legends.clear(), jj será o índice da nova legenda
                chartTemperatura.Legends.Add(chartTemperatura.ChartAreas[jj].Name).LegendItemOrder = LegendItemOrder.Auto;
                chartTemperatura.Legends[jj].Alignment = System.Drawing.StringAlignment.Center; // Alinhamento das legendas
                chartTemperatura.Legends[jj].LegendStyle = LegendStyle.Column; // legendas em uma coluna
            }


            //habilita o zoom
            chartTemperatura.ChartAreas["T"].CursorX.IsUserSelectionEnabled =
                chartTemperatura.ChartAreas["N"].CursorX.IsUserSelectionEnabled =
                chartTemperatura.ChartAreas["I"].CursorX.IsUserSelectionEnabled =
                chartTemperatura.ChartAreas["V"].CursorX.IsUserSelectionEnabled =
                chartTemperatura.ChartAreas["P"].CursorX.IsUserSelectionEnabled = true;
            // resolução máxima
            chartTemperatura.ChartAreas["T"].CursorX.Interval =
                chartTemperatura.ChartAreas["N"].CursorX.Interval =
                chartTemperatura.ChartAreas["I"].CursorX.Interval =
                chartTemperatura.ChartAreas["V"].CursorX.Interval =
                chartTemperatura.ChartAreas["P"].CursorX.Interval = 0.01;
            // Desabilita a escala no eixo X para quase todos os gráficos (exceto no gráfico do nível de óleo, esse fica na parte inferior)
            chartTemperatura.ChartAreas["P"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["V"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["T"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["I"].AxisX.LabelStyle.Enabled = false;
            // Alinhamento dos gráficos das chartareas (alinhados com o gráfico debaixo
            chartTemperatura.ChartAreas["P"].AlignWithChartArea =
                chartTemperatura.ChartAreas["V"].AlignWithChartArea =
                chartTemperatura.ChartAreas["T"].AlignWithChartArea =
                chartTemperatura.ChartAreas["I"].AlignWithChartArea = "N";

            // Etiquetas personalizadas no eixo de nível do óleo
            chartTemperatura.ChartAreas["N"].AxisY.CustomLabels.Add(0, Global.NOleoBaixo, "Baixo");
            chartTemperatura.ChartAreas["N"].AxisY.CustomLabels.Add(Global.NOleoBaixo, Global.NOleoAlto, "Normal");
            chartTemperatura.ChartAreas["N"].AxisY.CustomLabels.Add(Global.NOleoAlto, 10, "Alto");

            // Linhas entre os valores adjacentes:
            chartTemperatura.ChartAreas["N"].AxisY.MajorGrid.Interval = Global.NOleoAlto - Global.NOleoBaixo; // Com essa técnica, se o número que indica o nível do óleo >= diferença entre nível alto e baixo outra(s) linha(s) aparece(m)
            chartTemperatura.ChartAreas["N"].AxisY.MajorGrid.IntervalOffset = Global.NOleoBaixo;
            // Posiciona as várias chartáreas:
            //chartTemperatura.Legends["P"].DockedToChartArea = "P";
            // talvez seja necessário rever esses valores:
            float fTamanhoLegenda = 100f;
            float fLarguraLegenda = 20f;
            float fLargura = 100f * (chartTemperatura.Width - fTamanhoLegenda) / (chartTemperatura.Width * 1f);
            chartTemperatura.ChartAreas["P"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 0f, fLargura, 20f)); // 80% da largura e 20 % da altura do chart
            chartTemperatura.Legends["P"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, 0f, fLarguraLegenda - 0.2f, 20f)); //20 % da largura e 20% da altura

            chartTemperatura.ChartAreas["V"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 20f, fLargura, 20f));
            chartTemperatura.Legends["V"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, 20f, fLarguraLegenda - 0.2f, 20f));

            chartTemperatura.ChartAreas["I"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 40f, fLargura, 20f));
            chartTemperatura.Legends["I"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, 40f, fLarguraLegenda - 0.2f, 20f));

            chartTemperatura.ChartAreas["T"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 60f, fLargura, 20f));
            chartTemperatura.Legends["T"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, 60f, fLarguraLegenda - 0.2f, 20f));

            chartTemperatura.ChartAreas["N"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 80f, fLargura, 20f)); // gráfico do nível do óleo menor por conta da escala
            chartTemperatura.Legends["N"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, 80f, fLarguraLegenda - 0.2f, 20f));
            
            string[] strSeries = Global.strTodas();

            for (int jj = 0; jj < Global.strCategoria.Length; jj++)
            {
                Series srSerie = new Series(strSeries[jj]); // nova série
                srSerie.ChartArea =
                    srSerie.Legend = Global.strCategoria[jj]; // cada série associada com a chartárea e a legenda adequadas
                srSerie.XValueType = ChartValueType.Auto; // Bug do .NET
                srSerie.XValueType = ChartValueType.Time; // Bug do .NET
                srSerie.ChartType = SeriesChartType.StepLine; // gráfico em degraus (não sabemos o que acontece entre duas medidas
                srSerie.BorderWidth = 2; // tamanho da linha

                chartTemperatura.Series.Add(srSerie); // adiciona a série de dados ao gráfico
            }
            /*
            // Buscar informações no banco de dados
            string[] strVariaveis = Global.strTodas();
            string[] Indices = Global.striTodas();
            for (int kk = 0; kk < strVariaveis.Length; kk++)
            {
                //if (strVariaveis[kk] != Global.strNo)
                //{
                Series srTeste = new Series(strVariaveis[kk]);
                srTeste.ChartType = SeriesChartType.StepLine;
                srTeste.XValueType = ChartValueType.Auto;
                srTeste.BorderWidth = 2;

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
                    srTeste.Legend = Global.strCategoria[kk];
                    chartTemperatura.Series.Add(srTeste);
                }
                //}
            }
            foreach (Series Sr in chartTemperatura.Series)
            {
                Sr.XValueType = ChartValueType.Time;
            } //*/
        }

        private void PlotaGrafico(UInt32 Start, UInt32 End)
        {
            // Classifica por horário (no caso de alteração nos limites
            Registros.Sort((x, y) => x.Horario.CompareTo(y.Horario));


            /*
    // Buscar informações no banco de dados
    string[] strVariaveis = Global.strTodas();
    string[] Indices = Global.striTodas();
    for (int kk = 0; kk < strVariaveis.Length; kk++)
    {
        //if (strVariaveis[kk] != Global.strNo)
        //{
        Series srTeste = new Series(strVariaveis[kk]);
        srTeste.ChartType = SeriesChartType.StepLine;
        srTeste.XValueType = ChartValueType.Auto;
        srTeste.BorderWidth = 2;

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
            srTeste.Legend = Global.strCategoria[kk];
            chartTemperatura.Series.Add(srTeste);
        }
        //}
    }
    foreach (Series Sr in chartTemperatura.Series)
    {
        Sr.XValueType = ChartValueType.Time;
    } //*/

        }
        // Busca todas as informações do intervalo informado
        private void BuscaDados(UInt32 Inicio, UInt32 Final = 0)
        {
            // Medir performance:
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();
            //Verifica qual é o último registro no servidor
            string strTime = GetCSV(Global.strComandoHorario, Uteis.Time2Unix(DateTime.Now), Uteis.Time2Unix(DateTime.Now), Global.striP).Replace("\"", "");
            UInt32 Ultimo = Convert.ToUInt32(strTime); // Horário mais recente armazenado no servidor
            UInt32 _Final = Final;

            if (Final == 0) // especificação de data opcional
            {
                _Final = Uteis.Time2Unix(DateTime.Now);
            }
            if (Ultimo > _Final)
            {
                Ultimo = _Final;
            }
            // Busca o comando mais recente armazenado no servidor



            //lista de índices
            string[] strTodas = Global.striTodas();
            // para cada variável do servidor:
            for (int jj = 0; jj < Global.strCategoria.Length; jj++)
            {
                // Busca todos os valores do servidor
                string strTemp = GetCSV(Global.strComandoCSV, Inicio, Ultimo, strTodas[jj]);
                List<RegistroCSV> Dados = CSV2Matriz(strTemp);
                for (int kk = 0; kk < Dados.Count; kk++)
                {
                    UInt32 Horario = Dados[kk].timeUnix();
                    int indice = Registros.FindIndex(x => x.Horario == Horario);

                    if (indice < 0)
                    {
                        Registros.Add(new RegistroDB());
                        indice = Registros.Count - 1;
                    }
                    Registros[indice].Horario = Horario;
                    Registros[indice].P[jj] = (float)Dados[kk].valor();
                }
            }
            //sw.Stop(); Text = sw.Elapsed.ToString();
        }

        /*
        private void SalvarNoDBTemp(UInt32 Horario, double P, double Q, double S, double Va, double Vb, double Vc, double Ia, double Ib, double Ic, double No, double To, double Te)
        {
            string strComando = "INSERT INTO '" + Global.TabelaDados + "' " +
                "('Horario','" +
                Global.strVa + "','" + Global.strVb + "','" + Global.strVc + "','" +
                Global.strIa + "','" + Global.strIb + "','" + Global.strIc + "','" +
                Global.strP + "','" + Global.strQ + "','" + Global.strS + "','" +
                Global.strNo + "','" + Global.strTo + "','" + Global.strTe + "') " +
                "VALUES(" +
                Horario.ToString() + "," +
                Va.ToString() + "," + Vb.ToString() + "," + Vc.ToString() + "," +
                Ia.ToString() + "," + Ib.ToString() + "," + Ic.ToString() + "," +
                P.ToString() + "," + Q.ToString() + "," + S.ToString() + "," +
                No.ToString() + "," + To.ToString() + "," + Te.ToString() + ")";
            using (SQLiteCommand Comando = new SQLiteCommand(strComando, SQLDBTemp))
            {
                Comando.ExecuteNonQuery();
            }
        }//*/

        private void button1_Click(object sender, EventArgs e)
        {
            // rotina para ser executada apenas uma vez
            // Pegar o "time" mais recente:
            string strTime = GetCSV(Global.strComandoHorario,Uteis.Time2Unix( DateTime.Now),Uteis.Time2Unix( DateTime.Now), Global.striP).Replace("\"", "");
            tUltimaAtualizacao = Uteis.Unix2time(Convert.ToUInt32(strTime)); // Horário mais recente armazenado no servidor
            lblMensagens.Text = "Último registro: " + tUltimaAtualizacao.ToString();
            BuscaDados(Uteis.Time2Unix(DateTime.Now.AddDays(-1)));
            GerarGrafico();
            PlotaGrafico(Uteis.Time2Unix(DateTime.Now.AddDays(-1)), Uteis.Time2Unix(DateTime.Now));

            //picStatus.Image = InterfaceDesktop.Properties.Resources.Vermelho;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            // Botão "Configurações"
            Form frmconfig1 = new frmConfig();
            this.Hide();
            Global.restart = false;
            Global.ConfigObriatoria = false;
            frmconfig1.ShowDialog();
            if (Global.restart)
            {
                Global.restart = false;
                MessageBox.Show("É necessário nova autenticação");
                this.Close();
            }
            else
            {
                this.Show();
            }
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
                        while (!Global.restart)
                        {
                            Global.ConfigObriatoria = true;
                            frmConfig Config = new frmConfig();
                            Config.ShowDialog();
                        }
                        Global.ConfigObriatoria = false;
                        // Reinicia a rotina para [tentar] carregar as configurações
                        frmMain_Load(new object(), new EventArgs());
                        Global.restart = false;
                        return;
                    }
                }
            }
            // Buscar índices no servidor:
            string Requisicao = Global.Servidor + Global.strComandoFeedList + Global.APIKey;

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
            /*// Cria o banco de dados virtual local
            string strGerarTabela = "CREATE TABLE IF NOT EXISTS '" + Global.TabelaDados + "' (" +
                "'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                "'Horario' INTEGER,'" + // Horário (UNIX)
                Global.strVa + "' NUMERIC,'" + Global.strVb + "' NUMERIC,'" + Global.strVc + "' NUMERIC'" + //Tensões
                Global.strIa + "' NUMERIC,'" + Global.strIb + "' NUMERIC,'" + Global.strIc + "' NUMERIC,'" + //Correntes
                Global.strP + "' NUMERIC,'" + Global.strQ + "' NUMERIC,'" + Global.strS + "' NUMERIC,'" + // Potências
                Global.strNo + "' NUMERIC,'" + Global.strTo + "' NUMERIC,'" + Global.strTe + "' NUMERIC" + // Nível do óleo e temperaturas
                ");";
            SQLDBTemp = new SQLiteConnection("Data Source=:memory:");
            SQLDBTemp.Open();
            using (SQLiteCommand SQLCom = new SQLiteCommand(strGerarTabela, SQLDBTemp))
            {
                SQLCom.ExecuteNonQuery();
            }
            //SQLDBTemp.Close();//*/
            // Verifica se alguma variável não está associada a um índice
            if ((Global.striP == "") | (Global.striQ == "") | (Global.striS == "") | (Global.striVa == "") | (Global.striVb == "") | (Global.striVc == "") | (Global.striIa == "") | (Global.striIb == "") | (Global.striIc == "") | (Global.striNo == "") | (Global.striTo == "") | (Global.striTe == ""))
            {
                Global.ConfigObriatoria = true;
                while (!Global.restart)
                {
                    MessageBox.Show("Verifique os nomes das variáveis");
                    frmConfig Config = new frmConfig();
                    Config.ShowDialog();
                }
                Global.ConfigObriatoria = false;
                Global.restart = false;
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
            //if (true)
            //{
            string strTime = GetCSV(Global.strComandoHorario, 0, 0, Global.striP).Replace("\"","");//*não importa o resto*/ Uteis.Time2Unix(DateTime.Now), Uteis.Time2Unix(DateTime.Now), Global.striP).Replace("\"", "");

                int timerInterval = 15000;
                if (tmrGraficos.Interval != timerInterval)
                {
                    tUltimaAtualizacao = Uteis.Unix2time(Convert.ToUInt32(strTime)); // Horário mais recente armazenado no servidor
                    lblMensagens.Text = "Último registro: " + tUltimaAtualizacao.ToString();
                    BuscaDados(DateTime.Now.AddDays(-1),DateTime.Now);

                    GerarGrafico();
                    PlotaGrafico(DateTime.Now.AddDays(-1),DateTime.Now);
                }

                tmrGraficos.Interval = timerInterval; // 15 segundos
            //}
            // Atualiza os gráficos:


            //tmrGraficos.Enabled = false; return;
            //strTime = GetCSV(Global.strComandoHorario,Uteis.Time2Unix( DateTime.Now), Uteis.Time2Unix(DateTime.Now), Global.striP).Replace("\"", "");
            DateTime NovaAtualizaco = Uteis.Unix2time(Convert.ToUInt32(strTime));
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
                    strP  = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striP).Replace("\"", "").Replace(".", ",");
                    strQ  = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striQ).Replace("\"", "").Replace(".", ",");
                    strS  = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striS).Replace("\"", "").Replace(".", ",");
                    strVa = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striVa).Replace("\"", "").Replace(".", ",");
                    strVb = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striVb).Replace("\"", "").Replace(".", ",");
                    strVc = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striVc).Replace("\"", "").Replace(".", ",");
                    strIa = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striIa).Replace("\"", "").Replace(".", ",");
                    strIb = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striIb).Replace("\"", "").Replace(".", ",");
                    strIc = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striIc).Replace("\"", "").Replace(".", ",");
                    strNo = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striNo).Replace("\"", "").Replace(".", ",");
                    strTo = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striTo).Replace("\"", "").Replace(".", ",");
                    strTe = GetCSV(Global.strComandoValor,Uteis.Time2Unix(DateTime.Now),Uteis.Time2Unix(DateTime.Now), Global.striTe).Replace("\"", "").Replace(".", ",");
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
                aTo.Value(Convert.ToSingle(strTo));
                aTe.Value(Convert.ToSingle(strTe));
                
                lblTe.Text = strTe;

                int Noleo = Convert.ToInt32(strNo);

                if ((Noleo < Global.NOleoBaixo) | (Noleo > Global.NOleoAlto))
                {
                    picStatus.Image = InterfaceDesktop.Properties.Resources.Vermelho;
                }
                else
                {
                    picStatus.Image = InterfaceDesktop.Properties.Resources.Verde;
                }

                /* // Atualiza os gráficos:
                NovoPontoNoGrafico(Global.strP, strP); NovoPontoNoGrafico(Global.strQ, strQ); NovoPontoNoGrafico(Global.strS, strS);
                NovoPontoNoGrafico(Global.strVa, strVa); NovoPontoNoGrafico(Global.strVb, strVb); NovoPontoNoGrafico(Global.strVc, strVc);
                NovoPontoNoGrafico(Global.strIa, strIa); NovoPontoNoGrafico(Global.strIb, strIb); NovoPontoNoGrafico(Global.strIc, strIc);
                //NovoPontoNoGrafico(Global.strNo, strNo); //essa não vai pro gráfico
                NovoPontoNoGrafico(Global.strTo, strTo); NovoPontoNoGrafico(Global.strTe, strTe); NovoPontoNoGrafico(Global.strNo, strNo);
                chartTemperatura.Refresh();//*/
            }
        }

        private void PlotaGrafico(DateTime dateTime1, DateTime dateTime2)
        {
            PlotaGrafico(Uteis.Time2Unix(dateTime1), Uteis.Time2Unix(dateTime2));
        }

        private void BuscaDados(DateTime dateTime1, DateTime dateTime2)
        {
            BuscaDados(Uteis.Time2Unix(dateTime1), Uteis.Time2Unix(dateTime2));
        }

        /*private void NovoPontoNoGrafico(string Grafico, string Ponto)
        {
            // Redesenhar todas as séries
            chartTemperatura.Series[Grafico].Points.Clear();
            chartTemperatura.Series[Grafico].Points.AddXY(tUltimaAtualizacao, Convert.ToDouble(Ponto));
            //chartTemperatura.Series[Grafico].Points.RemoveAt(0);
        }//*/


        // Esta rotina deve ser utilizada considerando um intervalo de tempo limitado
        /// <summary>Retorna um arquivo CSV</summary>
        /// <param name="Comando">Comando para o servidor</param>
        /// <param name="Inicio">Tempo inicial</param>
        /// <param name="Fim">Tempo final</param>
        /// <param name="ID">ID do feed</param>
        /// <returns>String CSV</returns>
        private string GetCSV(string Comando, UInt32 Inicio, UInt32 Fim, string ID)
        {
            string strComando = Global.Servidor + Comando + ID +
                "&start=" + Inicio.ToString() +
                "&end=" + Fim.ToString() +
                "&apikey=" + Global.APIKey + "&interval=30&timeformat=0";
            WebClient Web = new WebClient();
            return Web.DownloadString(strComando);
        }

        // Transforma uma lista CSV em uma matriz
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
