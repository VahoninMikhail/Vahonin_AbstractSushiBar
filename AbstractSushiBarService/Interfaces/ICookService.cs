using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    public interface ICookService
    {
        List<CookViewModel> GetList();

        CookViewModel GetElement(int id);

        void AddElement(CookBindingModel model);

        void UpdElement(CookBindingModel model);

        void DelElement(int id);
    }
}
