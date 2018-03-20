using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    public interface IVisitorService
    {
        List<VisitorViewModel> GetList();

        VisitorViewModel GetElement(int id);

        void AddElement(VisitorBindingModel model);

        void UpdElement(VisitorBindingModel model);

        void DelElement(int id);
    }
}
