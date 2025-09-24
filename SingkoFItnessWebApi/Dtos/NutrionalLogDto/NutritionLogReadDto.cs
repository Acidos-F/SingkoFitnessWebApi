namespace SingkoFItnessWebApi.Dtos.NutrionalLogDto
{
    public class NutritionLogReadDto
    {
        public int NutritionId { get; set; }
        public DateTime Date { get; set; }
        public string MealType { get; set; }
        public int Calories { get; set; }
    }
}
