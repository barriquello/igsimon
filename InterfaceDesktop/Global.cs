using System.Data.SQLite;

namespace InterfaceDesktop
{
    class Global
    {
        #region Informações uteis
        // Separador de campo do CSV
        public const char SeparadorCSV = ',';// '\t';
        // Linha de cabeçalho no CSV
        public const bool CabecalhoCSV = true;

        public const int NOleoAlto = 7;
        public const int NOleoBaixo = 3;
        // Numero máximo de registros a manter na memória
        public const int RegistrosMAXIMO = 7 * 24 * 60 * 2; // 7 * 24 * 60 * 2 = 1 registro a cada 30 segundos por 7 dias
        // Intervalo entre comunicações com o servidor web
        public static int intTaxaAtualizacao = 5000;  //ms
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

        #region Comandos para o servidor
        /// <summary>Comando para obter um arquivo CSV</summary>
        public const  string strComandoCSV = "/feed/csvexport.json?id=";

        public const  string strComandoHorario = "/feed/get.json?field=time&id=";

        public const string strComandoValor = "/feed/get.json?field=value&id=";

        public const string strComandoFeedList = "/feed/list.json?apikey=";
        #endregion


        #region Nome das variáveis
        /// <summary>Nome da variável no servidor - Potência ativa</summary>
        public static string strP = "Node:1: P";
        /// <summary>Nome da variável no servidor - Potência reativa</summary>
        public static string strQ = "Node:1: Q";
        /// <summary>Nome da variável no servidor - Potência aparente</summary>
        public static string strS = "Node:1: S";
        /// <summary>Nome da variável no servidor - Va</summary>
        public static string strVa = "Node:1: Va";
        /// <summary>Nome da variável no servidor - Vb</summary>
        public static string strVb = "Node:1: Vb";
        /// <summary>Nome da variável no servidor - Vc</summary>
        public static string strVc = "Node:1: Vc";
        /// <summary>Nome da variável no servidor - Ia</summary>
        public static string strIa = "Node:1: Ia";
        /// <summary>Nome da variável no servidor - Ib</summary>
        public static string strIb = "Node:1: Ib";
        /// <summary>Nome da variável no servidor - Ic</summary>
        public static string strIc = "Node:1: Ic";
        /// <summary>Nome da variável no servidor - Nível do óleo</summary>
        public static string strNo = "Node:1: No";
        /// <summary>Nome da variável no servidor - Temperatura do óleo</summary>
        public static string strTo = "Node:1: To";
        /// <summary>Nome da variável no servidor - Temeperatura dos enrolamentos</summary>
        public static string strTe = "Node:1: Te";
        /// <summary>Função para gerar uma lista de variáveis</summary>
        /// <returns></returns>
        public static string[] strTodas()
        {
            return new string[] { strP, strQ, strS, strVa, strVb, strVc, strIa, strIb, strIc, strNo, strTo, strTe };
        }
        // Tipo de dado (definição de qual chartárea
        public static string[] strCategoria = { "P", "P", "P", "V", "V", "V", "I", "I", "I", "N", "T", "T" };
        // Índice na variável de registros
        public static int[] intIndiceRegistro = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        public static string[] striTodas()
        {
            return new string[] { striP, striQ, striS, striVa, striVb, striVc, striIa, striIb, striIc, striNo, striTo, striTe };
        }


        #endregion
        /// <summary>índices das variáveis no servidor web - buscar essas informações no servidor</summary>
        #region Indices das variáveis
        public static string striP = "";
        public static string striQ = "";
        public static string striS = "";
        public static string striVa = "";
        public static string striVb = "";
        public static string striVc = "";
        public static string striIa = "";
        public static string striIb = "";
        public static string striIc = "";
        public static string striNo = "";
        public static string striTo = "";
        public static string striTe = "";

        #endregion
        #region Autenticação
        /// <summary>Nome de usuário para autenticação (local)</summary>
        public static string Username = "";
        /// <summary>Senha para autenticação (criptografada) (local)</summary>
        public static string Senha = "";
        #endregion

        /// <summary>APIKey para autenticação com o servidor web</summary>
        public static string APIKey = "";

        public static string Servidor = "http://localhost";
        /// <summary>Modo online ou offline (remoto ou local)</summary>
        public static bool Online = true;

        #region Banco de dados
        /// <summary>Nome do arquivo de banco de dados</summary>
        public const string ArquivoDB = "db.db";
        /// <summary>Conexão com o banco de dados local</summary>        
        public const string Conexao = @"Data Source=|DataDirectory|\" + ArquivoDB + ";Pooling=True;Synchronous=Off;journal mode=Wal";
        /// <summary>Nome da tabela de usuários</summary>
        public const string TabelaUsers = "Usuarios";
        /// <summary>Nome da tabela de configurações</summary>
        public const string TabelaConfig = "Config";
        /// <summary>Nome da tabela de dados</summary>
        public const string TabelaDados = "Dados";
        /// <summary>Criar tabela de usuários</summary>
        public const string strCriarTabelaUsers = "CREATE TABLE IF NOT EXISTS '" + TabelaUsers + "' (" +
            "'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
            "'Username' TEXT," + // Nome de usuário para a autenticação
            "'Senha' TEXT" + // Senha do usuário criptografada com md5 modificado
            ");";
        /// <summary>Comando para adicionar ou trocar a senha de usuários</summary>
        public const string strComandoUsuario = "INSERT INTO '" + TabelaUsers + "' " +
            "('Username','Senha') " +
            "VALUES(@Username, @Senha)";
        /// <summary>Criar tabela de configurações</summary>
        public const string strCriarTabelaConfig = "CREATE TABLE IF NOT EXISTS '" + TabelaConfig + "' (" +
            "'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
            "'Servidor'	TEXT," + //Endereço do servidor
            "'Username'	TEXT," + //Nome do usuário que fez a modificação
            "'Node_P'	TEXT," + //Nome do campo Potência ativa
            "'Node_Q'	TEXT," + //Nome do campo Potência reativa
            "'Node_S'	TEXT," + //Nome do campo Potência aparente
            "'Node_Va'	TEXT," + //Nome do campo Tensão Va
            "'Node_Vb'	TEXT," + //Nome do campo Tensâo Vb
            "'Node_Vc'	TEXT," + //Nome do campo Tensão Vc
            "'Node_Ia'	TEXT," + //Nome do campo Corrente Ia
            "'Node_Ib'	TEXT," + //Nome do campo Corrente Ib
            "'Node_Ic'	TEXT," + //Nome do campo Corrente Ic
            "'Node_No'	TEXT," + //Nome do campo Nível do óleo
            "'Node_To'	TEXT," + //Nome do campo Temperatura do óleo
            "'Node_Te'	TEXT," + //Nome do campo Temperatura dos enrolamentos
            "'APIKEY'	TEXT" + // APIKey gerada pelo servidor WEB
            ");";
        /// <summary>Comando para adicionar configurações</summary>
        public const string strComandoConfig = "INSERT INTO '" + TabelaConfig + "' " +
            "('Servidor','Username','APIKEY') " +
            "VALUES(@Servidor, @Username, @APIKEY)";
        /*/// <summary>Criar a tabela de dados (adaptar essas configurações)</summary>
        public static string strCriarTabelaDados = "CREATE TABLE IF NOT EXISTS '" + TabelaDados + "' (" +
             "'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
             "'Horario' INTEGER," + // Horário (UNIX)
             "'VA' NUMERIC," + "'VB' NUMERIC," + "'VC' NUMERIC" + //Tensões por fase(a definir)
             "'IA' NUMERIC," + "'IB' NUMERIC," + "'IC' NUMERIC," + //Correntes por fase
             "'P' NUMERIC," + "'Q' NUMERIC," + "'S' NUMERIC," + // Potências
             "'Nivel' NUMERIC," + "'TOleo' NUMERIC," + "'TEnrol' NUMERIC" + // Nível do óleo e temperatura
             ");";//*/
        /*public static string strComandoDados = "INSERT INTO '" + TabelaDados + "' " +
            "('Horario','VA','VB','VC','IA','IB','IC','P','Q','S','Nivel','TOleo','TEnrol') " +
            "VALUES(@Horario,@VA,@VB,@VC,@IA,@IB,@IC,@P,@Q,@S,@Nivel,@TOleo,@TEnrol)";//*/
        #endregion
    }
}
