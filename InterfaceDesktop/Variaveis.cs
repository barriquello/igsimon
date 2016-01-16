using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceDesktop
{
    /// <summary>Função da variável feed</summary>
    public enum func
    {
        /// <summary>Potência (ativa, reativa, aparente...)</summary>
        Po,
        /// <summary>Tensão de fase (Van...)</summary>
        Vf,
        /// <summary>Tensão de linha (Vab...)</summary>
        Vl,
        /// <summary>Corrent (Ia...)</summary>
        Il,
        /// <summary>Fator de potência</summary>
        FP,
        /// <summary>Frequência</summary>
        Fr,
        /// <summary>Nível do óleo</summary>
        Ni,
        /// <summary>Temperaturas</summary>
        Te,
        /// <summary>Válvula de alivio de pressão</summary>
        Pr,
        /// <summary>Energia</summary>
        En
    }
    class FeedServidor
    {

        public FeedServidor(){}
        public FeedServidor(func funcao)
        {
            Funcao=funcao;
        }
        /// <summary>Função da variável (Po, Vf, Vl, Il, FP, Fr, Ni, Te, Pr</summary>
        public func Funcao;
        /// <summary>Nome do feed</summary>
        public string NomeFeed { get; set; }
        /// <summary>Índice do feed</summary>
        public string IndiceFeed = "";
    }
    class Variaveis
    {
        //Grandezas instantâneas:
        //Correntes I (A,B,C)
        public static FeedServidor fIa = new FeedServidor(func.Il);
        public static FeedServidor fIb = new FeedServidor(func.Il);
        public static FeedServidor fIc = new FeedServidor(func.Il);

        //Tensões; V (A-B,B-C,C-A, A-N, B-N, C-N)
        public static FeedServidor fVab = new FeedServidor(func.Vl);
        public static FeedServidor fVbc = new FeedServidor(func.Vl);
        public static FeedServidor fVca = new FeedServidor(func.Vl);

        public static FeedServidor fVan = new FeedServidor(func.Vf);
        public static FeedServidor fVbn = new FeedServidor(func.Vf);
        public static FeedServidor fVcn = new FeedServidor(func.Vf);

        //Freqüência (F);
        public static FeedServidor fFreq = new FeedServidor(func.Fr);

        //Potência Ativa (P);
        public static FeedServidor fP = new FeedServidor(func.Po);
        //Potência Aparente (S);
        public static FeedServidor fS = new FeedServidor(func.Po);
        //Potência Reativa (Q);
        public static FeedServidor fQ = new FeedServidor(func.Po);
        //Fator de Potência (FP);
        public static FeedServidor fFatorPotencia = new FeedServidor(func.FP);
        //Valores de Energia
        //Energia Total (EP);
        public static FeedServidor fEP = new FeedServidor(func.En);
        //Energia Reativa (EQ);
        public static FeedServidor fEQ = new FeedServidor(func.En);
        //Energia Aparente (ES);
        public static FeedServidor fES = new FeedServidor(func.En);
        //Valores de Demanda (média)
        //Corrente IM (A,B,C)
        public static FeedServidor fIMa = new FeedServidor(func.Il);
        public static FeedServidor fIMb = new FeedServidor(func.Il);
        public static FeedServidor fIMc = new FeedServidor(func.Il);
        //Potência Ativa, Reativa, Aparente; (PM, QM, SM)
        public static FeedServidor fPM = new FeedServidor(func.Po);
        public static FeedServidor fQM = new FeedServidor(func.Po);
        public static FeedServidor fSM = new FeedServidor(func.Po);
        //Valores de Demanda Máxima (de pico)
        //Corrente Máxima; IP (A,B,C)
        public static FeedServidor fIPa = new FeedServidor(func.Il);
        public static FeedServidor fIPb = new FeedServidor(func.Il);
        public static FeedServidor fIPc = new FeedServidor(func.Il);
        //Potência ativa máxima; (PP)
        public static FeedServidor fPP = new FeedServidor(func.Po );
        //Potência reativa máxima; (QP)
        public static FeedServidor fQP = new FeedServidor(func.Po);
        //Potência aparente máxima; (SP)
        public static FeedServidor fSP = new FeedServidor(func.Po);

        //temperatura do óleo,
        public static FeedServidor fTOleo = new FeedServidor(func.Te);
        //temperatura do enrolamento,
        public static FeedServidor fTEnrolamento = new FeedServidor(func.Te);
        //nível de óleo (alto, médio, baixo)
        public static FeedServidor fNivelOleo = new FeedServidor(func.Ni);
        //estado da válvula de pressão (atuado ou não-atuado)
        public static FeedServidor fValvulaPressao = new FeedServidor(func.Pr);
    }
}
