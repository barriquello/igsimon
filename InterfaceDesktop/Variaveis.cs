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
        //Grandezas instantâneas:
        //Correntes I (A,B,C)
        public static FeedServidor fIa = new FeedServidor(func.Il, 0);
        public static FeedServidor fIb = new FeedServidor(func.Il, 1);
        public static FeedServidor fIc = new FeedServidor(func.Il, 2);

        //Tensões; V (A-B,B-C,C-A, A-N, B-N, C-N)
        public static FeedServidor fVab = new FeedServidor(func.Vl, 3);
        public static FeedServidor fVbc = new FeedServidor(func.Vl, 4);
        public static FeedServidor fVca = new FeedServidor(func.Vl, 5);

        public static FeedServidor fVan = new FeedServidor(func.Vf, 6);
        public static FeedServidor fVbn = new FeedServidor(func.Vf, 7);
        public static FeedServidor fVcn = new FeedServidor(func.Vf, 8);

        //Freqüência (F);
        public static FeedServidor fFreq = new FeedServidor(func.Fr, 9);

        //Potência Ativa (P);
        public static FeedServidor fP = new FeedServidor(func.Po, 10);
        //Potência Aparente (S);
        public static FeedServidor fS = new FeedServidor(func.Po, 11);
        //Potência Reativa (Q);
        public static FeedServidor fQ = new FeedServidor(func.Po, 12);
        //Fator de Potência (FP);
        public static FeedServidor fFatorPotencia = new FeedServidor(func.FP, 13);
        //Valores de Energia
        //Energia Total (EP);
        public static FeedServidor fEP = new FeedServidor(func.En, 14);
        //Energia Reativa (EQ);
        public static FeedServidor fEQ = new FeedServidor(func.En, 15);
        //Energia Aparente (ES);
        public static FeedServidor fES = new FeedServidor(func.En, 16);
        //Valores de Demanda (média)
        //Corrente IM (A,B,C)
        public static FeedServidor fIMa = new FeedServidor(func.Il, 17);
        public static FeedServidor fIMb = new FeedServidor(func.Il, 18);
        public static FeedServidor fIMc = new FeedServidor(func.Il, 19);
        //Potência Ativa, Reativa, Aparente; (PM, QM, SM)
        public static FeedServidor fPM = new FeedServidor(func.Po, 20);
        public static FeedServidor fQM = new FeedServidor(func.Po, 21);
        public static FeedServidor fSM = new FeedServidor(func.Po, 22);
        //Valores de Demanda Máxima (de pico)
        //Corrente Máxima; IP (A,B,C)
        public static FeedServidor fIPa = new FeedServidor(func.Il, 23);
        public static FeedServidor fIPb = new FeedServidor(func.Il, 24);
        public static FeedServidor fIPc = new FeedServidor(func.Il, 25);
        //Potência ativa máxima; (PP)
        public static FeedServidor fPP = new FeedServidor(func.Po, 26);
        //Potência reativa máxima; (QP)
        public static FeedServidor fQP = new FeedServidor(func.Po, 27);
        //Potência aparente máxima; (SP)
        public static FeedServidor fSP = new FeedServidor(func.Po, 28);

        //temperatura do óleo,
        public static FeedServidor fTOleo = new FeedServidor(func.Te, 29);
        //temperatura do enrolamento,
        public static FeedServidor fTEnrolamento = new FeedServidor(func.Te, 30);
        //nível de óleo (alto, médio, baixo)
        public static FeedServidor fNivelOleo = new FeedServidor(func.Ni, 31);
        //estado da válvula de pressão (atuado ou não-atuado)
        public static FeedServidor fValvulaPressao = new FeedServidor(func.Pr, 32);
    }
}
