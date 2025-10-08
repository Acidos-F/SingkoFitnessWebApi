using AutoMapper;
using SingkoFitnessWebApi.Dtos.Exercise;
using SingkoFItnessWebApi.Dtos;
using SingkoFItnessWebApi.Models;

namespace SingkoFitnessWebApi.Profiles {
    public class WorkoutProfile : Profile {
        public WorkoutProfile() {
            // Workout → WorkoutReadDto
            CreateMap<Workout, WorkoutReadDto>();

            // WorkoutCreateDto → Workout
            CreateMap<WorkoutCreateDto, Workout>();

            // WorkoutUpdateDto → Workout
            CreateMap<WorkoutUpdateDto, Workout>();

            // Exercise → ExerciseReadDto
            CreateMap<Exercise, ExerciseReadDto>();
        }
    }
}