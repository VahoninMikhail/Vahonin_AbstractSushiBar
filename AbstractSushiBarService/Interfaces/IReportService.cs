using AbstractSushiBarService.Attributies;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    [CustomInterface("Интерфейс для работы с отчетами")]
    public interface IReportService
    {
        [CustomMethod("Метод сохранения списка суши в doc-файл")]
        void SaveSushiPrice(ReportBindingModel model);

        [CustomMethod("Метод получения списка складов с количеством ингредиентов на них")]
        List<StoragesLoadViewModel> GetStoragesLoad();

        [CustomMethod("Метод сохранения списка списка складов с количеством ингредиентов на них в xls-файл")]
        void SaveStoragesLoad(ReportBindingModel model);

        [CustomMethod("Метод получения списка заказов посетителей")]
        List<VisitorZakazsModel> GetVisitorZakazs(ReportBindingModel model);

        [CustomMethod("Метод сохранения списка заказов посетителей в pdf-файл")]
        void SaveVisitorZakazs(ReportBindingModel model);
    }
}
