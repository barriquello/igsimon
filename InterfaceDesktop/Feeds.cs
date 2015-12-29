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

  /*  class Registro
    {
        public int horarioUnix { get; set; }
        public DateTime Horario()
        {
            return Uteis.Unix2time(horarioUnix);
        }
        public float P { get; set; }
        public float Q { get; set; }
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
    }//*/

    // Converte um registro CSV em DateTime e float
    class RegistroCSV
    {
        public static string Time { get; set; }
        public static string Valor { get; set; }
        public static DateTime time()
        {
            return Uteis.Unix2time(Convert.ToInt32(Time));
        }
        public static float valor()
        {
            return Convert.ToSingle(Valor);
        }
        
    }
}
