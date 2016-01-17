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
        Po0, //0 = não plotar
        /// <summary>Tensão de fase (Van...)</summary>
        Vf, Vf0,
        /// <summary>Tensão de linha (Vab...)</summary>
        Vl, Vl0,
        /// <summary>Corrent (Ia...)</summary>
        Il, Il0,
        /// <summary>Fator de potência</summary>
        FP, FP0,
        /// <summary>Frequência</summary>
        Fr, Fr0,
        /// <summary>Nível do óleo</summary>
        Ni, Ni0,
        /// <summary>Temperaturas</summary>
        Te, Te0,
        /// <summary>Válvula de alivio de pressão</summary>
        Pr, Pr0,
        /// <summary>Energia</summary>
        En, En0
    }
}
