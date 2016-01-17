using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceDesktop
{
    class ComandosCSV
    {

        /// <summary>Comando para obter um arquivo CSV</summary>
        public const string strComandoCSV = "/feed/csvexport.json?id=";
        /// <summary>Comando para obter o horário</summary>
        public const string strComandoHorario = "/feed/get.json?field=time&id=";
        /// <summary>Comando para obter um arquivo único valor</summary>
        public const string strComandoValor = "/feed/get.json?field=value&id=";
        /// <summary>Comando para obter a lista de feeds</summary>
        public const string strComandoFeedList = "/feed/list.json?apikey=";
        /// <summary>Rotina para gerar o nome do arquivo CSV com base na data</summary>
        public static string ArquivoCSV(DateTime Data)
        {
            return "DB_" + Data.Year.ToString("D4") + "_" + Data.Month.ToString("D2") + "_" + Data.Day.ToString("D2") + ".csv";
        }
    }
}
