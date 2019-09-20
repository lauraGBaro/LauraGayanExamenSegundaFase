using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Repository;
using API.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : BaseController
    {
        private readonly ILogger<Divisa> _loggerD;
        private readonly ILogger<Transaccion> _loggerT;

        public TransaccionesController(ILogger<TransaccionesController> logger, ILogger<Divisa> loggerD, ILogger<Transaccion> loggerT) : base(logger)
        {
            _loggerD = loggerD;
            _loggerT = loggerT;
        }

        [HttpGet("Transactions")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DameTransacciones(string sku)
        {
            Logger.LogDebug("'{0}' - Inicio", nameof(DameTransacciones));
            List<Transaccion> listaTransacciones = new List<Transaccion>();

            try
            {
                var jsonTransactions = await HelperLectura.LecturaTransacciones();

                if (jsonTransactions == null)
                {
                    using (var unitOfWork = new UnitOfWork(_context, _loggerD, _loggerT))
                    {
                        var datos = await unitOfWork.RepoTransaccion.DameListado();

                        Logger.LogInformation("Datos obtenidos correctamente");

                        return Ok(datos);
                    }
                }

                using (var unitOfWork = new UnitOfWork(_context, _loggerD, _loggerT))
                {
                    var datos = await unitOfWork.RepoTransaccion.DameListado();
                    unitOfWork.RepoTransaccion.BorraListado(datos);
                    unitOfWork.Save();

                    unitOfWork.RepoTransaccion.GrabaListado(jsonTransactions);
                    unitOfWork.Save();

                    Logger.LogInformation("Las transacciones se han guardado correctamente");

                    return Ok(jsonTransactions);
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"Ha ocurrido un error en '{nameof(DameTransacciones)}': {ex}");

            }
            return BadRequest();
        }

        
        [HttpGet("TransactionsName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DameTransaccionesNombre(string sku)
        {
            Logger.LogDebug("'{0}' - Inicio", nameof(DameTransaccionesNombre));
            List<Transaccion> listaTransacciones = new List<Transaccion>();

            try
            {
                var jsonTransactions = await HelperLectura.LecturaTransacciones();

                if (jsonTransactions == null)
                {
                    using (var unitOfWork = new UnitOfWork(_context, _loggerD, _loggerT))
                    {
                        var datos = await unitOfWork.RepoTransaccion.DameListado();

                        if (!"".Equals(sku))
                        {
                            listaTransacciones = datos.ToList();
                            listaTransacciones = listaTransacciones.Where(m => m.Sku == sku).ToList();
                            List<decimal> cantidades = HelperConversor.SumatorioTransacciones(listaTransacciones, sku);
                            //TODO Falta sacar las conversiones
                            var cantTotalEuros = HelperConversor.ConversionEuros(null, null, null, cantidades);
                        }

                        Logger.LogInformation("Datos obtenidos correctamente");
                        return Ok(listaTransacciones);
                    }
                }
                
                using (var unitOfWork = new UnitOfWork(_context, _loggerD, _loggerT))
                {
                    var datos = await unitOfWork.RepoTransaccion.DameListado();
                    unitOfWork.RepoTransaccion.BorraListado(datos);
                    unitOfWork.Save();


                    unitOfWork.RepoTransaccion.GrabaListado(jsonTransactions);
                    unitOfWork.Save();

                    Logger.LogInformation("Las transacciones se han guardado correctamente");

                    listaTransacciones = jsonTransactions.ToList();

                    if (!"".Equals(sku))
                    {
                        listaTransacciones = listaTransacciones.Where(m => m.Sku == sku).ToList();
                        List<decimal> cantidades = HelperConversor.SumatorioTransacciones(listaTransacciones, sku);
                        //TODO Falta sacar las conversiones
                        var cantTotalEuros = HelperConversor.ConversionEuros(null, null, null, cantidades);
                    }

                    return Ok(listaTransacciones);
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical($"Ha ocurrido un error en '{nameof(DameTransaccionesNombre)}': {ex}");
            }
            return BadRequest();
        }
    }
}
