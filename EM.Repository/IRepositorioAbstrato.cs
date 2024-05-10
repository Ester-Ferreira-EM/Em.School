using EM.Domain.Interface;
using System.Linq.Expressions;

namespace EM.Repository
{
    public interface IRepositorioAbstrato<T> where T : IEntidade
    {
        public void Add(T objeto);

        public void Update(T objeto);

        public IEnumerable<T> GetAll();

        public IEnumerable<T> Get(Expression<Func<T, bool>> expression);

    }
}
