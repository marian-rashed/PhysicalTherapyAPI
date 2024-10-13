using AutoMapper;
using PhysicalTherapyAPI.DTOs;
using PhysicalTherapyAPI.Models;

namespace PhysicalTherapyAPI.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Exercise, ExerciseDTO>().ReverseMap();
        }
    }
}
