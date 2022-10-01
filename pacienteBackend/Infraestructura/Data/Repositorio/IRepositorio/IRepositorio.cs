using System.Linq.Expressions;

namespace Infraestructura.Data.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T: class
    {
         Task<IEnumerable<T>> ObtenerTodos(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IQueryable<T>> orderBy = null,
            string incluirPropiedades = null

         );

         Task<T> ObtenerPrimero(
             Expression<Func<T, bool>> filtro = null,
             string incluirPropiedades = null

         );

         Task Agregar(T entidad);

         void Remover( T entidad);

    }
}