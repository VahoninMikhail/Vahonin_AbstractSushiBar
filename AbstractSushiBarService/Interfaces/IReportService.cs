using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System.Collections.Generic;

namespace AbstractSushiBarService.Interfaces
{
    public interface IReportService
    {
        void SaveSushiPrice(ReportBindingModel model);

        List<StoragesLoadViewModel> GetStoragesLoad();

        void SaveStoragesLoad(ReportBindingModel model);

        List<VisitorZakazsModel> GetVisitorZakazs(ReportBindingModel model);

        void SaveVisitorZakazs(ReportBindingModel model);
    }
}
