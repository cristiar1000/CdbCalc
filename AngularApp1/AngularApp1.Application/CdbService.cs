using AngularApp1.Domain.Models;
using AngularApp1.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace AngularApp1.Application
{
    public class CdbService : ICdbService
    {
        private readonly ApplicationConfiguration _applicationConfiguration;
        private readonly IValidator<CalculoCdbInput> _calculoCdbInputValidator;

        public CdbService(IOptions<ApplicationConfiguration> applicationConfiguration,
            IValidator<CalculoCdbInput> calculoCdbInputValidator)
        {
            _applicationConfiguration = applicationConfiguration.Value;
            _calculoCdbInputValidator = calculoCdbInputValidator;
        }

        public ResgateCdbCalculado CalcularResgate(CalculoCdbInput calculoCdbInput)
        {
            var validacao = _calculoCdbInputValidator.Validate(calculoCdbInput);

            if (!validacao.IsValid)
            {
                throw new ArgumentException(validacao.ToString());
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
                irCdb => irCdb.QtdeMaximaMeses >= qtdeMeses)
                .OrderBy(irCdb => irCdb.QtdeMaximaMeses).First();

            var resultadoLiquido = rendimento - (rendimento * faixaIrCdb.TaxaIr / 100);
            return resultadoLiquido;
        }
    }
}
