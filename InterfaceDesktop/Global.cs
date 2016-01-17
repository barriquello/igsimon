using System;
using System.Drawing;
namespace InterfaceDesktop
{

    class Global
    {

        #region Cores dos gráficos
        /// <summary>Cores dos gráficos
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
            Color.OrangeRed
        };
        #endregion
        #region Informações uteis
        /// <summary>Separador de campo do CSV</summary>
        public const char SeparadorCSV = ',';// '\t';
        /// <summary>Linha de cabeçalho no CSV
        public const bool CabecalhoCSV = true;
        /// <summary>Nível alto
        public const int NOleoAlto = 7;
        /// <summary>Nível bbaixo
        public const int NOleoBaixo = 3;
        /// <summary>Numero máximo de registros a manter na memória
        public const int RegistrosMAXIMO = 7 * 24 * 60 * 2 *4; // 7 * 24 * 60 * 2 = 1 registro a cada 30 segundos por 7 dias
        /// <summary>Intervalo entre comunicações com o servidor web
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
    }
}
