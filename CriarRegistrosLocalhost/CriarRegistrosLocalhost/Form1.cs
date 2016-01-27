
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
namespace CriarRegistrosLocalhost
{

    public partial class Form1 : Form
    {
        private static byte Contador;
        public Form1()
        {
            InitializeComponent();

        }
        /// <summary>Converte Horário em horário Unix</summary>
        public Int32 Time2Unix(DateTime Horario)
        {
            return (Int32)Horario.Subtract(new DateTime(1970, 1, 1,0,0,0,DateTimeKind.Utc)).TotalSeconds;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Contador = (byte)sP.Value;
            // ponto ao invés de virgula
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;//.GetCultureInfo("en_US");
            WebClient Servidor = new WebClient();
            //URL1 = "http://localhost/input/post.json?node=1&json={";
            //PM-210
            DateTime Horario = DateTime.Now.ToUniversalTime();
            string URL1 = string.Format("http://localhost/monitor/set.json?monitorid=10&time={0}&data=0,0,{1},{2},{3},{4},{5},{6},", Time2Unix(Horario), Horario.Year, Horario.Month, Horario.Day, Horario.Hour, Horario.Minute, Horario.Second);//20,20,20,20&apikey=72d5d09d5ed08c6743d2c71006f3c9bd";
            URL1 += Dados();
            // Ajustar o número de bits de acordo com o decodificador.

            URL1 += "&apikey=" + txtAPIKEY.Text;
            try
            {
                textBox1.Text = Encoding.UTF8.GetString(
                Servidor.DownloadData(URL1));
            }
            catch (Exception Erro) { textBox1.Text = "Erro:\n" + Erro.Message; }
            timer1.Interval = Convert.ToInt32(textBox2.Text);
            sP.Value = Contador;
        }

        private string Dados()
        {
            Contador++;
            for (int mmm = 0; mmm < Controls.Count; mmm++)
            {
                if (Controls[mmm] is UserControl1)
                {
                    ((UserControl1)Controls[mmm]).text(((byte)(Contador + mmm)).ToString());
                }
            }


            return string.Format("{0},0,0,0,{1},0,0,0,{2},0,0,0,0,{3},0,{4},0,{5},0,{6},0,{7},0,{8},0,{9},0," +
                "{10},0,{11},0,{12},0,{13},0,{14},0,{15},0,{16},0,{17},0,{18},0,{19},0," +
                "{20},0,{21},0,{22},0,{23},0,{24},0,{25},0,{26},0,{27},0,{28},{29}," +
                "{30},{31},{32}",
                // EP, ES, EQ int32
                uc00.text() * 100 / 256, uc01.text() * 100 / 256, uc02.text() * 100 / 256,
                // P S Q int16
                uc03.text() * 100 / 256, uc04.text() * 100 / 256, uc05.text() * 100 / 256,
                // FP 16bits, escala 0,0001
                uc06.text() * 100 / 256,
                // Freq, 16bits, escala 0,01
                uc07.text() * 100 / 256,
                // PM, SM, QM, 16bits
                uc08.text() * 100 / 256, uc09.text() * 100 / 256, uc10.text() * 100 / 256,
                // PP QP SP, 16bits
                uc11.text() * 100 / 256, uc12.text() * 100 / 256, uc13.text() * 100 / 256,
                // Ia, Ib, Ic, 16bits
                uc14.text() * 100 / 256, uc15.text() * 100 / 256, uc16.text() * 100 / 256,
                // IMa IMb IMc, 16 bits
                uc17.text() * 100 / 256, uc18.text() * 100 / 256, uc19.text() * 100 / 256,
                // IPa IPb IPc, 16bits
                uc20.text() * 100 / 256, uc21.text() * 100 / 256, uc22.text() * 100 / 256,
                // Vab, Vbc, Vac, 16 bits
                uc23.text() * 100 / 256, uc24.text() * 100 / 256, uc25.text() * 100 / 256,
                // Van Vbn Vcn, 16bits
                uc26.text() * 100 / 256, uc27.text() * 100 / 256, uc28.text() * 100 / 256,
                // temperaturas, nível, válvula (não necessáriamente nessa ordem), 8bits
                uc29.text(), uc30.text() * 100 / 256, uc31.text() * 100 / 256, uc32.text() * 100 / 256);
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
            Contador = (byte)sP.Value;
            // ponto ao invés de virgula
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;//.GetCultureInfo("en_US");
            WebClient Servidor = new WebClient();
            int milisegundos = Convert.ToInt32(textBox2.Text);
            //URL1 = "http://localhost/input/post.json?node=1&json={";
            //PM-210
            DateTime Horario = dtInicio.Value.ToUniversalTime(); // DateTime.Now;
            SuspendLayout();
            do
            {
                Text = Horario.ToString();
                string URL1 = string.Format(
                    "http://localhost/monitor/set.json?monitorid=10&time={0}&data=0,0,{1},{2},{3},{4},{5},{6},",
                    Time2Unix(Horario),
                    Horario.Year, Horario.Month, Horario.Day,
                    Horario.Hour, Horario.Minute, Horario.Second);
                URL1 += Dados();
                // Ajustar o número de bits de acordo com o decodificador.

                URL1 += "&apikey=" + txtAPIKEY.Text;
                try
                {
                    textBox1.Text = Encoding.UTF8.GetString(
                    Servidor.DownloadData(URL1));
                }
                catch (Exception Erro) { textBox1.Text = "Erro:\n" + Erro.Message; }
                Horario = Horario.AddMilliseconds(milisegundos);
                //if ((Contador&0x1F)==0x1F)
                    Application.DoEvents();
            } while (Horario < DateTime.Now);
            ResumeLayout();
            timer1.Interval = Convert.ToInt32(textBox2.Text);
            sP.Value = Contador;
        }

     

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
        }
    }
}