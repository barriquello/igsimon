using System;
namespace InterfaceDesktop
{
    // Converte um registro CSV em DateTime e double
    class RegistroCSV
    {
        public string Time;
        public string Valor;
        public UInt32 timeUnix()
        {
            return Convert.ToUInt32(Time);
        }
        public DateTime time()
        {
            return Uteis.Unix2time(timeUnix());
        }
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

    // Informações do banco de dados do servidor armazenadas na memória (isso pode ocupar memória demais)
    public class RegistroDB
    {
        public UInt32 Horario;
        public float[] P = new float[Variaveis.NumVars];
    }
    class Feed
    {
        public string Nome;
        public string id;
    }
}
