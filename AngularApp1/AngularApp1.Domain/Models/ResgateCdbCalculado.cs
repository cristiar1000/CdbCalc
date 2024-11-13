namespace AngularApp1.Domain.Models
{
    public class ResgateCdbCalculado
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
