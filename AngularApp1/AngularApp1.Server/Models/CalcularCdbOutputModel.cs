﻿namespace AngularApp1.Server.Models
{
    /// <summary>
    /// Resultado do cálculo de investimento CDB
    /// </summary>
    public class CalcularCdbOutputModel
    {
        /// <summary>
        /// Resultado bruto, sem desconto de imposto de renda
        /// </summary>
        public decimal ResultadoBruto { get; set; }

        /// <summary>
        /// Valor líquido, obtido após desconto do imposto de renda
        /// </summary>
        public decimal ResultadoLiquido { get; set; }
    }
}
