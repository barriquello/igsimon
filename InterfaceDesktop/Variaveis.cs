using System.Drawing;
namespace InterfaceDesktop
{
    /// <summary>
    /// Variáveis de interesse do programa (tensão, corrente, temperatura, etc).
    /// </summary>
    class Variaveis
    {
        /// <summary>
        /// Número de variáveis.
        /// </summary>
        public static int NumVars = strVariaveis().Length;//=33
        /// <summary>
        /// Lista de variáveis.
        /// </summary>
        /// <returns>Retorna uma matriz com todas as variáveis de interesse.</returns>
        public static FeedServidor[] strVariaveis()
        {
            return new FeedServidor[]
            {
                fIa, fIb, fIc,
                fVab, fVbc, fVca,
                fVan,fVbn,fVcn,
                fFreq,
                fP,fQ,fS,
                fFatorPotencia,
                fEP,fEQ,fES,
                fIMa,fIMb,fIMc,
                fPM, fQM,fSM,
                fIPa,fIPb,fIPc,
                fPP,fQP,fSP,
                fTOleo,fTEnrolamento,
                fNivelOleo, fValvulaPressao
            };
        }
        /// <summary>
        /// Precisão da exibição dos valores.
        /// </summary>
        private static readonly string strPrecisao = "{0}";
        //Grandezas instantâneas:
        //Correntes I (A,B,C)
        /// <summary>
        /// Corrente Ia.
        /// </summary>
        public static FeedServidor fIa = new FeedServidor(func.Il, 0, "NIa", string.Format("Ia = {0} A", strPrecisao), Color.Red, "Corrente Ia");
        /// <summary>
        /// Corrente Ib.
        /// </summary>
        public static FeedServidor fIb = new FeedServidor(func.Il, 1, "NIb", string.Format("Ib = {0} A", strPrecisao), Color.Green, "Corrente Ib");
        /// <summary>
        /// Corrente Ic.
        /// </summary>
        public static FeedServidor fIc = new FeedServidor(func.Il, 2, "NIc", string.Format("Ic = {0} A", strPrecisao), Color.Blue, "Corrente Ic");

        //Tensões; V (A-B,B-C,C-A, A-N, B-N, C-N)
        /// <summary>
        /// Tensão Vab.
        /// </summary>
        public static FeedServidor fVab = new FeedServidor(func.Vl, 3, "NVab", string.Format("Vab = {0} V", strPrecisao), Color.Red, "Tensão Vab");
        /// <summary>
        /// Tensão Vab.
        /// </summary>
        public static FeedServidor fVbc = new FeedServidor(func.Vl, 4, "NVbc", string.Format("Vbc = {0} V", strPrecisao), Color.Green, "Tensão Vbc");
        /// <summary>
        /// Tensão Vca ou Vac.
        /// </summary>
        public static FeedServidor fVca = new FeedServidor(func.Vl, 5, "NVca", string.Format("Vca = {0} V", strPrecisao), Color.Blue, "Tensão Vca");
        /// <summary>
        /// Tensão Van.
        /// </summary>
        public static FeedServidor fVan = new FeedServidor(func.Vf, 6, "NVan", string.Format("Van = {0} V", strPrecisao), Color.Red, "Tensão Van");
        /// <summary>
        /// Tensão Vbn.
        /// </summary>
        public static FeedServidor fVbn = new FeedServidor(func.Vf, 7, "NVbn", string.Format("Vbn = {0} V", strPrecisao), Color.Green, "Tensão Vbn");
        /// <summary>
        /// Tensão Vcn.
        /// </summary>
        public static FeedServidor fVcn = new FeedServidor(func.Vf, 8, "NVcn", string.Format("Vcn = {0} V", strPrecisao), Color.Blue, "Tensão Vcn");

        //Freqüência (F);
        /// <summary>
        /// Frequência.
        /// </summary>
        public static FeedServidor fFreq = new FeedServidor(func.Fr0, 9, "NFreq", string.Format("Frequência = {0} Hz", strPrecisao), Color.Black, "Frequência");

        //Potência Ativa (P);
        /// <summary>
        /// Potência ativa P.
        /// </summary>
        public static FeedServidor fP = new FeedServidor(func.Po, 10, "NP", string.Format("P = {0} W", strPrecisao), Color.Red, "Potência Ativa");
        //Potência Aparente (S);
        /// <summary>
        /// Potência aparente S.
        /// </summary>
        public static FeedServidor fS = new FeedServidor(func.Po, 11, "NS", string.Format("S = {0} VA", strPrecisao), Color.Green, "Potência Aparente");
        //Potência Reativa (Q);
        /// <summary>
        /// Potência reativa Q.
        /// </summary>
        public static FeedServidor fQ = new FeedServidor(func.Po, 12, "NQ", string.Format("Q = {0} VAr", strPrecisao), Color.Blue, "Potência Reativa");
        //Fator de Potência (FP);
        /// <summary>
        /// Fator de potência (cos (\u03C6)) FP.
        /// </summary>
        public static FeedServidor fFatorPotencia = new FeedServidor(func.FP, 13, "NFP", string.Format("Cos (\u03C6) = {0}", strPrecisao), Color.Black, "Cos (\u03C6)");
        //Valores de Energia
        //Energia Total (EP);
        /// <summary>
        /// Energia ativa total EQ.
        /// </summary>
        public static FeedServidor fEP = new FeedServidor(func.En0, 14, "NEP", string.Format("EP = {0} Wh", strPrecisao), Color.Black, "Energia Ativa Total");
        //Energia Reativa (EQ);
        /// <summary>
        /// Energia reativa total EQ.
        /// </summary>
        public static FeedServidor fEQ = new FeedServidor(func.En0, 15, "NEQ", string.Format("EQ = {0} VArh", strPrecisao), Color.Black, "Energia Reativa Total");
        //Energia Aparente (ES);
        /// <summary>
        /// Energia aparente total ES.
        /// </summary>
        public static FeedServidor fES = new FeedServidor(func.En0, 16, "NES", string.Format("ES = {0} VAh", strPrecisao), Color.Black, "Energia Aparente Total");
        //Valores de Demanda (média)
        //Corrente IM (A,B,C)
        /// <summary>
        /// Corrente média IMa.
        /// </summary>
        public static FeedServidor fIMa = new FeedServidor(func.Il0, 17, "NIMa", string.Format("IMa = {0} A", strPrecisao), Color.Black, "Corrente de Demanda IMa");
        /// <summary>
        /// Corrente média IMb.
        /// </summary>
        public static FeedServidor fIMb = new FeedServidor(func.Il0, 18, "NIMb", string.Format("IMb = {0} A", strPrecisao), Color.Black, "Corrente de Demanda IMb");
        /// <summary>
        /// Corrente média IMc.
        /// </summary>
        public static FeedServidor fIMc = new FeedServidor(func.Il0, 19, "NIMc", string.Format("IMc = {0} A", strPrecisao), Color.Black, "Corrente de Demanda IMc");
        //Potência Ativa, Reativa, Aparente; (PM, QM, SM)
        /// <summary>
        /// Potência ativa média.
        /// </summary>
        public static FeedServidor fPM = new FeedServidor(func.Po0, 20, "NPM", string.Format("PM = {0} W", strPrecisao), Color.Black, "Potência Ativa de Demanda");
        /// <summary>
        /// Potência reativa média.
        /// </summary>
        public static FeedServidor fQM = new FeedServidor(func.Po0, 21, "NQM", string.Format("QM = {0} VAr", strPrecisao), Color.Black, "Potência Reativa de Demanda");
        /// <summary>
        /// Potência aparente média.
        /// </summary>
        public static FeedServidor fSM = new FeedServidor(func.Po0, 22, "NSM", string.Format("SM = {0} VA", strPrecisao), Color.Black, "Potência Aparente de Demanda");
        //Valores de Demanda Máxima (de pico)
        //Corrente Máxima; IP (A,B,C)
        /// <summary>
        /// Corrente máxima IPa.
        /// </summary>
        public static FeedServidor fIPa = new FeedServidor(func.Il0, 23, "NIPa", string.Format("IPa = {0} A", strPrecisao), Color.Black, "Corrente Máxima IPa");
        /// <summary>
        /// Corrente máxima IPb.
        /// </summary>
        public static FeedServidor fIPb = new FeedServidor(func.Il0, 24, "NIPb", string.Format("IPb = {0} A", strPrecisao), Color.Black, "Corrente Máxima IPb");
        /// <summary>
        /// Corrente máxima IPc.
        /// </summary>
        public static FeedServidor fIPc = new FeedServidor(func.Il0, 25, "NIPc", string.Format("IPc = {0} A", strPrecisao), Color.Black, "Corrente Máxima IPc");
        //Potência ativa máxima; (PP)
        /// <summary>
        /// Potência ativa máxima.
        /// </summary>
        public static FeedServidor fPP = new FeedServidor(func.Po0, 26, "NPP", string.Format("PP = {0} W", strPrecisao), Color.Black, "Potência Ativa Máxima");
        //Potência reativa máxima; (QP)
        /// <summary>
        /// Potência reativa máxima.
        /// </summary>
        public static FeedServidor fQP = new FeedServidor(func.Po0, 27, "NQP", string.Format("QP = {0} VAr", strPrecisao), Color.Black, "Potência Reativa Máxima");
        //Potência aparente máxima; (SP)
        /// <summary>
        /// Potência aparente máxima.
        /// </summary>
        public static FeedServidor fSP = new FeedServidor(func.Po0, 28, "NSP", string.Format("SP = {0} VA", strPrecisao), Color.Black, "Potência Aparente Máxima");

        //temperatura do óleo,
        /// <summary>
        /// Temperatura do óleo isolante.
        /// </summary>
        public static FeedServidor fTOleo = new FeedServidor(func.Te, 29, "NTO", string.Format("Temperatura do óleo = {0} °C", strPrecisao), Color.Red, "Temperatura do Óleo");
        //temperatura do enrolamento,
        /// <summary>
        /// Temperatura dos enrolamentos.
        /// </summary>
        public static FeedServidor fTEnrolamento = new FeedServidor(func.Te, 30, "NTE", string.Format("Temperatura dos enrolamentos = {0} °C", strPrecisao), Color.Green, "Temperatura dos Enrolamentos");
        //nível de óleo (alto, médio, baixo)
        /// <summary>
        /// Nível do óleo.
        /// </summary>
        public static FeedServidor fNivelOleo = new FeedServidor(func.Ni, 31, "NNO", "Nível do óleo = {0}", Color.Black, "Nível do Óleo");
        //estado da válvula de pressão (atuado ou não-atuado)
        /// <summary>
        /// Estado da válvula de alívio de pressão.
        /// </summary>
        public static FeedServidor fValvulaPressao = new FeedServidor(func.Pr0, 32, "NVP", "Válvula de alívio de pressão = {0}", Color.Black, "Válvula de alívio de pressão");
    }
}
