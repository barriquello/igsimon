using System.Data.SQLite;

namespace InterfaceDesktop
{
    class Global
    {
        #region TabPage no formulário configurações
        public static bool tabPage1 = true;
        public static bool tabPage2 = true;
        #endregion

        #region Acessório SQLite
        //public static SQLiteConnection SQLiteConn;
        //public static SQLiteCommand SQLiteComando;

        #endregion

        #region Nome das variáveis
        /// <summary>String Potência ativa</summary>
        public static string strP = "Node:1: P";
        /// <summary>String Potência reativa</summary>
        public static string strQ = "Node:1: Q";
        /// <summary>String Potência aparente</summary>
        public static string strS = "Node:1: S";
        /// <summary>String Va</summary>
        public static string strVa = "Node:1: Va";
        /// <summary>String Vb</summary>
        public static string strVb = "Node:1: Vb";
        /// <summary>String Vc</summary>
        public static string strVc = "Node:1: Vc";
        /// <summary>String Ia</summary>
        public static string strIa = "Node:1: Ia";
        /// <summary>String Ib</summary>
        public static string strIb = "Node:1: Ib";
        /// <summary>String Ic</summary>
        public static string strIc = "Node:1: Ic";
        /// <summary>String Nível do óleo</summary>
        public static string strNo = "Node:1: No";
        /// <summary>String Temperatura do óleo</summary>
        public static string strTo = "Node:1: To";
        /// <summary>String Temeperatura dos enrolamentos</summary>
        public static string strTe = "Node:1: Te";
        #endregion
        /// <summary>Buscar essas informações no servidor</summary>
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
        /// <summary>Senha para autenticação (local)</summary>
        public static string Senha = "";
        #endregion

        /// <summary>APIKey para autenticação com o servidor web</summary>
        public static string APIKey = "";

        public static string Servidor = "http://localhost";
        /// <summary>Modo online ou offline (remoto ou local)</summary>
        public static bool Online = true;

        #region Banco de dados
        /// <summary>Nome do arquivo de banco de dados</summary>
        public static string ArquivoDB = "db.db";
        /// <summary>Conexão com o banco de dados local</summary>        
        public static string Conexao = @"Data Source=|DataDirectory|\" + ArquivoDB + ";Pooling=True;Synchronous=Off;journal mode=Wal";
        /// <summary>Nome da tabela de usuários</summary>
        public static string TabelaUsers = "Usuarios";
        /// <summary>Nome da tabela de configurações</summary>
        public static string TabelaConfig = "Config";
        /// <summary>Nome da tabela de dados</summary>
        public static string TabelaDados = "Dados";
        /// <summary>Criar tabela de usuários</summary>
        public static string strCriarTabelaUsers = "CREATE TABLE IF NOT EXISTS '" + TabelaUsers + "' (" +
            "'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
            "'Username' TEXT," + // Nome de usuário para a autenticação
            "'Senha' TEXT" + // Senha do usuário criptografada com md5 modificado
            ");";
        /// <summary>Comando para adicionar ou trocar a senha de usuários</summary>
        public static string strComandoUsuario = "INSERT INTO '" + TabelaUsers + "' " +
            "('Username','Senha') " +
            "VALUES(@Username, @Senha)";
        /// <summary>Criar tabela de configurações</summary>
        public static string strCriarTabelaConfig = "CREATE TABLE IF NOT EXISTS '" + TabelaConfig + "' (" +
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
        public static string strComandoConfig = "INSERT INTO '" + TabelaConfig + "' " +
            "('Servidor','Username','APIKEY') " +
            "VALUES(@Servidor, @Username, @APIKEY)";
        /// <summary>Criar a tabela de dados (adaptar essas configurações)</summary>
        public static string strCriarTabelaDados = "CREATE TABLE IF NOT EXISTS '" + TabelaDados + "' (" +
            "'ID' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
            "'Horario' INTEGER," + // Horário (UNIX)
            "'VA' NUMERIC," + "'VB' NUMERIC," + "'VC' NUMERIC" + //Tensões por fase(a definir)
            "'IA' NUMERIC," + "'IB' NUMERIC," + "'IC' NUMERIC," + //Correntes por fase
            "'P' NUMERIC," + "'Q' NUMERIC," + "'S' NUMERIC," + // Potências
            "'Nivel' NUMERIC," + "'TOleo' NUMERIC," + "'TEnrol' NUMERIC" + // Nível do óleo e temperatura
            ");";
        public static string strComandoDados = "INSERT INTO '" + TabelaDados + "' " +
            "('Horario','VA','VB','VC','IA','IB','IC','P','Q','S','Nivel','TOleo','TEnrol') " +
            "VALUES(@Horario,@VA,@VB,@VC,@IA,@IB,@IC,@P,@Q,@S,@Nivel,@TOleo,@TEnrol)";
        #endregion
    }
}
