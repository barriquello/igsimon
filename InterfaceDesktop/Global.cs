using System;
using System.Data.SQLite;
using System.Drawing;
namespace InterfaceDesktop
{
    class ComandosCSV
    {

        /// <summary>Comando para obter um arquivo CSV</summary>
        public const string strComandoCSV = "/feed/csvexport.json?id=";
        /// <summary>Comando para obter o horário</summary>
        public const string strComandoHorario = "/feed/get.json?field=time&id=";
        /// <summary>Comando para obter um arquivo único valor</summary>
        public const string strComandoValor = "/feed/get.json?field=value&id=";
        /// <summary>Comando para obter a lista de feeds</summary>
        public const string strComandoFeedList = "/feed/list.json?apikey=";
        /// <summary>Rotina para gerar o nome do arquivo CSV com base na data</summary>
        public static string ArquivoCSV(DateTime Data)
        {
            return "DB_" + Data.Year.ToString("D4") + "_" + Data.Month.ToString("D2") + "_" + Data.Day.ToString("D2") + ".csv";
        }
    }
    class Global
    {

        #region Cores dos gráficos
        public static Color[] Cores = 
        {
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.BlueViolet,
            Color.Chartreuse,
            Color.OrangeRed,
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.BlueViolet,
            Color.Chartreuse,
            Color.OrangeRed,
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.BlueViolet,
            Color.Chartreuse,
            Color.OrangeRed,
            Color.Red,
            Color.Blue,
            Color.Green,
            Color.BlueViolet,
            Color.Chartreuse,
            Color.OrangeRed,
        };
        #endregion
        #region Informações uteis
        // Separador de campo do CSV
        public const char SeparadorCSV = ',';// '\t';
        // Linha de cabeçalho no CSV
        public const bool CabecalhoCSV = true;

        public const int NOleoAlto = 7;
        public const int NOleoBaixo = 3;
        // Numero máximo de registros a manter na memória
        public const int RegistrosMAXIMO = 7 * 24 * 60 * 2 *4; // 7 * 24 * 60 * 2 = 1 registro a cada 30 segundos por 7 dias
        // Intervalo entre comunicações com o servidor web
        public static int intTaxaAtualizacao = 15000;  //ms
        #endregion

        #region Configurações relativas ao comportamento do formulário de configurações
        // Flag para indicar somente a obrigatoriedade da criação de um usuário
        public static bool tabPage1 = true;
        // Flag para indicar a configuração obrigatória
        public static bool tabPage2 = true;

        /// <summary>Flag para indiar a necessidade de reiniciar o formulário principal</summary>
        public static bool restart = false;
        /// <summary>Flag para controlar o comportamento do botão cancelar do formulário de configuração</summary>
        public static bool ConfigObriatoria = false;

        #endregion


        #region Autenticação
        /// <summary>Nome de usuário para autenticação (local)</summary>
        public static string Username = "";
        /// <summary>Senha para autenticação (criptografada) (local)</summary>
        public static string Senha = "";
        #endregion

        /// <summary>APIKey para autenticação com o servidor web</summary>
        public static string APIKey = "";

        public static string Servidor = "";
    }
}
