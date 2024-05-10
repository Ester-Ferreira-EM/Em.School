using EM.Domain;
using EM.Domain.Enums;
using EM.Domain.ExtensionMethods;
using System.Data.Common;
using System.Linq.Expressions;

namespace EM.Repository
{
    public class RepositorioAluno : IRepositorioAbstrato<Aluno>, IRepositorioAluno<Aluno>
    {
        public IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate) => GetAll().Where(predicate.Compile());

        public void Add(Aluno aluno)
        {
            using DbConnection connection = ConexaoBancoDeDados.ConexaoBD.CrieConexao();
            using DbCommand comando = connection.CreateCommand();

            comando.CommandText = "INSERT INTO ALUNOS (NOME, CPF, DATANASCIMENTO, SEXO, CIDADE) " +
                             "VALUES (@NOME, @CPF, @DATANASCIMENTO, @SEXO, @CIDADE)";

            comando.Parameters.CreateParameter("@Nome", aluno.Nome);
            comando.Parameters.CreateParameter("@CPF", aluno.CPF);
            comando.Parameters.CreateParameter("@DataNascimento", aluno.DataNascimento);
            comando.Parameters.CreateParameter("@Sexo", aluno.Sexo);
            comando.Parameters.CreateParameter("@CIDADE", aluno.Cidade.Id);

            comando.ExecuteNonQuery();
        }

        public void Update(Aluno aluno)
        {
            using DbConnection connection = ConexaoBancoDeDados.ConexaoBD.CrieConexao();
            using DbCommand comando = connection.CreateCommand();
            comando.CommandText = "UPDATE ALUNOS " +
                "SET NOME = @Nome, " +
                "CPF = @CPF, " +
                "DATANASCIMENTO = @DataNascimento, " +
                "SEXO = @Sexo, " +
                "CIDADE = @Cidade " +
                "WHERE MATRICULA = @Matricula";

            comando.Parameters.CreateParameter("@Nome", aluno.Nome);
            comando.Parameters.CreateParameter("@CPF", aluno.CPF);
            comando.Parameters.CreateParameter("@DataNascimento", aluno.DataNascimento);
            comando.Parameters.CreateParameter("@Sexo", aluno.Sexo);
            comando.Parameters.CreateParameter("@Matricula", aluno.Matricula);
            comando.Parameters.CreateParameter("@Cidade", aluno.Cidade.Id);

            comando.ExecuteNonQuery();
        }

        public IEnumerable<Aluno> GetAll()
        {
            List<Aluno> alunos = [];

            using (DbConnection connection = ConexaoBancoDeDados.ConexaoBD.CrieConexao())
            {
                using DbCommand comando = connection.CreateCommand();
                comando.CommandText = @"
                SELECT A.matricula, A.nome, A.sexo, A.dataNascimento, A.CPF, C.Nome AS NOMECIDADE, C.UF AS UFCIDADE
                FROM Alunos A
                INNER JOIN Cidade C ON A.cidade = C.ID";

                DbDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cidade cidade = new Cidade(
                        nome: reader.GetString(reader.GetOrdinal("NOMECIDADE")),
                        uf: (UF)Enum.Parse(typeof(UF), reader["UFCIDADE"].ToString())
                    );

                    Aluno aluno = new()
                    {
                        Matricula = Convert.ToInt32(reader["matricula"]),
                        Sexo = (Sexo)Convert.ToInt32(reader["sexo"]),
                        Nome = reader["nome"].ToString(),
                        CPF = reader["CPF"].ToString(),
                        DataNascimento = Convert.ToDateTime(reader["dataNascimento"]),
                        Cidade = cidade
                    };
                    alunos.Add(aluno);
                }
            }
            return alunos;
        }

        public void Remove(Aluno aluno)
        {
            using DbConnection connection = ConexaoBancoDeDados.ConexaoBD.CrieConexao();
            using DbCommand comando = connection.CreateCommand();
            comando.CommandText = "DELETE FROM ALUNOS WHERE MATRICULA = @Matricula";

            comando.Parameters.CreateParameter("@Matricula", aluno.Matricula);

            comando.ExecuteNonQuery();
        }

        public Aluno GetByMatricula(int matricula) => GetAll().First(c => c.Matricula == matricula);

        public IEnumerable<Aluno> GetByContendoNoNome(string parteDoNome) => GetAll().
            Where(a => a.Nome.Contains(parteDoNome, StringComparison.OrdinalIgnoreCase));

    }
}
