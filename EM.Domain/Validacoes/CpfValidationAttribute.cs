using System.ComponentModel.DataAnnotations;

namespace EM.Domain.Validacoes
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value == string.Empty)
            {
                return ValidationResult.Success;
            }

            string cpf = value.ToString();

            return !ValidaCpf.CPFValidacao(cpf) ? new ValidationResult("CPF inválido") : ValidationResult.Success;
        }
    }
}

