using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using System;
using System.Web.Http;

namespace AbstractSushiBarRestApi.Controllers
{
    public class BaseController : ApiController
    {
        private readonly IBaseService _service;

        public BaseController(IBaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateZakaz(ZakazBindingModel model)
        {
            _service.CreateZakaz(model);
        }

        [HttpPost]
        public void TakeZakazInWork(ZakazBindingModel model)
        {
            _service.TakeZakazInWork(model);
        }

        [HttpPost]
        public void FinishZakaz(ZakazBindingModel model)
        {
            _service.FinishZakaz(model.Id);
        }

        [HttpPost]
        public void PayZakaz(ZakazBindingModel model)
        {
            _service.PayZakaz(model.Id);
        }

        [HttpPost]
        public void PutIngredientOnStorage(StorageIngredientBindingModel model)
        {
            _service.PutIngredientOnStorage(model);
        }
    }
}
