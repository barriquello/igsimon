using System;
namespace InterfaceDesktop
{
	// Utilizado em conjunto com a biblioteca para a des-serialização de strings JSON
    class Feeds
    {		
        public string id { get; set; }
        public string name { get; set; }
        public string tag { get; set; }
    }

    class Registro
    {
        public int horarioUnix { get; set; }
        public DateTime Horario ()
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
    }

    /// <summary>Índices das variáveis no servidor</summary>
    class Indices
    {
        public static string P = "0";
        public static string Q = "0";
        public static string S = "0";
        public static string Va = "0";
        public static string Vb = "0";
        public static string Vc = "0";
        public static string Ia = "0";
        public static string Ib = "0";
        public static string Ic = "0";
        public static string No = "0";
        public static string To = "0";
        public static string Te = "0";
    }
}
