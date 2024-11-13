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

        public ResgateCdbCalculado CalcularResgate(decimal valorInicial, int qtdeMeses)
        {
            if (valorInicial <= 0)
            {
                throw new ArgumentException("Valor inicial deve ser maior que zero.");
            }

            if (qtdeMeses <= 1)
            {
                throw new ArgumentException("Quantidade de meses deve ser maior que hum.");
            }

            decimal valorAtualizado = valorInicial;

            for(int i = 0; i < qtdeMeses; i++)
            {
                valorAtualizado = valorAtualizado * (1 +
                    (_applicationConfiguration.Cdi * _applicationConfiguration.Tb));
            }

            var rendimentoBruto = valorAtualizado - valorInicial;
            var rendimentoLiquido = CalcularRendimentoLiquido(rendimentoBruto, qtdeMeses);

            return new ResgateCdbCalculado
            {
                ResultadoBruto = valorAtualizado,
                ResultadoLiquido = valorInicial + rendimentoLiquido,
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
