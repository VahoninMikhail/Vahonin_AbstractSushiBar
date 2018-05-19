using AbstractSushiBarService.Attributies;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с поварами")]
    public interface ICookService
    {
        [CustomMethod("Метод получения списка поваров")]
        List<CookViewModel> GetList();

        [CustomMethod("Метод получения повара по id")]
        CookViewModel GetElement(int id);

        [CustomMethod("Метод добавления повара")]
        void AddElement(CookBindingModel model);

        [CustomMethod("Метод изменения данных по повару")]
        void UpdElement(CookBindingModel model);

        [CustomMethod("Метод удаления повара")]
        void DelElement(int id);
    }
}
