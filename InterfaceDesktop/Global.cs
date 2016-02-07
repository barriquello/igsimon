using System;
using System.Drawing;
namespace InterfaceDesktop
{
    class Global
    {
        #region Informações uteis
        /// <summary>Separador de campo do CSV no servidor web</summary>
        public static readonly char charSeparadorCSV = '\t';//',';// '\t';
        /// <summary>Separador CSV para o arquivo CSV a ser exportado</summary>
        public static readonly char charSeparadorCSVCSV = ';';
        /// <summary>Linha de cabeçalho no CSV</summary>
        public static readonly bool boolCabecalhoCSV = true;
        /// <summary>Nível alto</summary>
        public static readonly int intNOleoAlto = 5;
        /// <summary>Nível bbaixo</summary>
        public static readonly int intNOleoBaixo = 3;
        /// <summary>Temperatura crítica para o óleo do transformador</summary>
        public static readonly float floatTOleoH = 105f;
        /// <summary>Temperatura crítica para os enrolamentos do transformador</summary>
        public static readonly float floatTEnrH = 105f;
        public static readonly string strNOleoAlto = "Nível\nAlto";
        public static readonly string strNOleoBaixo = "Nível\nBaixo ";
        public static readonly string strNOleoNormal = "Nível\nMédio";

        public static readonly string strValvulaNormal = " em condições normais";
        public static readonly string strValvulaAtivada = " ativada";

        /// <summary>Numero máximo de registros a manter na memória</summary>
        public static readonly int intRegistrosMAXIMO = 7 * 24 * 60 * 2; // 7 * 24 * 60 * 2 = 1 registro a cada 30 segundos por 7 dias
        /// <summary>Intervalo entre comunicações com o servidor web</summary>
        public static readonly int intTaxaAtualizacao = 15000;  //ms
        /// <summary>Precisão de exibição de dados na tela</summary>
        public static readonly string strPrecisao = "{0}";
        #endregion

        #region Configurações relativas ao comportamento do formulário de configurações
        // Flag para indicar somente a obrigatoriedade da criação de um usuário
        public static bool boolNovoUsuario = true;
        // Flag para indicar a configuração obrigatória
        //public static bool boolNovaConfiguracao = true;

        /// <summary>Flag para indiar a necessidade de reiniciar o formulário principal</summary>
        public static bool boolReiniciar = false;
        /// <summary>Flag para controlar o comportamento do botão cancelar do formulário de configuração</summary>
        public static bool boolConfigObriatoria = false;

        #endregion
    }
}
