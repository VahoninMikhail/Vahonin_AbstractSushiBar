namespace AbstractSushiBarModel
{
    public class SushiIngredient
    {
        public int Id { get; set; }

        public int SushiId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }

        public virtual Sushi Sushi { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
