﻿using System;
//using System.Collections.Generic; //Formulários e controles
//using System.IO; // Sistema de arquivos
using System.Security.Cryptography; // sistema de criptografia
using System.Text; //Codificação de texto
//using System.Windows.Forms;
//using System.Linq;
namespace InterfaceDesktop
{
    /// <summary>
    /// Classe contendo subrotinas de uso global do aplicativo
    /// </summary>
    class Uteis
    {
        /// <summary>
        /// Algoritmo MD5 modificado (sistema de criptografia para senhas)
        /// </summary>
        /// <param name="Input">Senha.</param>
        /// <returns>Senha criptografada.</returns>
        public static string getMD5(string Input)
        {
            MD5 md5 = MD5.Create();
            byte[] InputBytes = Encoding.ASCII.GetBytes(Input);
            byte[] md5Hash = md5.ComputeHash(InputBytes);
            StringBuilder SB = new System.Text.StringBuilder();
            for (int ii = 0; ii < md5Hash.Length; ii++)
                SB.Append(md5Hash[md5Hash.Length - ii - 1].ToString("X2"));
            return SB.ToString();
        }


        /// <summary>
        /// Converte data/hora em Unix time.
        /// </summary>
        /// <param name="Horario">Horário no formato data/hora.</param>
        /// <returns>Horário no formato Unix time.</returns>
        public static UInt32 Time2Unix(DateTime Horario)
        {
            return (UInt32)Horario.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }


        /// <summary>
        /// Converte horário Unix time em data/hora.
        /// </summary>
        /// <param name="Unix">Horário na notação Unix Time.</param>
        /// <returns>Data e hora do registro.</returns>
        public static DateTime Unix2time(UInt32 Unix)
        {
            DateTime Horario  = 
            new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc).AddSeconds(Unix);
            return Horario.ToLocalTime();
        }

    }
}