using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractSushiBarService.ImplementationsBD
{
    public class CookServiceBD : ICookService
    {
        private AbstractDbContext context;

        public CookServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<CookViewModel> GetList()
        {
            List<CookViewModel> result = context.Cooks
                .Select(rec => new CookViewModel
                {
                    Id = rec.Id,
                    CookFIO = rec.CookFIO
                })
                .ToList();
            return result;
        }

        public CookViewModel GetElement(int id)
        {
            Cook element = context.Cooks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CookViewModel
                {
                    Id = element.Id,
                    CookFIO = element.CookFIO
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CookBindingModel model)
        {
            Cook element = context.Cooks.FirstOrDefault(rec => rec.CookFIO == model.CookFIO);
            if (element != null)
            {
                throw new Exception("Уже есть повар с таким ФИО");
            }
            context.Cooks.Add(new Cook
            {
                CookFIO = model.CookFIO
            });
            context.SaveChanges();
        }

        public void UpdElement(CookBindingModel model)
        {
            Cook element = context.Cooks.FirstOrDefault(rec =>
                                        rec.CookFIO == model.CookFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть повар с таким ФИО");
            }
            element = context.Cooks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CookFIO = model.CookFIO;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Cook element = context.Cooks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Cooks.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}

