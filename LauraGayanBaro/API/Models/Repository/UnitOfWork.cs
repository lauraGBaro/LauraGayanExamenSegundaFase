using Microsoft.Extensions.Logging;
using System;

namespace API.Models.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly MyDbContext _context;

        public RepositorioGenerico<Divisa> RepoDivisa;
        public RepositorioGenerico<Transaccion> RepoTransaccion;

        public UnitOfWork(MyDbContext contexto, ILogger<Divisa> loggerD, ILogger<Transaccion> loggerT)
        {
            _context = contexto;
            RepoDivisa = new RepositorioGenerico<Divisa>(_context, loggerD);
            RepoTransaccion = new RepositorioGenerico<Transaccion>(_context, loggerT);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
