using System;
using System.Windows.Forms;
namespace InterfaceDesktop
{
    static class Program
    {
        /// <summary>The main entry point for the application</summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CarregaConfig();

            while (Servidor.Username == "")
            {
                // Criar um novo usuário
                Global.tabPage1 = false;
                // Carrega o formulario de configuração para criar um novo usuário
                //frmConfig Config = new frmConfig();
                //Config.ShowDialog();
                Application.Run(new frmConfig());
                Global.tabPage1 = true;
                CarregaConfig();
            }
            while ((Servidor.APIKey == "")|(Servidor.Server==""))
            {
                // Carrega o formulário de configuração para configurar o servidor
                Application.Run(new frmConfig());
                CarregaConfig();
            }

            //Application.Run(new frmLogin());
            Application.Run(new frmMain());
        }

        private static void CarregaConfig()
        {
            // Carrega os nomes das variaveis
            Variaveis.fEP.NomeFeed = Properties.Settings.Default.sEP;
            Variaveis.fEQ.NomeFeed = Properties.Settings.Default.sEQ;
            Variaveis.fES.NomeFeed = Properties.Settings.Default.sES;
            Variaveis.fFatorPotencia.NomeFeed = Properties.Settings.Default.sFP;
            Variaveis.fFreq.NomeFeed = Properties.Settings.Default.sFreq;
            Variaveis.fIa.NomeFeed = Properties.Settings.Default.sIa;
            Variaveis.fIb.NomeFeed = Properties.Settings.Default.sIb;
            Variaveis.fIc.NomeFeed = Properties.Settings.Default.sIc;
            Variaveis.fIMa.NomeFeed = Properties.Settings.Default.sIMa;
            Variaveis.fIMb.NomeFeed = Properties.Settings.Default.sIMb;
            Variaveis.fIMc.NomeFeed = Properties.Settings.Default.sIMc;
            Variaveis.fIPa.NomeFeed = Properties.Settings.Default.sIPa;
            Variaveis.fIPb.NomeFeed = Properties.Settings.Default.sIPb;
            Variaveis.fIPc.NomeFeed = Properties.Settings.Default.sIPc;
            Variaveis.fNivelOleo.NomeFeed = Properties.Settings.Default.sNivelOleo;
            Variaveis.fP.NomeFeed = Properties.Settings.Default.sP;
            Variaveis.fPM.NomeFeed = Properties.Settings.Default.sPM;
            Variaveis.fPP.NomeFeed = Properties.Settings.Default.sPP;
            Variaveis.fQ.NomeFeed = Properties.Settings.Default.sQ;
            Variaveis.fQM.NomeFeed = Properties.Settings.Default.sQM;
            Variaveis.fQP.NomeFeed = Properties.Settings.Default.sQP;
            Variaveis.fS.NomeFeed = Properties.Settings.Default.sS;
            Variaveis.fSM.NomeFeed = Properties.Settings.Default.sSM;
            Variaveis.fSP.NomeFeed = Properties.Settings.Default.sSP;
            Variaveis.fTEnrolamento.NomeFeed = Properties.Settings.Default.sTEntolamento;
            Variaveis.fTOleo.NomeFeed = Properties.Settings.Default.sTOleo;
            Variaveis.fVab.NomeFeed = Properties.Settings.Default.sVab;
            Variaveis.fValvulaPressao.NomeFeed = Properties.Settings.Default.sValvulaPressao;
            Variaveis.fVan.NomeFeed = Properties.Settings.Default.sVan;
            Variaveis.fVbc.NomeFeed = Properties.Settings.Default.sVbc;
            Variaveis.fVbn.NomeFeed = Properties.Settings.Default.sVbn;
            Variaveis.fVca.NomeFeed = Properties.Settings.Default.sVca;
            Variaveis.fVcn.NomeFeed = Properties.Settings.Default.sVcn;

            Servidor.APIKey = Properties.Settings.Default.APIKEY;
            Servidor.Server = Properties.Settings.Default.Servidor;
            Servidor.Username = Properties.Settings.Default.Usuario;
            Servidor.Senha = Properties.Settings.Default.Senha;

        }
    }
}
