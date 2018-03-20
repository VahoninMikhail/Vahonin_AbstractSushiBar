using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractSushiBarService.ImplementationsList
{
    public class VisitorServiceList : IVisitorService
    {
        private InformationList source;

        public VisitorServiceList()
        {
            source = InformationList.GetInstance();
        }

        public List<VisitorViewModel> GetList()
        {
            List<VisitorViewModel> result = new List<VisitorViewModel>();
            for (int i = 0; i < source.Visitors.Count; ++i)
            {
                result.Add(new VisitorViewModel
                {
                    Id = source.Visitors[i].Id,
                    VisitorFIO = source.Visitors[i].VisitorFIO
                });
            }
            return result;
        }

        public VisitorViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Visitors.Count; ++i)
            {
                if (source.Visitors[i].Id == id)
                {
                    return new VisitorViewModel
                    {
                        Id = source.Visitors[i].Id,
                        VisitorFIO = source.Visitors[i].VisitorFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(VisitorBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Visitors.Count; ++i)
            {
                if (source.Visitors[i].Id > maxId)
                {
                    maxId = source.Visitors[i].Id;
                }
                if (source.Visitors[i].VisitorFIO == model.VisitorFIO)
                {
                    throw new Exception("Уже есть покупатель с таким ФИО");
                }
            }
            source.Visitors.Add(new Visitor
            {
                Id = maxId + 1,
                VisitorFIO = model.VisitorFIO
            });
        }

        public void UpdElement(VisitorBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Visitors.Count; ++i)
            {
                if (source.Visitors[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Visitors[i].VisitorFIO == model.VisitorFIO &&
                    source.Visitors[i].Id != model.Id)
                {
                    throw new Exception("Уже есть покупатель с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Visitors[index].VisitorFIO = model.VisitorFIO;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Visitors.Count; ++i)
            {
                if (source.Visitors[i].Id == id)
                {
                    source.Visitors.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
