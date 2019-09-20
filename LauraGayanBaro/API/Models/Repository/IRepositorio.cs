using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Models.Repository
{
    public interface IRepositorio<T> where T : class
    {
        Task<IEnumerable<T>> DameListado();
        void Modifica(T modificado);
        void Graba(T elemento);
        void GrabaListado(IEnumerable<T> elementos);
        void Borra(T elemento);
        void BorraListado(IEnumerable<T> elementos);
    }
}
