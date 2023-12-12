using DataSignosVitales.Data;
using DataSignosVitales.DTOs;
using DataSignosVitales.Interfaces;
using LogicaSignosVitales.Exceptions;
using LogicaSignosVitales.Interfaces;
using LogicaSignosVitales.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace WebApiSignosVitales.Controllers
{

    [Route("api/v1.0/[controller]")]
    [ApiController]
    [Authorize]
    public class SignoVitalController : ControllerBase
    {
        private readonly INotaEnfermeriaService _notaEnfermeriaService;
        public SignoVitalController(INotaEnfermeriaService notaEnfermeriaService)
        {
            _notaEnfermeriaService = notaEnfermeriaService;
        }

        [HttpPost("CrearSignoVital")]
        public async Task<IActionResult> CrearSignoVital(NotaEnfermeriaDTOs notaEnfermeria)
        {
         
            try
            {
                await _notaEnfermeriaService.CrearSignoVital(notaEnfermeria);

                var message = "Signos vitales guardados exitosamente.";
                return Ok( new { mensaje = message });
            }
            catch (ValidationNumeroSignoVitalDbUpdateException )
            {
                return BadRequest(new { mensaje = "El numero del signo vital ya existe" });
            }
            catch (ValidationEstudioException)
            {
                return BadRequest(new { mensaje = "El Estudio es invalido" });
            }
            catch (ValidationCodigoEnfermeraExcepcion)
            {
                return BadRequest(new { mensaje = "El codigo de la enfermera no se encuentra registrada " });
            }

        }

        [HttpGet("ObtenerSignoVital")]
        public async Task<IActionResult> ObtenerSignoVital(string? numero_Signovital, string? nroevolucionrelacionado)
        {
            try
            {
                // Validar que solo uno de los dos parámetros esté presente
                if (!string.IsNullOrEmpty(numero_Signovital) && !string.IsNullOrEmpty(nroevolucionrelacionado))
                {
                    return BadRequest(new { mensaje = "Solo se permite uno de los dos parámetros: numero_Signovital o nroevolucionrelacionado" });                    
                }

                if (!string.IsNullOrEmpty(numero_Signovital))
                {
                    var result = await _notaEnfermeriaService.ObtenerSignoVital(numero_Signovital);
                    return Ok(result);
                }
                else if (!string.IsNullOrEmpty(nroevolucionrelacionado))
                {
                    var result = await _notaEnfermeriaService.ObtenerSignoVitalxEvolucion(nroevolucionrelacionado);
                    return Ok(result);
                }
                else
                {                                        
                    return BadRequest(new { mensaje = "Se debe proporcionar uno de los dos parámetros: numero_Signovital o nroevolucionrelacionado" });
                }
            }
            catch (ValidationNumeroSignoVitalRequeridoException)
            {
                return BadRequest(new { mensaje = "numero_SignoVital es obligatorio" });
            }
            catch (ValidationNroEvolucionSignoVitalRequeridoException)
            {
                return BadRequest(new { mensaje = "nroevolucionrelacionado es obligatorio" });
            }
            catch (ValidationEntityExisting)
            {
                return BadRequest(new { mensaje = "El objeto no existe" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }


        [HttpPut("ActualizarSignoVital")]
        public async Task<IActionResult> ActualizarSignoVital(string? numero_Signovital, string? nroEvolucionRelacionado, NotaEnfermeriaDTOs notaEnfermeria)
        {
            try
            {
                if (numero_Signovital != null)
                {
                    await _notaEnfermeriaService.ActualizarPorNumeroSignovital(numero_Signovital, notaEnfermeria);
                }
                else if (nroEvolucionRelacionado != null)
                {
                    await _notaEnfermeriaService.ActualizarPorNroEvolucionRelacionado(nroEvolucionRelacionado, notaEnfermeria);
                }
                else
                {
                    return BadRequest(new { mensaje = "Debe proporcionar al menos uno de los valores: numero_Signovital o nroEvolucionRelacionado." });
                }

                var message = $"Signos vitales se ha actualizado exitosamente.";
                return Ok(new { mensaje = message });
            }
            catch (ValidationEntityExisting)
            {
                return BadRequest(new { mensaje = "El registro a actualizar no existe." });
            }
        }

        [HttpGet("HistoricoSignoVital")]
        public async Task<IActionResult> MostrarHistorico(int estudio)
        {

            try
            {
                var historico = await _notaEnfermeriaService.MostarHistorico(estudio);
                return Ok(new { historico });
            }
            catch (ValidationArgumentsEntityNullException)
            {
                return BadRequest(new { mensaje = "El historico no existe " });
            }
            catch (ValidationListaConValidacionNullReferenceException)
            {
                return BadRequest(new { mensaje = "Lista Nulla Invalida" });
            }


        }

    }
}
