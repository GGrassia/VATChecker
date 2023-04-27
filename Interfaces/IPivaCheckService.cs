using VATChecker.WebAPI.Models;

namespace VATChecker.WebAPI.Interfaces
{
    /// <summary>
    /// Servizio usato per ottenere i dettagli di una partita iva
    /// </summary>
    public interface IPivaCheckService
    {
        /// <summary>
        /// Ottieni i dettagli di una partita iva
        /// </summary>
        /// <param name="input"> P.IVA compreso prefisso nazionale </param>
        Task<PivaDetails> GetDetails(string input);
    }
}
