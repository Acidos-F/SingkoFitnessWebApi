using System;
using System.ComponentModel.DataAnnotations;
namespace SingkoFItnessWebApi.Dtos {
    public class ProgressLogsReadDto {
        public int ProgressId { get; set; }
        public int UserId { get; set; }
        public DateOnly Date { get; set; }
        public decimal? Weight { get; set; }
        public decimal? BodyFatPercentage { get; set; }
        public decimal? MuscleMass { get; set; }
    }
}