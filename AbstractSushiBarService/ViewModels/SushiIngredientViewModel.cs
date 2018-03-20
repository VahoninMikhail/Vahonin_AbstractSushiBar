namespace AbstractSushiBarService.ViewModels
{
    public class SushiIngredientViewModel
    {
        public int Id { get; set; }

        public int SushiId { get; set; }

        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public int Count { get; set; }
    }
}
