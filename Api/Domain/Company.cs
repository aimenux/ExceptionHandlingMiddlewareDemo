namespace Api.Domain
{
    public class Company
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string RegistrationNumber { get; set; }

        public CompanyStatus CompanyStatus { get; set; }
    }
}
