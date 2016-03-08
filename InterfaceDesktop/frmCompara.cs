using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace InterfaceDesktop
{
    /// <summary>
    /// Formulário para a comparação de uma variável em dois intervalos diferentes.
    /// </summary>
    public partial class frmCompara : Form
    {
        /// <summary>
        /// Janela de tempo a ser considerada (padrão = 2 horas, definido na interface principal).
        /// </summary>
        TimeSpan JanelaTempo = new TimeSpan(2, 0, 1);
        /// <summary>
        /// Inicio do primeiro intervalo.
        /// </summary>
        UInt32 Inicio1 = 0;
        /// <summary>
        /// Inicio do segundo intervalo.
        /// </summary>
        UInt32 Inicio2 = 0;
        /// <summary>
        /// Cor da curva do primeiro intervalo.
        /// </summary>
        Color Cor1 = Color.Green;
        /// <summary>
        /// Cor da curva do segundo intervalo.
        /// </summary>
        Color Cor2 = Color.Red;
        /// <summary>
        /// Lista de variáveis.
        /// </summary>
        FeedServidor[] vars = Variaveis.strVariaveis();
        /// <summary>
        /// Lista de registros para apresentar na tabela lateral.
        /// </summary>
        public static List<RegistroDB> reg1 = new List<RegistroDB>();
        /// <summary>
        /// Lista de registros carregados do arquivo CSV.
        /// </summary>
        public static List<RegistroDB> Registros = new List<RegistroDB>();
        /// <summary>
        /// Função para inicialização da clase.
        /// </summary>
        public frmCompara()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evento do temporizador responsável pela atualização da quantidade de memória ram utilizada.
        /// </summary>
        /// <param name="sender">Objeto responsável pelo acionamento do evento.</param>
        /// <param name="e">Parâmetros adicionais do evento.</param>
        private void tmrRelogio_Tick(object sender, EventArgs e)
        {
            lblRelogio.Text = DateTime.Now.ToString();
            lblStatus.Text = string.Format("Memória utilizada {0:G5} MB", System.Diagnostics.Process.GetCurrentProcess().PagedMemorySize64 / 1024f / 1024f);
        }
        /// <summary>
        /// Evento relacionado ao carregamento do formulário.
        /// Esse evento carrega a lista de arquvios de banco de dados presentes e define um intervalo de seleção para o usuário escolher, além de atualizar a interface principal.
        /// </summary>
        /// <param name="sender">Objeto responsável pelo evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
        private void frmCompara_Load(object sender, EventArgs e)
        {
            string[] ListaDeArquivos = System.IO.Directory.GetFiles(Application.StartupPath, "DB_*.csv");
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            if (ListaDeArquivos.Length > 0)
            {
                dtpInicio1.MinDate =
                    dtpInicio2.MinDate = ArquivoParaData(ListaDeArquivos[0]);
                dtpInicio1.MaxDate =
                    dtpInicio2.MaxDate =
                    ArquivoParaData(ListaDeArquivos[ListaDeArquivos.Length - 1]).Add(new TimeSpan(23, 59, 59));

                dtpInicio2.Value = dtpInicio2.MaxDate.Date;
                if (dtpInicio1.MinDate < dtpInicio1.MaxDate.AddDays(-7))
                {
                    dtpInicio1.Value = dtpInicio1.MaxDate.Date.AddDays(-7);
                }
                else
                {
                    dtpInicio1.Value = dtpInicio1.MinDate;//.AddDays(-7);
                }

                pic1.BackColor = Cor1;
                pic2.BackColor = Cor2;
                for (int jj = 0; jj < vars.Length; jj++)
                {
                    cmbCategoria.Items.Add(vars[jj].NomeFeed);
                }
                cmbCategoria.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Nenhum registro armazenado");
                this.Close();
            }
        }
        /// <summary>
        /// Rotina responsável pela detecção da data de um arquivo de banco de dados local.
        /// </summary>
        /// <param name="p">Nome do arquivo.</param>
        /// <returns>Retorna a data dos dados do arquivo.</returns>
        private DateTime ArquivoParaData(string p)
        {
            int Ano = 1970; int Mes = 1; int Dia = 1;
            // Nome do arquivo = DB_2016_01_21.csv
            DateTime data = new DateTime(1970, 1, 1);
            try
            {
                Ano = Convert.ToInt16(p.Substring(p.Length - 14, 4));
                Mes = Convert.ToInt16(p.Substring(p.Length - 9, 2));
                Dia = Convert.ToInt16(p.Substring(p.Length - 6, 2));
                data = new DateTime(Ano, Mes, Dia);
            }
            catch { }
            return data;
        }
        /// <summary>
        /// Subrotina responsável por intepretar o texto inserido referente ao tamanho da janela de tempo dos intervalos.
        /// A janela de tempo informada pode ser em semanas, dias, horas ou em minutos.
        /// </summary>
        /// <param name="strJanela">Texto informado.</param>
        /// <returns>Retorna um intervalo de tempo.</returns>
        private TimeSpan JanelaText(string strJanela)
        {
            bool erro = false;
            TimeSpan retorno = JanelaTempo;
            try
            {
                string[] horario = strJanela.ToLower().Split(' ');
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
                                    cmbJanela.Text = string.Format("{0} minutos", JanelaTempo.TotalMinutes);
                                }
                            }
                        }
                    }
                }
                if (dia + hora + minuto + segundo != 0)
                {
                    retorno = new TimeSpan(dia, hora, minuto, segundo);
                }
                else
                {
                    erro = true;
                }
            }
            catch
            {
                erro = true;
            }
            if (erro)
            {
                if (JanelaTempo.TotalMinutes <= 120)
                {
                    cmbJanela.Text = string.Format("{0} minutos", JanelaTempo.TotalMinutes);
                }
                else if (JanelaTempo.TotalHours <= 48)
                {
                    cmbJanela.Text = string.Format("{0} Horas", JanelaTempo.TotalHours);
                }
                else
                {
                    cmbJanela.Text = string.Format("{0} Dias", JanelaTempo.TotalDays);
                }

            }
            return retorno;
        }
        /// <summary>
        /// Evento acionado ao clicar no botão buscar da barra de ferramentas.
        /// Essa subrotina é responsável por chamar a subrotina de busca de dados, classificar a lista de registros, chamar a rotina de gerar o gráfico e habilitar o botão para exportar os dados.
        /// </summary>
        /// <param name="sender">Objeto responsável pelo acionamento do evento.</param>
        /// <param name="e">Parâmetros adicionais para o evento.</param>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //calcula o intervalo de tempo informado
            JanelaTempo = JanelaText(cmbJanela.Text);
            // limpa a variável de dados
            Registros.Clear();
            // limpa a tabela da interface
            Inicio1 = Time2Unix(dtpInicio1.Value);
            Inicio2 = Time2Unix(dtpInicio2.Value);

            BuscaDadosCSV(dtpInicio1.Value, dtpInicio1.Value.Add(JanelaTempo));
            BuscaDadosCSV(dtpInicio2.Value, dtpInicio2.Value.Add(JanelaTempo));

            Registros = Registros.OrderBy(x => x.Horario).ToList<RegistroDB>();
            // Mostra no gráfico
            chrGrafico1.SuspendLayout();
            GeraGradico();

            UInt32 _Final_1 = Time2Unix(dtpInicio1.Value.Add(JanelaTempo));
            UInt32 _Final_2 = Time2Unix(dtpInicio2.Value.Add(JanelaTempo));

            int indice = 0; // índice da variável (procurar em vars o item selecionado na lista)
            for (int mm = 0; mm < vars.Length; mm++)
            {
                if (vars[mm].NomeFeed == cmbCategoria.Text)
                {
                    indice = mm;
                }
            }
            for (int mm = 0; mm < Registros.Count; mm++)
            {
                if ((Registros[mm].Horario > Inicio1) & (Registros[mm].Horario < _Final_1))
                {
                    if (!float.IsNaN(Registros[mm].P[indice]))
                    {
                        chrGrafico1.Series["1"].Points.AddXY(Registros[mm].Horario - Inicio1, Registros[mm].P[indice]);
                    }
                }
                if ((Registros[mm].Horario > Inicio2) & (Registros[mm].Horario < _Final_2))
                {
                    if (!float.IsNaN(Registros[mm].P[indice]))
                    {
                        chrGrafico1.Series["2"].Points.AddXY(Registros[mm].Horario - Inicio2, Registros[mm].P[indice]);
                    }
                }
            }
            chrGrafico1.ResumeLayout();
            AtualizaLista(RegistroMaisProximo(Inicio1), RegistroMaisProximo(Inicio2));
            btnExcel.Enabled = true;
        }
        /// <summary>
        /// Subrotina responsável por detectar o registro (carregado na memória ram) mais próximo ao momento informado.
        /// </summary>
        /// <param name="Comeco">Momento informado.</param>
        /// <returns>Retorna o registro anterior mais próximo do momento informado.</returns>
        private RegistroDB RegistroMaisProximo(uint Comeco)
        {
            int indice = -1;
            for (int mm = 0; mm < Registros.Count; mm++)
            {
                if (Registros[mm].Horario > Comeco)
                {
                    if (Registros.Count > mm + 1)
                    {
                        if ((Registros[mm].Horario - Comeco) > (Registros[mm + 1].Horario - Comeco))
                        {
                            indice = mm;
                        }
                        else
                        {
                            indice = mm + 1;
                        }
                    }
                    else
                    {
                        indice = mm;
                    }
                    break;
                }
            }
            if (indice < 0)
            {
                return new RegistroDB();
            }
            else
            {
                return Registros[indice];
            }
        }
        /// <summary>
        /// Subrotina responsável pela atualização da tabela lateral.
        /// </summary>
        /// <param name="registro1">Registro referente ao primeiro intervalo.</param>
        /// <param name="registro2">Registro referente ao segundo intervalo.</param>
        private void AtualizaLista(RegistroDB registro1, RegistroDB registro2)
        {
            reg1.Clear();
            reg1.Add(registro1);
            reg1.Add(registro2);
            if ((registro1.Horario < Inicio1) | (registro1.Horario > Inicio1 + JanelaTempo.TotalSeconds))
            {
                registro1 = new RegistroDB();
            }
            if ((registro2.Horario < Inicio2) | (registro2.Horario > Inicio2 + JanelaTempo.TotalSeconds))
            {
                registro2 = new RegistroDB();
            }

            dgLista.SuspendLayout();
            dgLista.Rows.Clear();
            dgLista.Columns.Clear();
            dgLista.Columns.Add("0", "");
            dgLista.Columns.Add("1", "Intervalo 1");
            dgLista.Columns.Add("2", "Intervalo 2");
            for (int kk = 0; kk < dgLista.Columns.Count; kk++)
            {
                dgLista.Columns[kk].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgLista.Rows.Add(new string[] { string.Format("Registros (Total: {0})",  Registros.Count), 
                chrGrafico1.Series["1"].Points.Count.ToString(), 
                chrGrafico1.Series["2"].Points.Count.ToString() });

            dgLista.Rows.Add(new string[] { "Horário", 
                Uteis.Unix2time(registro1.Horario).ToString(), 
                Uteis.Unix2time(registro2.Horario).ToString() });
            dgLista.Rows.Add(new string[] { "Variáveis elétricas" });
            dgLista.Rows.Add(new string[] { "  Grandezas instanâneas" });
            dgLista.Rows.Add(new string[] { "    Potência" });
            dgLista.Rows.Add(new string[] { "       P (kW)", 
                ExibeFloat(registro1.P[Variaveis.fP.indice]), 
                ExibeFloat(registro2.P[Variaveis.fP.indice]) });
            dgLista.Rows.Add(new string[] { "       Q (kVAr)", 
                ExibeFloat(registro1.P[Variaveis.fQ.indice]), 
                ExibeFloat(registro2.P[Variaveis.fQ.indice]) });
            dgLista.Rows.Add(new string[] { "       S (kVA)", 
                ExibeFloat(registro1.P[Variaveis.fS.indice]), 
                ExibeFloat(registro2.P[Variaveis.fS.indice]) });
            dgLista.Rows.Add(new string[] { "    Tensão" });
            dgLista.Rows.Add(new string[] { "      Tensão de linha" });
            dgLista.Rows.Add(new string[] { "        Vab (V)",
                ExibeFloat(registro1.P[Variaveis.fVab.indice]),
                ExibeFloat(registro2.P[Variaveis.fVab.indice]) });
            dgLista.Rows.Add(new string[] { "        Vbc (V)",
                ExibeFloat(registro1.P[Variaveis.fVbc.indice]),
                ExibeFloat(registro2.P[Variaveis.fVbc.indice]) });
            dgLista.Rows.Add(new string[] { "        Vca (V)", 
                ExibeFloat(registro1.P[Variaveis.fVca.indice]),
                ExibeFloat(registro2.P[Variaveis.fVca.indice]) });
            dgLista.Rows.Add(new string[] { "      Tensão de fase" });
            dgLista.Rows.Add(new string[] { "    Corrente" });
            dgLista.Rows.Add(new string[] { "      Ia (A)",
                ExibeFloat(registro1.P[Variaveis.fIa.indice]),
                ExibeFloat(registro2.P[Variaveis.fIa.indice]) });
            dgLista.Rows.Add(new string[] { "      Ib (A)",
                ExibeFloat(registro1.P[Variaveis.fIb.indice]),
                ExibeFloat(registro2.P[Variaveis.fIb.indice]) });
            dgLista.Rows.Add(new string[] { "      Ic (A)",
                ExibeFloat(registro1.P[Variaveis.fIc.indice]), 
                ExibeFloat(registro2.P[Variaveis.fIc.indice]) });
            dgLista.Rows.Add(new string[] { "    Frequência (Hz)",
                ExibeFloat(registro1.P[Variaveis.fFreq.indice]), 
                ExibeFloat(registro2.P[Variaveis.fFreq.indice]) });
            dgLista.Rows.Add(new string[] { "    Fator de potência",
                ExibeFloat(registro1.P[Variaveis.fFatorPotencia.indice]),
                ExibeFloat(registro2.P[Variaveis.fFatorPotencia.indice]) });
            dgLista.Rows.Add(new string[] { "  Valores de energia" });
            dgLista.Rows.Add(new string[] { "    EP (kWh)", 
                ExibeFloat(registro1.P[Variaveis.fEP.indice]), 
                ExibeFloat(registro2.P[Variaveis.fEP.indice]) });
            dgLista.Rows.Add(new string[] { "    EQ (kVArh)",
                ExibeFloat(registro1.P[Variaveis.fEQ.indice]),
                ExibeFloat(registro2.P[Variaveis.fEQ.indice]) });
            dgLista.Rows.Add(new string[] { "    ES (kVAh)",
                ExibeFloat(registro1.P[Variaveis.fES.indice]), 
                ExibeFloat(registro2.P[Variaveis.fES.indice]) });
            dgLista.Rows.Add(new string[] { "  Valores de demanda média" });
            dgLista.Rows.Add(new string[] { "    Corrente IM" });
            dgLista.Rows.Add(new string[] { "      IMa (A)", 
                ExibeFloat(registro1.P[Variaveis.fIMa.indice]),
                ExibeFloat(registro2.P[Variaveis.fIMa.indice]) });
            dgLista.Rows.Add(new string[] { "      IMb (A)", 
                ExibeFloat(registro1.P[Variaveis.fIMb.indice]), 
                ExibeFloat(registro2.P[Variaveis.fIMb.indice]) });
            dgLista.Rows.Add(new string[] { "      IMc (A)", 
                ExibeFloat(registro1.P[Variaveis.fIMc.indice]), 
                ExibeFloat(registro2.P[Variaveis.fIMc.indice]) });
            dgLista.Rows.Add(new string[] { "    Potência" });
            dgLista.Rows.Add(new string[] { "      PM (kW)", 
                ExibeFloat(registro1.P[Variaveis.fPM.indice]), 
                ExibeFloat(registro2.P[Variaveis.fPM.indice]) });
            dgLista.Rows.Add(new string[] { "      QM (kVAr)", 
                ExibeFloat(registro1.P[Variaveis.fQM.indice]), 
                ExibeFloat(registro2.P[Variaveis.fQM.indice]) });
            dgLista.Rows.Add(new string[] { "      SM (kVA)", 
                ExibeFloat(registro1.P[Variaveis.fSM.indice]), 
                ExibeFloat(registro2.P[Variaveis.fSM.indice]) });
            dgLista.Rows.Add(new string[] { "   Valores de demanda máxima" });
            dgLista.Rows.Add(new string[] { "      Corrente Máxima" });
            dgLista.Rows.Add(new string[] { "        IPa (A)", 
                ExibeFloat(registro1.P[Variaveis.fIPa.indice]), 
                ExibeFloat(registro2.P[Variaveis.fIPa.indice]) });
            dgLista.Rows.Add(new string[] { "        IPb (A)", 
                ExibeFloat(registro1.P[Variaveis.fIPb.indice]), 
                ExibeFloat(registro2.P[Variaveis.fIPb.indice]) });
            dgLista.Rows.Add(new string[] { "        IPc (A)", 
                ExibeFloat(registro1.P[Variaveis.fIPc.indice]), 
                ExibeFloat(registro2.P[Variaveis.fIPc.indice]) });
            dgLista.Rows.Add(new string[] { "      Potência" });
            dgLista.Rows.Add(new string[] { "        PP (kW)", 
                ExibeFloat(registro1.P[Variaveis.fPP.indice]), 
                ExibeFloat(registro2.P[Variaveis.fPP.indice]) });
            dgLista.Rows.Add(new string[] { "        QP (kVAr)", 
                ExibeFloat(registro1.P[Variaveis.fQP.indice]), 
                ExibeFloat(registro2.P[Variaveis.fQP.indice]) });
            dgLista.Rows.Add(new string[] { "        SP (kVA)", 
                ExibeFloat(registro1.P[Variaveis.fSP.indice]), 
                ExibeFloat(registro2.P[Variaveis.fSP.indice]) });
            dgLista.Rows.Add(new string[] { "Outras variáveis" });
            dgLista.Rows.Add(new string[] { "  Temperatura" });
            dgLista.Rows.Add(new string[] { "    Enrolamentos (°C)", 
                ExibeFloat(registro1.P[Variaveis.fTEnrolamento.indice]), 
                ExibeFloat(registro2.P[Variaveis.fTEnrolamento.indice]) });
            dgLista.Rows.Add(new string[] { "    Óleo (°C)", 
                ExibeFloat(registro1.P[Variaveis.fTOleo.indice]), 
                ExibeFloat(registro2.P[Variaveis.fTOleo.indice]) });
            dgLista.Rows.Add(new string[] { "  Nível do óleo",
                ((registro1.P[Variaveis.fNivelOleo.indice]==Global.intNOleoBaixo)?Global.strNOleoBaixo:(registro1.P[Variaveis.fNivelOleo.indice]>=Global.intNOleoAlto)?Global.strNOleoAlto:Global.strNOleoNormal).Replace('\n',' '),
                ((registro2.P[Variaveis.fNivelOleo.indice]==Global.intNOleoBaixo)?Global.strNOleoBaixo:(registro2.P[Variaveis.fNivelOleo.indice]>=Global.intNOleoAlto)?Global.strNOleoAlto:Global.strNOleoNormal).Replace('\n',' ')});
            dgLista.Rows.Add(new string[] { "  Válvula de alívio de pressão",
                ((registro1.P[Variaveis.fValvulaPressao.indice]==0)?Global.strValvulaNormal:Global.strValvulaAtivada),//});
                ((registro2.P[Variaveis.fValvulaPressao.indice]==0)?Global.strValvulaNormal:Global.strValvulaAtivada)});
                //registro2.P[Variaveis.fValvulaPressao.indice].ToString(), registro2.P[Variaveis.fValvulaPressao.indice].ToString() });

            dgLista.ResumeLayout();
        }
        /// <summary>
        /// Rotina responsável por apresentar corretamente os valores para tabela lateral.
        /// </summary>
        /// <param name="valor">Número informado.</param>
        /// <returns>"-" se não for um número válido, "xyz" se o número for igual a xyz.</returns>
        private string ExibeFloat(float valor)
        {
            if (float.IsNaN(valor))
            {
                return "-";
            }
            else
            {
                return valor.ToString();
            }
        }
        /// <summary>
        /// Função responsável por transformar uma data em unix time.
        /// Essa função considera o fuso-horário e horário de verão de acordo com as configurações do computador.
        /// </summary>
        /// <param name="dateTime">Data/hora desejada.</param>
        /// <returns>Unix time correspondente.</returns>
        private uint Time2Unix(DateTime dateTime)
        {
            return (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }
        /// <summary>
        /// Subrotina responsável por gerar o gráfico a ser exibido.
        /// Essa rotina limpa totalmente o gráfico, cria e configura uma ChartArea, cria, configura e exibe duas séries de dados.
        /// </summary>
        private void GeraGradico()
        {
            chrGrafico1.Series.Clear();
            chrGrafico1.Legends.Clear();
            chrGrafico1.ChartAreas.Clear();

            chrGrafico1.ChartAreas.Add("0");

            chrGrafico1.Legends.Add("0").Enabled = false;
            // Habilita os cursores
            chrGrafico1.ChartAreas["0"].CursorX.IsUserEnabled = true;
            chrGrafico1.ChartAreas["0"].CursorX.LineWidth = 2;
            chrGrafico1.ChartAreas["0"].CursorX.LineColor = System.Drawing.Color.Red;
            chrGrafico1.ChartAreas["0"].CursorX.LineDashStyle = ChartDashStyle.Dot;
            chrGrafico1.ChartAreas["0"].CursorX.SelectionColor = System.Drawing.Color.DeepSkyBlue;
            // Melhoras no visual
            chrGrafico1.ChartAreas["0"].AxisX.ScrollBar.Size = 10;
            chrGrafico1.ChartAreas["0"].AxisX.ScrollBar.IsPositionedInside = false;
            // Habilita o zoom
            chrGrafico1.ChartAreas["0"].CursorX.IsUserSelectionEnabled = true;
            // Resolução máxima
            chrGrafico1.ChartAreas["0"].CursorX.IntervalType = DateTimeIntervalType.Minutes;
            chrGrafico1.ChartAreas["0"].CursorX.Interval = 10;// minutos
            chrGrafico1.ChartAreas["0"].AxisX.ScaleView.SmallScrollMinSize = 1;
            chrGrafico1.ChartAreas["0"].AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Minutes;
            chrGrafico1.ChartAreas["0"].AxisX.Enabled = AxisEnabled.False;
            Series srSerie1 = new Series("1"); // nova série
            Series srSerie2 = new Series("2"); // nova série
            srSerie1.Legend = //"LegendaOculta";
                srSerie2.Legend =
                srSerie1.ChartArea =
                srSerie2.ChartArea = "0";// cada série associada com a chartárea e a legenda adequadas
            srSerie1.XValueType =
                srSerie2.XValueType = ChartValueType.Time;
            srSerie1.ChartType =
                srSerie2.ChartType = SeriesChartType.StepLine; // gráfico em degraus (não sabemos o que acontece entre duas medidas
            srSerie1.BorderWidth =
                srSerie2.BorderWidth = 2; // tamanho da linha
            srSerie1.Color = Cor1;
            srSerie2.Color = Cor2;

            chrGrafico1.Series.Add(srSerie1);
            chrGrafico1.Series.Add(srSerie2);
        }
        /// <summary>
        /// Evento ao clicar na cor do primeiro intervalo.
        /// Esse evento modifica a cor do primeiro intervalo.
        /// </summary>
        /// <param name="sender">Objeto responsável por disparar o evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
        private void pic1_Click(object sender, EventArgs e)
        {
            Cor1 =
                pic1.BackColor = escolheCor(pic1.BackColor);
        }
        /// <summary>
        /// Função responsável por exibir a janela de selção de cor e traduzir a cor selecionada no formulário em uma cor RGB.
        /// </summary>
        /// <param name="CorAntiga">Cor antiga, para o caso de o usuário selecionar "Cancelar".</param>
        /// <returns>Retorna a cor selecionada.</returns>
        private Color escolheCor(Color CorAntiga)
        {
            ColorDialog Cor = new ColorDialog();
            if (Cor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return Cor.Color;
            }
            return CorAntiga;
        }
        /// <summary>
        /// Evento ao clicar na cor do segundo intervalo.
        /// Esse evento modifica a cor do segundo intervalo.
        /// </summary>
        /// <param name="sender">Objeto responsável por disparar o evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
        private void pic2_Click(object sender, EventArgs e)
        {
            Cor2 =
                pic2.BackColor = escolheCor(pic2.BackColor);
        }

        /// <summary>
        /// Subrotina responsável por buscar as informações no banco de dados local.
        /// Essa subrotina carrega cada linha de cada arquivo de banco de dados local dentro do intervalo informado, analiza se a informação está no intervalo e armazena as informações na memória RAM.
        /// </summary>
        /// <param name="inicio">Inicio do intervalo.</param>
        /// <param name="fim">Final do intervalo.</param>
        private void BuscaDadosCSV(DateTime inicio, DateTime fim)
        {
            int[] indices = new int[vars.Length + 1];
            string Arquivo = ComandosCSV.ArquivoCSV(inicio);
            // primeiro arquivo: verificar horário de inicio antes de adicionar
            if (File.Exists(Arquivo))
            {
                using (StreamReader leitor = new StreamReader(Arquivo))
                {
                    // Identifica a ordem das variáveis
                    Arquivo = leitor.ReadLine();
                    string[] campos = Arquivo.Split(Global.charSeparadorCSV);
                    for (int jj = 1; jj < campos.Length; jj++)
                    {
                        for (int mm = 0; mm < vars.Length; mm++)
                        {
                            if (campos[jj] == vars[mm].NomeFeed)
                            {
                                indices[jj] = mm;
                                break;
                            }
                        }
                    }
                    DateTime horariodoregistro;
                    while (!leitor.EndOfStream)
                    {
                        RegistroDB reg = new RegistroDB();
                        Arquivo = leitor.ReadLine().Replace('.', ',');
                        campos = Arquivo.Split(Global.charSeparadorCSV);
                        reg.Horario = Convert.ToUInt32(campos[0]);
                        for (int jj = 1; jj < campos.Length; jj++)
                        {
                            if (campos[jj] == "")
                            {
                                reg.P[indices[jj]] = float.NaN;
                            }
                            else
                            {
                                reg.P[indices[jj]] = Convert.ToSingle(campos[jj]);
                            }
                        }
                        horariodoregistro = Uteis.Unix2time(reg.Horario);
                        if ((inicio.Subtract(horariodoregistro).TotalSeconds < 0) & (fim.Subtract(horariodoregistro).TotalSeconds > 0))
                        {
                            if (Registros.FindIndex(x => x.Horario == reg.Horario) < 0)
                            {
                                Registros.Add(reg); // ignorar repetidos
                            }
                        }
                    }
                }
            }
            inicio = inicio.AddDays(1); //próximo
            // Não precisa verificar nada
            if (inicio.Date <= fim.Date)
            {
                do
                {
                    Arquivo = ComandosCSV.ArquivoCSV(inicio);
                    if (File.Exists(Arquivo))
                    {
                        using (StreamReader leitor = new StreamReader(Arquivo))
                        {
                            // Identifica a ordem das variáveis
                            Arquivo = leitor.ReadLine();
                            string[] campos = Arquivo.Split(Global.charSeparadorCSV);
                            for (int jj = 1; jj < campos.Length; jj++)
                            {
                                for (int mm = 0; mm < vars.Length; mm++)
                                {
                                    if (campos[jj] == vars[mm].NomeFeed)
                                    {
                                        indices[jj] = mm;
                                        break;
                                    }
                                }
                            }
                            while (!leitor.EndOfStream)
                            {
                                RegistroDB reg = new RegistroDB();
                                Arquivo = leitor.ReadLine().Replace('.', ',');
                                campos = Arquivo.Split(Global.charSeparadorCSV);
                                reg.Horario = Convert.ToUInt32(campos[0]);
                                for (int jj = 1; jj < campos.Length; jj++)
                                {
                                    reg.P[indices[jj]] = Convert.ToSingle(campos[jj]);
                                }
                                Registros.Add(reg);
                            }
                        }
                    }
                    inicio = inicio.AddDays(1); //próximo
                } while (fim.Subtract(inicio).Days > 1);
            }
            // último dia (verificar horário de término)
            if (fim > inicio)
            {
                Arquivo = ComandosCSV.ArquivoCSV(inicio);
                if (File.Exists(Arquivo) & (inicio < fim))
                {
                    using (StreamReader leitor = new StreamReader(Arquivo))
                    {
                        // Identifica a ordem das variáveis
                        Arquivo = leitor.ReadLine();
                        string[] campos = Arquivo.Split(Global.charSeparadorCSV);
                        for (int jj = 1; jj < campos.Length; jj++)
                        {
                            for (int mm = 0; mm < vars.Length; mm++)
                            {
                                if (campos[jj] == vars[mm].NomeFeed)
                                {
                                    indices[jj] = mm;
                                    break;
                                }
                            }
                        }
                        while (!leitor.EndOfStream)
                        {
                            RegistroDB reg = new RegistroDB();
                            Arquivo = leitor.ReadLine().Replace('.', ',');
                            campos = Arquivo.Split(Global.charSeparadorCSV);
                            reg.Horario = Convert.ToUInt32(campos[0]);
                            for (int jj = 1; jj < campos.Length; jj++)
                            {
                                reg.P[indices[jj]] = Convert.ToSingle(campos[jj]);
                            }
                            if (fim.Subtract(Uteis.Unix2time(reg.Horario)).TotalSeconds > 0)
                            {
                                Registros.Add(reg);
                            }
                        }
                    }
                }
            }
            Registros = Registros.OrderBy(registro => registro.Horario).ToList<RegistroDB>();
        }
        /// <summary>
        /// Evento disparado ao soltar um botão do mouse sobre o gráfico. Esse evento tem o objetivo de resetar a situação de zoom do gráfico.
        /// </summary>
        /// <param name="sender">Objeto responsável por disparar o evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
        private void chrGrafico1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (chrGrafico1.ChartAreas["0"].AxisX.ScaleView.IsZoomed)
                {
                    chrGrafico1.ChartAreas["0"].AxisX.ScaleView.ZoomReset();
                }
            }
        }
        /// <summary>
        /// Evento acionado ao modificar a posição do cursor do gráfico.
        /// Esse evento poderá chamar uma rotina para atualizar a tabela lateral.
        /// </summary>
        /// <param name="sender">Objeto responsável por disparar o evento.</param>
        /// <param name="e">Parâmetros adicionais do evento.</param>
        private void chrGrafico1_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            if (Registros.Count > 1)
            {
                if (!(double.IsNaN(e.NewPosition)))
                {
                    if (e.NewPosition > 0)
                    {
                        UInt32 posicao = Convert.ToUInt32(e.NewPosition);
                        int indice1 = 0;
                        int indice2 = 0;
                        for (int jj = 0; jj < Registros.Count; jj++)
                        {
                            if (posicao + Inicio1 <= Registros[jj].Horario)
                            {
                                if (indice1 == 0)
                                {
                                    indice1 = (jj > 0) ? jj - 1 : jj;
                                }
                            }
                            if (posicao + Inicio2 <= Registros[jj].Horario)
                            {
                                if (indice2 == 0)
                                {
                                    indice2 = (jj > 0) ? jj - 1 : jj;
                                }
                            }
                        }
                        AtualizaLista(Registros[indice1], Registros[indice2]);
                        chrGrafico1.ChartAreas["0"].CursorX.Position = Registros[indice1].Horario - Inicio1;
                    }
                }
            }
        }
        /// <summary>
        /// Evento acionado ao clicar no botão 'Exportar'.
        /// Essa subrotina é responsável por exibir uma janela para o usuário escolher onde salvar o arquivo e qual tipo de arquivo salvar.
        /// Após isso, a subrotina detecta se o usuário selecionou a opção "Cancelar" ou o formato do arquivo e o endereço onde salvar o arquivo.
        /// Em seguida a subrotina chama a subrotina necessária para salvar o arquivo.
        /// </summary>
        /// <param name="sender">Objeto responsável por disparar o evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
        private void btnExcel_Click(object sender, EventArgs e)
        {
            string Pasta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            SaveFileDialog SalvaArquivo = new SaveFileDialog();
            SalvaArquivo.FileName = "Comparativo.xlsx";
            //SalvaArquivo.FileName = "testes.xlsx";
            SalvaArquivo.InitialDirectory = Pasta;
            SalvaArquivo.Filter = "Arquivo XLSX|*.xlsx|Arquivo CSV|*.csv|Imagem PNG|*.png|Imagem JPG|*.jpg|Imagem BMP|*.bmp";
            //SalvaArquivo.Filter = "Imagem PNG|*.png|Imagem JPG|*.jpg|Imagem BMP|*.bmp";
            SalvaArquivo.DefaultExt = "xlsx";
            //SalvaArquivo.ShowDialog();
            //if (SalvaArquivo.FileName.Length > 3)
            if (SalvaArquivo.ShowDialog() == DialogResult.OK)
            {
                int ponto = SalvaArquivo.FileName.LastIndexOf('.');
                if (ponto < 0) ponto = SalvaArquivo.FileName.Length - 3;
                string Formato = SalvaArquivo.FileName.Substring(ponto).ToLower();
                switch (Formato)
                {
                    case ".csv":
                        {
                            {
                                // Salvar CSV
                                using (StreamWriter GravarArquivoCSV = new StreamWriter(SalvaArquivo.FileName, false))
                                {
                                    System.Globalization.NumberFormatInfo SeparadorDecimal = System.Globalization.NumberFormatInfo.CurrentInfo;// System.Globalization.NumberFormatInfo.InvariantInfo;
                                    StringBuilder bstr = new StringBuilder("Horario");
                                    for (int jj = 0; jj < vars.Length; jj++)
                                    {
                                        bstr.Append(Global.charSeparadorCSVCSV);
                                        bstr.Append(vars[jj].NomeFeed);
                                    }
                                    GravarArquivoCSV.WriteLine(bstr);
                                    for (int jj = 0; jj < reg1.Count; jj++)
                                    {
                                        bstr = new StringBuilder(Registros[jj].Horario.ToString());
                                        for (int kk = 0; kk < vars.Length; kk++)
                                        {
                                            bstr.Append(Global.charSeparadorCSVCSV);
                                            if (!float.IsNaN(reg1[jj].P[vars[kk].indice]))
                                            {
                                                bstr.Append(reg1[jj].P[vars[kk].indice].ToString(SeparadorDecimal));
                                            }
                                        }
                                        GravarArquivoCSV.WriteLine(bstr);
                                    }
                                }
                            }
                            break;
                        }
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".bmp":
                        {
                            Graphics Imagem = chrGrafico1.CreateGraphics();
                            Bitmap bitmat = new Bitmap(chrGrafico1.Width, chrGrafico1.Height, Imagem);
                            Rectangle retangulo = new Rectangle(0, 0, chrGrafico1.Width, chrGrafico1.Height);
                            chrGrafico1.DrawToBitmap(bitmat, retangulo);
                            System.Drawing.Imaging.ImageFormat imgFormato = System.Drawing.Imaging.ImageFormat.Jpeg;
                            if (Formato == ".bmp")
                                imgFormato = System.Drawing.Imaging.ImageFormat.Bmp;
                            if (Formato == ".png")
                                imgFormato = System.Drawing.Imaging.ImageFormat.Png;
                            bitmat.Save(SalvaArquivo.FileName, imgFormato);
                            break;
                        }
                    case ".xlsx":
                    case ".xls":
                        {
                            new SalvarExcel().SalvarXLSX(SalvaArquivo.FileName, 0, uint.MaxValue, FormSalvarExcel.frmComparacao);
                            Type VerificaExcel = Type.GetTypeFromProgID("Excel.Application");
                            if (VerificaExcel == null)
                            {
                                System.Diagnostics.Process.Start("explorer.exe", "/select," + SalvaArquivo.FileName);
                            }
                            else
                            {
                                System.Diagnostics.Process.Start(SalvaArquivo.FileName);
                            }
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Formato não suportado");
                            break;
                        }
                }
            }
        }
        /// <summary>
        /// Evento disparado ao fechar o formulário.
        /// Esse evento tem como objetivo acelerar o processo de coleta de lixo, liberando rapidamente a memória utilizada pelo formulário.
        /// </summary>
        /// <param name="sender">Objeto responsável por acionar o evento.</param>
        /// <param name="e">Parâmetros adicionais.</param>
        private void frmCompara_FormClosing(object sender, FormClosingEventArgs e)
        {
            Registros.Clear();
            chrGrafico1.Dispose();
            GC.Collect(); GC.WaitForPendingFinalizers();
        }
    }
}
