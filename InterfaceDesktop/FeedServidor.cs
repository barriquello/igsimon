using System;
//using System.Collections.Generic;
using System.Drawing;
//using System.Linq;
//using System.Text;

namespace InterfaceDesktop
{
    /// <summary>
    /// Classe que armazena as informações sobre os feeds do servidor e as características das variáveis (tensão, corrente, etc).
    /// </summary>
    class FeedServidor
    {
        /// <summary>
        /// Inicializa a classe vazia, inicializando as variáveis com os valores padrão.
        /// </summary>
        public FeedServidor() { }
        /// <summary>
        /// Inicializa a classe com o indice e a função.
        /// </summary>
        /// <param name="funcao">Funcção da classe (tensão, corrente, etc).</param>
        /// <param name="Indice">Íncice da variável no banco de dados local.</param>
        public FeedServidor(func funcao, int Indice)
        {
            Funcao = funcao;
            indice = Indice;
        }
        /// <summary>
        /// Inicializa a classe com a função, nome, indice, cor, nó e formato.
        /// </summary>
        /// <param name="funcao">Tipo de variável (tensão, corrente, etc).</param>
        /// <param name="Indice">Índice da variável no banco de dados local.</param>
        /// <param name="Node">Nó da variável nos listview.</param>
        /// <param name="Formato">Formato de exibição do texto.</param>
        /// <param name="cor">Cor utilizada nos gráficos.</param>
        /// <param name="nome">Nome do feed nas tabelas.</param>
        public FeedServidor(func funcao, int Indice, string Node, string Formato, Color cor, string nome)
        {
            formato = Formato;
            NodeTv1 = Node;
            Funcao = funcao;
            indice = Indice;
            Cor = cor;
            NomeTabela = nome;
        }
        /// <summary>Função da variável (Po, Vf, Vl, Il, FP, Fr, Ni, Te, Pr, etc).</summary>
        public func Funcao;
        /// <summary>
        /// Cor a exibir nos gráficos.
        /// </summary>
        public Color Cor;
        /// <summary>Nome do feed no serivdor, no banco de dados local, nos gráficos e nos arquivos exportados.</summary>
        public string NomeFeed;
        /// <summary>Nome nas tabelas.</summary>
        public string NomeTabela;
        /// <summary>Índice do feed no servidor.</summary>
        public string IndiceFeed = "";
        /// <summary>Índice da variável no banco de dados local (Registro).</summary>
        public int indice = 0;
        /// <summary>"Índice" do feed no treeview.</summary>
        public string NodeTv1 = "";
        /// <summary>Formato de exibição string.Format().</summary>
        public string formato = "";
    }
}
