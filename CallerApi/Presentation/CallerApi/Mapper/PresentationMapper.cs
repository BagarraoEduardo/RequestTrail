using System;
using AutoMapper;
using CallerApi.Domain;
using CallerApi.Domain.Base;
using CallerApi.Dtos;
using CallerApi.Dtos.Base;

namespace CallerApi.Mapper;

public class PresentationMapper : Profile
{
    public PresentationMapper()
    {
        CreateMap<PostExampleRequestDto, PostExampleRequest>()
            .ReverseMap();
        CreateMap<PostExampleResponse, PostExampleResponseDto>()
            .ReverseMap();

    }
}
