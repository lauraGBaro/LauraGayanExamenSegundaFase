using API.Shared;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pruebas
{
    public class PruebasConexion
    {
        [Fact]
        public async Task TestLecturaDivisasUrl()
        {
            HelperLectura.UrlRates = "http://quiet-stone-2094.herokuapp.com/rates.json";
            var lista = await HelperLectura.LecturaDivisas();
            Assert.Equal(6, lista.Count());
        }

        [Fact]
        public async Task TestLecturaTransaccionesUrl()
        {
            HelperLectura.UrlTransactions = "http://quiet-stone-2094.herokuapp.com/transactions.json";
            var lista = await HelperLectura.LecturaTransacciones();
            Assert.True(lista.Any());
        }


        [Fact]
        public async Task TestObtenerTransaccionesProductoUrl()
        {
            HelperLectura.UrlTransactions = "http://quiet-stone-2094.herokuapp.com/transactions.json";
            var lista = await HelperLectura.LecturaTransacciones();
            var item = lista.FirstOrDefault();
            var listaFiltrada = lista.Where(m => m.Sku.Equals(item.Sku));
            Assert.True(listaFiltrada.Any());
        }

        [Fact]
        public async Task TestLecturaTransaccionesUrlNotFound()
        {
            HelperLectura.UrlTransactions = "http://quiet-stone-2094.herokuapp.com/transactions.j";
            var lista = await HelperLectura.LecturaTransacciones();
            Assert.True(lista is null);
        }


    }
}
