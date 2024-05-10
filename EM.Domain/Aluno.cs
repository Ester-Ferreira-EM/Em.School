using EM.Domain.Enums;
using EM.Domain.Interface;
using EM.Domain.Validacoes;
using System.ComponentModel.DataAnnotations;

namespace EM.Domain
{
    public class Aluno : IEntidade
    {
        public int? Matricula { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; }
        private string? cpf = string.Empty;

        [CpfValidation]
        public string? CPF
        {
            get { return cpf; }
            set { cpf = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim(); }
        }

        [DataNascimentoValidation]
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public Cidade Cidade { get; set; }

        public Aluno() { }

        public Aluno(string nome, string cPF, DateTime dataNascimento, Sexo sexo, Cidade cidade)
        {
            Nome = nome;
            CPF = cPF;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Cidade = cidade;
        }

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => base.ToString();
    }
}
