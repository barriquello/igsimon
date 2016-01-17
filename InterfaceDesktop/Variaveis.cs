namespace InterfaceDesktop
{

    class Variaveis
    {
        public static int NumVars = strVariaveis().Length;//=33
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
        private const string strPrecisao = "{0}";
        //Grandezas instantâneas:
        //Correntes I (A,B,C)
        public static FeedServidor fIa = new FeedServidor(func.Il, 0, "NIa","Ia = "+strPrecisao+" A");
        public static FeedServidor fIb = new FeedServidor(func.Il, 1, "NIb", "Ib = " + strPrecisao + " A");
        public static FeedServidor fIc = new FeedServidor(func.Il, 2, "NIc", "Ic = " + strPrecisao + " A");

        //Tensões; V (A-B,B-C,C-A, A-N, B-N, C-N)
        public static FeedServidor fVab = new FeedServidor(func.Vl, 3, "NVab","Vab = "+strPrecisao + " V");
        public static FeedServidor fVbc = new FeedServidor(func.Vl, 4, "NVbc","Vbc = " +strPrecisao+ " V");
        public static FeedServidor fVca = new FeedServidor(func.Vl, 5, "NVca","Vca = " +strPrecisao+ " V");

        public static FeedServidor fVan = new FeedServidor(func.Vf, 6, "NVan","Van = " +strPrecisao+ " V");
        public static FeedServidor fVbn = new FeedServidor(func.Vf, 7, "NVbn","Vbn = " +strPrecisao+ " V");
        public static FeedServidor fVcn = new FeedServidor(func.Vf, 8, "NVcn","Vcn = " +strPrecisao+ " V");

        //Freqüência (F);
        public static FeedServidor fFreq = new FeedServidor(func.Fr0, 9, "NFreq","Frequência = " +strPrecisao);

        //Potência Ativa (P);
        public static FeedServidor fP = new FeedServidor(func.Po, 10, "NP", "P = " + strPrecisao +" W");
        //Potência Aparente (S);
        public static FeedServidor fS = new FeedServidor(func.Po, 11, "NS", "S = " + strPrecisao +" VA");
        //Potência Reativa (Q);
        public static FeedServidor fQ = new FeedServidor(func.Po, 12, "NQ", "Q = " + strPrecisao + " VAr");
        //Fator de Potência (FP);
        public static FeedServidor fFatorPotencia = new FeedServidor(func.FP, 13, "NFP", "Cos (\u03C6) = " + strPrecisao);
        //Valores de Energia
        //Energia Total (EP);
        public static FeedServidor fEP = new FeedServidor(func.En0, 14, "NEP", "EP = " + strPrecisao +" Wh");
        //Energia Reativa (EQ);
        public static FeedServidor fEQ = new FeedServidor(func.En0, 15, "NEQ", "EQ = " + strPrecisao+" VArh");
        //Energia Aparente (ES);
        public static FeedServidor fES = new FeedServidor(func.En0, 16, "NES", "ES = " + strPrecisao+" VAh");
        //Valores de Demanda (média)
        //Corrente IM (A,B,C)
        public static FeedServidor fIMa = new FeedServidor(func.Il0, 17, "NIMa", "IMa = " + strPrecisao+" A");
        public static FeedServidor fIMb = new FeedServidor(func.Il0, 18, "NIMb", "IMb = " + strPrecisao + " A");
        public static FeedServidor fIMc = new FeedServidor(func.Il0, 19, "NIMc", "IMc = " + strPrecisao + " A");
        //Potência Ativa, Reativa, Aparente; (PM, QM, SM)
        public static FeedServidor fPM = new FeedServidor(func.Po0, 20, "NPM", "PM = " + strPrecisao +" W");
        public static FeedServidor fQM = new FeedServidor(func.Po0, 21, "NQM", "QM = " + strPrecisao+" VAr");
        public static FeedServidor fSM = new FeedServidor(func.Po0, 22, "NSM", "SM = " + strPrecisao+" VA");
        //Valores de Demanda Máxima (de pico)
        //Corrente Máxima; IP (A,B,C)
        public static FeedServidor fIPa = new FeedServidor(func.Il0, 23, "NIPa", "IPa = " + strPrecisao + " A");
        public static FeedServidor fIPb = new FeedServidor(func.Il0, 24, "NIPb", "IPb = " + strPrecisao + " A");
        public static FeedServidor fIPc = new FeedServidor(func.Il0, 25, "NIPc", "IPc = " + strPrecisao + " A");
        //Potência ativa máxima; (PP)
        public static FeedServidor fPP = new FeedServidor(func.Po0, 26, "NPP", "PP = " + strPrecisao+" W");
        //Potência reativa máxima; (QP)
        public static FeedServidor fQP = new FeedServidor(func.Po0, 27, "NQP", "QP = " + strPrecisao+" VAr");
        //Potência aparente máxima; (SP)
        public static FeedServidor fSP = new FeedServidor(func.Po0, 28, "NSP", "SP = " + strPrecisao+ " VA");

        //temperatura do óleo,
        public static FeedServidor fTOleo = new FeedServidor(func.Te, 29, "NTO", "Óleo isolante = " + strPrecisao + " ºC");
        //temperatura do enrolamento,
        public static FeedServidor fTEnrolamento = new FeedServidor(func.Te, 30, "NTE", "Enrolamentos = " + strPrecisao + " ºC");
        //nível de óleo (alto, médio, baixo)
        public static FeedServidor fNivelOleo = new FeedServidor(func.Ni, 31, "NNO", "Nível do óleo = {0}");
        //estado da válvula de pressão (atuado ou não-atuado)
        public static FeedServidor fValvulaPressao = new FeedServidor(func.Pr0, 32, "NVP", "Válvula de segurança = {0}");
    }
}
