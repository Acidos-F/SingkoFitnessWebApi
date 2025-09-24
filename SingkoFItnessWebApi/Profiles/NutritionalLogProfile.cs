using AutoMapper;
using SingkoFItnessWebApi.Dtos;
using SingkoFItnessWebApi.Dtos.NutrionalLogDto;
using SingkoFItnessWebApi.Models;

namespace SingkoFItnessWebApi.Profiles
{
    public class NutritionalLogProfile:Profile{
        public NutritionalLogProfile()
        {
            CreateMap<NutritionLog, NutritionLogReadDto>().ReverseMap();
            CreateMap<NutritionLogCreateDto, NutritionLog>();
            CreateMap<NutritionLogUpdateDto, NutritionLog>();
        }
    }

}
