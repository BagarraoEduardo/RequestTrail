using System;
using AutoMapper;
using CallerApi.Domain;
using CallerApi.Domain.Base;
using CallerApi.Integration.Generated.CalledApi;

namespace CallerApi.Business.Mapper;

public class BusinessMapper : Profile
{
    public BusinessMapper()
    {

        CreateMap<(bool Success, string ErrorMessage), BaseResponse>()
            .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.Success))
            .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => src.ErrorMessage));


        CreateMap<(bool Success, string ErrorMessage, BaseResponseDTO Data), PostExampleResponse>()
            .ForMember(dest => dest.Success, opt => opt.MapFrom(src => src.Success))
            .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => src.ErrorMessage))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Data.Message));

        
        CreateMap<PostExampleRequest, BaseRequestDTO>();
    }
}
