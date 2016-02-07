using System; // Tipos de variáveis menos comuns
using System.Net; // Comunicação web
using System.Text; // Codificação de texto
using System.Windows.Forms; // Formulários
using System.Collections.Generic; // Variáveis anonimas (json)
using System.Windows.Forms.DataVisualization.Charting; //Gráficos
using System.Linq;
using System.IO;
using System.Drawing;

namespace InterfaceDesktop
{
    public partial class frmMain : Form
    {
        /// <summary>Componente para comunicação via internet</summary>
        WebClient ServidorWeb = new WebClient();
        /// <summary>Data do registro mais recente no servidor</summary>
        DateTime tUltimaAtualizacao;
        /// <summary>Registro mais antigo buscado</summary>
        UInt32 PrimeiroValor = UInt32.MaxValue;
        /// <summary>Alarme</summary>
        int blink = 1;
        Color[] Fundo = new Color[] { Color.Red, Color.Yellow };
        /// <summary>Janela de tempo</summary>
        TimeSpan JanelaDeTempo = new TimeSpan(1, 0, 0, 0); // Um dia exato
        /// <summary>Matriz de registros</summary>
        public static List<RegistroDB> Registros = new List<RegistroDB>();
        private bool Encerrando = false;

        public frmMain()
        {
            InitializeComponent();
        }

        // Cria a ESTRUTURA do gráfico
        private bool GerarGrafico()
        {
            // Limpa o gráfico (tudo nele)
            chartTemperatura.Series.Clear();
            chartTemperatura.ChartAreas.Clear();
            chartTemperatura.Legends.Clear();
            // Inserir chartareas
            chartTemperatura.ChartAreas.Add("P");
            chartTemperatura.ChartAreas.Add("Vl");
            chartTemperatura.ChartAreas.Add("Vf");
            chartTemperatura.ChartAreas.Add("I");
            chartTemperatura.ChartAreas.Add("T");

            // Adiciona legendas
            for (int kk = 0; kk < chartTemperatura.ChartAreas.Count; kk++)
            {
                // por consequência do chartTemperatura.Legends.clear(), jj será o índice da nova legenda
                chartTemperatura.Legends.Add(chartTemperatura.ChartAreas[kk].Name).LegendItemOrder = LegendItemOrder.Auto;
                chartTemperatura.Legends[kk].Alignment = System.Drawing.StringAlignment.Center; // Alinhamento das legendas
                chartTemperatura.Legends[kk].LegendStyle = LegendStyle.Column; // legendas em uma coluna
                chartTemperatura.Legends[kk].BackColor = chartTemperatura.BackColor;
                // Habilita os cursores
                chartTemperatura.ChartAreas[kk].CursorX.IsUserEnabled = true;
                chartTemperatura.ChartAreas[kk].CursorX.LineWidth = 2;
                chartTemperatura.ChartAreas[kk].CursorX.LineColor = System.Drawing.Color.Red;
                chartTemperatura.ChartAreas[kk].CursorX.LineDashStyle = ChartDashStyle.Dot;
                chartTemperatura.ChartAreas[kk].CursorX.SelectionColor = System.Drawing.Color.DeepSkyBlue;
                // Melhoras no visual
                chartTemperatura.ChartAreas[kk].AxisX.ScrollBar.Size = 10;
                chartTemperatura.ChartAreas[kk].AxisX.ScrollBar.IsPositionedInside = false;
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
                chartTemperatura.ChartAreas["Vl"].AxisX.ScrollBar.Enabled =
                chartTemperatura.ChartAreas["Vf"].AxisX.ScrollBar.Enabled =
                chartTemperatura.ChartAreas["I"].AxisX.ScrollBar.Enabled = false;
            // Remove o botão zoom out

            // Desabilita a escala no eixo X para quase todos os gráficos (exceto no gráfico do nível de óleo, esse fica na parte inferior)
            chartTemperatura.ChartAreas["P"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["Vl"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["Vf"].AxisX.LabelStyle.Enabled =
                //chartTemperatura.ChartAreas["T"].AxisX.LabelStyle.Enabled =
                chartTemperatura.ChartAreas["I"].AxisX.LabelStyle.Enabled = false;
            // Alinhamento dos gráficos das chartareas (alinhados com o gráfico debaixo)
            chartTemperatura.ChartAreas["P"].AlignWithChartArea =
                chartTemperatura.ChartAreas["Vl"].AlignWithChartArea =
                chartTemperatura.ChartAreas["Vf"].AlignWithChartArea =
                //chartTemperatura.ChartAreas["T"].AlignWithChartArea =
                chartTemperatura.ChartAreas["I"].AlignWithChartArea = "T";

            // Posiciona as várias chartáreas:
            // talvez seja necessário rever esses valores:
            float fLarguraLegenda = 15f; //%
            float fAltura = 20f;
            float fLargura = 100 - fLarguraLegenda; 
            chartTemperatura.ChartAreas["P"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 0f, fLargura, fAltura)); // 80% da largura e 20 % da altura do chart
            chartTemperatura.Legends["P"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, 0f, fLarguraLegenda - 0.2f, fAltura)); //20 % da largura e 20% da altura

            chartTemperatura.ChartAreas["Vl"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 20f, fLargura, fAltura));
            chartTemperatura.Legends["Vl"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, fAltura, fLarguraLegenda - 0.2f, fAltura));

            chartTemperatura.ChartAreas["Vf"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 40f, fLargura, fAltura));
            chartTemperatura.Legends["Vf"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, fAltura * 2, fLarguraLegenda - 0.2f, fAltura));

            chartTemperatura.ChartAreas["I"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 60f, fLargura, fAltura));
            chartTemperatura.Legends["I"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, fAltura * 3, fLarguraLegenda - 0.2f, fAltura));

            chartTemperatura.ChartAreas["T"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 80f, fLargura, fAltura));
            chartTemperatura.Legends["T"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, fAltura * 4, fLarguraLegenda - 0.2f, fAltura));

            //Título nas legendas
            chartTemperatura.Legends["P"].Title = "Potência";
            chartTemperatura.Legends["Vl"].Title = "Tensão de Linha";
            chartTemperatura.Legends["Vf"].Title = "Tensão de Fase";
            chartTemperatura.Legends["I"].Title = "Corrente";
            chartTemperatura.Legends["T"].Title = "Temperatura";
            //chartTemperatura.Legends["N"].Title = "Nível";
            for (int mm = 0; mm < chartTemperatura.Legends.Count; mm++)
            {
                // Alinhamento do título da legenda
                chartTemperatura.Legends[mm].TitleAlignment = System.Drawing.StringAlignment.Near;
                // linha separando o título da legenda
                chartTemperatura.Legends[mm].TitleSeparator = LegendSeparatorStyle.Line;
            }
            FeedServidor[] strSeries = Variaveis.strVariaveis();

            for (int jj = 0; jj < strSeries.Length; jj++)
            {
                if (Func2str(strSeries[jj].Funcao) != "")
                {
                    Series srSerie = new Series(strSeries[jj].NomeFeed); // nova série
                    srSerie.Legend = //"LegendaOculta";
                        srSerie.ChartArea = Func2str(strSeries[jj].Funcao);// cada série associada com a chartárea e a legenda adequadas
                    srSerie.XValueType = ChartValueType.Time; //
                    srSerie.ChartType = SeriesChartType.StepLine; // gráfico em degraus (não sabemos o que acontece entre duas medidas
                    srSerie.BorderWidth = 2; // tamanho da linha
                    srSerie.Color = strSeries[jj].Cor;
                    try
                    {
                        chartTemperatura.Series.Add(srSerie); // adiciona a série de dados ao gráfico
                    }
                    catch
                    {
                        MessageBox.Show(string.Format("Erro ao gerar o gráfico\nVerifique se há algum nome de variável repetido.\nEm especial a variável {0}", strSeries[jj].NomeFeed));
                        frmConfig Config = new frmConfig();
                        this.Hide();
                        tmrRelogio.Enabled = tmrGraficos.Enabled = false;
                        Config.ShowDialog();
                        this.Close();
                        return false;
                    }
                }
            }
            return true;
        }

        private string Func2str(func funcao)
        {
            if (funcao == func.Il) return "I";
            if (funcao == func.Po) return "P";
            if (funcao == func.Te) return "T";
            if (funcao == func.Vf) return "Vf";
            if (funcao == func.Vl) return "Vl";
            return "";

        }

        private void LimpaSeries()
        {
            for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
                chartTemperatura.Series[jj].Points.Clear();
        }

        private void PlotaGrafico(UInt32 Start, UInt32 End)
        {
            // Classifica por horário (no caso de alteração nos limites
            Registros =
                Registros.OrderBy(RegistroDB => RegistroDB.Horario).ToList<RegistroDB>();
            // Elimina registros mais antigos com a finalidade de reduzir o uso de memória
            while (Registros.Count > Global.intRegistrosMAXIMO)
            {
                Registros.RemoveAt(0);
            }
            SuspendLayout(); chartTemperatura.Series.SuspendUpdates();
            for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
            {
                chartTemperatura.Series[jj].XValueType = ChartValueType.Auto;//bug do .NET
            }
            FeedServidor[] strTodas = Variaveis.strVariaveis();
            for (int mm = 0; mm < Registros.Count; mm++)
            {
                if (Registros[mm].Horario >= Start)
                    if (Registros[mm].Horario > End)
                    {
                        break; // Os dados estão em ordem cronológica
                    }
                    else
                    {
                        DateTime Horario = Uteis.Unix2time(Registros[mm].Horario);
                        for (int kk = 0; kk < strTodas.Length; kk++)
                        {
                            if (Func2str(strTodas[kk].Funcao) != "")
                                chartTemperatura.Series[strTodas[kk].NomeFeed].Points.AddXY(Horario, Registros[mm].P[strTodas[kk].indice]);
                        }
                    }
            }
            ChartValueType TipoJanela;
            if ((End - Start) > Uteis.Time2Unix(new DateTime(1970, 1, 1).AddDays(1)))
            {
                TipoJanela = ChartValueType.DateTime;
            }
            else
            {
                TipoJanela = ChartValueType.Time;
            }
            for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
            {
                chartTemperatura.Series[jj].XValueType = TipoJanela;
            }

            ResumeLayout(); chartTemperatura.Series.ResumeUpdates();
        }

        // Busca todas as informações do intervalo informado
        private void BuscaDados(UInt32 Inicio, UInt32 Final)
        {
            toolStrip1.Enabled =
                tmrGraficos.Enabled =
                chartTemperatura.Enabled = false;
            List<RegistroDB> Registros2 = new List<RegistroDB>();
            //Verifica qual é o último registro no servidor
            string strTime = GetCSV(ComandosCSV.strComandoHorario, Uteis.Time2Unix(DateTime.Now), Uteis.Time2Unix(DateTime.Now), Variaveis.fP.IndiceFeed).Replace("\"", "");
            UInt32 Ultimo = Uteis.Time2Unix(DTData2DateTime(strTime)); // Horário mais recente armazenado no servidor
            UInt32 _Final = Final;
            if (PrimeiroValor > Inicio)
            {
                PrimeiroValor = Inicio;
            }
            if (Ultimo > _Final)
            {
                Ultimo = _Final;
            }
            // Busca o comando mais recente armazenado no servidor

            //lista de índices
            FeedServidor[] strTodas = Variaveis.strVariaveis();
            // para cada variável do servidor:
            List<int> Salvar = new List<int>();

            UInt32 Inicio2 = Inicio;
            UInt32 janela = 24 * 60 * 60; // 1 dia
            toolStripProgressBar1.Maximum = 1000; //100%
            float progresso = 0;
            float passo = 1000 / ((float)(strTodas.Length + 1) * (float)(Ultimo - Inicio) / (float)janela);
            do
            {
                Salvar.Clear();
                Registros2.Clear();
                UInt32 final;
                if ((Inicio2 + janela) > Ultimo)
                {
                    final = Ultimo;
                }
                else
                {
                    final = Inicio2 + janela;
                }

                for (int jj = 0; jj < strTodas.Length; jj++)
                {
                    try
                    {
                        toolStripProgressBar1.Value = (int)(progresso);
                    }
                    catch
                    {
                        toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
                    }
                    progresso += passo;
                    tmrGraficos.Enabled = false;
                    // Busca todos os valores do servidor
                    string strTemp = GetCSV(ComandosCSV.strComandoCSV, Inicio2, final, strTodas[jj].IndiceFeed);
                    tmrGraficos.Enabled = false;
                    Application.DoEvents();
                    if (Encerrando) return;
                    tmrGraficos.Enabled = false;
                    List<RegistroCSV> Dados = CSV2Matriz(strTemp);

                    // Guarda os dados em uma variável temporária
                    UInt32 Horario_;
                    for (int kk = 0; kk < Dados.Count; kk++)
                    {
                        Horario_ = Dados[kk].timeUnix();
                        int indice = Registros2.FindIndex(x => x.Horario == Horario_);
                        if (indice < 0) // se não existe vamos criar um novo
                        {
                            Registros2.Add(new RegistroDB() { Horario = Horario_ });
                            indice = Registros2.Count - 1;
                        }
                        Registros2[indice].P[strTodas[jj].indice] = (float)Dados[kk].valor();
                    }

                }
                for (int mm = 0; mm < Registros2.Count; mm++)
                {
                    int indice = Registros.FindIndex(x => x.Horario == Registros2[mm].Horario); //2,5s

                    if (indice < 0)
                    {
                        Registros.Add(Registros2[mm]);
                        Salvar.Add(mm);
                    }
                }
                Inicio2 += janela;
                if (Salvar.Count > 0)
                {
                    HashSet<string> Conteudo;
                    DateTime Data = new DateTime(1970, 1, 1);
                    DateTime DataNova = Uteis.Unix2time(Registros2[Salvar[0]].Horario);
                    string Arquivo = Path.Combine(Application.StartupPath, ComandosCSV.ArquivoCSV(DataNova));
                    StreamWriter Gravar;
                    string strLinha = "";
                    if (!(new FileInfo(Arquivo).Exists))
                    {
                        Conteudo = new HashSet<string>();
                        Gravar = new StreamWriter(Arquivo, true);
                        // Gera Arquivo CSV com cabeçalho
                        strLinha = "Horario";
                        for (int mmm = 0; mmm < strTodas.Length; mmm++)
                        {
                            strLinha += Global.charSeparadorCSV + strTodas[mmm].NomeFeed;
                        }
                        Gravar.WriteLine(strLinha);
                    }
                    else
                    {
                        Conteudo = new HashSet<string>(File.ReadAllLines(Arquivo));
                        Gravar = new StreamWriter(Arquivo, true);
                    }
                    for (int mm = 0; mm < Salvar.Count; mm++)
                    {
                        DataNova = Uteis.Unix2time(Registros2[Salvar[mm]].Horario);
                        if (Data.Date != DataNova.Date)
                        {
                            Data = DataNova;
                            Gravar.Dispose();
                            Arquivo = Path.Combine(Application.StartupPath, ComandosCSV.ArquivoCSV(Data));
                            if (!(new FileInfo(Arquivo).Exists))
                            {
                                Conteudo = new HashSet<string>();
                                Gravar = new StreamWriter(Arquivo, true);
                                // Gera Arquivo CSV com cabeçalho
                                strLinha = "Horario";
                                for (int mmm = 0; mmm < strTodas.Length; mmm++)
                                {
                                    strLinha += Global.charSeparadorCSV + strTodas[mmm].NomeFeed;
                                }
                                Gravar.WriteLine(strLinha);
                            }
                            else
                            {
                                Conteudo = new HashSet<string>(File.ReadAllLines(Arquivo));
                                Gravar = new StreamWriter(Arquivo, true);
                            }
                        }
                        SalvarCSV(Registros2[Salvar[mm]], Gravar, Conteudo);

                    }
                    Gravar.Close();
                    while (Registros.Count > Global.intRegistrosMAXIMO)
                    {
                        Registros.RemoveAt(0);
                    }
                }
            } while (Inicio2 < Ultimo);
            Registros =
                Registros.OrderBy(RegistroDB => RegistroDB.Horario).ToList<RegistroDB>();
            toolStripProgressBar1.Value = 0;
            toolStrip1.Enabled =
                chartTemperatura.Enabled = true;
        }

        /// <summary>Salva num arquivo CSV os dados</summary>
        private void SalvarCSV(RegistroDB reg, StreamWriter Gravar, HashSet<string> Lista)
        {
            System.Globalization.NumberFormatInfo SeparadorDecimal = System.Globalization.NumberFormatInfo.InvariantInfo;
            string HorarioNoCSV = reg.Horario.ToString();
            if (!Lista.Any(x => x.StartsWith(HorarioNoCSV)))
            {
                StringBuilder Linha = new StringBuilder(HorarioNoCSV);
                for (int jj = 0; jj < reg.P.Length; jj++)
                {
                    Linha.Append(Global.charSeparadorCSV);
                    Linha.Append(reg.P[jj].ToString(SeparadorDecimal));
                }
                Gravar.WriteLine(Linha);
            }
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            // Buscar índices no servidor:
            string Requisicao = Servidor.Server + ComandosCSV.strComandoFeedList + Servidor.APIKey;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            try
            {
                Requisicao = ServidorWeb.DownloadString(Requisicao);
                List<Feed> Fdd = json.json2Feed(Requisicao);
                for (int kk = 0; kk < Fdd.Count; kk++)
                {

                    if (Fdd[kk].Nome == Variaveis.fEP.NomeFeed)
                        Variaveis.fEP.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fEQ.NomeFeed)
                        Variaveis.fEQ.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fES.NomeFeed)
                        Variaveis.fES.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fFatorPotencia.NomeFeed)
                        Variaveis.fFatorPotencia.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fFreq.NomeFeed)
                        Variaveis.fFreq.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIa.NomeFeed)
                        Variaveis.fIa.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIb.NomeFeed)
                        Variaveis.fIb.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIc.NomeFeed)
                        Variaveis.fIc.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIMa.NomeFeed)
                        Variaveis.fIMa.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIMb.NomeFeed)
                        Variaveis.fIMb.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIMc.NomeFeed)
                        Variaveis.fIMc.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIPa.NomeFeed)
                        Variaveis.fIPa.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIPb.NomeFeed)
                        Variaveis.fIPb.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fIPc.NomeFeed)
                        Variaveis.fIPc.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fNivelOleo.NomeFeed)
                        Variaveis.fNivelOleo.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fP.NomeFeed)
                        Variaveis.fP.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fPM.NomeFeed)
                        Variaveis.fPM.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fPP.NomeFeed)
                        Variaveis.fPP.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fQ.NomeFeed)
                        Variaveis.fQ.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fQM.NomeFeed)
                        Variaveis.fQM.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fQP.NomeFeed)
                        Variaveis.fQP.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fS.NomeFeed)
                        Variaveis.fS.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fSM.NomeFeed)
                        Variaveis.fSM.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fSP.NomeFeed)
                        Variaveis.fSP.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fTEnrolamento.NomeFeed)
                        Variaveis.fTEnrolamento.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fTOleo.NomeFeed)
                        Variaveis.fTOleo.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fVab.NomeFeed)
                        Variaveis.fVab.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fValvulaPressao.NomeFeed)
                        Variaveis.fValvulaPressao.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fVan.NomeFeed)
                        Variaveis.fVan.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fVbc.NomeFeed)
                        Variaveis.fVbc.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fVbn.NomeFeed)
                        Variaveis.fVbn.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fVca.NomeFeed)
                        Variaveis.fVca.IndiceFeed = Fdd[kk].id;
                    if (Fdd[kk].Nome == Variaveis.fVcn.NomeFeed)
                        Variaveis.fVcn.IndiceFeed = Fdd[kk].id;
                }
            }
            catch (Exception Erro)
            {
                // Trocar para um alerta na barra inferior
                MessageBox.Show(Erro.Message);
            }
            // Verifica se alguma variável não está associada a um índice
            bool VarSemIndice = false;
            FeedServidor[] strTodas = Variaveis.strVariaveis();
            for (int jj = 0; jj < strTodas.Length; jj++)
                if (strTodas[jj].IndiceFeed == "")
                    VarSemIndice = true;
            if (VarSemIndice)
            {
                Global.boolConfigObriatoria = true;
                int contagem = 0;
                while (!Global.boolReiniciar)
                {
                    if (contagem++ > 3)
                    {
                        MessageBox.Show("O programa será encerrado agora");
                        this.Close();
                        return;
                    }
                    frmConfig Config = new frmConfig();
                    MessageBox.Show("Verifique os nomes das variáveis");
                    Config.ShowDialog();
                    Config.Dispose();
                }
                Global.boolConfigObriatoria = false;
                Global.boolReiniciar = false;
                frmMain_Load(sender, e);
                return;
            }
            JanelaDeTempo = new TimeSpan(2, 0, 1);
            cmbJanela.Text = Properties.Settings.Default.Janela;
        }

        private List<RegistroDB> LeituraCSVs(string strArquivoCSV)
        {
            List<RegistroDB> Regs = new List<RegistroDB>();
            if (new FileInfo(strArquivoCSV).Exists)
            {
                using (StreamReader strRead = new StreamReader(strArquivoCSV))
                {
                    FeedServidor[] Todas = Variaveis.strVariaveis();
                    int[] iCampos = new int[Todas.Length];
                    bool cabecalho = true;
                    if (!strRead.EndOfStream)
                        while (!strRead.EndOfStream)
                        {
                            RegistroDB Reg = new RegistroDB();
                            string Linha = strRead.ReadLine();
                            string[] Campos = Linha.Split(Global.charSeparadorCSV);

                            if (cabecalho)
                            {
                                cabecalho = false;
                                for (int mm = 1; mm < Campos.Length; mm++)
                                {
                                    for (int jj = 0; jj < Todas.Length; jj++)
                                    {
                                        if (Campos[mm] == Todas[jj].NomeFeed)
                                        {
                                            iCampos[jj] = mm; // Procura pelo campo e retorna o índice
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Reg.Horario = Convert.ToUInt32(Campos[0]);
                                //1     2       3   4       5       6       7   8       9       10      11    12
                                //strP, strQ, strS, strVa, strVb, strVc, strIa, strIb, strIc, strNo, strTo, strTe 
                                for (int jj = 0; jj < Todas.Length; jj++)
                                {
                                    Reg.P[jj] = Convert.ToSingle(Campos[iCampos[jj]].Replace('.', ','));
                                }
                                Regs.Add(Reg);
                            }
                        }
                }
            }
            return Regs;
        }


        private void timerRelogio_Tick(object sender, EventArgs e)
        {
            // Relógio
            lblHora.Text = Convert.ToString(DateTime.Now);
            System.Diagnostics.Process Processo = System.Diagnostics.Process.GetCurrentProcess();
            lblMEM.Text = string.Format("{0} registros na memória | Memória utilizada = {1:G5} MB", Registros.Count, Processo.PagedMemorySize64 / 1024f / 1024f);
        }

        // Atualiza os gráficos
        private void tmrGraficos_Tick(object sender, EventArgs e)
        {
            tmrGraficos.Enabled = false;
            bool Plotar = false;
            // Rotina para buscar novas informações no servidor e exibir na tela
            string strTime = GetCSV(ComandosCSV.strComandoHorario, Uteis.Time2Unix(DateTime.Now), Uteis.Time2Unix(DateTime.Now), Variaveis.fP.IndiceFeed).Replace("\"", "");
            DateTime VelhaUltimaAtualizacao = tUltimaAtualizacao;
            if (strTime.Length > 1)
                tUltimaAtualizacao = DTData2DateTime(strTime);
            else
                return;
            if (tmrGraficos.Interval != Global.intTaxaAtualizacao)
            {
                // Primeira execução
                BuscaDados(tUltimaAtualizacao.Subtract(JanelaDeTempo), tUltimaAtualizacao);
                if (Encerrando) return;
                if (!GerarGrafico()) return;
                if (Registros.Count > 0)
                    Plotar = true;
            }
            else
            {
                if (tUltimaAtualizacao.CompareTo(VelhaUltimaAtualizacao) != 0)
                {
                    Plotar = true;
                    BuscaDados(VelhaUltimaAtualizacao, tUltimaAtualizacao);
                    if (Encerrando) return;
                    LimpaSeries();

                }

            }
            if (Plotar)
            {
                PlotaGrafico(tUltimaAtualizacao.Subtract(JanelaDeTempo), tUltimaAtualizacao);
                lblMensagens.Text = string.Format("Último registro: {0} |", tUltimaAtualizacao.ToLocalTime());
            }

            // Atualiza as etiquetas
            if (Registros.Count > 0)
                AtualizaLabels(Registros[Registros.Count - 1]);

            DateTime NovaAtualizaco = DTData2DateTime(strTime);
            ResumeLayout();
            tmrGraficos.Interval = Global.intTaxaAtualizacao;
            tmrGraficos.Enabled = true;
        }

        private DateTime DTData2DateTime(string strTime)
        {
            if (strTime.Length > 1)
            {
                if (strTime.Contains('-'))
                {
                    // Formato de data = "2015-12-30 03:57:57"
                    return DateTime.Parse(strTime, System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat);
                }
                else
                {
                    // formato unix
                    return Uteis.Unix2time(Convert.ToUInt32(strTime));
                }
            }
            else
                //qualquer coisa
                return DateTime.Now;
        }

        private void AtualizaLabels(RegistroDB registroDB)
        {
            // relóginhos
            aTo.Value(registroDB.P[Variaveis.fTOleo.indice]);
            aTe.Value(registroDB.P[Variaveis.fTEnrolamento.indice]);
            float f_Max_TE = float.MinValue; float f_Min_TE = float.MaxValue;
            float f_Max_TO = float.MinValue; float f_Min_TO = float.MaxValue;
            UInt32 Inicio = Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo));
            for (int jj = 0; jj < Registros.Count; jj++)
            {
                if (Registros[jj].Horario >= Inicio)
                {
                    if (Registros[jj].P[Variaveis.fTOleo.indice] > f_Max_TO) 
                        f_Max_TO = Registros[jj].P[Variaveis.fTOleo.indice];
                    if (Registros[jj].P[Variaveis.fTOleo.indice] < f_Min_TO) 
                        f_Min_TO = Registros[jj].P[Variaveis.fTOleo.indice];
                    if (Registros[jj].P[Variaveis.fTEnrolamento.indice] > f_Max_TE) 
                        f_Max_TE = Registros[jj].P[Variaveis.fTEnrolamento.indice];
                    if (Registros[jj].P[Variaveis.fTEnrolamento.indice] < f_Min_TE) 
                        f_Min_TE = Registros[jj].P[Variaveis.fTEnrolamento.indice];
                }
            }
            aTo.ValorMaximo(f_Max_TO);
            aTo.ValorMinimo(f_Min_TO);
            aTe.ValorMaximo(f_Max_TE);
            aTe.ValorMinimo(f_Min_TE);

            lblTo.Text = string.Format(Variaveis.fTOleo.formato, registroDB.P[Variaveis.fTOleo.indice]);
            lblTe.Text = string.Format(Variaveis.fTEnrolamento.formato, registroDB.P[Variaveis.fTEnrolamento.indice]);

            lblNo.Text = (registroDB.P[Variaveis.fNivelOleo.indice] < Global.intNOleoBaixo) ? Global.strNOleoBaixo : ((registroDB.P[Variaveis.fNivelOleo.indice] > Global.intNOleoAlto) ? Global.strNOleoAlto : Global.strNOleoNormal);
            lblVL.Text = (registroDB.P[Variaveis.fValvulaPressao.indice] > 0) ? Global.strValvulaAtivada : Global.strValvulaNormal;

            if ((registroDB.P[Variaveis.fNivelOleo.indice] > Global.intNOleoAlto) | (registroDB.P[Variaveis.fNivelOleo.indice] < Global.intNOleoBaixo))
                picStatus.Image = Properties.Resources.Vermelho;
            else
                picStatus.Image = Properties.Resources.Verde;

            if ((registroDB.P[Variaveis.fValvulaPressao.indice]) == 0)
                picValvula.Image = Properties.Resources.Verde;
            else
                picValvula.Image = Properties.Resources.Vermelho;

            tv1.SuspendLayout();
            if (tv1.Nodes.Count == 0)
            {
                tv1.Nodes.Add("ve", "Variáveis Elétricas");
                {
                    tv1.Nodes["ve"].Nodes.Add("gi", "Grandezas instantâneas");
                    {
                        tv1.Nodes["ve"].Nodes["gi"].Nodes.Add("p", "Potência");
                        {
                            tv1.Nodes["ve"].Nodes["gi"].Nodes["p"].Nodes.Add(Variaveis.fP.NodeTv1, FormataTexto(Variaveis.fP, registroDB)).Tag = Variaveis.fP.NomeFeed;
                            tv1.Nodes["ve"].Nodes["gi"].Nodes["p"].Nodes.Add(Variaveis.fQ.NodeTv1, FormataTexto(Variaveis.fQ, registroDB)).Tag = Variaveis.fQ.NomeFeed;
                            tv1.Nodes["ve"].Nodes["gi"].Nodes["p"].Nodes.Add(Variaveis.fS.NodeTv1, FormataTexto(Variaveis.fS, registroDB)).Tag = Variaveis.fS.NomeFeed;
                        }

                        tv1.Nodes["ve"].Nodes["gi"].Nodes.Add("v", "Tensões");
                        {
                            tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes.Add("vlinha", "Tensões de linha");
                            {
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vlinha"].Nodes.Add(Variaveis.fVab.NodeTv1, FormataTexto(Variaveis.fVab, registroDB)).Tag = Variaveis.fVab.NomeFeed;
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vlinha"].Nodes.Add(Variaveis.fVbc.NodeTv1, FormataTexto(Variaveis.fVbc, registroDB)).Tag = Variaveis.fVbc.NomeFeed;
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vlinha"].Nodes.Add(Variaveis.fVca.NodeTv1, FormataTexto(Variaveis.fVca, registroDB)).Tag = Variaveis.fVca.NomeFeed;
                            }
                            tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes.Add("vfase", "Tensões de fase");
                            {
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vfase"].Nodes.Add(Variaveis.fVan.NodeTv1, FormataTexto(Variaveis.fVan, registroDB)).Tag = Variaveis.fVan.NomeFeed;
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vfase"].Nodes.Add(Variaveis.fVbn.NodeTv1, FormataTexto(Variaveis.fVbn, registroDB)).Tag = Variaveis.fVbn.NomeFeed;
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vfase"].Nodes.Add(Variaveis.fVcn.NodeTv1, FormataTexto(Variaveis.fVcn, registroDB)).Tag = Variaveis.fVcn.NomeFeed;
                            }
                        }
                        tv1.Nodes["ve"].Nodes["gi"].Nodes.Add("i", "Corrente");
                        {
                            tv1.Nodes["ve"].Nodes["gi"].Nodes["i"].Nodes.Add(Variaveis.fIa.NodeTv1, FormataTexto(Variaveis.fIa, registroDB)).Tag = Variaveis.fIa.NomeFeed;
                            tv1.Nodes["ve"].Nodes["gi"].Nodes["i"].Nodes.Add(Variaveis.fIb.NodeTv1, FormataTexto(Variaveis.fIb, registroDB)).Tag = Variaveis.fIb.NomeFeed;
                            tv1.Nodes["ve"].Nodes["gi"].Nodes["i"].Nodes.Add(Variaveis.fIc.NodeTv1, FormataTexto(Variaveis.fIc, registroDB)).Tag = Variaveis.fIc.NomeFeed;
                        }
                        tv1.Nodes["ve"].Nodes["gi"].Nodes.Add(Variaveis.fFreq.NodeTv1, FormataTexto(Variaveis.fFreq, registroDB)).Tag = Variaveis.fFreq.NomeFeed;

                        tv1.Nodes["ve"].Nodes["gi"].Nodes.Add(Variaveis.fFatorPotencia.NodeTv1, FormataTexto(Variaveis.fFatorPotencia, registroDB)).Tag = Variaveis.fFatorPotencia.NomeFeed;
                    }
                    tv1.Nodes["ve"].Nodes.Add("ven", "Valores de Energia");
                    {
                        tv1.Nodes["ve"].Nodes["ven"].Nodes.Add(Variaveis.fEP.NodeTv1, FormataTexto(Variaveis.fEP, registroDB)).Tag = Variaveis.fEP.NomeFeed;
                        tv1.Nodes["ve"].Nodes["ven"].Nodes.Add(Variaveis.fEQ.NodeTv1, FormataTexto(Variaveis.fEQ, registroDB)).Tag = Variaveis.fEQ.NomeFeed;
                        tv1.Nodes["ve"].Nodes["ven"].Nodes.Add(Variaveis.fES.NodeTv1, FormataTexto(Variaveis.fES, registroDB)).Tag = Variaveis.fES.NomeFeed;
                    }
                    tv1.Nodes["ve"].Nodes.Add("vd", "Valores de Demanda (Média)");
                    {
                        tv1.Nodes["ve"].Nodes["vd"].Nodes.Add("c", "Corrente IM");
                        {
                            tv1.Nodes["ve"].Nodes["vd"].Nodes["c"].Nodes.Add(Variaveis.fIMa.NodeTv1, FormataTexto(Variaveis.fIMa, registroDB)).Tag = Variaveis.fIMa.NomeFeed;
                            tv1.Nodes["ve"].Nodes["vd"].Nodes["c"].Nodes.Add(Variaveis.fIMb.NodeTv1, FormataTexto(Variaveis.fIMb, registroDB)).Tag = Variaveis.fIMb.NomeFeed;
                            tv1.Nodes["ve"].Nodes["vd"].Nodes["c"].Nodes.Add(Variaveis.fIMc.NodeTv1, FormataTexto(Variaveis.fIMc, registroDB)).Tag = Variaveis.fIMc.NomeFeed;
                        }
                        tv1.Nodes["ve"].Nodes["vd"].Nodes.Add("p2", "Potência");
                        {
                            tv1.Nodes["ve"].Nodes["vd"].Nodes["p2"].Nodes.Add(Variaveis.fPM.NodeTv1, FormataTexto(Variaveis.fPM, registroDB)).Tag = Variaveis.fPM.NomeFeed;
                            tv1.Nodes["ve"].Nodes["vd"].Nodes["p2"].Nodes.Add(Variaveis.fQM.NodeTv1, FormataTexto(Variaveis.fQM, registroDB)).Tag = Variaveis.fQM.NomeFeed;
                            tv1.Nodes["ve"].Nodes["vd"].Nodes["p2"].Nodes.Add(Variaveis.fSM.NodeTv1, FormataTexto(Variaveis.fSM, registroDB)).Tag = Variaveis.fSM.NomeFeed;
                        }
                    }
                    tv1.Nodes["ve"].Nodes.Add("vm", "Valores de Demanda Máxima");
                    {
                        tv1.Nodes["ve"].Nodes["vm"].Nodes.Add("c2", "Corrente Máxima");
                        {
                            tv1.Nodes["ve"].Nodes["vm"].Nodes["c2"].Nodes.Add(Variaveis.fIPa.NodeTv1, FormataTexto(Variaveis.fIPa, registroDB)).Tag = Variaveis.fIPa.NomeFeed;
                            tv1.Nodes["ve"].Nodes["vm"].Nodes["c2"].Nodes.Add(Variaveis.fIPb.NodeTv1, FormataTexto(Variaveis.fIPb, registroDB)).Tag = Variaveis.fIPb.NomeFeed;
                            tv1.Nodes["ve"].Nodes["vm"].Nodes["c2"].Nodes.Add(Variaveis.fIPc.NodeTv1, FormataTexto(Variaveis.fIPc, registroDB)).Tag = Variaveis.fIPc.NomeFeed;
                        }
                        tv1.Nodes["ve"].Nodes["vm"].Nodes.Add("p3", "Potência");
                        {
                            tv1.Nodes["ve"].Nodes["vm"].Nodes["p3"].Nodes.Add(Variaveis.fPP.NodeTv1, FormataTexto(Variaveis.fPP, registroDB)).Tag = Variaveis.fPP.NomeFeed;
                            tv1.Nodes["ve"].Nodes["vm"].Nodes["p3"].Nodes.Add(Variaveis.fQP.NodeTv1, FormataTexto(Variaveis.fQP, registroDB)).Tag = Variaveis.fQP.NomeFeed;
                            tv1.Nodes["ve"].Nodes["vm"].Nodes["p3"].Nodes.Add(Variaveis.fSP.NodeTv1, FormataTexto(Variaveis.fSP, registroDB)).Tag = Variaveis.fSP.NomeFeed;
                        }
                    }
                }
                tv1.Nodes.Add("ov", "Outras Variáveis");
                {
                    tv1.Nodes["ov"].Nodes.Add("te", "Temperatura");
                    {
                        tv1.Nodes["ov"].Nodes["te"].Nodes.Add(Variaveis.fTEnrolamento.NodeTv1, FormataTexto(Variaveis.fTEnrolamento, registroDB)).Tag = Variaveis.fTEnrolamento.NomeFeed;
                        tv1.Nodes["ov"].Nodes["te"].Nodes.Add(Variaveis.fTOleo.NodeTv1, FormataTexto(Variaveis.fTOleo, registroDB)).Tag = Variaveis.fTOleo.NomeFeed;
                    }
                    tv1.Nodes["ov"].Nodes.Add(Variaveis.fNivelOleo.NodeTv1, ((registroDB.P[Variaveis.fNivelOleo.indice] < Global.intNOleoBaixo) ? Global.strNOleoBaixo : ((registroDB.P[Variaveis.fNivelOleo.indice] > Global.intNOleoAlto) ? Global.strNOleoAlto : Global.strNOleoNormal))).Tag = Variaveis.fNivelOleo.NomeFeed;
                    tv1.Nodes["ov"].Nodes.Add(Variaveis.fValvulaPressao.NodeTv1, ((registroDB.P[Variaveis.fValvulaPressao.indice] == 0) ? "Válvula em condições normais" : "Válvula acionada"));

                }
                MarcarTodas(tv1.Nodes, true);
                tv1.ExpandAll();
                tv1.SelectedNode = tv1.Nodes[0];
            }
            else
            {
                {
                    {
                        {
                            {
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["i"].Nodes[Variaveis.fIa.NodeTv1].Text = FormataTexto(Variaveis.fIa, registroDB);
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["i"].Nodes[Variaveis.fIb.NodeTv1].Text = FormataTexto(Variaveis.fIb, registroDB);
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["i"].Nodes[Variaveis.fIc.NodeTv1].Text = FormataTexto(Variaveis.fIc, registroDB);
                            }
                            {
                                {
                                    tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vlinha"].Nodes[Variaveis.fVab.NodeTv1].Text = FormataTexto(Variaveis.fVab, registroDB);
                                    tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vlinha"].Nodes[Variaveis.fVbc.NodeTv1].Text = FormataTexto(Variaveis.fVbc, registroDB);
                                    tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vlinha"].Nodes[Variaveis.fVca.NodeTv1].Text = FormataTexto(Variaveis.fVca, registroDB);
                                }
                                {
                                    tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vfase"].Nodes[Variaveis.fVan.NodeTv1].Text = FormataTexto(Variaveis.fVan, registroDB);
                                    tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vfase"].Nodes[Variaveis.fVbn.NodeTv1].Text = FormataTexto(Variaveis.fVbn, registroDB);
                                    tv1.Nodes["ve"].Nodes["gi"].Nodes["v"].Nodes["vfase"].Nodes[Variaveis.fVcn.NodeTv1].Text = FormataTexto(Variaveis.fVcn, registroDB);
                                }
                            }
                            tv1.Nodes["ve"].Nodes["gi"].Nodes[Variaveis.fFreq.NodeTv1].Text = FormataTexto(Variaveis.fFreq, registroDB);
                            {
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["p"].Nodes[Variaveis.fP.NodeTv1].Text = FormataTexto(Variaveis.fP, registroDB);
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["p"].Nodes[Variaveis.fQ.NodeTv1].Text = FormataTexto(Variaveis.fQ, registroDB);
                                tv1.Nodes["ve"].Nodes["gi"].Nodes["p"].Nodes[Variaveis.fS.NodeTv1].Text = FormataTexto(Variaveis.fS, registroDB);
                            }
                            tv1.Nodes["ve"].Nodes["gi"].Nodes[Variaveis.fFatorPotencia.NodeTv1].Text = FormataTexto(Variaveis.fFatorPotencia, registroDB);
                        }
                        {
                            tv1.Nodes["ve"].Nodes["ven"].Nodes[Variaveis.fEP.NodeTv1].Text = FormataTexto(Variaveis.fEP, registroDB);
                            tv1.Nodes["ve"].Nodes["ven"].Nodes[Variaveis.fEQ.NodeTv1].Text = FormataTexto(Variaveis.fEQ, registroDB);
                            tv1.Nodes["ve"].Nodes["ven"].Nodes[Variaveis.fES.NodeTv1].Text = FormataTexto(Variaveis.fES, registroDB);
                        }
                        {
                            {
                                tv1.Nodes["ve"].Nodes["vd"].Nodes["c"].Nodes[Variaveis.fIMa.NodeTv1].Text = FormataTexto(Variaveis.fIMa, registroDB);
                                tv1.Nodes["ve"].Nodes["vd"].Nodes["c"].Nodes[Variaveis.fIMb.NodeTv1].Text = FormataTexto(Variaveis.fIMb, registroDB);
                                tv1.Nodes["ve"].Nodes["vd"].Nodes["c"].Nodes[Variaveis.fIMc.NodeTv1].Text = FormataTexto(Variaveis.fIMc, registroDB);
                            }
                            {
                                tv1.Nodes["ve"].Nodes["vd"].Nodes["p2"].Nodes[Variaveis.fPM.NodeTv1].Text = FormataTexto(Variaveis.fPM, registroDB);
                                tv1.Nodes["ve"].Nodes["vd"].Nodes["p2"].Nodes[Variaveis.fQM.NodeTv1].Text = FormataTexto(Variaveis.fQM, registroDB);
                                tv1.Nodes["ve"].Nodes["vd"].Nodes["p2"].Nodes[Variaveis.fSM.NodeTv1].Text = FormataTexto(Variaveis.fSM, registroDB);
                            }
                        }
                        {
                            {
                                tv1.Nodes["ve"].Nodes["vm"].Nodes["c2"].Nodes[Variaveis.fIPa.NodeTv1].Text = FormataTexto(Variaveis.fIPa, registroDB);
                                tv1.Nodes["ve"].Nodes["vm"].Nodes["c2"].Nodes[Variaveis.fIPb.NodeTv1].Text = FormataTexto(Variaveis.fIPb, registroDB);
                                tv1.Nodes["ve"].Nodes["vm"].Nodes["c2"].Nodes[Variaveis.fIPc.NodeTv1].Text = FormataTexto(Variaveis.fIPc, registroDB);
                            }
                            {
                                tv1.Nodes["ve"].Nodes["vm"].Nodes["p3"].Nodes[Variaveis.fPP.NodeTv1].Text = FormataTexto(Variaveis.fPP, registroDB);
                                tv1.Nodes["ve"].Nodes["vm"].Nodes["p3"].Nodes[Variaveis.fQP.NodeTv1].Text = FormataTexto(Variaveis.fQP, registroDB);
                                tv1.Nodes["ve"].Nodes["vm"].Nodes["p3"].Nodes[Variaveis.fSP.NodeTv1].Text = FormataTexto(Variaveis.fSP, registroDB);
                            }
                        }
                    }
                    {
                        {
                            tv1.Nodes["ov"].Nodes["te"].Nodes[Variaveis.fTEnrolamento.NodeTv1].Text = FormataTexto(Variaveis.fTEnrolamento, registroDB);
                            tv1.Nodes["ov"].Nodes["te"].Nodes[Variaveis.fTOleo.NodeTv1].Text = FormataTexto(Variaveis.fTOleo, registroDB);
                        }
                        tv1.Nodes["ov"].Nodes[Variaveis.fNivelOleo.NodeTv1].Text = ((registroDB.P[Variaveis.fNivelOleo.indice] < Global.intNOleoBaixo) ? "Nível baixo" : ((registroDB.P[Variaveis.fNivelOleo.indice] > Global.intNOleoAlto) ? "Nível alto" : "Nível normal"));
                        tv1.Nodes["ov"].Nodes[Variaveis.fValvulaPressao.NodeTv1].Text = ((registroDB.P[Variaveis.fValvulaPressao.indice] == 0f) ? "Válvula em condições normais" : "Válvula acionada");
                    }
                }

            }
            tv1.ResumeLayout();
        }

        // Rotina recursiva para marcar todas as checkbox do treeview
        private void MarcarTodas(TreeNodeCollection treeNodeCollection, bool Marcar)
        {
            foreach (TreeNode No in treeNodeCollection)
            {
                if (No.Nodes.Count > 0)
                    MarcarTodas(No.Nodes, Marcar);
                No.Checked = Marcar;
                try
                {
                    chartTemperatura.Series[(string)No.Tag].Enabled = No.Checked;
                }
                catch { }
            }
        }

        private string FormataTexto(FeedServidor feedServidor, RegistroDB REG)
        {
            return string.Format(feedServidor.formato, REG.P[feedServidor.indice]);
        }

        private void PlotaGrafico(DateTime dateTime1, DateTime dateTime2)
        {
            PlotaGrafico(Uteis.Time2Unix(dateTime1), Uteis.Time2Unix(dateTime2));
        }

        private void BuscaDados(DateTime dateTime1, DateTime dateTime2)
        {
            BuscaDados(Uteis.Time2Unix(dateTime1), Uteis.Time2Unix(dateTime2));
        }

        // Esta rotina deve ser utilizada considerando um intervalo de tempo limitado
        /// <summary>Retorna um arquivo CSV</summary>
        private string GetCSV(string Comando, UInt32 Inicio, UInt32 Fim, string ID)
        {
            try
            {
                string strComando = string.Format("{0}{1}{2}&start={3}&end={4}&apikey={5}&interval=1&timeformat=0", Servidor.Server, Comando, ID, Inicio, Fim, Servidor.APIKey);
                WebClient Web = new WebClient();
                strComando = Web.DownloadString(strComando);
                // Verifica a APIKey
                if (strComando.Contains("API"))
                {
                    MessageBox.Show("APIKey incorreta");
                    this.Hide();
                    frmConfig Config = new frmConfig();
                    tmrRelogio.Enabled =
                        tmrGraficos.Enabled = false;
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
            if (Global.boolCabecalhoCSV)
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
                while ((!(strCSV[jj] == Global.charSeparadorCSV)) & (jj < strCSV.Length - 1))
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

        private void chartTemperatura_AxisViewChanged(object sender, ViewEventArgs e)
        {
            ChartValueType Escala; //Nova escala (data ou hora)
            if (e.NewSize < double.MaxValue) // se não houver reset no zoom
            {
                if (e.NewSize <= 1) // se a janela for menor que ou igual a um dia
                {
                    Escala = ChartValueType.Time; // Hora
                }
                else
                {
                    Escala = ChartValueType.DateTime; //data
                }
            }
            else // se houver reset no zoom
            {
                if (JanelaDeTempo.TotalDays > 1)
                {
                    Escala = ChartValueType.DateTime;
                }
                else
                {
                    Escala = ChartValueType.Time;
                }
            }

            if (chartTemperatura.Series[0].XValueType != Escala)
            {
                for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
                {
                    chartTemperatura.Series[jj].XValueType = Escala;
                }
            }
        }

        private void tooConfig_Click(object sender, EventArgs e)
        {
            if (Servidor.Permissoes == 1)
            {
                // Botão "Configurações"
                Form frmconfig1 = new frmConfig();
                this.Hide();
                Global.boolReiniciar = false;
                Global.boolConfigObriatoria = false;
                tmrGraficos.Enabled = false;
                frmconfig1.ShowDialog();
                tmrGraficos.Enabled = true;
                if (Global.boolReiniciar)
                {
                    Global.boolReiniciar = false;
                    MessageBox.Show("É necessário reiniciar o programa");
                    this.Close();
                }
                else
                {
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Este usuário não tem permissões para acessar o painel de configurações");
            }
        }

        private void chartTemperatura_CursorPositionChanged(object sender, CursorEventArgs e)
        {
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            // Converter o texto para intervalo de tempo
            try
            {
                string[] horario = cmbJanela.Text.ToLower().Split(' ');
                int dia = 0; int hora = 0; int minuto = 0; int segundo = 0;
                double detectado = Convert.ToDouble(horario[0]);
                if (horario[1].StartsWith("mes") | horario[1].StartsWith("mês"))
                {
                    dia = (int)Math.Floor(detectado * 30);
                }
                else
                {
                    if (horario[1].StartsWith("semana"))
                    {
                        dia = (int)Math.Floor(detectado * 7);
                    }
                    else
                    {
                        if (horario[1].StartsWith("dia"))
                        {
                            dia = (int)Math.Floor(detectado);
                            hora = (int)Math.Floor((detectado - dia) * 24);
                        }
                        else
                        {
                            if (horario[1].StartsWith("hora"))
                            {
                                hora = (int)Math.Floor(detectado);
                                minuto = (int)Math.Floor((detectado - hora) * 60);
                            }
                            else
                            {
                                if (horario[1].StartsWith("minuto"))
                                {
                                    minuto = (int)Math.Floor(detectado);
                                    segundo = (int)Math.Floor((detectado - minuto) * 60);
                                }
                                else
                                {
                                    cmbJanela.BackColor = System.Drawing.Color.RosyBrown;
                                }
                            }
                        }
                    }
                }
                if (dia + hora + minuto + segundo != 0)
                {
                    TimeSpan NovaJanela = new TimeSpan(dia, hora, minuto, segundo);
                    if (NovaJanela != JanelaDeTempo)
                    {
                        JanelaDeTempo = NovaJanela;
                        if (Registros.Count>0)
                            AtualizaLabels(Registros[Registros.Count-1]);
                        // Atualizar tudo
                        tmrGraficos.Enabled = false; tmrGraficos.Enabled = true;
                        LimpaSeries();
                        try
                        {
                            if (Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo)) < PrimeiroValor)
                            {
                                BuscaDados(Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo)), PrimeiroValor);
                                if (Encerrando) return;
                            }
                            PlotaGrafico(tUltimaAtualizacao.Subtract(JanelaDeTempo), tUltimaAtualizacao);
                        }
                        catch
                        { }
                    }
                    cmbJanela.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    cmbJanela.BackColor = System.Drawing.Color.Yellow;
                }
            }
            catch
            {
                cmbJanela.BackColor = System.Drawing.Color.Yellow;
            }
        }

        private void chartTemperatura_MouseUp(object sender, MouseEventArgs e)
        {
            // botão direito
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (chartTemperatura.ChartAreas["T"].AxisX.ScaleView.IsZoomed)
                    chartTemperatura.ChartAreas["T"].AxisX.ScaleView.ZoomReset();
                if (JanelaDeTempo.TotalDays > 1)
                {
                    for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
                    {
                        chartTemperatura.Series[jj].XValueType = ChartValueType.DateTime;
                    }
                }
                else
                {
                    for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
                    {
                        chartTemperatura.Series[jj].XValueType = ChartValueType.Time;
                    }
                }
            }
        }

        private void tv1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if ((e.Action == TreeViewAction.ByKeyboard) | (e.Action == TreeViewAction.ByMouse))
            {
                try
                {
                    try
                    {
                        chartTemperatura.Series[e.Node.Tag.ToString()].Enabled = e.Node.Checked;
                    }
                    catch { }
                    MarcarTodasAbaixo(tv1.Nodes, e.Node.Name, e.Node.Checked);
                }
                catch { }
            }
            chartTemperatura.ChartAreas["I"].AxisX.LabelStyle.Enabled =
                !((chartTemperatura.Series[Variaveis.fTEnrolamento.NomeFeed].Enabled) | (chartTemperatura.Series[Variaveis.fTOleo.NomeFeed].Enabled));
            chartTemperatura.ChartAreas["Vf"].AxisX.LabelStyle.Enabled =
                (!((chartTemperatura.Series[Variaveis.fIa.NomeFeed].Enabled) | (chartTemperatura.Series[Variaveis.fIb.NomeFeed].Enabled) | (chartTemperatura.Series[Variaveis.fIc.NomeFeed].Enabled))) & chartTemperatura.ChartAreas["I"].AxisX.LabelStyle.Enabled;
            chartTemperatura.ChartAreas["Vl"].AxisX.LabelStyle.Enabled =
                (!((chartTemperatura.Series[Variaveis.fVan.NomeFeed].Enabled) | (chartTemperatura.Series[Variaveis.fVbn.NomeFeed].Enabled) | (chartTemperatura.Series[Variaveis.fVcn.NomeFeed].Enabled))) & chartTemperatura.ChartAreas["Vf"].AxisX.LabelStyle.Enabled;
            chartTemperatura.ChartAreas["P"].AxisX.LabelStyle.Enabled =
                (!((chartTemperatura.Series[Variaveis.fVab.NomeFeed].Enabled) | (chartTemperatura.Series[Variaveis.fVbc.NomeFeed].Enabled) | (chartTemperatura.Series[Variaveis.fVca.NomeFeed].Enabled))) & chartTemperatura.ChartAreas["Vl"].AxisX.LabelStyle.Enabled;
        }
        /// <summary>Procura um item na treeview e marca/desmarca tudo abaixo</summary>
        private void MarcarTodasAbaixo(TreeNodeCollection Item, string p1, bool p2)
        {
            for (int jj = 0; jj < Item.Count; jj++)
            {
                if (Item[jj].Name == p1)
                {
                    MarcarTodas(Item[jj].Nodes, p2);
                }
                else
                {
                    if (Item[jj].Nodes.Count > 0)
                    {
                        MarcarTodasAbaixo(Item[jj].Nodes, p1, p2);
                    }
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Encerrando = true;
            Properties.Settings.Default.Janela = cmbJanela.Text;
            Properties.Settings.Default.Save();
            Registros.Clear();
            chartTemperatura.Dispose();
            GC.Collect(); GC.WaitForPendingFinalizers();
        }

        private void toolGraficos_Click(object sender, EventArgs e)
        {
            tmrGraficos.Enabled = false;
            frmGraficos Graficos = new frmGraficos();
            Hide();
            Graficos.ShowDialog();
            Graficos.Dispose();
            Show();
            tmrGraficos.Enabled = true;
            tmrGraficos_Tick(null, null);
        }

        private void toolExcel_Click(object sender, EventArgs e)
        {
            string Pasta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            SaveFileDialog SalvaArquivo = new SaveFileDialog();
            SalvaArquivo.FileName = ComandosCSV.ArquivoCSV(tUltimaAtualizacao.ToLocalTime()).Replace(".csv", ".xlsx");
            SalvaArquivo.InitialDirectory = Pasta;
            SalvaArquivo.Filter = "Arquivo XLSX|*.xlsx|Arquivo CSV|*.csv|Imagem PNG|*.png|Imagem JPG|*.jpg|Imagem BMP|*.bmp";
            SalvaArquivo.DefaultExt = "*.xlsx";
            tmrGraficos.Enabled = false;
            if (SalvaArquivo.ShowDialog() == DialogResult.OK)
            {
                int ponto = SalvaArquivo.FileName.LastIndexOf('.');
                if (ponto < 0) ponto = SalvaArquivo.FileName.Length - 3;
                string Formato = SalvaArquivo.FileName.Substring(ponto).ToLower();
                switch (Formato)
                {
                    case ".csv":
                        {
                            // Salvar CSV
                            UInt32 inicio = Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo));
                            UInt32 Final = Uteis.Time2Unix(tUltimaAtualizacao);
                            using (StreamWriter GravarArquivoCSV = new StreamWriter(SalvaArquivo.FileName, false))
                            {

                                System.Globalization.NumberFormatInfo SeparadorDecimal = System.Globalization.NumberFormatInfo.CurrentInfo;
                                FeedServidor[] vars = Variaveis.strVariaveis();
                                StringBuilder bstr = new StringBuilder("Horario");
                                for (int jj = 0; jj < vars.Length; jj++)
                                {
                                    bstr.Append(Global.charSeparadorCSVCSV);
                                    bstr.Append(vars[jj].NomeFeed);
                                }
                                GravarArquivoCSV.WriteLine(bstr);
                                for (int jj = 0; jj < Registros.Count; jj++)
                                {
                                    if ((inicio < Registros[jj].Horario) & (Final > Registros[jj].Horario))
                                    {
                                        bstr = new StringBuilder(Registros[jj].Horario.ToString());
                                        for (int kk = 0; kk < vars.Length; kk++)
                                        {
                                            bstr.Append(Global.charSeparadorCSVCSV);
                                            bstr.Append(Registros[jj].P[vars[kk].indice].ToString(SeparadorDecimal));
                                        }
                                        GravarArquivoCSV.WriteLine(bstr);
                                    }
                                }
                            }
                        }
                        break;
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".bmp":
                        {
                            Graphics Imagem = chartTemperatura.CreateGraphics();
                            Bitmap bitmat = new Bitmap(chartTemperatura.Width, chartTemperatura.Height, Imagem);
                            Rectangle retangulo = new Rectangle(0, 0, chartTemperatura.Width, chartTemperatura.Height);
                            chartTemperatura.DrawToBitmap(bitmat, retangulo);
                            System.Drawing.Imaging.ImageFormat imgFormato = System.Drawing.Imaging.ImageFormat.Jpeg;
                            if (Formato == ".bmp")
                                imgFormato = System.Drawing.Imaging.ImageFormat.Bmp;
                            if (Formato == ".png")
                                imgFormato = System.Drawing.Imaging.ImageFormat.Png;

                            bitmat.Save(SalvaArquivo.FileName, imgFormato);
                        }
                        break;
                    case ".xls":
                    case ".xlsx":
                    default:
                        {
                            // Salvar
                            new SalvarExcel().SalvarXLSX(SalvaArquivo.FileName, Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo)), Uteis.Time2Unix(tUltimaAtualizacao), FormSalvarExcel.frmMain);
                            Type VerificaExcel = Type.GetTypeFromProgID("Excel.Application");
                            if (VerificaExcel == null)
                            {
                                System.Diagnostics.Process.Start("explorer.exe", "/select," + SalvaArquivo.FileName);
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(SalvaArquivo.FileName);
                            }
                        }
                        tmrGraficos.Enabled = true;
                        break;
                }
            }
            tmrGraficos.Enabled = true;
        }

        private void tmrBlink_Tick(object sender, EventArgs e)
        {
            // Nível do óleo alto ou baixo
            if (Registros.Count > 0)
            {
                if ((Registros[Registros.Count - 1].P[Variaveis.fNivelOleo.indice] > Global.intNOleoAlto) | (Registros[Registros.Count - 1].P[Variaveis.fNivelOleo.indice] < Global.intNOleoBaixo))
                {
                    lblNivel.BackColor = Fundo[blink];
                    if (Registros[Registros.Count - 1].P[Variaveis.fNivelOleo.indice] > Global.intNOleoAlto)
                    {
                        if (blink == 1)
                        {
                            picNivelOleo.Image = Properties.Resources.NivelOleoHR;
                        }
                        else
                        {
                            picNivelOleo.Image = Properties.Resources.NivelOleoHY;
                        }
                    }
                    else
                    {
                        if (blink == 1)
                        {
                            picNivelOleo.Image = Properties.Resources.NivelOleoLR;
                        }
                        else
                        {
                            picNivelOleo.Image = Properties.Resources.NivelOleoLY;
                        }
                    }
                }
                else
                {
                    lblNivel.BackColor = Color.Transparent;
                    picNivelOleo.Image=Properties.Resources.NivelOleoNG;
                }

                // Válvula de alívio de pressão
                if (Registros[Registros.Count - 1].P[Variaveis.fValvulaPressao.indice] != 0)
                {
                    lblValvula.BackColor = Fundo[blink];
                }
                else
                {
                    lblValvula.BackColor = Color.Transparent;
                }

                // Temperatura do óleo...
                if (Registros[Registros.Count - 1].P[Variaveis.fTOleo.indice] >= Global.floatTOleoH)
                {
                    lblTo.BackColor = Fundo[blink];
                }
                else
                {
                    lblTo.BackColor = Color.Transparent;
                }

                // Temperatura dos enrolamentos...
                if (Registros[Registros.Count - 1].P[Variaveis.fTEnrolamento.indice] >= Global.floatTEnrH)
                {
                    lblTe.BackColor = Fundo[blink];
                }
                else
                {
                    lblTe.BackColor = Color.Transparent;
                }
            }

            blink = 1 - blink;

        }

        private void toolComparar_Click(object sender, EventArgs e)
        {
            tmrGraficos.Enabled = false;
            frmCompara Compara = new frmCompara();
            this.Hide();
            Compara.ShowDialog();
            Compara.Dispose();
            this.Show();
            tmrGraficos.Enabled = true;
        }
    }
}
