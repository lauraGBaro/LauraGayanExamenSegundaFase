using API.Models;
using API.Models.Repository;
using API.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisasController : BaseController
    {
        private readonly ILogger<Divisa> _loggerD;
        private readonly ILogger<Transaccion> _loggerT;

        public DivisasController(ILogger<DivisasController> logger, ILogger<Divisa> loggerD, ILogger<Transaccion> loggerT) : base(logger)
        {
            _loggerD = loggerD;
            _loggerT = loggerT;
        }

        [HttpGet("Rates")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DameDivisas()
        {
            Logger.LogDebug("'{0}' - Inicio", nameof(DameDivisas));
            
            try
            {
                var jsonRates = await HelperLectura.LecturaDivisas();

                if (jsonRates == null)
                {
                    using (var unitOfWork = new UnitOfWork(_context, _loggerD, _loggerT))
                    {
                        var datos = await unitOfWork.RepoDivisa.DameListado();
                        
                        Logger.LogInformation("Datos obtenidos correctamente");

                        return Ok(datos);
                    }
                }
                
                using (var unitOfWork = new UnitOfWork(_context, _loggerD, _loggerT))
                {
                    var datos = await unitOfWork.RepoDivisa.DameListado();
                    unitOfWork.RepoDivisa.BorraListado(datos);
                    unitOfWork.Save();
                    
                    List<string> monedas = new List<string> { "EUR", "USD", "AUD", "CAD" };
                    List<Divisa> listaDivisas = jsonRates.ToList();
                    List<Divisa> listaBusquedaConversion = new List<Divisa>();
                    var conversionDirecta = false;
                    
                    for (int i = 0; i < monedas.Count; i++)
                    {
                        conversionDirecta = false;
                        for (int k = 0; k < monedas.Count; k++)
                        {
                            if (monedas[i] != monedas[k])
                            {
                                conversionDirecta = listaDivisas.Where(m => m.From == monedas[i] && m.To == monedas[k]).ToList().Count == 1;
                                if (!conversionDirecta)
                                {
                                    listaDivisas.AddRange(HelperAlgoritmo.BuscarRecorridoDivisas(monedas[i], monedas[k], listaBusquedaConversion, listaDivisas));
                                }
                            }
                        }
                    }
                    
                    unitOfWork.RepoDivisa.GrabaListado(listaDivisas);
                    unitOfWork.Save();

                    Logger.LogInformation("Las divisas se han guardado correctamente");

                    return Ok(jsonRates);
                }

            }
            catch (Exception ex)
            {
                Logger.LogCritical($"Ha ocurrido un error en '{nameof(DameDivisas)}': {ex}");
               
            }
            return BadRequest();
        }
    }
}
