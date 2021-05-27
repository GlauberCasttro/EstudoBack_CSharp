using System;
using AutoMapper;
using System.Collections.Generic;
using System.Text;
using DEMO_DDD.DOMAIN.Entidades;
using DEMO_DDD.APPLICATION.ViewModels;

namespace DEMO_DDD.APPLICATION.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Cliente, ClienteViewModel>();
        }
    }
}
