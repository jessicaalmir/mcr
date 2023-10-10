using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using mcr.Data.DTO;
using mcr.Data.Models;

namespace mcr.API.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles(){
            CreateMap<UserRegistrationDto, AppUser>();
        }
    }
}