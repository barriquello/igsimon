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
    public partial class frmCompara : Form
    {
        TimeSpan JanelaTempo = new TimeSpan(2, 0, 1);

        UInt32 Inicio1 = 0;
        UInt32 Inicio2 = 0;

        Color Cor1 = Color.Green;
        Color Cor2 = Color.Red;
        FeedServidor[] vars = Variaveis.strVariaveis();

        public static List<RegistroDB> Registros = new List<RegistroDB>();

        public frmCompara()
        {
            InitializeComponent();
        }

        private void tmrRelogio_Tick(object sender, EventArgs e)
        {
            lblRelogio.Text = DateTime.Now.ToString();
            lblStatus.Text = string.Format("Memória utilizada {0:G5} MB", System.Diagnostics.Process.GetCurrentProcess().PagedMemorySize64 / 1024f / 1024f);
        }

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
                    chrGrafico1.Series["1"].Points.AddXY(Registros[mm].Horario - Inicio1, Registros[mm].P[indice]);
                }
                if ((Registros[mm].Horario > Inicio2) & (Registros[mm].Horario < _Final_2))
                {
                    chrGrafico1.Series["2"].Points.AddXY(Registros[mm].Horario - Inicio2, Registros[mm].P[indice]);
                }
            }
            chrGrafico1.ResumeLayout();
            AtualizaLista(RegistroMaisProximo(Inicio1), RegistroMaisProximo(Inicio2));
            btnExcel.Enabled = true;
        }

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

        private void AtualizaLista(RegistroDB registro1, RegistroDB registro2)
        {
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
            dgLista.Rows.Add(new string[] { string.Format("Registros (Total: {0})", Registros.Count), chrGrafico1.Series["1"].Points.Count.ToString(), chrGrafico1.Series["2"].Points.Count.ToString() });

            //dgLista.Rows.Add(new string[] {"Número total de registros", Registros.Count.ToString() });
            //dgLista.Rows.Add(new string[] { "Primeira ocorrência", Inicio1.ToString(), Inicio2.ToString() });
            //dgLista.Rows.Add(new string[] { "Última ocorrência", , DateTime.Now.AddSeconds(120).ToString() });
            dgLista.Rows.Add(new string[] { "Horário", Uteis.Unix2time(registro1.Horario).ToString(), Uteis.Unix2time(registro2.Horario).ToString() });
            dgLista.Rows.Add(new string[] { "Variáveis elétricas" });
            dgLista.Rows.Add(new string[] { "  Grandezas instanâneas" });
            dgLista.Rows.Add(new string[] { "    Potência" });
            dgLista.Rows.Add(new string[] { "       P", registro1.P[Variaveis.fP.indice].ToString(), registro2.P[Variaveis.fP.indice].ToString() });
            dgLista.Rows.Add(new string[] { "       Q", registro1.P[Variaveis.fQ.indice].ToString(), registro2.P[Variaveis.fQ.indice].ToString() });
            dgLista.Rows.Add(new string[] { "       S", registro1.P[Variaveis.fS.indice].ToString(), registro2.P[Variaveis.fS.indice].ToString() });
            dgLista.Rows.Add(new string[] { "    Tensão" });
            dgLista.Rows.Add(new string[] { "      Tensão de linha" });
            dgLista.Rows.Add(new string[] { "        Vab", registro1.P[Variaveis.fVab.indice].ToString(), registro2.P[Variaveis.fVab.indice].ToString() });
            dgLista.Rows.Add(new string[] { "        Vbc", registro1.P[Variaveis.fVbc.indice].ToString(), registro2.P[Variaveis.fVbc.indice].ToString() });
            dgLista.Rows.Add(new string[] { "        Vca", registro1.P[Variaveis.fVca.indice].ToString(), registro2.P[Variaveis.fVca.indice].ToString() });
            dgLista.Rows.Add(new string[] { "      Tensão de fase" });
            dgLista.Rows.Add(new string[] { "    Corrente" });
            dgLista.Rows.Add(new string[] { "      Ia", registro1.P[Variaveis.fIa.indice].ToString(), registro2.P[Variaveis.fIa.indice].ToString() });
            dgLista.Rows.Add(new string[] { "      Ib", registro1.P[Variaveis.fIb.indice].ToString(), registro2.P[Variaveis.fIb.indice].ToString() });
            dgLista.Rows.Add(new string[] { "      Ic", registro1.P[Variaveis.fIc.indice].ToString(), registro2.P[Variaveis.fIc.indice].ToString() });
            dgLista.Rows.Add(new string[] { "    Frequência", registro1.P[Variaveis.fFreq.indice].ToString(), registro2.P[Variaveis.fFreq.indice].ToString() });
            dgLista.Rows.Add(new string[] { "    Fator de potência", registro1.P[Variaveis.fFatorPotencia.indice].ToString(), registro2.P[Variaveis.fFatorPotencia.indice].ToString() });
            dgLista.Rows.Add(new string[] { "  Valores de energia" });
            dgLista.Rows.Add(new string[] { "    EP", registro1.P[Variaveis.fEP.indice].ToString(), registro2.P[Variaveis.fEP.indice].ToString() });
            dgLista.Rows.Add(new string[] { "    EQ", registro1.P[Variaveis.fEQ.indice].ToString(), registro2.P[Variaveis.fEQ.indice].ToString() });
            dgLista.Rows.Add(new string[] { "    ES", registro1.P[Variaveis.fES.indice].ToString(), registro2.P[Variaveis.fES.indice].ToString() });
            dgLista.Rows.Add(new string[] { "  Valores de demanda média" });
            dgLista.Rows.Add(new string[] { "    Corrente IM" });
            dgLista.Rows.Add(new string[] { "      IMa", registro1.P[Variaveis.fIMa.indice].ToString(), registro2.P[Variaveis.fIMa.indice].ToString() });
            dgLista.Rows.Add(new string[] { "      IMb", registro1.P[Variaveis.fIMb.indice].ToString(), registro2.P[Variaveis.fIMb.indice].ToString() });
            dgLista.Rows.Add(new string[] { "      IMc", registro1.P[Variaveis.fIMc.indice].ToString(), registro2.P[Variaveis.fIMc.indice].ToString() });
            dgLista.Rows.Add(new string[] { "    Potência" });
            dgLista.Rows.Add(new string[] { "      PM", registro1.P[Variaveis.fPM.indice].ToString(), registro2.P[Variaveis.fPM.indice].ToString() });
            dgLista.Rows.Add(new string[] { "      QM", registro1.P[Variaveis.fQM.indice].ToString(), registro2.P[Variaveis.fQM.indice].ToString() });
            dgLista.Rows.Add(new string[] { "      SM", registro1.P[Variaveis.fSM.indice].ToString(), registro2.P[Variaveis.fSM.indice].ToString() });
            dgLista.Rows.Add(new string[] { "   Valores de demanda máxima" });
            dgLista.Rows.Add(new string[] { "      Corrente Máxima" });
            dgLista.Rows.Add(new string[] { "        IPa", registro1.P[Variaveis.fIPa.indice].ToString(), registro2.P[Variaveis.fIPa.indice].ToString() });
            dgLista.Rows.Add(new string[] { "        IPb", registro1.P[Variaveis.fIPb.indice].ToString(), registro2.P[Variaveis.fIPb.indice].ToString() });
            dgLista.Rows.Add(new string[] { "        IPc", registro1.P[Variaveis.fIPc.indice].ToString(), registro2.P[Variaveis.fIPc.indice].ToString() });
            dgLista.Rows.Add(new string[] { "      Potência" });
            dgLista.Rows.Add(new string[] { "        PP", registro1.P[Variaveis.fPP.indice].ToString(), registro2.P[Variaveis.fPP.indice].ToString() });
            dgLista.Rows.Add(new string[] { "        QP", registro1.P[Variaveis.fQP.indice].ToString(), registro2.P[Variaveis.fQP.indice].ToString() });
            dgLista.Rows.Add(new string[] { "        SP", registro1.P[Variaveis.fSP.indice].ToString(), registro2.P[Variaveis.fSP.indice].ToString() });
            dgLista.Rows.Add(new string[] { "Outras variáveis" });
            dgLista.Rows.Add(new string[] { "  Temperatura" });
            dgLista.Rows.Add(new string[] { "    Enrolamentos", registro1.P[Variaveis.fTEnrolamento.indice].ToString(), registro2.P[Variaveis.fTEnrolamento.indice].ToString() });
            dgLista.Rows.Add(new string[] { "    Óleo", registro1.P[Variaveis.fTOleo.indice].ToString(), registro2.P[Variaveis.fTOleo.indice].ToString() });
            dgLista.Rows.Add(new string[] { "  Nível do óleo", registro1.P[Variaveis.fNivelOleo.indice].ToString(), registro2.P[Variaveis.fNivelOleo.indice].ToString() });
            dgLista.Rows.Add(new string[] { "  Válvula de segurança", registro1.P[Variaveis.fValvulaPressao.indice].ToString(), registro2.P[Variaveis.fValvulaPressao.indice].ToString() });

            dgLista.ResumeLayout();
        }

        private uint Time2Unix(DateTime dateTime)
        {
            return (UInt32)dateTime.Subtract(new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }

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
                srSerie2.ChartArea = "0";// Tipos[jj].ToString();// Global.strCategoria[jj]; // cada série associada com a chartárea e a legenda adequadas
            //srSerie.XValueType = ChartValueType.Auto; // Bug do .NET
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

        private void pic1_Click(object sender, EventArgs e)
        {
            Cor1 =
                pic1.BackColor = escolheCor(pic1.BackColor);
        }

        private Color escolheCor(Color CorAntiga)
        {
            ColorDialog Cor = new ColorDialog();
            if (Cor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return Cor.Color;
            }
            return CorAntiga;
        }

        private void pic2_Click(object sender, EventArgs e)
        {
            Cor2 =
                pic2.BackColor = escolheCor(pic2.BackColor);
        }


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
                            reg.P[indices[jj]] = Convert.ToSingle(campos[jj]);
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
        }

        private void chrGrafico1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (chrGrafico1.ChartAreas["0"].AxisX.ScaleView.IsZoomed)
                {
                    chrGrafico1.ChartAreas["0"].AxisX.ScaleView.ZoomReset();
                }
                //ChartValueType EscalaX = ChartValueType.DateTime;
                //if (Registros[Registros.Count - 1].Horario - Registros[0].Horario <= (24 * 60 * 60))
                //{
                //    EscalaX = ChartValueType.Time;
                //}
                //for (int mm = 0; mm < chrGrafico1.Series.Count; mm++)
                //{
                //    chrGrafico1.Series[mm].XValueType = EscalaX;
                //}
            }
        }

        private void chrGrafico1_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            if (Registros.Count > 1)
            {
                if (!(double.IsNaN(e.NewPosition)))
                {
                    if (e.NewPosition > 0)
                    {
                        UInt32 posicao = Convert.ToUInt32(e.NewPosition);// Time2Unix(DateTime.FromOADate(e.NewPosition));
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
                                //break;
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
                        //Text = string.Format("{0} {1}", indice1, indice2);
                        chrGrafico1.ChartAreas["0"].CursorX.Position = /*Uteis.Unix2time(*/Registros[indice1].Horario - Inicio1;//).ToOADate();
                    }
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            string Pasta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            SaveFileDialog SalvaArquivo = new SaveFileDialog();
            SalvaArquivo.FileName = "Comparativo.png";
            //SalvaArquivo.FileName = "testes.xlsx";
            SalvaArquivo.InitialDirectory = Pasta;
            //Type VerificaExcel = Type.GetTypeFromProgID("Excel.Application");
            SalvaArquivo.Filter = "Imagem PNG|*.png|Imagem JPG|*.jpg|Imagem BMP|*.bmp";
            SalvaArquivo.DefaultExt = "*.png";
            //SalvaArquivo.ShowDialog();
            //if (SalvaArquivo.FileName.Length > 3)
            if (SalvaArquivo.ShowDialog() == DialogResult.OK)
            {
                int ponto = SalvaArquivo.FileName.LastIndexOf('.');
                if (ponto < 0) ponto = SalvaArquivo.FileName.Length - 3;
                string Formato = SalvaArquivo.FileName.Substring(ponto).ToLower();
                switch (Formato)
                {
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
                        }
                        break;
                    default:
                        {
                            MessageBox.Show("Formato não suportado");
                            break;
                        }
                }
            }
        }

        private void frmCompara_FormClosing(object sender, FormClosingEventArgs e)
        {
            Registros.Clear();
            chrGrafico1.Dispose();
            GC.Collect(); GC.WaitForPendingFinalizers();
        }
    }
}
