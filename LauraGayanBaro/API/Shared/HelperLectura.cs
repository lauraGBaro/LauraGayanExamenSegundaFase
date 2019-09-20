using API.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Shared
{
    public static class HelperLectura
    {
        public static string UrlRates, UrlTransactions;

        public static async Task<IEnumerable<Divisa>> LecturaDivisas()
        {
            using (HttpClient client = new HttpClient())
            {
                var resultado = await client.GetAsync(UrlRates);
                if (resultado.IsSuccessStatusCode)
                {
                    var texto = await resultado.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Divisa>>(texto);
                }
                return null;
            }
        }

        public static async Task<IEnumerable<Transaccion>> LecturaTransacciones()
        {
            using (HttpClient client = new HttpClient())
            {
                var resultado = await client.GetAsync(UrlTransactions);

                if (resultado.IsSuccessStatusCode)
                {
                    var texto = await resultado.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Transaccion>>(texto);
                }

                return null;
            }
        }
    }
}
