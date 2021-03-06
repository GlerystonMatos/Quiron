﻿using AutoMapper;
using Quiron.Domain.Dto;
using Quiron.Domain.Entities;

namespace Quiron.Service.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Espaco, EspacoDto>()
                .ReverseMap();

            CreateMap<Estado, EstadoDto>().
                ForMember(dto => dto.Descricao, opt => opt.MapFrom(entity => entity.Descricao)).
                ReverseMap();
        }
    }
}