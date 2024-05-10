using EM.Domain.Enums;
using EM.Domain.Interface;
using System.ComponentModel.DataAnnotations;

namespace EM.Domain
{
    public class Cidade : IEntidade
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public UF Uf { get; set; }

        public Cidade() { }
        public Cidade(string nome, UF uf)
        {
            Nome = nome;
            Uf = uf;
        }
        
        public Cidade(int id, string nome, UF uf)
        {
            Id = id;
            Nome = nome;
            Uf = uf;
        }

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => base.ToString();


    }
}
