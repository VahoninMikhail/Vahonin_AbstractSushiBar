using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
