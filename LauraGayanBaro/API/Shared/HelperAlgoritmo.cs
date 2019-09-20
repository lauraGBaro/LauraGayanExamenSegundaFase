using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Shared
{
    public static class HelperAlgoritmo
    {
        public static List<Divisa> BuscarRecorridoDivisas(string origen, string destino, List<Divisa> listaBusquedaConversion, List<Divisa> listaInicialTodo)
        {
            List<Divisa> listaOrigen = listaInicialTodo.Where(m => m.From.Equals(origen)).ToList();
            List<Divisa> listaDestino = listaInicialTodo.Where(m => m.To.Equals(destino)).ToList();
            var finBusqueda = false;
            int i, k;

            for (i = 0; i < listaOrigen.Count && finBusqueda == false; i++)
            {
                for (k = 0; k < listaDestino.Count && finBusqueda == false; k++)
                {
                    if (listaOrigen[i].To.Equals(listaDestino[k].From))
                    {
                        listaBusquedaConversion.Add(listaOrigen[i]);
                        listaBusquedaConversion.Add(listaDestino[k]);
                        finBusqueda = true;
                    }
                }
            }

            if (!finBusqueda)
            {
                listaBusquedaConversion.Add(listaOrigen[0]);
                BuscarRecorridoDivisas(listaOrigen[0].To, destino, listaBusquedaConversion, listaInicialTodo);
            }

            return listaBusquedaConversion;
        }
    }
}
