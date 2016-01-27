using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;

namespace InterfaceDesktop
{
    public partial class frmGraficos : Form
    {
        private static List<RegistroDB> Registros = new List<RegistroDB>();
        FeedServidor[] vars = Variaveis.strVariaveis();

        private static DateTime Inicio;
        private static DateTime Fim;

        public frmGraficos()
        {
            InitializeComponent();
        }

        private void Graficos_Load(object sender, EventArgs e)
        {
            string[] ListaDeArquivos = System.IO.Directory.GetFiles(Application.StartupPath, "DB_*.csv");
            if (ListaDeArquivos.Length > 0)
            {
                dtpFim.MinDate =
                    dtpInicio.MinDate = ArquivoParaData(ListaDeArquivos[0]);
                dtpFim.MaxDate =
                    dtpInicio.MaxDate =
                    dtpFim.Value =
                    ArquivoParaData(ListaDeArquivos[ListaDeArquivos.Length - 1]).Add(new TimeSpan(23, 59, 59));
                dtpInicio.Value = ArquivoParaData(ListaDeArquivos[ListaDeArquivos.Length - 1]);
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (dtpInicio.Value < dtpFim.Value)
            {
                Inicio = dtpInicio.Value;
                Fim = dtpFim.Value;
            }
            else
            {
                Inicio = dtpFim.Value;
                Fim = dtpInicio.Value;
            }

            Registros.Clear();

            chrGrafico.ChartAreas.Clear();
            chrGrafico.Series.Clear();
            chrGrafico.Legends.Clear();

            BuscaDadosCSV(Inicio, Fim);
            //Registros.Sort();
            Registros =
                Registros.OrderBy(RegistroDB => RegistroDB.Horario).ToList<RegistroDB>();
            lstValores.Items.Clear();
            GeraGrafico();
            if (Registros.Count > 1)
            {
                PlotaGrafico();
                btnExcel.Enabled = true;
                lstValores.SuspendLayout();
                RegistroDB reg = Registros[Registros.Count - 1];
                lstValores.Items.Add(string.Format("Horário = {0}", Uteis.Unix2time(reg.Horario)));
                for (int jj = 0; jj < vars.Length; jj++)
                {
                    lstValores.Items.Add(string.Format(vars[jj].formato, reg.P[vars[jj].indice]));
                }
                lstValores.ResumeLayout();
            }
        }

        private void BuscaDadosCSV(DateTime _inicio, DateTime _fim)
        {
            DateTime inicio = _inicio;
            FeedServidor[] fdd = Variaveis.strVariaveis();
            int[] indices = new int[fdd.Length + 1];
            string Arquivo = "";
            while (inicio < Fim)
            {
                Arquivo = ComandosCSV.ArquivoCSV(inicio);
                if (File.Exists(Arquivo))
                {
                    using (StreamReader leitor = new StreamReader(Arquivo))
                    {
                        // Identifica a ordem das variáveis
                        Arquivo = leitor.ReadLine();
                        string[] campos = Arquivo.Split(Global.SeparadorCSV);
                        for (int jj = 1; jj < campos.Length; jj++)
                        {
                            for (int mm = 0; mm < fdd.Length; mm++)
                            {
                                if (campos[jj] == fdd[mm].NomeFeed)
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
                            campos = Arquivo.Split(Global.SeparadorCSV);
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
            }
        }

        private void GeraGrafico()
        {
            chrGrafico.Series.Clear();
            chrGrafico.ChartAreas.Clear();
            chrGrafico.Legends.Clear();
            chrGrafico.ChartAreas.Add("P");
            chrGrafico.ChartAreas.Add("Vl");
            chrGrafico.ChartAreas.Add("Vf");
            chrGrafico.ChartAreas.Add("I");
            chrGrafico.ChartAreas.Add("T");

            for (int kk = 0; kk < chrGrafico.ChartAreas.Count; kk++)
            {
                // por consequência do chrGrafico.Legends.clear(), jj será o índice da nova legenda
                chrGrafico.Legends.Add(chrGrafico.ChartAreas[kk].Name).LegendItemOrder = LegendItemOrder.Auto;
                chrGrafico.Legends[kk].Alignment = System.Drawing.StringAlignment.Center; // Alinhamento das legendas
                chrGrafico.Legends[kk].LegendStyle = LegendStyle.Column; // legendas em uma coluna
                chrGrafico.Legends[kk].BackColor = chrGrafico.BackColor;
                // Alinhamento do título da legenda
                chrGrafico.Legends[kk].TitleAlignment = System.Drawing.StringAlignment.Near;
                // linha separando o título da legenda
                chrGrafico.Legends[kk].TitleSeparator = LegendSeparatorStyle.Line;
                // Habilita os cursores
                chrGrafico.ChartAreas[kk].CursorX.IsUserEnabled = true;
                chrGrafico.ChartAreas[kk].CursorX.LineWidth = 2;
                chrGrafico.ChartAreas[kk].CursorX.LineColor = System.Drawing.Color.Red;
                chrGrafico.ChartAreas[kk].CursorX.LineDashStyle = ChartDashStyle.Dot;
                chrGrafico.ChartAreas[kk].CursorX.SelectionColor = System.Drawing.Color.DeepSkyBlue;
                // Melhoras no visual
                chrGrafico.ChartAreas[kk].AxisX.ScrollBar.Size = 10;
                chrGrafico.ChartAreas[kk].AxisX.ScrollBar.IsPositionedInside = false;
                // Habilita o zoom
                chrGrafico.ChartAreas[kk].CursorX.IsUserSelectionEnabled = true;
                // Resolução máxima
                chrGrafico.ChartAreas[kk].CursorX.IntervalType = DateTimeIntervalType.Minutes;
                chrGrafico.ChartAreas[kk].CursorX.Interval = 1;// minutos
                chrGrafico.ChartAreas[kk].AxisX.ScaleView.SmallScrollMinSize = 1;
                chrGrafico.ChartAreas[kk].AxisX.ScaleView.SmallScrollMinSizeType = DateTimeIntervalType.Minutes;

            }


            // desabilita as barras de rolagem dos gráficos de cima
            chrGrafico.ChartAreas["P"].AxisX.ScrollBar.Enabled =
                chrGrafico.ChartAreas["Vl"].AxisX.ScrollBar.Enabled =
                chrGrafico.ChartAreas["Vf"].AxisX.ScrollBar.Enabled =
                chrGrafico.ChartAreas["I"].AxisX.ScrollBar.Enabled = false;

            // Desabilita a escala no eixo X para quase todos os gráficos (exceto no gráfico do nível de óleo, esse fica na parte inferior)
            chrGrafico.ChartAreas["P"].AxisX.LabelStyle.Enabled =
                chrGrafico.ChartAreas["Vl"].AxisX.LabelStyle.Enabled =
                chrGrafico.ChartAreas["Vf"].AxisX.LabelStyle.Enabled =
                chrGrafico.ChartAreas["I"].AxisX.LabelStyle.Enabled = false;
            // Alinhamento dos gráficos das chartareas (alinhados com o gráfico debaixo)
            chrGrafico.ChartAreas["P"].AlignWithChartArea =
                chrGrafico.ChartAreas["Vl"].AlignWithChartArea =
                chrGrafico.ChartAreas["Vf"].AlignWithChartArea =
                chrGrafico.ChartAreas["I"].AlignWithChartArea = "T";
            float fLarguraLegenda = 15f; //%
            float fAltura = 20f;
            float fLargura = 100 - fLarguraLegenda; // = 100f * (chrGrafico.Width - fTamanhoLegenda) / (chrGrafico.Width * 1f);
            chrGrafico.ChartAreas["P"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 0f, fLargura, fAltura)); // 80% da largura e 20 % da altura do chart
            chrGrafico.Legends["P"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, 0f, fLarguraLegenda - 0.2f, fAltura)); //20 % da largura e 20% da altura

            chrGrafico.ChartAreas["Vl"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 20f, fLargura, fAltura));
            chrGrafico.Legends["Vl"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, fAltura, fLarguraLegenda - 0.2f, fAltura));

            chrGrafico.ChartAreas["Vf"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 40f, fLargura, fAltura));
            chrGrafico.Legends["Vf"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, fAltura * 2, fLarguraLegenda - 0.2f, fAltura));

            chrGrafico.ChartAreas["I"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 60f, fLargura, fAltura));
            chrGrafico.Legends["I"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, fAltura * 3, fLarguraLegenda - 0.2f, fAltura));

            chrGrafico.ChartAreas["T"].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 80f, fLargura, fAltura));
            chrGrafico.Legends["T"].Position.FromRectangleF(new System.Drawing.RectangleF(fLargura + 0.2f, fAltura * 4, fLarguraLegenda - 0.2f, fAltura));
            //Título nas legendas
            chrGrafico.Legends["P"].Title = "Potência";
            chrGrafico.Legends["Vl"].Title = "Tensão de Linha";
            chrGrafico.Legends["Vf"].Title = "Tensão de Fase";
            chrGrafico.Legends["I"].Title = "Corrente";
            chrGrafico.Legends["T"].Title = "Temperatura";

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
        private void PlotaGrafico()
        {
            chrGrafico.Series.Clear();
            for (int jj = 0; jj < vars.Length; jj++)
            {
                if (Func2str(vars[jj].Funcao) != "")
                {
                    int Indice = vars[jj].indice;
                    Series serie = new Series(vars[jj].NomeFeed);
                    serie.ChartArea = serie.Legend = Func2str(vars[jj].Funcao);
                    serie.BorderWidth = 2;
                    serie.Color = vars[jj].Cor;
                    serie.ChartType = SeriesChartType.StepLine;
                    serie.XValueType = ChartValueType.DateTime;

                    for (int mm = 0; mm < Registros.Count; mm++)
                    {
                        serie.Points.AddXY(Uteis.Unix2time(Registros[mm].Horario), Registros[mm].P[Indice]);
                    }
                    if (Registros[Registros.Count - 1].Horario - Registros[0].Horario <= 24 * 60 * 60)
                    {
                        serie.XValueType = ChartValueType.Time;
                    }
                    chrGrafico.Series.Add(serie);
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            string Pasta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            SaveFileDialog SalvaArquivo = new SaveFileDialog();
            SalvaArquivo.FileName = System.IO.Path.Combine(Pasta, "Exportar.xlsx");
            SalvaArquivo.InitialDirectory = Pasta;
            SalvaArquivo.Filter = "Arquivo XLSX|*.xlsx|Arquivo CSV|*.csv|Imagem PNG|*.png|Imagem JPG|*.jpg|Imagem BMP|*.bmp";
            SalvaArquivo.DefaultExt = "*.xlsx";
            if (SalvaArquivo.ShowDialog() == DialogResult.OK)
            {
                int ponto = SalvaArquivo.FileName.LastIndexOf('.');
                if (ponto < 0)
                {
                    ponto = SalvaArquivo.FileName.Length - 3;
                }
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
                                        bstr.Append(Global.SeparadorCSVCSV);
                                        bstr.Append(vars[jj].NomeFeed);
                                    }
                                    GravarArquivoCSV.WriteLine(bstr);
                                    for (int jj = 0; jj < Registros.Count; jj++)
                                    {
                                        bstr = new StringBuilder(Registros[jj].Horario.ToString());
                                        for (int kk = 0; kk < vars.Length; kk++)
                                        {
                                            bstr.Append(Global.SeparadorCSVCSV);
                                            bstr.Append(Registros[jj].P[vars[kk].indice].ToString(SeparadorDecimal));
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
                            Graphics Imagem = chrGrafico.CreateGraphics();
                            Bitmap bitmat = new Bitmap(chrGrafico.Width, chrGrafico.Height, Imagem);
                            Rectangle retangulo = new Rectangle(0, 0, chrGrafico.Width, chrGrafico.Height);
                            chrGrafico.DrawToBitmap(bitmat, retangulo);
                            System.Drawing.Imaging.ImageFormat imgFormato = System.Drawing.Imaging.ImageFormat.Jpeg;
                            if (Formato == ".bmp")
                                imgFormato = System.Drawing.Imaging.ImageFormat.Bmp;
                            if (Formato == ".png")
                                imgFormato = System.Drawing.Imaging.ImageFormat.Png;
                            bitmat.Save(SalvaArquivo.FileName, imgFormato);
                            break;
                        }
                    case ".xls":
                    case ".xlsx":
                    default:
                        {
                            // Salvar
                            new SalvarExcel().SalvarXLSX(SalvaArquivo.FileName, UInt32.MinValue, UInt32.MaxValue, Registros);
                            Type VerificaExcel = Type.GetTypeFromProgID("Excel.Application");
                            // Abrir arquivo
                            //if (VerificaExcel == null)
                            //{
                            //    System.Diagnostics.Process.Start("explorer.exe", "/select," + SalvaArquivo.FileName);
                            //    //System.Diagnostics.Process.Start(SalvaArquivo.FileName);
                            //}
                            //else
                            //{
                            //    System.Diagnostics.Process.Start(SalvaArquivo.FileName);
                            //}
                            break;
                        }
                }
            }
        }

        private void chrGrafico_AxisViewChanged(object sender, ViewEventArgs e)
        {
            ChartValueType Escala = ChartValueType.Time;
            if (e.NewSize < double.MaxValue)
            {
                if (e.NewSize > 1)
                {
                    Escala = ChartValueType.DateTime;
                }
            }
            else
            {
                if (Registros.Count > 1)
                {
                    if ((Registros[Registros.Count - 1].Horario - Registros[0].Horario) > (60 * 60 * 24))
                    {
                        Escala = ChartValueType.DateTime;
                    }
                }
            }
            if (chrGrafico.Series.Count > 0)
            {
                if (chrGrafico.Series[0].XValueType != Escala)
                {
                    for (int mm = 0; mm < chrGrafico.Series.Count; mm++)
                    {
                        chrGrafico.Series[mm].XValueType = Escala;
                    }
                }
            }
        }

        private void chrGrafico_CursorPositionChanged(object sender, CursorEventArgs e)
        {
            UInt32 posicao;
            if (Registros.Count > 1)
            {
                RegistroDB reg = Registros[Registros.Count - 1];
                if (!(double.IsNaN(e.NewPosition)))
                {

                    posicao = Time2Unix(DateTime.FromOADate(e.NewPosition));
                    for (int jj = 0; jj < Registros.Count; jj++)
                    {
                        if (posicao <= Registros[jj].Horario)
                        {
                            reg = Registros[jj];
                            break;
                        }
                    }
                    lstValores.SuspendLayout();
                    lstValores.Items.Clear();
                    lstValores.Items.Add(string.Format("Horário = {0}", Uteis.Unix2time(reg.Horario)));
                    for (int jj = 0; jj < vars.Length; jj++)
                    {
                        lstValores.Items.Add(string.Format(vars[jj].formato, reg.P[vars[jj].indice]));
                    }
                    lstValores.ResumeLayout();
                }
            }
        }

        private void chrGrafico_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (chrGrafico.ChartAreas["T"].AxisX.ScaleView.IsZoomed)
                {
                    chrGrafico.ChartAreas["T"].AxisX.ScaleView.ZoomReset();
                }
                ChartValueType EscalaX = ChartValueType.DateTime;
                if (Registros[Registros.Count - 1].Horario - Registros[0].Horario <= (24 * 60 * 60))
                {
                    EscalaX = ChartValueType.Time;
                }
                for (int mm = 0; mm < chrGrafico.Series.Count; mm++)
                {
                    chrGrafico.Series[mm].XValueType = EscalaX;
                }
            }
        }
        /// <summary>Transforma datetime para unixtime Rotina para contrornar o problema de fusohorário</summary>
        private static UInt32 Time2Unix(DateTime Horario)
        {
            return (UInt32)Horario.Subtract(new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                analogico1.Min(Convert.ToSingle(textBox1.Text));
            }
            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                analogico1.Value(Convert.ToSingle(textBox2.Text));
            }
            catch { }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                analogico1.Max(Convert.ToSingle(textBox3.Text));
            }
            catch { }
        }
    }
}
