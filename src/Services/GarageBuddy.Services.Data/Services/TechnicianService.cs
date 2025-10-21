namespace GarageBuddy.Services.Data.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Contracts;
    using GarageBuddy.Data.Common.Repositories;
    using GarageBuddy.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Technician;

    public class TechnicianService : BaseService<Technician, Guid>, ITechnicianService
    {
        private readonly IDeletableEntityRepository<Technician, Guid> technicianRepository;

        public TechnicianService(
            IDeletableEntityRepository<Technician, Guid> entityRepository,
            IMapper mapper)
            : base(entityRepository, mapper)
        {
            this.technicianRepository = entityRepository;
        }

        public async Task<ICollection<TechnicianServiceModel>> GetAllAsync()
        {
            var technicians = await this.technicianRepository.All()
                .Include(t => t.ApplicationUser)
                .Select(t => new TechnicianServiceModel
                {
                    Id = t.Id,
                    Name = t.ApplicationUser.UserName,
                })
                .ToListAsync();

            return technicians;
        }
    }
}
