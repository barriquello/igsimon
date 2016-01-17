using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceDesktop
{
    class FeedServidor
    {
        public FeedServidor() { }
        public FeedServidor(func funcao, int Indice)
        {
            Funcao = funcao;
            indice = Indice;
        }
        /// <summary>Função da variável (Po, Vf, Vl, Il, FP, Fr, Ni, Te, Pr</summary>
        public func Funcao;
        /// <summary>Nome do feed</summary>
        public string NomeFeed { get; set; }
        /// <summary>Índice do feed</summary>
        public string IndiceFeed = "";
        /// <summary>Índice da variável no banco de dados local (Registro)</summary>
        public int indice = 0;
    }
}
