using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceDesktop
{
    /// <summary>Função da variável feed</summary>
    public enum func
    {
        /// <summary>Potência (ativa, reativa, aparente) - a plotar</summary>
        Po,
        /// <summary>Potência (ativa, reativa, aparente) - não plotar</summary>
        Po0, //0 = não plotar
        /// <summary>Tensão de fase (Van, Vbn, Vcn) - a plotar</summary>
        Vf,
        /// <summary>Tensão de fase (Van, Vbn, Vcn) - não plotar</summary>
        Vf0,
        /// <summary>Tensão de linha (Vab, Vbc, Vca) - a plotar</summary>
        Vl,
        /// <summary>Tensão de linha (Vab, Vbc, Vca) - não plotar</summary>
        Vl0,
        /// <summary>Corrente (Ia, Ib, Ic) - a plotar</summary>
        Il,
        /// <summary>Corrente (Ia, Ib, Ic) - não plotar</summary>
        Il0,
        /// <summary>Fator de potência - a plotar</summary>
        FP,
        /// <summary>Fator de potência - não plotar</summary>
        FP0,
        /// <summary>Frequência - a plotar</summary>
        Fr,
        /// <summary>Frequência - não plotar</summary>
        Fr0,
        /// <summary>Nível do óleo - a plotar</summary>
        Ni,
        /// <summary>Nível do óleo - não plotar</summary>
        Ni0,
        /// <summary>Temperaturas - a plotar</summary>
        Te,
        /// <summary>Temperaturas - não plotar</summary>
        Te0,
        /// <summary>Válvula de alivio de pressão - a plotar</summary>
        Pr,
        /// <summary>Válvula de alivio de pressão - não plotar</summary>
        Pr0,
        /// <summary>Energia - a plotar</summary>
        En,
        /// <summary>Energia - não plotar</summary>
        En0
    }
    /// <summary>Identificador da chamada para exportar para excel (xlsx)</summary>
    public enum FormSalvarExcel
    {
        /// <summary>
        /// Formulário principal (online)
        /// </summary>
        frmMain, 
        /// <summary>
        /// Formulário de gráficos (offline)
        /// </summary>
        frmGraficos, 
        /// <summary>
        /// Formulário de comparação
        /// </summary>
        frmComparacao
    }
}
