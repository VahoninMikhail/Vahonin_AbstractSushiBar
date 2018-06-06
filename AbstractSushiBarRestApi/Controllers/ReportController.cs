using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using System;
using System.Web.Http;

namespace AbstractSushiBarRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetStoragesLoad()
        {
            var list = _service.GetStoragesLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetVisitorZakazs(ReportBindingModel model)
        {
            var list = _service.GetVisitorZakazs(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SaveSushiPrice(ReportBindingModel model)
        {
            _service.SaveSushiPrice(model);
        }

        [HttpPost]
        public void SaveStoragesLoad(ReportBindingModel model)
        {
            _service.SaveStoragesLoad(model);
        }

        [HttpPost]
        public void SaveVisitorZakazs(ReportBindingModel model)
        {
            _service.SaveVisitorZakazs(model);
        }
    }
}
