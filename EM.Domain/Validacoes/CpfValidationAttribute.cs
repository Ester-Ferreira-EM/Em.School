using System.ComponentModel.DataAnnotations;

namespace EM.Domain.Validacoes
{
    public class CpfValidationAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string cpf = value.ToString();

            if (!ValidaCpf.CPFValidacao(cpf))
            {
                return new ValidationResult("CPF inválido");
            }

            return ValidationResult.Success;
        }
    }
}

