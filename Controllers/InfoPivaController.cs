using Microsoft.AspNetCore.Mvc;
using VATChecker.WebAPI.Interfaces;
using VATChecker.WebAPI.Models;

namespace VATChecker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // GET api/<InfoPiva>
    public class InfoPivaController : ControllerBase
    {
        private readonly IPivaCheckService _pivaCheckService;

        public InfoPivaController(IPivaCheckService pivaCheckService)
        {
            _pivaCheckService = pivaCheckService;
        }

        // Controlla la validità della partita iva e restituisce valori associati in caso di p.iva valida.
        // Potrebbe valere la pena una struct con singolo field per far serializzare in json gli errori in futuro
        [HttpGet]
        public async Task<ActionResult<PivaDetails>> GetInfo(string piva)
        {
            if (string.IsNullOrEmpty(piva))
            {
                return BadRequest("Il numero di P.iva è necessario");
            }

            return await _pivaCheckService.GetDetails(piva);
        }
    }
}
