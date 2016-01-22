using System;
using System.Drawing;
namespace InterfaceDesktop
{

    class Global
    {
        #region Informações uteis
        /// <summary>Separador de campo do CSV</summary>
        public static readonly char SeparadorCSV = '\t';//',';// '\t';
        /// <summary>Linha de cabeçalho no CSV</summary>
        public static readonly bool CabecalhoCSV = true;
        /// <summary>Nível alto</summary>
        public static readonly int NOleoAlto = 7;
        /// <summary>Nível bbaixo</summary>
        public static readonly int NOleoBaixo = 3;
        /// <summary>Numero máximo de registros a manter na memória</summary>
        public static readonly int RegistrosMAXIMO = 7 * 24 * 60 * 2; // 7 * 24 * 60 * 2 = 1 registro a cada 30 segundos por 7 dias
        /// <summary>Intervalo entre comunicações com o servidor web</summary>
        public static readonly int intTaxaAtualizacao = 15000;  //ms
        /// <summary>Precisão de exibição de dados na tela</summary>
        public static readonly string strPrecisao = "{0}";
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
