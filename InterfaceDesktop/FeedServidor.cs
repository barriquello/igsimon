using System;
using System.Collections.Generic;
using System.Drawing;
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
        public FeedServidor(func funcao, int Indice, string Node, string Formato, Color cor)
        {
            formato = Formato;
            NodeTv1 = Node;
            Funcao = funcao;
            indice = Indice;
            Cor = cor;
        }
        /// <summary>Função da variável (Po, Vf, Vl, Il, FP, Fr, Ni, Te, Pr</summary>
        public func Funcao;
        public Color Cor;// { get; set; }
        /// <summary>Nome do feed</summary>
        public string NomeFeed;// { get; set; }
        /// <summary>Índice do feed</summary>
        public string IndiceFeed = "";
        /// <summary>Índice da variável no banco de dados local (Registro)</summary>
        public int indice = 0;
        /// <summary>Índice do feed no treeview1</summary>
        public string NodeTv1 = "";
        /// <summary>Formato de exibição</summary>
        public string formato = "";
    }
}
