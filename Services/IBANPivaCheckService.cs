using HtmlAgilityPack;
using VATChecker.WebAPI.Interfaces;
using VATChecker.WebAPI.Models;

namespace VATChecker.WebAPI.Services
{
    public class IBANPivaCheckService : IPivaCheckService
    {
        // Per fare una sola richiesta POST
        private HtmlDocument Doc { get; set; }

        public async Task<PivaDetails> GetDetails(string input)
        {
            if (string.IsNullOrEmpty(input) | !await ValidityCheck(input))
            {
                return new PivaDetails { IsValid = false };
            }

            var piva = new PivaDetails
            {
                IsValid = true,
                CompanyName = GetTextFromTable(2),
                Address = GetTextFromTable(3),
                City = GetTextFromTable(4),
                Zip = GetTextFromTable(5),
                CountryName = GetTextFromTable(6),
                CountryCode = GetTextFromTable(7)
            };
            return piva;
        }

        // Per cambiare XPath guardare l'HTML dalla risposta
        private string GetTextFromTable(int row)
        {
            return Doc.DocumentNode.SelectSingleNode($"//table[1]//tr[{row}]//td[2]").InnerText;
        }

        /// <summary>
        /// Metodo per il controllo della partita iva, setta Doc per l'uso successivo.
        /// </summary>
        /// <param name="input">Partita iva da controllare.</param>
        private async Task<bool> ValidityCheck(string input)
        {
            using var httpClient = new HttpClient();

            // Dizionario per formattare i dati necessari per la richiesta POST a iban.com
            var formData = new Dictionary<string, string>
            {
                { "vat_id", input }
            };
            var body = new FormUrlEncodedContent(formData);
            using var response = await httpClient.PostAsync("https://www.iban.com/vat-checker", body);
            string htmlText = await response.Content.ReadAsStringAsync();
            var document = new HtmlDocument();
            document.LoadHtml(htmlText);
            Doc = document;

            // parsing dell'HTML per vedere se la Piva è valida
            var table = Doc.DocumentNode.SelectSingleNode("//table//strong[contains(text(), 'VALID')]/text()").InnerText;
            return table != null && table == "VALID";
        }
    }
}
