using Api.Domain;

namespace Api.Presentation.ViewModels
{
    public class CompanyResponseDto
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string RegistrationNumber { get; set; }

        public CompanyResponseDto(Company company)
        {
            Name = company.Name;
            Address = company.Address;
            RegistrationNumber = company.RegistrationNumber;
        }
    }
}
