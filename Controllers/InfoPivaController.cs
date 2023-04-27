using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using VATChecker.WebAPI.Interfaces;
using VATChecker.WebAPI.Models;
using VATChecker.WebAPI.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VATChecker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoPivaController : ControllerBase
    {
        private readonly IPivaCheckService _pivaCheckService;

        public InfoPivaController(IPivaCheckService pivaCheckService)
        {
            _pivaCheckService = pivaCheckService;
        }

        // POST api/<InfoPivaController>
        // Controlla la validità della partita iva e restituisce valori associati in caso di p.iva valida.
        [HttpGet]
        public async Task<PivaDetails> GetInfo(string piva)
        {
            return await _pivaCheckService.GetDetails(piva);
        }
    }
}
