using System.Collections.Generic;

namespace AbstractSushiBarService.ViewModels
{
    public class SushiViewModel
    {
        public int Id { get; set; }

        public string SushiName { get; set; }

        public decimal Price { get; set; }

        public List<SushiIngredientViewModel> SushiIngredients { get; set; }
    }
}
