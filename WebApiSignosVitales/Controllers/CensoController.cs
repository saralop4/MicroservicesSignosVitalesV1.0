using LogicaSignosVitales.Exceptions;
using LogicaSignosVitales.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApiSignosVitales.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    [Authorize]
    public class CensoController : ControllerBase
    {
        private readonly ICensoService _censoService;
        public CensoController(ICensoService censoService)
        {
            _censoService = censoService;

        }
       
        [HttpGet("MostrarCamas")]
        public async Task<IActionResult> MostrarCamas(int codigo_pabellon)
        {
            try
            {
                var pabellon = await _censoService.MostrarCamas(codigo_pabellon);                
                return Ok(new { pabellon });
            }
            catch (ValidationArgumentsEntityNullException)
            {
                return BadRequest(new { mensaje= "El pabellon no existe " });
            }
            catch (ValidationListaConValidacionNullReferenceException)
            {
                return BadRequest(new { mensaje = "Lista Nulla Invalida" });
            }
           

            
        }

    }
}
    
