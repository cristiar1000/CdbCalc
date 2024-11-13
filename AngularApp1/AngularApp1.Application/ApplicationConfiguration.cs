using AngularApp1.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngularApp1.Application
{
    public class ApplicationConfiguration
    {
        /// <summary>
        /// Quanto o banco paga sobre o CDI
        /// </summary>
        public decimal Tb { get; set; }

        /// <summary>
        /// Taxa de CDI - Certificado de Depósito Interbancário
        /// </summary>
        public decimal Cdi { get; set; }

        /// <summary>
        /// Faixas de imposto por meses de investimento:
        ///     - ordenado pela maior taxa e consequentemente, menor prazo 
        /// </summary>
        public IEnumerable<FaixaIrCdb>? IrCdb { get; set; }
    }
}
