using System; // Tipos de variáveis menos comuns
using System.Net; // Comunicação web
using System.Text; // Codificação de texto
using System.Windows.Forms; // Formulários
using Newtonsoft.Json; // JSON
using System.Collections.Generic; // Variáveis anonimas (json)
using System.Data.SQLite; // Bancos de dados
using System.Windows.Forms.DataVisualization.Charting; //Gráficos
using System.Linq;

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
        TimeSpan JanelaDeTempo = new TimeSpan(1, 0, 0, 0); // Um dia exato
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
            for (int kk = 0; kk < chartTemperatura.ChartAreas.Count; kk++)
            {
                // Habilita o zoom
                chartTemperatura.ChartAreas[kk].CursorX.IsUserSelectionEnabled = true;
                // Resolução máxima
                chartTemperatura.ChartAreas[kk].CursorX.IntervalType = DateTimeIntervalType.Minutes;
                chartTemperatura.ChartAreas[kk].CursorX.Interval = 10;// minutos
                chartTemperatura.ChartAreas[kk].AxisX.ScaleView.SmallScrollMinSize = 1;
                chartTemperatura.ChartAreas[kk].AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Minutes;
            }
            // desabilita as barras de rolagem dos gráficos de cima
            chartTemperatura.ChartAreas["P"].AxisX.ScrollBar.Enabled =
                chartTemperatura.ChartAreas["V"].AxisX.ScrollBar.Enabled =
                chartTemperatura.ChartAreas["I"].AxisX.ScrollBar.Enabled =
                chartTemperatura.ChartAreas["T"].AxisX.ScrollBar.Enabled = false;

            // Desabilita a escala no eixo X para quase todos os gráficos (exceto no gráfico do nível de óleo, esse fica na parte inferior)
            chartTemperatura.ChartAreas["P"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["V"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["T"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["I"].AxisX.LabelStyle.Enabled = false;
            // Alinhamento dos gráficos das chartareas (alinhados com o gráfico debaixo)
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
                //srSerie.XValueType = ChartValueType.Auto; // Bug do .NET
                srSerie.XValueType = ChartValueType.Time; //
                srSerie.ChartType = SeriesChartType.StepLine; // gráfico em degraus (não sabemos o que acontece entre duas medidas
                srSerie.BorderWidth = 2; // tamanho da linha

                chartTemperatura.Series.Add(srSerie); // adiciona a série de dados ao gráfico
            }
        }

        private void LimpaSeries()
        {
            for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
                chartTemperatura.Series[jj].Points.Clear();
        }

        private void PlotaGrafico(UInt32 Start, UInt32 End)
        {
            // Classifica por horário (no caso de alteração nos limites
            Registros.OrderBy(RegistroDB => RegistroDB.Horario);
            // Elimina registros mais antigos com a finalidade de reduzir o uso de memória
            while (Registros.Count > Global.RegistrosMAXIMO)
            {
                Registros.RemoveAt(0);
            }
            SuspendLayout();
            for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
            {
                chartTemperatura.Series[jj].XValueType = ChartValueType.Auto;//bug do .NET
            }
            System.Diagnostics.Stopwatch A = new System.Diagnostics.Stopwatch();
            for (int mm = 0; mm < Registros.Count; mm++)
            {
                if (Registros[mm].Horario >= Start)
                    if (Registros[mm].Horario > End)
                    {
                        break;
                    }
                    else
                    {
                        AdicionaPonto(Registros[mm]);
                    }
            }
            for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
            {
                if ((End - Start) > Uteis.Time2Unix(new DateTime(1970, 1, 1).AddDays(1))) // Se o intervalo for maior que 24 horas, considerar a escala em data, se não for, considerar como hora
                {
                    chartTemperatura.Series[jj].XValueType = ChartValueType.DateTime;
                }
                else
                {
                    chartTemperatura.Series[jj].XValueType = ChartValueType.Time;
                }
            }
            ResumeLayout();
        }
        // Adiciona pontos ao gráfico
        private void AdicionaPonto(RegistroDB registro)
        {
            string[] strTodas = Global.strTodas();
            DateTime Horario = Uteis.Unix2time(registro.Horario);
            for (int mm = 0; mm < Global.intIndiceRegistro.Length; mm++)
            {
                chartTemperatura.Series[strTodas[mm]].Points.AddXY(Horario, registro.P[Global.intIndiceRegistro[mm]]);
            }
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
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();

            for (int jj = 0; jj < Global.strCategoria.Length; jj++)
            {
                // Busca todos os valores do servidor
                string strTemp = GetCSV(Global.strComandoCSV, Inicio, Ultimo, strTodas[jj]);

                List<RegistroCSV> Dados = CSV2Matriz(strTemp);

                for (int kk = 0; kk < Dados.Count; kk++)
                {
                    UInt32 Horario_ = Dados[kk].timeUnix();
                    int indice = Registros.FindIndex(x => x.Horario == Horario_); //2,5s
                    //int indice = Registros.IndexOf(new RegistroDB() { Horario = Horario_ }); //8 segundos
                    //int indice = Registros.TakeWhile(hora => hora.Horario !=Horario_ ).Count(); // 3,5s
                    if (indice < 0) // se não existe vamos criar um novo
                    //if (indice >= Registros.Count) ;
                    {
                        Registros.Add(new RegistroDB());
                        indice = Registros.Count - 1;
                    }
                    Registros[indice].Horario = Horario_;
                    Registros[indice].P[Global.intIndiceRegistro[jj]] = (float)Dados[kk].valor();
                }

            }
            sw.Stop(); Text = "Total " + sw.ElapsedMilliseconds.ToString() + " ms.";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // rotina para ser executada apenas uma vez
            // Pegar o "time" mais recente:
            string strTime = GetCSV(Global.strComandoHorario, Uteis.Time2Unix(DateTime.Now), Uteis.Time2Unix(DateTime.Now), Global.striP).Replace("\"", "");
            tUltimaAtualizacao = Uteis.Unix2time(Convert.ToUInt32(strTime)); // Horário mais recente armazenado no servidor
            lblMensagens.Text = "Último registro: " + tUltimaAtualizacao.ToString();
            BuscaDados(Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo)), Uteis.Time2Unix(tUltimaAtualizacao));
            GerarGrafico();
            PlotaGrafico(Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo)), Uteis.Time2Unix(tUltimaAtualizacao));
            //chartTemperatura.Printing.PrintPreview();
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
            System.Diagnostics.Process Processo = System.Diagnostics.Process.GetCurrentProcess();
            lblMEM.Text = string.Format("| {0} registros na memória ", Registros.Count);
            lblMEM.Text += string.Format("| Memória utilizada = {0:#,#0} MB ", Processo.PeakPagedMemorySize64 / 1024 / 1024);
        }

        // Atualiza os gráficos
        private void tmrGraficos_Tick(object sender, EventArgs e)
        {
            bool Plotar = false;
            // Rotina para buscar novas informações no servidor e exibir na tela
            string strTime = GetCSV(Global.strComandoHorario, 0, 0, Global.striP).Replace("\"", "");
            DateTime VelhaUltimaAtualizacao = tUltimaAtualizacao;
            if (strTime.Length > 1)
                tUltimaAtualizacao = Uteis.Unix2time(Convert.ToUInt32(strTime)); // Horário mais recente armazenado no servidor
            else
                return;
            tmrGraficos.Enabled = false;
            if (tmrGraficos.Interval != Global.intTaxaAtualizacao)
            {
                // Primeira execução
                BuscaDados(tUltimaAtualizacao.Subtract(JanelaDeTempo), tUltimaAtualizacao);
                GerarGrafico();
                if (Registros.Count > 0)
                    Plotar = true;
            }
            else
            {
                if (tUltimaAtualizacao.CompareTo(VelhaUltimaAtualizacao) != 0)
                {
                    Plotar = true;
                    BuscaDados(VelhaUltimaAtualizacao, tUltimaAtualizacao);
                    LimpaSeries();

                }

            }
            if (Plotar)
            {
                //GerarGrafico();
                PlotaGrafico(tUltimaAtualizacao.Subtract(JanelaDeTempo), tUltimaAtualizacao);
                lblMensagens.Text = string.Format("Último registro: {0}", tUltimaAtualizacao);
            }

            // Atualiza as etiquetas
            if (Registros.Count > 0)
                AtualizaLabels(Registros[Registros.Count - 1]);

            //tmrGraficos.Enabled = false; return;
            //strTime = GetCSV(Global.strComandoHorario,Uteis.Time2Unix( DateTime.Now), Uteis.Time2Unix(DateTime.Now), Global.striP).Replace("\"", "");
            DateTime NovaAtualizaco = Uteis.Unix2time(Convert.ToUInt32(strTime));

            tmrGraficos.Interval = Global.intTaxaAtualizacao;
            tmrGraficos.Enabled = true;
        }

        private void AtualizaLabels(RegistroDB registroDB)
        {
            //{ 0       1   2       3       4   5       6       7       8   9       10      11
            //{ strP, strQ, strS, strVa, strVb, strVc, strIa, strIb, strIc, strNo, strTo, strTe };
            // Exibe na tela:
            // Texto
            lblP.Text = registroDB.P[0].ToString(); lblQ.Text = registroDB.P[1].ToString(); lblS.Text = registroDB.P[2].ToString();
            lblVa.Text = registroDB.P[3].ToString(); lblVb.Text = registroDB.P[4].ToString(); lblVc.Text = registroDB.P[5].ToString();
            lblIa.Text = registroDB.P[6].ToString(); lblIb.Text = registroDB.P[7].ToString(); lblIc.Text = registroDB.P[8].ToString();
            lblNo.Text = registroDB.P[9].ToString(); lblTo.Text = registroDB.P[10].ToString(); lblTe.Text = registroDB.P[11].ToString();
            // Ponteiros
            aTo.Value(registroDB.P[10]); aTe.Value(registroDB.P[11]);
            // "Led"
            int Noleo = Convert.ToInt32(registroDB.P[9]);
            picStatus.Image = ((Noleo < Global.NOleoBaixo) | (Noleo > Global.NOleoAlto)) ? picStatus.Image = InterfaceDesktop.Properties.Resources.Vermelho : picStatus.Image = InterfaceDesktop.Properties.Resources.Verde;
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
            try
            {
                string strComando = Global.Servidor + Comando + ID +
                    "&start=" + Inicio.ToString() +
                    "&end=" + Fim.ToString() +
                    "&apikey=" + Global.APIKey + "&interval=1&timeformat=0";
                WebClient Web = new WebClient();
                strComando = Web.DownloadString(strComando);
                // Verifica a APIKey
                if (strComando.Contains("API"))
                {
                    MessageBox.Show("APIKey incorreta");
                    this.Hide();
                    frmConfig Config = new frmConfig();
                    Config.ShowDialog();
                    this.Close();
                }
                return strComando;
            }
            catch
            {
                return "";
            }
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rd7d.Checked)
            {
                JanelaDeTempo = new TimeSpan(7, 0, 0, 0);
            }
            if (rd1d.Checked)
            {
                JanelaDeTempo = new TimeSpan(1, 0, 0, 0, 0);
            }
            if (rd12h.Checked)
            {
                JanelaDeTempo = new TimeSpan(12, 0, 0);
            }
            if (rd1h.Checked)
            {
                JanelaDeTempo = new TimeSpan(1, 0, 0);
            }
        }
    }
}
