using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractSushiBarService.ImplementationsList
{
    public class CookServiceList : ICookService
    {
        private InformationList source;

        public CookServiceList()
        {
            source = InformationList.GetInstance();
        }

        public List<CookViewModel> GetList()
        {
            List<CookViewModel> result = new List<CookViewModel>();
            for (int i = 0; i < source.Cooks.Count; ++i)
            {
                result.Add(new CookViewModel
                {
                    Id = source.Cooks[i].Id,
                    CookFIO = source.Cooks[i].CookFIO
                });
            }
            return result;
        }

        public CookViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Cooks.Count; ++i)
            {
                if (source.Cooks[i].Id == id)
                {
                    return new CookViewModel
                    {
                        Id = source.Cooks[i].Id,
                        CookFIO = source.Cooks[i].CookFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(CookBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Cooks.Count; ++i)
            {
                if (source.Cooks[i].Id > maxId)
                {
                    maxId = source.Cooks[i].Id;
                }
                if (source.Cooks[i].CookFIO == model.CookFIO)
                {
                    throw new Exception("Уже есть повар с таким ФИО");
                }
            }
            source.Cooks.Add(new Cook
            {
                Id = maxId + 1,
                CookFIO = model.CookFIO
            });
        }

        public void UpdElement(CookBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Cooks.Count; ++i)
            {
                if (source.Cooks[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Cooks[i].CookFIO == model.CookFIO &&
                    source.Cooks[i].Id != model.Id)
                {
                    throw new Exception("Уже есть повар с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Cooks[index].CookFIO = model.CookFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Cooks.Count; ++i)
            {
                if (source.Cooks[i].Id == id)
                {
                    source.Cooks.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
