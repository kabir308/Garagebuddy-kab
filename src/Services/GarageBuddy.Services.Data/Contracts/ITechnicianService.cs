namespace GarageBuddy.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Technician;

    public interface ITechnicianService
    {
        public Task<ICollection<TechnicianServiceModel>> GetAllAsync();
    }
}
