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
}
