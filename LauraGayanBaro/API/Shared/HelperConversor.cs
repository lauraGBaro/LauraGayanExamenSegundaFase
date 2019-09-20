using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Shared
{
    public static class HelperConversor
    {
        public static List<decimal> SumatorioTransacciones(List<Transaccion> lista, string sku)
        {

            if (!"".Equals(sku))
            {
                lista = lista.Where(m => sku.Equals(m.Sku)).ToList();
            }

            List<decimal> cantidades = new List<decimal>
            {
                lista.Where(m => "EUR".Equals(m.Currency)).Sum(m => m.Amount),
                lista.Where(m => "USD".Equals(m.Currency)).Sum(m => m.Amount),
                lista.Where(m => "AUD".Equals(m.Currency)).Sum(m => m.Amount),
                lista.Where(m => "CAD".Equals(m.Currency)).Sum(m => m.Amount)
            };

            return cantidades;
        }

        public static decimal ConversionEuros(List<Divisa> rateUsdEur, List<Divisa> rateAudEur, List<Divisa> rateCadEur, List<decimal> cantidades)
        {
            var cantTotalEuros = cantidades[0];
            cantTotalEuros += rateUsdEur.Select(m => m.Rate).Aggregate(1m, (anterior, actual) => anterior * actual) * cantidades[1];
            cantTotalEuros += rateAudEur.Select(m => m.Rate).Aggregate(1m, (anterior, actual) => anterior * actual) * cantidades[2];
            cantTotalEuros += rateCadEur.Select(m => m.Rate).Aggregate(1m, (anterior, actual) => anterior * actual) * cantidades[3];
           
            return cantTotalEuros;
        }
    }
}
