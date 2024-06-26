﻿using EM.Domain;
using EM.Domain.Interface;

namespace EM.Repository
{
    public interface IRepositorioAluno<T> where T : IEntidade
    {
        public Aluno GetByMatricula(int matricula);

        public IEnumerable<T> GetByContendoNoNome(string parteDoNome);

        public void Remove(T objeto);
    }
}
