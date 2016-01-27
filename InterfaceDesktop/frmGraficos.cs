using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace InterfaceDesktop
{
    public partial class frmGraficos : Form
    {
        private static List<RegistroDB> Registros = new List<RegistroDB>();

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
                listBox1.Items.AddRange(ListaDeArquivos);
                dtpFim.MinDate =
                    dtpInicio.MinDate = ArquivoParaData(ListaDeArquivos[0]);
                dtpFim.MaxDate =
                    dtpInicio.MaxDate =
                    dtpFim.Value =
                    ArquivoParaData(ListaDeArquivos[ListaDeArquivos.Length - 1]).Add(new TimeSpan(23, 59, 59));
                dtpInicio.Value = ArquivoParaData(ListaDeArquivos[ListaDeArquivos.Length - 1]);
                for (int mm = 0; mm < ListaDeArquivos.Length; mm++)
                {
                    listBox1.Items.Add(ArquivoParaData(ListaDeArquivos[mm]));
                }
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
            GeraGrafico();
            PlotaGrafico();
            btnExcel.Enabled = true;
            // Limpeza de memória para economizar memória ( vai depender da velocidade da busca local)
            //Registros.Clear();
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
        }

        private void PlotaGrafico()
        {
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
                                    FeedServidor[] vars = Variaveis.strVariaveis();
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
    }
}
