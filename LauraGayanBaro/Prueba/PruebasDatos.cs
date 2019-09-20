using API.Models;
using API.Shared;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Pruebas
{
    public class PruebasDatos
    {
        [Fact]
        public void TestBuscarCaminosDivisasQueFaltan()
        {
            var origen = "EUR";
            var destino = "AUD";
            var hayConversionDirecta = DatosPruebas.listaDivisas.Where(m => m.From == origen && m.To == destino).ToList().Count() == 1;
            List<Divisa> listaFinalConversion = new List<Divisa>();

            if (hayConversionDirecta == false)
            {
                listaFinalConversion = HelperAlgoritmo.BuscarRecorridoDivisas(origen, destino, listaFinalConversion, DatosPruebas.listaDivisas);
                Assert.Equal(2, listaFinalConversion.Count());
            }
            else
            {
                listaFinalConversion.Add(DatosPruebas.listaDivisas.Where(m => m.From == origen && m.To == destino).FirstOrDefault());
                Assert.True(hayConversionDirecta);
                Assert.Single(listaFinalConversion);
            }
        }


        [Fact]
        public void TestConversionEuros()
        {
            List<decimal> cantidades = new List<decimal> { 12m, 24m, 67.97m, 9.5m };
            var totalEuros = HelperConversor.ConversionEuros(DatosPruebas.listaUsd, DatosPruebas.listaAud, DatosPruebas.listaCad, cantidades);
            
            Assert.Equal(93.033769m, totalEuros);
        }
  
   
        [Fact]
        public void TestSumaTotalEurosTransaccionesPorSku()
        {
            var cantidades = HelperConversor.SumatorioTransacciones(DatosPruebas.listaTransacciones, "B7574");
           
            var totalEuros = HelperConversor.ConversionEuros(DatosPruebas.listaUsd, DatosPruebas.listaAud, DatosPruebas.listaCad, cantidades);
            
            Assert.Equal(86.98118m, totalEuros);
        }


        [Fact]
        public void TestSumaTotalEurosTransacciones()
        {
            var cantidades = HelperConversor.SumatorioTransacciones(DatosPruebas.listaTransacciones, "");

            var totalEuros = HelperConversor.ConversionEuros(DatosPruebas.listaUsd, DatosPruebas.listaAud, DatosPruebas.listaCad, cantidades);
            
            Assert.Equal(140.01272m, totalEuros);
        }
    }
}
