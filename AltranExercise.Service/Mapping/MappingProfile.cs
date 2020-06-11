using AltranExercise.Data.Entities;
using AltranExercise.Service.DTOs;
using AutoMapper;

namespace AltranExercise.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClientDto, Client>();
            CreateMap<Client, ClientDto>();
            CreateMap<PolicyDto, Policy>();
            CreateMap<Policy, PolicyDto>();
        }
    }
}
