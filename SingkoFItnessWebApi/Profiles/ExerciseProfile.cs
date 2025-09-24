using AutoMapper;
using SingkoFitnessWebApi.Dtos.Exercise;
using SingkoFItnessWebApi.Models;

namespace SingkoFitnessWebApi.Profiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            // Entity → Read DTO
            CreateMap<Exercise, ExerciseReadDto>();

            // Create DTO → Entity
            CreateMap<ExerciseCreateDto, Exercise>();

            // Update DTO → Entity
            CreateMap<ExerciseUpdateDto, Exercise>();
        }
    }
}