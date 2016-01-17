
using System.Collections.Generic;
namespace InterfaceDesktop
{
    class json
    {
        public static List<Feed> json2Feed(string Requisicao)
        {
            const string strListaRemover = "[{\":,}]";
            // Requisição = [{campo:valor, campo:valor, ...}, {campo:valor, ...},...]
            List<Feed> FDD = new List<Feed>();
            string[] Linha = Requisicao.Split('}'); //Linha = {campo:valor, campo:valor, ...}
            for (int jj = 0; jj < Linha.Length; jj++)
            {
                string[] Campo = Linha[jj].Split(','); // Campo = campo:valor
                // Adicionar elementos ao feed
                Feed Fdd = new Feed();
                int Verificador = 0;
                for (int kk = 0; kk < Campo.Length; kk++)
                {
                    string[] Elemento = Campo[kk].Split(':'); // Elemento = campo ou valor
                    if (Elemento.Length == 2)
                    {
                        {
                            if (Elemento[0].Contains("\"id\""))
                            {
                                for (int mm = 0; mm < strListaRemover.Length; mm++)  //remover caracteres especiais
                                {
                                    Elemento[1] = Elemento[1].Replace(strListaRemover.Substring(mm, 1), "");
                                }
                                Fdd.id = Elemento[1];
                                Verificador += 5;
                            }
                            if (Elemento[0].Contains("\"name\""))
                            {
                                for (int mm = 0; mm < strListaRemover.Length; mm++)  //remover caracteres especiais
                                {
                                    Elemento[1] = Elemento[1].Replace(strListaRemover.Substring(mm, 1), "");
                                }
                                Fdd.Nome = Elemento[1];
                                Verificador += 7;
                            }
                        }
                        if (Verificador == 5 + 7)
                        {
                            FDD.Add(Fdd);
                            Verificador = 0;
                            Fdd = new Feed();
                            break;
                        }
                    }
                }
            }
            return FDD;
        }

    }
}
