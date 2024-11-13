using AngularApp1.Domain.Models;

namespace AngularApp1.Domain.Services
{
    /// <summary>
    /// Serviço de operações com investimento CDB
    /// </summary>
    public interface ICdbService
    {
        /// <summary>
        /// Calcula um investimento CDB com valor inicial e prazo em meses
        /// </summary>
        /// <param name="calculoCdbInput">Dados de entrada para cálculo</param>
        /// <returns>CDB calculado bruto e líquido</returns>
        ResgateCdbCalculado CalcularResgate(CalculoCdbInput calculoCdbInput);
    }
}
