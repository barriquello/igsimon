
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
namespace CriarRegistrosLocalhost
{

    public partial class Form1 : Form
    {
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
            int contador = sP.Value;
            // ponto ao invés de virgula
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;//.GetCultureInfo("en_US");
            WebClient Servidor = new WebClient();
            //URL1 = "http://localhost/input/post.json?node=1&json={";
            //PM-210
            DateTime Horario = DateTime.Now;
            string URL1 = string.Format("http://localhost/monitor/set.json?monitorid=10&time={0}&data=0,0,{1},{2},{3},{4},{5},{6},", Time2Unix(Horario), Horario.Year, Horario.Month, Horario.Day, Horario.Hour, Horario.Minute, Horario.Second);//20,20,20,20&apikey=72d5d09d5ed08c6743d2c71006f3c9bd";
            URL1 += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},"+
                "{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},"+
                "{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},"+
                "{30},{31},{32}",
                // P, S, Q
                dec(uc00.text(),4), dec(uc01.text(),4),dec( uc02.text(),4), 
                
                //uc03.text(), uc04.text(), uc05.text(), uc06.text(), uc07.text(), uc08.text(), uc09.text(),
                uc10.text(), uc11.text(), uc12.text(), uc13.text(), uc14.text(), uc15.text(), uc16.text(), uc17.text(), uc18.text(), uc19.text(),
                uc20.text(), uc21.text(), uc22.text(), uc23.text(), uc24.text(), uc25.text(), uc26.text(), uc27.text(), uc28.text(), uc29.text(),
                uc30.text(), uc31.text(), uc32.text());
            // Ajustar o número de bits de acordo com o decodificador.

            URL1 += "&apikey=" + txtAPIKEY.Text;
            try
            {
                textBox1.Text = Encoding.UTF8.GetString(
                Servidor.DownloadData(URL1));
            }
            catch (Exception Erro) { textBox1.Text = "Erro:\n" + Erro.Message; }
            timer1.Interval = Convert.ToInt32(textBox2.Text);

            contador++;
            if (contador > sP.Maximum)
                contador = sP.Minimum;
            sP.Value = contador;
        }

        private string dec(string p1, int p2)
        {
            string retorno = string.Format("{0}", p1);
            for (int jj = 0; jj < p2; jj++)
            {

                retorno += "0,";
            }
            return retorno;
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

            uc00.caption("EP");
            uc01.caption("ES");
            uc02.caption("EQ");
            uc03.caption("P");
            uc04.caption("S");
            uc05.caption("Q");
            uc06.caption("FP");
            uc07.caption("FREQ");
            uc08.caption("PM");
            uc09.caption("SM");
            uc10.caption("QM");
            uc11.caption("PP");
            uc12.caption("PS");
            uc13.caption("PQ");
            uc14.caption("IA");
            uc15.caption("IB");
            uc16.caption("IC");
            uc17.caption("IMA");
            uc18.caption("IMB");
            uc19.caption("IMC");
            uc20.caption("IPA");
            uc21.caption("IPB");
            uc22.caption("IPC");
            uc23.caption("VAB");
            uc24.caption("VBC");
            uc25.caption("VAC");
            uc26.caption("VAN");
            uc27.caption("VBN");
            uc28.caption("VCN");
            uc29.caption("TO");
            uc30.caption("TE");
            uc31.caption("VL");
            uc32.caption("NO");
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}