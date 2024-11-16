using AngularApp1.Domain.Models;
using FluentValidation;

namespace AngularApp1.Application
{
    public class CalculoCdbInputValidator: AbstractValidator<CalculoCdbInput>
    {
        public CalculoCdbInputValidator()
        {
            RuleFor(c => c.ValorInicial)
                .GreaterThan(0)
                .WithMessage("Valor inicial deve ser maior que zero.");

            RuleFor(c => c.QtdeMeses)
                .GreaterThan(1)
                .WithMessage("Quantidade de meses deve ser maior que hum.");
        }
    }
}
