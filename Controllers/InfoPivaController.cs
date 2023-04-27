using Microsoft.AspNetCore.Mvc;
using VATChecker.WebAPI.Interfaces;
using VATChecker.WebAPI.Models;

namespace VATChecker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // POST api/<InfoPivaController>
    public class InfoPivaController : ControllerBase
    {
        private readonly IPivaCheckService _pivaCheckService;

        public InfoPivaController(IPivaCheckService pivaCheckService)
        {
            _pivaCheckService = pivaCheckService;
        }

        // Controlla la validità della partita iva e restituisce valori associati in caso di p.iva valida.
        [HttpGet]
        public async Task<PivaDetails> GetInfo(string piva)
        {
            return await _pivaCheckService.GetDetails(piva);
        }
    }
}
