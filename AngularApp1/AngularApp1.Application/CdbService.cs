using AngularApp1.Domain.Models;
using AngularApp1.Domain.Services;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace AngularApp1.Application
{
    public class CdbService : ICdbService
    {
        private readonly ApplicationConfiguration _applicationConfiguration;

        public CdbService(IOptions<ApplicationConfiguration> applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration.Value;
        }

        public ResgateCdbCalculado CalcularResgate(CalculoCdbInput calculoCdbInput)
        {
            if (calculoCdbInput.ValorInicial <= 0)
            {
                throw new ArgumentException("Valor inicial deve ser maior que zero.");
            }

            if (calculoCdbInput.QtdeMeses <= 1)
            {
                throw new ArgumentException("Quantidade de meses deve ser maior que hum.");
            }

            decimal valorAtualizado = calculoCdbInput.ValorInicial;

            for(int i = 0; i < calculoCdbInput.QtdeMeses; i++)
            {
                valorAtualizado = valorAtualizado * (1 +
                    (_applicationConfiguration.Cdi * _applicationConfiguration.Tb));
            }

            var rendimentoBruto = valorAtualizado - calculoCdbInput.ValorInicial;
            var rendimentoLiquido = CalcularRendimentoLiquido(
                rendimentoBruto, calculoCdbInput.QtdeMeses);

            return new ResgateCdbCalculado
            {
                ResultadoBruto = Math.Round(valorAtualizado, 2),
                ResultadoLiquido = Math.Round(calculoCdbInput.ValorInicial + rendimentoLiquido, 2),
            };             
        }

        private decimal CalcularRendimentoLiquido(decimal rendimento, int qtdeMeses)
        {
            var faixaIrCdb = _applicationConfiguration.IrCdb.AsQueryable().Where(
                irCdb => irCdb.QtdeMaximaMeses > qtdeMeses)
                .OrderBy(irCdb => irCdb.QtdeMaximaMeses).First();

            var resultadoLiquido = rendimento - (rendimento * faixaIrCdb.TaxaIr / 100);
            return resultadoLiquido;
        }
    }
}
