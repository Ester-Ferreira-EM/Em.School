using EM.Domain;
using EM.Domain.Enums;
using EM.Domain.ExtensionMethods;
using EM.Repository.ConexaoBancoDeDados;
using System.Data.Common;
using System.Linq.Expressions;

namespace EM.Repository
{
    public class RepositorioCidade : IRepositorioAbstrato<Cidade>
    {
        public void Add(Cidade cidade)
        {
            using DbConnection connection = ConexaoBD.CrieConexao();
            using DbCommand comando = connection.CreateCommand();
            comando.CommandText = "INSERT INTO CIDADE (NOME, UF)" +
                "VALUES (@Nome, @Uf)";

            comando.Parameters.CreateParameter("@Nome", cidade.Nome);
            comando.Parameters.CreateParameter("@Uf", cidade.Uf.ToString());

            comando.ExecuteNonQuery();
        }

        public void Update(Cidade cidade)
        {
            using DbConnection connection = ConexaoBD.CrieConexao();
            using DbCommand comando = connection.CreateCommand();
            comando.CommandText = "UPDATE CIDADE SET NOME = @Nome, UF = @UF WHERE ID = @Id";

            comando.Parameters.CreateParameter("@Nome", cidade.Nome);
            comando.Parameters.CreateParameter("@UF", cidade.Uf.ToString());
            comando.Parameters.CreateParameter("@Id", cidade.Id);

            comando.ExecuteNonQuery();
        }


        public IEnumerable<Cidade> GetAll()
        {
            using DbConnection connection = ConexaoBD.CrieConexao();
            using DbCommand comando = connection.CreateCommand();

            List<Cidade> cidades = [];
            comando.CommandText = "SELECT ID, NOME, UF FROM CIDADE";

            DbDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["ID"]);
                string nome = reader.GetString(1);
                UF uf = (UF)Enum.Parse(typeof(UF), reader.GetString(2));

                Cidade cidade = new(id, nome, uf);
                cidades.Add(cidade);
            }
            return cidades;
        }
        public IEnumerable<Cidade> Get(Expression<Func<Cidade, bool>> predicate) => GetAll().Where(predicate.Compile());

    }
}
