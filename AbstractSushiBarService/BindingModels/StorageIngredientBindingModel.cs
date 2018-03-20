namespace AbstractSushiBarService.BindingModels
{
    public class StorageIngredientBindingModel
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }
    }
}
