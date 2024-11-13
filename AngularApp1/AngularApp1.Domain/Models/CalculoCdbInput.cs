namespace AngularApp1.Domain.Models
{
    public class CalculoCdbInput
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
