using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractSushiBarModel
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string IngredientName { get; set; }

        [ForeignKey("IngredientId")]
        public virtual List<SushiIngredient> SushiIngredients { get; set; }

        [ForeignKey("IngredientId")]
        public virtual List<StorageIngredient> StorageIngredients { get; set; }
    }
}
