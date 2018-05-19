using AbstractSushiBarService.Attributies;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с суши")]
    public interface ISushiService
    {
        [CustomMethod("Метод получения списка суши")]
        List<SushiViewModel> GetList();

        [CustomMethod("Метод получения суши по id")]
        SushiViewModel GetElement(int id);

        [CustomMethod("Метод добавления суши")]
        void AddElement(SushiBindingModel model);

        [CustomMethod("Метод изменения данных по суши")]
        void UpdElement(SushiBindingModel model);

        [CustomMethod("Метод удаления суши")]
        void DelElement(int id);
    }
}
