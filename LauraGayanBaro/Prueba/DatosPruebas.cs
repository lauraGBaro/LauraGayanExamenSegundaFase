using API.Models;
using System.Collections.Generic;

namespace Pruebas
{
    public static class DatosPruebas
    {
        public static readonly List<Transaccion> listaTransacciones = new List<Transaccion>
            {
                new Transaccion { Sku = "B7574",Amount = 33.4m , Currency = "AUD"},
                new Transaccion { Sku = "J5883",Amount = 31.5m , Currency = "CAD"},
                new Transaccion { Sku = "B7574",Amount = 22.7m , Currency = "USD"},
                new Transaccion { Sku = "B7574",Amount = 19.8m , Currency = "EUR"},
                new Transaccion { Sku = "F2602",Amount = 30.2m , Currency = "AUD"},
                new Transaccion { Sku = "B7574",Amount = 24.1m , Currency = "EUR"}
            };

        public static readonly List<Divisa> listaUsd = new List<Divisa> { new Divisa { From = "USD", To = "EUR", Rate = 0.68m } };

        public static readonly List<Divisa> listaCad = new List<Divisa> { new Divisa { From = "CAD", To = "EUR", Rate = 0.89m } };

        public static readonly List<Divisa> listaAud = new List<Divisa>{new Divisa { From = "AUD", To = "CAD", Rate = 0.93m },
                                                   new Divisa { From = "CAD", To = "EUR", Rate = 0.89m } };


        public static readonly List<Divisa> listaDivisas = new List<Divisa>
            {
                new Divisa
                {
                    From = "EUR",
                    To = "USD",
                    Rate = 1.46m
                },
                new Divisa
                {
                    From = "EUR",
                    To = "CAD",
                    Rate = 1.12m
                },
                new Divisa
                {
                    From = "USD",
                    To = "EUR",
                    Rate = 0.68m
                },
                new Divisa
                {
                    From = "CAD",
                    To = "EUR",
                    Rate = 0.89m
                },
                new Divisa
                {
                    From = "CAD",
                    To = "AUD",
                    Rate = 1.07m
                },
                new Divisa
                {
                    From = "AUD",
                    To = "CAD",
                    Rate = 0.93m
                }
            };
    }
}
