﻿namespace AngularApp1.Server.Models
{
    /// <summary>
    /// Dados de entrada para cálculo de investimento CDB
    /// </summary>
    public class CalcularCdbInputModel
    {
        /// <summary>
        /// Valor do investimento
        /// </summary>
        public decimal ValorInicial { get; set; }
        
        /// <summary>
        /// Prazo para resgate do investimento em meses
        /// </summary>
        public int QtdeMeses { get; set; }
    }
}
