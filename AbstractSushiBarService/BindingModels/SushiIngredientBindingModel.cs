namespace AbstractSushiBarService.BindingModels
{
    public class SushiIngredientBindingModel
    {
        public int Id { get; set; }

        public int SushiId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }
    }
}
