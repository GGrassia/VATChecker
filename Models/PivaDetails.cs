namespace VATChecker.WebAPI.Models
{
    public class PivaDetails
    {
        public bool IsValid { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }
}
