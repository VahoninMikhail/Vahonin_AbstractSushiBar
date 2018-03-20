using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractSushiBarService.ImplementationsList
{
    public class BaseServiceList : IBaseService
    {
        private InformationList source;

        public BaseServiceList()
        {
            source = InformationList.GetInstance();
        }

        public List<ZakazViewModel> GetList()
        {
            List<ZakazViewModel> result = source.Zakazs
                .Select(rec => new ZakazViewModel
                {
                    Id = rec.Id,
                    VisitorId = rec.VisitorId,
                    SushiId = rec.SushiId,
                    CookId = rec.CookId,
                    DateCreate = rec.DateCreate.ToLongDateString(),
                    DateImplement = rec.DateImplement?.ToLongDateString(),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    VisitorFIO = source.Visitors
                                    .FirstOrDefault(recV => recV.Id == rec.VisitorId)?.VisitorFIO,
                    SushiName = source.Sushis
                                    .FirstOrDefault(recS => recS.Id == rec.SushiId)?.SushiName,
                    CookName = source.Cooks
                                    .FirstOrDefault(recC => recC.Id == rec.CookId)?.CookFIO
                })
                .ToList();
            return result;
        }

        public void CreateZakaz(ZakazBindingModel model)
        {
            int maxId = source.Zakazs.Count > 0 ? source.Zakazs.Max(rec => rec.Id) : 0;
            source.Zakazs.Add(new Zakaz
            {
                Id = maxId + 1,
                VisitorId = model.VisitorId,
                SushiId = model.SushiId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ZakazStatus.Принят
            });
        }

        public void TakeZakazInWork(ZakazBindingModel model)
        {
            Zakaz element = source.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            var sushiIngredients = source.SushiIngredients.Where(rec => rec.SushiId == element.SushiId);
            foreach (var sushiIngredient in sushiIngredients)
            {
                int countOnStorages = source.StorageIngredients
                                            .Where(rec => rec.IngredientId == sushiIngredient.IngredientId)
                                            .Sum(rec => rec.Count);
                if (countOnStorages < sushiIngredient.Count * element.Count)
                {
                    var ingredientName = source.Ingredients
                                    .FirstOrDefault(rec => rec.Id == sushiIngredient.IngredientId);
                    throw new Exception("Не достаточно ингредиента " + ingredientName?.IngredientName +
                        ", требуется " + sushiIngredient.Count + ", в наличии " + countOnStorages);
                }
            }
            foreach (var sushiIngredient in sushiIngredients)
            {
                int countOnStorages = sushiIngredient.Count * element.Count;
                var storageIngredients = source.StorageIngredients
                                            .Where(rec => rec.IngredientId == sushiIngredient.IngredientId);
                foreach (var storageIngredient in storageIngredients)
                {
                    if (storageIngredient.Count >= countOnStorages)
                    {
                        storageIngredient.Count -= countOnStorages;
                        break;
                    }
                    else
                    {
                        countOnStorages -= storageIngredient.Count;
                        storageIngredient.Count = 0;
                    }
                }
            }
            element.CookId = model.CookId;
            element.DateImplement = DateTime.Now;
            element.Status = ZakazStatus.Выполняется;
        }

        public void FinishZakaz(int id)
        {
            Zakaz element = source.Zakazs.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ZakazStatus.Готов;
        }

        public void PayZakaz(int id)
        {
            Zakaz element = source.Zakazs.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ZakazStatus.Оплачен;
        }

        public void PutIngredientOnStorage(StorageIngredientBindingModel model)
        {
            StorageIngredient element = source.StorageIngredients
                                                .FirstOrDefault(rec => rec.StorageId == model.StorageId &&
                                                                    rec.IngredientId == model.IngredientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                int maxId = source.StorageIngredients.Count > 0 ? source.StorageIngredients.Max(rec => rec.Id) : 0;
                source.StorageIngredients.Add(new StorageIngredient
                {
                    Id = ++maxId,
                    StorageId = model.StorageId,
                    IngredientId = model.IngredientId,
                    Count = model.Count
                });
            }
        }
    }
}
