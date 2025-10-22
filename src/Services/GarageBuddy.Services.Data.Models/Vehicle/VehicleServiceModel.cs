namespace GarageBuddy.Services.Data.Models.Vehicle
{
    using GarageBuddy.Data.Models.Vehicle;
    using GarageBuddy.Services.Mapping;

    public class VehicleServiceModel : IMapFrom<Vehicle>
    {
        public Guid Id { get; set; }

        public string Make { get; set; } = null!;

        public string Model { get; set; } = null!;
    }
}
