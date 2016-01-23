using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InterfaceDesktop
{
    public partial class frmGraficos : Form
    {
        private static List<RegistroDB> Registros = new List<RegistroDB>();
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
                dtpFim.Value =
                    dtpFim.MaxDate =
                    dtpInicio.Value =
                    dtpInicio.MaxDate = ArquivoParaData(ListaDeArquivos[ListaDeArquivos.Length - 1]);
                dtpFim.MaxDate += new TimeSpan(23, 59, 59);
                dtpFim.Value = dtpInicio.Value.Add(new TimeSpan(23, 59, 59));
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
    }
}
