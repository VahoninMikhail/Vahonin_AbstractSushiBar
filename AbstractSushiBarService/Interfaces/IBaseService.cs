using AbstractSushiBarService.Attributies;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с заказами")]
    public interface IBaseService
    {
        [CustomMethod("Метод получения списка заказов")]
        List<ZakazViewModel> GetList();

        [CustomMethod("Метод создания заказа")]
        void CreateZakaz(ZakazBindingModel model);

        [CustomMethod("Метод передачи заказа в работу")]
        void TakeZakazInWork(ZakazBindingModel model);

        [CustomMethod("Метод передачи заказа на оплату")]
        void FinishZakaz(int id);

        [CustomMethod("Метод фиксирования оплаты по заказу")]
        void PayZakaz(int id);

        [CustomMethod("Метод пополнения ингредиентов на складе")]
        void PutIngredientOnStorage(StorageIngredientBindingModel model);
    }
}
