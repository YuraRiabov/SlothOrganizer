﻿using AutoMapper;
using SlothOrganizer.Contracts.DTO.Tasks.Dashboard;
using SlothOrganizer.Domain.Entities;

namespace SlothOrganizer.Services.Tasks
{
    internal class DashboardMappingProfile : Profile
    {
        public DashboardMappingProfile()
        {
            CreateMap<NewDashboardDto, Dashboard>();
            CreateMap<Dashboard, DashboardDto>();
        }
    }
}
