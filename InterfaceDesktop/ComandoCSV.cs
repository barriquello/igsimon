using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace InterfaceDesktop
{
    class ComandosCSV
    {

        /// <summary>Comando para obter um arquivo CSV</summary>
        public static readonly string strComandoCSV = "/feed/csvexport.json?id=";
        /// <summary>Comando para obter o horário</summary>
        public static readonly string strComandoHorario = "/feed/get.json?field=time&id=";
        /// <summary>Comando para obter um arquivo único valor</summary>
        public static readonly string strComandoValor = "/feed/get.json?field=value&id=";
        /// <summary>Comando para obter a lista de feeds</summary>
        public static readonly string strComandoFeedList = "/feed/list.json?apikey=";
        /// <summary>Rotina para gerar o nome do arquivo CSV com base na data</summary>
        public static string ArquivoCSV(DateTime Data)
        {
            return string.Format("DB_{0:D4}_{1:D2}_{2:D2}.csv", Data.Year, Data.Month, Data.Day);
        }
    }
}
