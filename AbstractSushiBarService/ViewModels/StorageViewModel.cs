using System.Collections.Generic;

namespace AbstractSushiBarService.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }

        public string StorageName { get; set; }

        public List<StorageIngredientViewModel> StorageIngredients { get; set; }
    }
}
