﻿namespace GarageBuddy.Services.Data.Models.Customer
{
    using System;

    using AutoMapper;

    using Base;

    using GarageBuddy.Data.Models;

    using Mapping;

    public class CustomerServiceModel : BaseListServiceModel,
        IMapFrom<Customer>, IMapTo<Customer>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Address { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? CompanyName { get; set; }

        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        public Guid? ApplicationUserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<CustomerServiceModel, Customer>()
                .ForMember(d => d.Id,
                    opt => opt.Ignore());
        }
    }
}
