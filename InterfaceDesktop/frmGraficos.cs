using System;
using System.Collections.Generic;
using System.IO;
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
            // Limpeza de memória para economizar memória ( vai depender da velocidade da busca local)
            //Registros.Clear();
        }

        private void BuscaDadosCSV(DateTime _inicio, DateTime _fim)
        {
            DateTime inicio=_inicio;
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
                            Arquivo = leitor.ReadLine().Replace('.',',');
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
    }
}
