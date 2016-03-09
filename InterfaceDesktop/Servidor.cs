namespace InterfaceDesktop
{
    /// <summary>
    /// Informações a respeito do servidor e autenticação.
    /// </summary>
    class Servidor
    {
        /// <summary>Nome de usuário para autenticação (local).</summary>
        public static string Username = "";
        /// <summary>Senha para autenticação (criptografada) (local).</summary>
        public static string Senha = "";
        /// <summary>Premissões do usuário: 0 = sem permissão para acessar configurações; 1 = com permissão para acessar configurações.</summary>
        public static int Permissoes = 0;
        /// <summary>APIKey para autenticação com o servidor web.</summary>
        public static string APIKey = "";
        /// <summary>
        /// Endereço do servidor.
        /// </summary>
        public static string Server = "";
    }
}
