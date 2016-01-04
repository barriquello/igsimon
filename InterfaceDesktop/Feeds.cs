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
        public  DateTime time()
        {
            return Uteis.Unix2time(Convert.ToUInt32(Time));
        }
        public  double valor()
        {
            return Convert.ToDouble(Valor);
        }
        
    }
}
