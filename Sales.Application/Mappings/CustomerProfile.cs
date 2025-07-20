using AutoMapper;
using Sales.Application.DTOs;
using Sales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Application.Mappings
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() {
            CreateMap<Customer, CustomerDto>().ReverseMap();        
        }
    }
    
}
