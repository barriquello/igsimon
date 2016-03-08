using System;
namespace InterfaceDesktop
{
    // Converte um registro CSV em DateTime e double.
    /// <summary>
    /// Classe que converte um registro recebido do servidor web para valores apropriados para a exibição em gráficos.
    /// </summary>
    class RegistroCSV
    {
        /// <summary>
        /// String Time (entrada).
        /// Valor a ser convertido de string para uint32.
        /// </summary>
        public string Time;
        /// <summary>
        /// Valor (entrada).
        /// Valor a ser convertido de srting para float.
        /// </summary>
        public string Valor;
        /// <summary>
        /// Converte string time em unix time (int).
        /// </summary>
        /// <returns>número de 32 bits sem sinal que representa unix time.</returns>
        public UInt32 timeUnix()
        {
            return Convert.ToUInt32(Time);
        }
        /// <summary>
        /// Converte string time para datetime.
        /// </summary>
        /// <returns>data e hora do registro.</returns>
        public DateTime time()
        {
            return Uteis.Unix2time(timeUnix());
        }
        /// <summary>
        /// Converte string valor para double.
        /// </summary>
        /// <returns>retorna o valor como double ou -1 em caso de erro.</returns>
        public double valor()
        {
            double retorno;
            try
            {
                retorno = Convert.ToDouble(Valor);
            }
            catch
            {
                retorno = -1;
            }
            return retorno;
        }

    }

    // Informações do banco de dados do servidor armazenadas na memória (isso pode ocupar bastante memória)
    /// <summary>
    /// Classe que armazena cada registro do servidor na memória RAM.
    /// Essa classe é capaz de armazenar um único registro contendo todas as variáveis do servidor, mantendo os dados sincronizados.
    /// </summary>
    public class RegistroDB
    {
        /// <summary>
        /// Horário do servidor.
        /// </summary>
        public UInt32 Horario;
        /// <summary>
        /// Variáveis como float.
        /// </summary>
        public float[] P = new float[Variaveis.NumVars];
    }
    /// <summary>
    /// Armazena as informações sobre cada um dos feeds.
    /// </summary>
    class Feed
    {
        /// <summary>
        /// nome do feed.
        /// </summary>
        public string Nome;
        /// <summary>
        /// número do feed no servidor.
        /// </summary>
        public string id;
    }
}
