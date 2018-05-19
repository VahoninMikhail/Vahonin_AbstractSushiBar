using AbstractSushiBarService.Attributies;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с письмами")]
    public interface IMessageInfoService
    {
        [CustomMethod("Метод получения списка писем")]
        List<MessageInfoViewModel> GetList();

        [CustomMethod("Метод получения письма по id")]
        MessageInfoViewModel GetElement(int id);

        [CustomMethod("Метод добавления письма")]
        void AddElement(MessageInfoBindingModel model);
    }
}
