using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyoCarsClients.Domain.DTOs.Response;
using ToyoCarsClients.Domain.Entities;

namespace ToyoCarsClients.Business.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponseDto>();
        }
    }
}
