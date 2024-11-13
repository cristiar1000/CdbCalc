using AngularApp1.Domain.Models;

namespace AngularApp1.Domain.Services
{
    public interface ICdbService
    {
        ResgateCdbCalculado CalcularResgate(decimal valorInicial, int qtdeMeses);
    }
}
