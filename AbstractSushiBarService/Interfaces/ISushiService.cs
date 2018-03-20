using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    public interface ISushiService
    {
        List<SushiViewModel> GetList();

        SushiViewModel GetElement(int id);

        void AddElement(SushiBindingModel model);

        void UpdElement(SushiBindingModel model);

        void DelElement(int id);
    }
}
