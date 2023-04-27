using HtmlAgilityPack;
using VATChecker.WebAPI.Interfaces;
using VATChecker.WebAPI.Models;

namespace VATChecker.WebAPI.Services
{
    public class IBANPivaCheckService : IPivaCheckService
    {
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

        private string GetTextFromTable(int row)
        {
            return Doc.DocumentNode.SelectSingleNode($"//table[1]//tr[{row}]//td[2]").InnerText;
        }

        private async Task<bool> ValidityCheck(string input)
        {
            using var httpClient = new HttpClient();

            // dizionario per formattare i dati per la richiesta POST
            var formData = new Dictionary<string, string>
            {
                { "vat_id", input }
            };
            var body = new FormUrlEncodedContent(formData);
            using var response = await httpClient.PostAsync("https://www.iban.com/vat-checker", body);

            // read the response content as a string
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
