
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
namespace CriarRegistrosLocalhost
{

    public partial class Form1 : Form
    {
        private static string[] Lista = {
            "Ia", "Ib", "Ic",
            "Vab", "Vbc", "Vca",
            "Van", "Vbn", "Vcn",
            "Freq",
            "P", "Q", "S",
            "FP",
            "EP", "EQ", "ES",
            "IMa", "IMb", "IMc",
            "PM", "QM", "SM",
            "IPa", "IPb", "IPc",
            "PP", "QP", "SP",
            "Toleo",  "Tenr",
            "Noleo",  "Valvula"
                                        };
        private double Contador = 0;
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();

        }
        /// <summary>Converte Horário em horário Unix</summary>
        public Int32 Time2Unix(DateTime Horario)
        {
            return (Int32)Horario.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // ponto ao invés de virgula
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;//.GetCultureInfo("en_US");
            string URL1 = "";
            WebClient Servidor = new WebClient();
            //textBox1.Text = "";
            URL1 = "http://localhost/input/post.json?node=1&json={";
            double Fase = Math.PI * 2 / (Lista.Length);
            double Numero = 0;
            for (int mmm = 0; mmm < Lista.Length - 1; mmm++)
            {
                if (Lista[mmm].ToLower() == "noleo")
                {
                    Numero = 5 + 5 * Math.Sin(Contador * 2 * Math.PI / 50 + (mmm + 1) * Fase);
                }
                else
                {
                    Numero = 50 + 50 * Math.Sin(Contador * 2 * Math.PI / 50 + (mmm + 1) * Fase);
                }
                URL1 += string.Format("[{0}:{1}],", Lista[mmm], Numero.ToString());
            }
            Numero = (1 + Math.Sign(Math.Sin(Contador * 2 * Math.PI / 50))) / 2;
            URL1 += string.Format("[{0}:{1}]", Lista[Lista.Length - 1], Numero.ToString());
            URL1 += "}&apikey=" + txtAPIKEY.Text +
                    "&time=" + Time2Unix(DateTime.Now).ToString();
            Contador++;
            if (Contador > 100) Contador = 0;
            Text = DateTime.Now.ToString();
            //Console.WriteLine(URL1);
            try
            {
                textBox1.Text = Encoding.UTF8.GetString(
                Servidor.DownloadData(URL1));
            }
            catch { }

            if (sP.Value == sP.Maximum)
                sP.Value = sP.Minimum;
            else
                sP.Value++;
            timer1.Interval = Convert.ToInt32(textBox2.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(textBox2.Text);
            timer1.Enabled = checkBox1.Checked;
            btnGerar.Enabled = !checkBox1.Checked;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtInicio.Format = DateTimePickerFormat.Custom;
            dtInicio.CustomFormat = "dd / MM / yyyy    HH : mm";
            dtInicio.Value = DateTime.Now.AddDays(-7);

            int variavel = 0;
            for (int mm = 0; mm < this.Controls.Count; mm++)
                if (this.Controls[mm] is UserControl1)
                {
                    ((UserControl1)Controls[mm]).caption(Lista[variavel++]);
                }


        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            btnGerar.Enabled = false;
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;//.GetCultureInfo("en_US");
                WebClient Servidor = new WebClient();
                string URL1;
                int contador = 0;
                double Intervalo = Convert.ToDouble(textBox2.Text);
                DateTime Agora = DateTime.Now;
                DateTime valor = dtInicio.Value;
                while (valor <= Agora)
                {
                    Application.DoEvents();
                    if (!this.Visible)
                        return;
                    // ponto ao invés de virgula
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;//.GetCultureInfo("en_US");
                    textBox1.Text = "";
                    URL1 = "http://localhost/input/post.json?node=1&json={";
                    double Fase = Math.PI * 2 / (Lista.Length);
                    double Numero = 0;
                    for (int mmm = 0; mmm < Lista.Length - 1; mmm++)
                    {
                        if (Lista[mmm].ToLower() == "noleo")
                        {
                            Numero = 5 + 5 * Math.Sin(Contador * 2 * Math.PI / 50 + (mmm + 1) * Fase);
                        }
                        else
                        {
                            Numero = 50 + 50 * Math.Sin(Contador * 2 * Math.PI / 50 + (mmm + 1) * Fase);
                        }

                        URL1 += string.Format("[{0}:{1}],", Lista[mmm], Numero.ToString());
                    }
                    Numero = (1 + Math.Sign(Math.Sin(Contador * 2 * Math.PI / 50))) / 2;
                    URL1 += string.Format("[{0}:{1}]", Lista[Lista.Length - 1], Numero.ToString());
                    URL1 += "}&apikey=" + txtAPIKEY.Text +
                        "&time=" + Time2Unix(valor).ToString();
                    Contador++;
                    if (Contador > 100) Contador = 0;
                    //Console.WriteLine(URL1);
                    try
                    {
                        textBox1.Text = Encoding.UTF8.GetString(
                            Servidor.DownloadData(URL1));
                    }
                    catch { }
                    contador++;
                    if (contador > 99)
                        contador = 0;
                    valor = valor.AddMilliseconds(Intervalo);
                    Text = valor.ToString();
                }


            }
            catch (Exception Erro)
            {
                MessageBox.Show("Erro!!!\n" + Erro.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cont = 0;
            string URL1 = "http://localhost/input/post.json?node=1&json={";
            for (int mm = 0; mm<this.Controls.Count;mm++)
                if (this.Controls[mm] is UserControl1)
                {
                    if (cont<Lista.Length-1)
                        URL1 += string.Format("[{0}:{1}],", ((UserControl1)Controls[mm]).caption(), ((UserControl1)Controls[mm]).text());
                    else
                        URL1 += string.Format("[{0}:{1}]", ((UserControl1)Controls[mm]).caption(), ((UserControl1)Controls[mm]).text());
                    cont++;
                }
            URL1 += "}&apikey=" + string.Format("{0}&time={1}", txtAPIKEY.Text, Time2Unix(DateTime.Now));
            try
            {
                WebClient Servidor = new WebClient();
                textBox1.Text = Encoding.UTF8.GetString(
                Servidor.DownloadData(URL1));
            }
            catch { }
        }
    }
}