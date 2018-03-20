using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    public interface IBaseService
    {
        List<ZakazViewModel> GetList();

        void CreateZakaz(ZakazBindingModel model);

        void TakeZakazInWork(ZakazBindingModel model);

        void FinishZakaz(int id);

        void PayZakaz(int id);

        void PutIngredientOnStorage(StorageIngredientBindingModel model);
    }
}
