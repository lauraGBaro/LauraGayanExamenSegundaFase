using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Models.Repository
{
    public class RepositorioGenerico<T> : IRepositorio<T> where T : class, new()
    {
        private readonly DbSet<T> _table;
        private MyDbContext _context { get; }
        private readonly ILogger<T> _logger;

        public RepositorioGenerico(MyDbContext contexto, ILogger<T> logger)
        {
            _context = contexto;
            _table = _context.Set<T>();
            _logger = logger;
        }

        public void Borra(T elemento)
        {
            _table.Remove(elemento);
        }

        public void BorraListado(IEnumerable<T> elementos)
        {
            _table.RemoveRange(elementos);
        }

        public async Task<IEnumerable<T>> DameListado()
        {
            return await _table.ToListAsync();
        }

        public void Graba(T elemento)
        {
            try
            {
                _table.Add(elemento);
            }
            catch (NullReferenceException e)
            {
                _logger.LogCritical($"Ha ocurrido un error en '{nameof(Graba)}': {e}");
            }
        }

        public void GrabaListado(IEnumerable<T> elementos)
        {
            _table.AddRange(elementos);
        }

        public void Modifica(T modificado)
        {
            try
            {
                _table.Update(modificado);
            }
            catch (NullReferenceException e)
            {
                _logger.LogCritical($"Ha ocurrido un error en '{nameof(Modifica)}': {e}");
            }
        }
    }
}
