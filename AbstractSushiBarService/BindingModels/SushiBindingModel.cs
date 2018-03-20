using System.Collections.Generic;

namespace AbstractSushiBarService.BindingModels
{
    public class SushiBindingModel
    {
        public int Id { get; set; }

        public string SushiName { get; set; }

        public decimal Price { get; set; }

        public List<SushiIngredientBindingModel> SushiIngredients { get; set; }
    }
}
