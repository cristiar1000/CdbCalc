using AngularApp1.Domain.Models;
using AngularApp1.Domain.Services;
using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp1.Server.Controllers
{
    /// <summary>
    /// Operações com investimento CDB
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CdbController : ControllerBase
    {
        private readonly ICdbService _cdbService;

        public CdbController(ICdbService cdbService)
        {
            _cdbService = cdbService;
        }

        /// <summary>
        /// Obtem cálculo de investimento CDB
        /// </summary>
        /// <param name="calcularCdbInputModel">Dados de entrada para cálculo CDB</param>
        /// <returns>Investimento calculado bruto e líquido</returns>
        [HttpPost(Name = "GetCalculoCdb")]
        [ProducesResponseType(typeof(CalcularCdbOutputModel), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult Post(CalcularCdbInputModel calcularCdbInputModel)
        {
            try
            {
                var cdbCalculado = _cdbService.CalcularResgate(
                    new CalculoCdbInput
                    {
                        QtdeMeses = calcularCdbInputModel.QtdeMeses,
                        ValorInicial = calcularCdbInputModel.ValorInicial
                    });

                return Ok(new CalcularCdbOutputModel
                {
                    ResultadoBruto = cdbCalculado.ResultadoBruto,
                    ResultadoLiquido = cdbCalculado.ResultadoLiquido
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
