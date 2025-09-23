using AutoMapper;
using SingkoFItnessWebApi.Dtos;
using SingkoFItnessWebApi.Models;

namespace SingkoFItnessWebApi.Profiles {
    public class ProgressLogProfile : Profile {
        public ProgressLogProfile() {
            CreateMap<ProgressLog, ProgressLogsReadDto>().ReverseMap();
            CreateMap<ProgressLogsCreateDto, ProgressLog>();
            CreateMap<ProgressLogsUpdateDto, ProgressLog>();
        }
    }
}
