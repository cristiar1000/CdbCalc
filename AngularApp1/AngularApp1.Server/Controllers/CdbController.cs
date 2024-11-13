using AngularApp1.Domain.Services;
using AngularApp1.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CdbController : ControllerBase
    {
        private readonly ILogger<CdbController> _logger;
        private readonly ICdbService _cdbService;

        public CdbController(ILogger<CdbController> logger,
            ICdbService cdbService)
        {
            _logger = logger;
            _cdbService = cdbService;
        }

        [HttpPost(Name = "GetCalculoCdb")]
        public CalcularCdbOutputModel CalcularCdb(CalcularCdbInputModel calcularCdbInputModel)
        {
            var cdbCalculado = _cdbService.CalcularResgate(
                calcularCdbInputModel.ValorInicial, calcularCdbInputModel.QtdeMeses);

            return new CalcularCdbOutputModel
            {
                ResultadoBruto = cdbCalculado.ResultadoBruto,
                ResultadoLiquido = cdbCalculado.ResultadoLiquido
            };
        }
    }
}
