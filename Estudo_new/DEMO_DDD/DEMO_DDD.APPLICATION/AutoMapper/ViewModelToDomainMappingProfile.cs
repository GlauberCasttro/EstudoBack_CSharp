using AutoMapper;
using DEMO_DDD.APPLICATION.ViewModels;
using DEMO_DDD.DOMAIN.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEMO_DDD.APPLICATION.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteViewModel, Cliente>();
        }
    }
}
