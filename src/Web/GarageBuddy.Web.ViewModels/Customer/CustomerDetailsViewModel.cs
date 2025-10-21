namespace GarageBuddy.Web.ViewModels.Customer
{
    using System.Collections.Generic;
    using Vehicle;

    public class CustomerDetailsViewModel : CustomerListViewModel
    {
        public ICollection<VehicleListViewModel> Vehicles { get; set; } = new List<VehicleListViewModel>();
    }
}
