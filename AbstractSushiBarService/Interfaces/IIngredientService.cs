using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    public interface IIngredientService
    {
        List<IngredientViewModel> GetList();

        IngredientViewModel GetElement(int id);

        void AddElement(IngredientBindingModel model);

        void UpdElement(IngredientBindingModel model);

        void DelElement(int id);
    }
}
