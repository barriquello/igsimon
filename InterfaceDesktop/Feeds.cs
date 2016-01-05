using System;
namespace InterfaceDesktop
{
    // Utilizado em conjunto com a biblioteca para a des-serialização de strings JSON
    class Feed
    {
        public string id { get; set; }
        public string name { get; set; }
        public string tag { get; set; }
    }

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
        public float[] P = new float[Global.strCategoria.Length];//{ get; set; }
        /*        public float Q { get; set; }
                public float S { get; set; }
                public float Va { get; set; }
                public float Vb { get; set; }
                public float Vc { get; set; }
                public float Ia { get; set; }
                public float Ib { get; set; }
                public float Ic { get; set; }
                public float No { get; set; }
                public float To { get; set; }
                public float Te { get; set; }
                //*/
    }
}
