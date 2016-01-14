using System; // Tipos de variáveis menos comuns
using System.Net; // Comunicação web
using System.Text; // Codificação de texto
using System.Windows.Forms; // Formulários
//using Newtonsoft.Json; // JSON
using System.Collections.Generic; // Variáveis anonimas (json)
using System.Data.SQLite; // Bancos de dados
using System.Windows.Forms.DataVisualization.Charting; //Gráficos
using System.Linq;
using System.IO;

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
                chartTemperatura.ChartAreas["V"].AxisX.ScrollBar.Enabled =
                chartTemperatura.ChartAreas["I"].AxisX.ScrollBar.Enabled =
                chartTemperatura.ChartAreas["T"].AxisX.ScrollBar.Enabled = false;
            // Remove o botão zoom out
            //chartTemperatura.ChartAreas["N"].AxisX.ScrollBar.ButtonStyle -= ScrollBarButtonStyles.ResetZoom;

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
            // Limites absolutos
            chartTemperatura.ChartAreas["N"].AxisY.Minimum = 0;
            chartTemperatura.ChartAreas["N"].AxisY.Maximum = 10;

            // Posiciona as várias chartáreas:
            //chartTemperatura.Legends["P"].DockedToChartArea = "P";
            // talvez seja necessário rever esses valores:
            //float fTamanhoLegenda = 100f;
            float fLarguraLegenda = 15f; //%
            float fLargura = 100 - fLarguraLegenda; // = 100f * (chartTemperatura.Width - fTamanhoLegenda) / (chartTemperatura.Width * 1f);
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
                srSerie.Legend = //"LegendaOculta";
                    srSerie.ChartArea = Global.strCategoria[jj]; // cada série associada com a chartárea e a legenda adequadas
                //srSerie.XValueType = ChartValueType.Auto; // Bug do .NET
                srSerie.XValueType = ChartValueType.Time; //
                srSerie.ChartType = SeriesChartType.StepLine; // gráfico em degraus (não sabemos o que acontece entre duas medidas
                srSerie.BorderWidth = 2; // tamanho da linha
                srSerie.Color = Global.Cores[jj];
                try
                {
                    chartTemperatura.Series.Add(srSerie); // adiciona a série de dados ao gráfico
                }
                catch
                {
                    MessageBox.Show("Erro ao gerar o gráfico, verifique se há algum nome de variável repetido");
                    frmConfig Config = new frmConfig();
                    Config.ShowDialog();
                    this.Close();
                    return;
                }
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
            Registros =
                Registros.OrderBy(RegistroDB => RegistroDB.Horario).ToList<RegistroDB>();
            // Elimina registros mais antigos com a finalidade de reduzir o uso de memória
            while (Registros.Count > Global.RegistrosMAXIMO)
            {
                Registros.RemoveAt(0);
            }
            SuspendLayout(); chartTemperatura.Series.SuspendUpdates();
            for (int jj = 0; jj < chartTemperatura.Series.Count; jj++)
            {
                chartTemperatura.Series[jj].XValueType = ChartValueType.Auto;//bug do .NET
            }
            //System.Diagnostics.Stopwatch A = new System.Diagnostics.Stopwatch();
            for (int mm = 0; mm < Registros.Count; mm++)
            {
                if (Registros[mm].Horario >= Start)
                    if (Registros[mm].Horario > End)
                    {
                        break; // Os dados estão em ordem cronológica
                    }
                    else
                    {
                        string[] strTodas = Global.strTodas();
                        DateTime Horario = Uteis.Unix2time(Registros[mm].Horario);
                        for (int kk = 0; kk < Global.intIndiceRegistro.Length; kk++)
                        {
                            chartTemperatura.Series[strTodas[kk]].Points.AddXY(Horario, Registros[mm].P[Global.intIndiceRegistro[kk]]);
                        }
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
            chartTemperatura.Series[Global.strP].Enabled = chkP.Checked;
            chartTemperatura.Series[Global.strQ].Enabled = chkQ.Checked;
            chartTemperatura.Series[Global.strS].Enabled = chkS.Checked;
            chartTemperatura.Series[Global.strVa].Enabled = chkVa.Checked;
            chartTemperatura.Series[Global.strVb].Enabled = chkVb.Checked;
            chartTemperatura.Series[Global.strVc].Enabled = chkVc.Checked;
            chartTemperatura.Series[Global.strIa].Enabled = chkIa.Checked;
            chartTemperatura.Series[Global.strIb].Enabled = chkIb.Checked;
            chartTemperatura.Series[Global.strIc].Enabled = chkIc.Checked;
            chartTemperatura.Series[Global.strNo].Enabled = chkNo.Checked;
            chartTemperatura.Series[Global.strTo].Enabled = chkTo.Checked;
            chartTemperatura.Series[Global.strTe].Enabled = chkTe.Checked;

            ResumeLayout(); chartTemperatura.Series.ResumeUpdates();
        }

        // Busca todas as informações do intervalo informado
        private void BuscaDados(UInt32 Inicio, UInt32 Final = 0)
        {
            // Medir performance:
            //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); sw.Start();
            //Verifica qual é o último registro no servidor
            string strTime = GetCSV(Global.strComandoHorario, Uteis.Time2Unix(DateTime.Now), Uteis.Time2Unix(DateTime.Now), Global.striP).Replace("\"", "");
            UInt32 Ultimo = Uteis.Time2Unix(DTData2DateTime(strTime));// Convert.ToUInt32(strTime); // Horário mais recente armazenado no servidor
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
            List<int> Salvar = new List<int>();
            toolStripProgressBar1.Maximum = Global.strCategoria.Length + 1;
            for (int jj = 0; jj < Global.strCategoria.Length; jj++)
            {
                toolStripProgressBar1.Value = jj;
                // Busca todos os valores do servidor
                string strTemp = GetCSV(Global.strComandoCSV, Inicio, Ultimo, strTodas[jj]);

                List<RegistroCSV> Dados = CSV2Matriz(strTemp);
                for (int kk = 0; kk < Dados.Count; kk++)
                {
                    UInt32 Horario_ = Dados[kk].timeUnix();

                    int indice = Registros.FindIndex(x => x.Horario == Horario_); //2,5s
                    //int indice = BuscaIndice(Horario_);

                    //int indice = Registros.IndexOf(new RegistroDB() { Horario = Horario_ }); //8 segundos
                    //int indice = Registros.TakeWhile(hora => hora.Horario !=Horario_ ).Count(); // 3,5s
                    if (indice < 0) // se não existe vamos criar um novo
                    {
                        Registros.Add(new RegistroDB());
                        indice = Registros.Count - 1;
                        Salvar.Add(indice); // salvar no banco de dados o item atual
                    }
                    Registros[indice].Horario = Horario_;
                    Registros[indice].P[Global.intIndiceRegistro[jj]] = (float)Dados[kk].valor();
                }
            }
            toolStripProgressBar1.Value = 0;
            if (Salvar.Count > 0)
            {
                DateTime Data = new DateTime(1970, 1, 1);
                DateTime DataNova = Uteis.Unix2time(Registros[Salvar[0]].Horario);
                string Arquivo = Path.Combine(Application.StartupPath, Global.ArquivoCSV(DataNova));
                //string Arquivo = Path.Combine(Application.StartupPath, Global.ArquivoCSV(Uteis.Unix2time(reg.Horario)));
                StreamWriter Gravar;// = new StreamWriter(Arquivo, true);
                if (!(new FileInfo(Arquivo).Exists))
                {
                    Gravar = new StreamWriter(Arquivo, true);
                    // Gera Arquivo CSV com cabeçalho
                    Gravar.WriteLine("{0}{13}{1}{13}{2}{13}{3}{13}{4}{13}{5}{13}{6}{13}{7}{13}{8}{13}{9}{13}{10}{13}{11}{13}{12}", "Horário",
                        Global.strP, Global.strQ, Global.strS,
                        Global.strVa, Global.strVb, Global.strVc,
                        Global.strIa, Global.strIb, Global.strIc,
                        Global.strNo, Global.strTo, Global.strTe, Global.SeparadorCSV);
                }
                else
                {
                    Gravar = new StreamWriter(Arquivo, true);
                }
                for (int mm = 0; mm < Salvar.Count; mm++)
                {
                    DataNova = Uteis.Unix2time(Registros[Salvar[mm]].Horario);
                    if (Data.Date != DataNova.Date)
                    {
                        Data = DataNova;
                        Gravar.Dispose();
                        Arquivo = Path.Combine(Application.StartupPath, Global.ArquivoCSV(Data));
                        if (!(new FileInfo(Arquivo).Exists))
                        {
                            Gravar = new StreamWriter(Arquivo, true);
                            // Gera Arquivo CSV com cabeçalho
                            Gravar.WriteLine("{0}{13}{1}{13}{2}{13}{3}{13}{4}{13}{5}{13}{6}{13}{7}{13}{8}{13}{9}{13}{10}{13}{11}{13}{12}", "Horario",
                                Global.strP, Global.strQ, Global.strS,
                                Global.strVa, Global.strVb, Global.strVc,
                                Global.strIa, Global.strIb, Global.strIc,
                                Global.strNo, Global.strTo, Global.strTe, Global.SeparadorCSV);
                        }
                        else
                        {
                            Gravar = new StreamWriter(Arquivo, true);
                        }
                    }
                    SalvarCSV(Registros[Salvar[mm]], Gravar);

                }
                Gravar.Close();
            }
        }

        /// <summary>Salva num arquivo CSV os dados</summary>
        private void SalvarCSV(RegistroDB reg, StreamWriter Gravar)
        {
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            System.Globalization.NumberFormatInfo SeparadorDecimal = System.Globalization.NumberFormatInfo.InvariantInfo;
            Gravar.WriteLine("{0}{13}{1}{13}{2}{13}{3}{13}{4}{13}{5}{13}{6}{13}{7}{13}{8}{13}{9}{13}{10}{13}{11}{13}{12}", reg.Horario.ToString(),
                reg.P[0].ToString(SeparadorDecimal), reg.P[1].ToString(SeparadorDecimal), reg.P[2].ToString(SeparadorDecimal),
                reg.P[3].ToString(SeparadorDecimal), reg.P[4].ToString(SeparadorDecimal), reg.P[5].ToString(SeparadorDecimal),
                reg.P[6].ToString(SeparadorDecimal), reg.P[7].ToString(SeparadorDecimal), reg.P[8].ToString(SeparadorDecimal),
                reg.P[9].ToString(SeparadorDecimal), reg.P[10].ToString(SeparadorDecimal), reg.P[11].ToString(SeparadorDecimal), Global.SeparadorCSV);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // rotina para ser executada apenas uma vez
            // Pegar o "time" mais recente:
            string strTime = GetCSV(Global.strComandoHorario, Uteis.Time2Unix(DateTime.Now), Uteis.Time2Unix(DateTime.Now), Global.striP).Replace("\"", "");
            tUltimaAtualizacao = DTData2DateTime(strTime); //Uteis.Unix2time(Convert.ToUInt32(strTime)); // Horário mais recente armazenado no servidor
            lblMensagens.Text = "Último registro: " + tUltimaAtualizacao.ToString();
            BuscaDados(Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo)), Uteis.Time2Unix(tUltimaAtualizacao));
            GerarGrafico();
            PlotaGrafico(Uteis.Time2Unix(tUltimaAtualizacao.Subtract(JanelaDeTempo)), Uteis.Time2Unix(tUltimaAtualizacao));
            //chartTemperatura.Printing.PrintPreview();
            //picStatus.Image = InterfaceDesktop.Properties.Resources.Vermelho;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {

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

                 string[] ListaDeArquivos = System.IO.Directory.GetFiles(Application.StartupPath,"DB_*.csv");
                 MessageBox.Show(ListaDeArquivos.Length.ToString());
            }
            // Buscar índices no servidor:
            string Requisicao = Global.Servidor + Global.strComandoFeedList + Global.APIKey;

            try
            {
                Requisicao = Servidor.DownloadString(Requisicao);
                List<Feed> Fdd = json2Feed(Requisicao);
                //List<Feed> Fdd = JsonConvert.DeserializeObject<List<Feed>>(Requisicao);
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
                    frmConfig Config = new frmConfig();
                    MessageBox.Show("Verifique os nomes das variáveis");
                    Config.ShowDialog();
                    Config.Dispose();
                }
                Global.ConfigObriatoria = false;
                Global.restart = false;
                frmMain_Load(sender, e);
                return;
            }
            toolStripComboBox1_TextChanged(new object(), new EventArgs());
            tmrGraficos.Enabled = true;
        }

        private List<Feed> json2Feed(string Requisicao)
        {
            const string strListaRemover = "[{\":,}]";
            // Requisição = [{campo:valor, campo:valor, ...}, {campo:valor, ...},...]
            List<Feed> FDD = new List<Feed>();
            string[] Linha = Requisicao.Split('}'); //Linha = {campo:valor, campo:valor, ...}
            for (int jj = 0; jj < Linha.Length; jj++)
            {
                string[] Campo = Linha[jj].Split(','); // Campo = campo:valor
                // Adicionar elementos ao feed
                Feed Fdd = new Feed();
                int Verificador = 0;
                for (int kk = 0; kk < Campo.Length; kk++)
                {
                    string[] Elemento = Campo[kk].Split(':'); // Elemento = campo ou valor
                    if (Elemento.Length == 2)
                    {
                        {
                            if (Elemento[0].Contains("\"id\""))
                            {
                                for (int mm = 0; mm < strListaRemover.Length; mm++)  //remover caracteres especiais
                                {
                                    Elemento[1] = Elemento[1].Replace(strListaRemover.Substring(mm, 1), "");
                                }
                                Fdd.id = Elemento[1];
                                Verificador += 5;
                            }
                            if (Elemento[0].Contains("\"name\""))
                            {
                                for (int mm = 0; mm < strListaRemover.Length; mm++)  //remover caracteres especiais
                                {
                                    Elemento[1] = Elemento[1].Replace(strListaRemover.Substring(mm, 1), "");
                                }
                                Fdd.name = Elemento[1];
                                Verificador += 7;
                            }
                        }
                        if (Verificador == 5 + 7)
                        {
                            FDD.Add(Fdd);
                            Verificador = 0;
                            Fdd = new Feed();
                            break;
                        }
                    }
                }
            }
            return FDD;

        }

        private void timerRelogio_Tick(object sender, EventArgs e)
        {
            // Relógio
            lblHora.Text = Convert.ToString(DateTime.Now);
            System.Diagnostics.Process Processo = System.Diagnostics.Process.GetCurrentProcess();
            lblMEM.Text = string.Format("{0} registros na memória | Memória utilizada = {1:G5} MB", Registros.Count, Processo.PeakPagedMemorySize64 / 1024f / 1024f);
        }

        // Atualiza os gráficos
        private void tmrGraficos_Tick(object sender, EventArgs e)
        {
            bool Plotar = false;
            // Rotina para buscar novas informações no servidor e exibir na tela
            string strTime = GetCSV(Global.strComandoHorario, 0, 0, Global.striP).Replace("\"", "");
            DateTime VelhaUltimaAtualizacao = tUltimaAtualizacao;
            if (strTime.Length > 1)
                    tUltimaAtualizacao = DTData2DateTime(strTime);
//                        Uteis.Unix2time(Convert.ToUInt32(strTime)); // Horário mais recente armazenado no servidor
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
            //{ 0       1   2       3       4   5       6       7       8   9       10      11
            //{ strP, strQ, strS, strVa, strVb, strVc, strIa, strIb, strIc, strNo, strTo, strTe };
            // Exibe na tela:
            // Texto
            lblP.Text = registroDB.P[0].ToString(); lblQ.Text = registroDB.P[1].ToString(); lblS.Text = registroDB.P[2].ToString();
            lblVa.Text = registroDB.P[3].ToString(); lblVb.Text = registroDB.P[4].ToString(); lblVc.Text = registroDB.P[5].ToString();
            lblIa.Text = registroDB.P[6].ToString(); lblIb.Text = registroDB.P[7].ToString(); lblIc.Text = registroDB.P[8].ToString();
            lblNo.Text = lblNo2.Text = registroDB.P[9].ToString(); lblTo.Text = lblTo2.Text = registroDB.P[10].ToString(); lblTe.Text = lblTe2.Text = registroDB.P[11].ToString();
            // Ponteiros
            aTo.Value(registroDB.P[10]); aTe.Value(registroDB.P[11]);
            // "Led"
            int Noleo = Convert.ToInt32(registroDB.P[9]);
            picStatus.Image = picStatus2.Image = ((Noleo < Global.NOleoBaixo) | (Noleo > Global.NOleoAlto)) ? picStatus.Image = InterfaceDesktop.Properties.Resources.Vermelho : picStatus.Image = InterfaceDesktop.Properties.Resources.Verde;
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

        private void chartTemperatura_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            if (!(double.IsNaN(e.NewPosition)))
            {
                Text = string.Format("{0} - {1}", DateTime.FromOADate(e.NewPosition).ToString(), Uteis.Time2Unix(DateTime.FromOADate(e.NewPosition)));
            }
        }

        private void chkP_CheckedChanged(object sender, EventArgs e)
        {
            string[] Todas = Global.strTodas();
            try
            {
                CheckBox Sender = (CheckBox)sender;
                chartTemperatura.Series[Todas[Convert.ToInt32(Sender.Tag)]].Enabled = Sender.Checked;
            }
            catch { }
            chkNo2.Checked = chkNo.Checked;
            chkTo2.Checked = chkTo.Checked;
            chkTe2.Checked = chkTe.Checked;
            // Reajustar gráfico no caso de uma chartárea vazia
            chartTemperatura.ChartAreas["P"].Visible = chkP.Checked | chkQ.Checked | chkS.Checked;
            chartTemperatura.ChartAreas["V"].Visible = chkVa.Checked | chkVb.Checked | chkVc.Checked;
            chartTemperatura.ChartAreas["I"].Visible = chkIa.Checked | chkIb.Checked | chkIc.Checked;
            chartTemperatura.ChartAreas["T"].Visible = chkTe.Checked | chkTo.Checked;
            chartTemperatura.ChartAreas["N"].Visible = chkNo.Checked;
            ReposicionaChartAreas();
        }

        private void ReposicionaChartAreas()
        {
            // Altura das chartareas (%)
            float AlturaP = chartTemperatura.ChartAreas["P"].Visible ? 20 : 0;
            float AlturaV = chartTemperatura.ChartAreas["V"].Visible ? 20 : 0;
            float AlturaI = chartTemperatura.ChartAreas["I"].Visible ? 20 : 0;
            float AlturaT = chartTemperatura.ChartAreas["T"].Visible ? 20 : 0;
            float AlturaN = chartTemperatura.ChartAreas["N"].Visible ? 20 : 0;
            float AlturaTotal = AlturaP + AlturaV + AlturaI + AlturaT + AlturaN; // = 100%, se todas visiveis
            // Nova altura
            AlturaP *= 100 / AlturaTotal;
            AlturaV *= 100 / AlturaTotal;
            AlturaI *= 100 / AlturaTotal;
            AlturaT *= 100 / AlturaTotal;
            AlturaN *= 100 / AlturaTotal;
            // Posiciona as chartAreas
            chartTemperatura.ChartAreas["P"].Position.Y = 0;
            chartTemperatura.ChartAreas["P"].Position.Height = AlturaP;
            chartTemperatura.ChartAreas["V"].Position.Y = AlturaP;
            chartTemperatura.ChartAreas["V"].Position.Height = AlturaV;
            chartTemperatura.ChartAreas["I"].Position.Y = AlturaP + AlturaV;
            chartTemperatura.ChartAreas["I"].Position.Height = AlturaI;
            chartTemperatura.ChartAreas["T"].Position.Y = AlturaP + AlturaV + AlturaI;
            chartTemperatura.ChartAreas["T"].Position.Height = AlturaT;
            chartTemperatura.ChartAreas["N"].Position.Y = AlturaP + AlturaV + AlturaI + AlturaT;
            chartTemperatura.ChartAreas["N"].Position.Height = AlturaN;

            for (int mm = 0; mm < chartTemperatura.ChartAreas.Count; mm++)
            {
                chartTemperatura.Legends[chartTemperatura.ChartAreas[mm].Name].Position.Y = chartTemperatura.ChartAreas[mm].Position.Y;
                chartTemperatura.Legends[chartTemperatura.ChartAreas[mm].Name].Position.Height = chartTemperatura.ChartAreas[mm].Position.Height;
                chartTemperatura.Legends[chartTemperatura.ChartAreas[mm].Name].Enabled = chartTemperatura.ChartAreas[mm].Visible;
            }
            // Scrollbar apenas no gráfico debaixo
            string Ordem = "NTIVP";
            string Alinhamento = "N";
            for (int mm = 1; mm < Ordem.Length; mm++)
            {
                chartTemperatura.ChartAreas[Ordem.Substring(mm, 1)].AxisX.ScrollBar.Enabled =
                    chartTemperatura.ChartAreas[Ordem.Substring(mm - 1, 1)].AxisX.ScrollBar.Enabled & (!(chartTemperatura.ChartAreas[Ordem.Substring(mm - 1, 1)].Visible));
                Alinhamento = chartTemperatura.ChartAreas[Ordem.Substring(mm, 1)].AxisX.ScrollBar.Enabled ? Ordem.Substring(mm, 1) : Alinhamento;
            }

            for (int mm = 0; mm < Ordem.Length; mm++)
            {
                chartTemperatura.ChartAreas[mm].AlignWithChartArea = Alinhamento;
            }
        }

        private void chkNo2_CheckedChanged(object sender, EventArgs e)
        {
            chkNo.Checked = chkNo2.Checked;
        }

        private void chkTo2_CheckedChanged(object sender, EventArgs e)
        {
            chkTo.Checked = chkTo2.Checked;
        }

        private void chkTe2_CheckedChanged(object sender, EventArgs e)
        {
            chkTe.Checked = chkTe2.Checked;
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
            // Converter o texto para intervalo de tempo
            try
            {
                // Horário[0] = 1, //horário[1] = Hora
                string[] horario = cmbJanela.Text.ToString().ToLower().Split(' ');
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
                        //Text = string.Format("Antiga = {0}, Nova = {1}", JanelaDeTempo, NovaJanela);
                        JanelaDeTempo = NovaJanela;// new TimeSpan(dia, hora, minuto, segundo);
                        // Atualizar tudo
                        timerRelogio_Tick(new object(), new EventArgs());
                    }
                    cmbJanela.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    cmbJanela.BackColor = System.Drawing.Color.RosyBrown;
                }
            }
            catch
            {
                cmbJanela.BackColor = System.Drawing.Color.RosyBrown;
            }
        }

        private void chartTemperatura_MouseUp(object sender, MouseEventArgs e)
        {
            // botão direito
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (chartTemperatura.ChartAreas["N"].AxisX.ScaleView.IsZoomed)
                    chartTemperatura.ChartAreas["N"].AxisX.ScaleView.ZoomReset();
            }
        }
    }
}
