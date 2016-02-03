using System;
//using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
//using System.Text;

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
        public FeedServidor(func funcao, int Indice, string Node, string Formato, Color cor, string nome)
        {
            formato = Formato;
            NodeTv1 = Node;
            Funcao = funcao;
            indice = Indice;
            Cor = cor;
            NomeTabela = nome;
        }
        /// <summary>Função da variável (Po, Vf, Vl, Il, FP, Fr, Ni, Te, Pr</summary>
        public func Funcao;
        public Color Cor;
        /// <summary>Nome do feed no serivdor, no banco de dados local, nos gráficos e nos arquivos exportados</summary>
        public string NomeFeed;
        /// <summary>Nome nas tabelas</summary>
        public string NomeTabela;
        /// <summary>Índice do feed no servidor</summary>
        public string IndiceFeed = "";
        /// <summary>Índice da variável no banco de dados local (Registro)</summary>
        public int indice = 0;
        /// <summary>"Índice" do feed no treeview1</summary>
        public string NodeTv1 = "";
        /// <summary>Formato de exibição string.Format()</summary>
        public string formato = "";
    }
}
