﻿using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using System;
using System.Web.Http;

namespace AbstractSushiBarRestApi.Controllers
{
    public class VisitorController : ApiController
    {
        private readonly IVisitorService _service;

        public VisitorController(IVisitorService service)
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

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(VisitorBindingModel model)
        {
            _service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(VisitorBindingModel model)
        {
            _service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(VisitorBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}
