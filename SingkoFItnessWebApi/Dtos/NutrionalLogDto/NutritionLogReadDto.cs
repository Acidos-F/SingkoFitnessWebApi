namespace SingkoFItnessWebApi.Dtos.NutrionalLogDto
{
    public class NutritionLogReadDto
    {
        public int UserId { get; set; }
        public int NutritionId { get; set; }
        public DateOnly Date { get; set; }
        public string MealType { get; set; }
        public int Calories { get; set; }
    }
}
