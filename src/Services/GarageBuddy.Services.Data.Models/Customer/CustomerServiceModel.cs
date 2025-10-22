namespace GarageBuddy.Services.Data.Models.Customer
{
    using GarageBuddy.Data.Models;
    using GarageBuddy.Services.Mapping;

    public class CustomerServiceModel : IMapFrom<Customer>
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? CompanyName { get; set; }
    }
}
