﻿namespace AbstractSushiBarModel
{
    public class StorageIngredient
    {
        public int Id { get; set; }

        public int StorageId { get; set; }

        public int IngredientId { get; set; }

        public int Count { get; set; }

        public virtual Storage Storage { get; set; }

        public virtual Ingredient Ingredient { get; set; }
    }
}
