using AngularApp1.Domain.Models;
using AngularApp1.Domain.Services;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AngularApp1.Application.Tests
{
    [TestClass]
    public sealed class CdbServiceTests
    {
        private readonly ICdbService _cdbService;
        private readonly IOptions<ApplicationConfiguration> _optionsApplicationConfiguration;
        private readonly IValidator<CalculoCdbInput> _calculoCdbInputValidator;

        public CdbServiceTests()
        {
            var applicationConfiguration = new ApplicationConfiguration
            {
                Cdi = 0.009M,
                Tb = 1.08M,
                IrCdb = new List<FaixaIrCdb>
                {
                    new FaixaIrCdb
                    {
                        QtdeMaximaMeses = 6,
                        TaxaIr = 22.5M
                    },
                    new FaixaIrCdb
                    {
                        QtdeMaximaMeses = 12,
                        TaxaIr = 20.0M
                    },
                    new FaixaIrCdb
                    {
                        QtdeMaximaMeses = 24,
                        TaxaIr = 17.5M
                    },
                    new FaixaIrCdb
                    {
                        QtdeMaximaMeses = 9999999,
                        TaxaIr = 15.0M
                    }
                }
            };


            _optionsApplicationConfiguration = Options.Create(applicationConfiguration);
            _calculoCdbInputValidator = new CalculoCdbInputValidator();
            _cdbService = new CdbService(
                _optionsApplicationConfiguration, _calculoCdbInputValidator);
        }

        [TestMethod]
        [DataRow(1.0, 10)]
        [DataRow(0.1, 10)]
        public void CalcularResgate_EntradasValidas_Sucesso(
            double valorInicial, int qtdeMeses)
        {
            var resgateCdbCalculado = _cdbService.CalcularResgate(new CalculoCdbInput
            {
                QtdeMeses = qtdeMeses,
                ValorInicial = (decimal)valorInicial
            });

            Assert.IsNotNull(resgateCdbCalculado);
            Assert.IsTrue(resgateCdbCalculado.ResultadoLiquido > (decimal)valorInicial);
        }

        [TestMethod]
        [DataRow(0.0, 10)]
        [DataRow(-1.0, 10)]
        public void CalcularResgate_ValorInicialPositivo_Falha(
            double valorInicial, int qtdeMeses)
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _cdbService.CalcularResgate(new CalculoCdbInput
                {
                    QtdeMeses = qtdeMeses,
                    ValorInicial = (decimal)valorInicial
                }));

        }

        [TestMethod]
        [DataRow(1, 0)]
        [DataRow(1, -1)]
        public void CalcularResgate_PrazoMaiorQueHum_Falha(
            double valorInicial, int qtdeMeses)
        {
            Assert.ThrowsException<ArgumentException>(() =>
                _cdbService.CalcularResgate(new CalculoCdbInput
                {
                    QtdeMeses = qtdeMeses,
                    ValorInicial = (decimal)valorInicial
                }));

        }
    }
}
