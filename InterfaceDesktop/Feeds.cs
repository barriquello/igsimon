using System;
namespace InterfaceDesktop
{
    // Converte um registro CSV em DateTime e double
    class RegistroCSV
    {
        public string Time { get; set; }
        public string Valor { get; set; }
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
            return Convert.ToDouble(Valor);
        }

    }

    // Informações do banco de dados do servidor armazenadas na memória (isso pode ocupar memória demais)
    class RegistroDB
    {
        public UInt32 Horario { get; set; }
        public float[] P = new float[Variaveis.NumVars];
    }
    class Feed
    {
        public string Nome;
        public string id;
    }
}
