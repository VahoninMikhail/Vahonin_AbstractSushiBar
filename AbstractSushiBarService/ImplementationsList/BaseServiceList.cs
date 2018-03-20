using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;

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
            List<ZakazViewModel> result = new List<ZakazViewModel>();
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                string visitorFIO = string.Empty;
                for (int j = 0; j < source.Visitors.Count; ++j)
                {
                    if (source.Visitors[j].Id == source.Zakazs[i].VisitorId)
                    {
                        visitorFIO = source.Visitors[j].VisitorFIO;
                        break;
                    }
                }
                string sushiName = string.Empty;
                for (int j = 0; j < source.Sushis.Count; ++j)
                {
                    if (source.Sushis[j].Id == source.Zakazs[i].SushiId)
                    {
                        sushiName = source.Sushis[j].SushiName;
                        break;
                    }
                }
                string cookFIO = string.Empty;
                if (source.Zakazs[i].CookId.HasValue)
                {
                    for (int j = 0; j < source.Cooks.Count; ++j)
                    {
                        if (source.Cooks[j].Id == source.Zakazs[i].CookId.Value)
                        {
                            cookFIO = source.Cooks[j].CookFIO;
                            break;
                        }
                    }
                }
                result.Add(new ZakazViewModel
                {
                    Id = source.Zakazs[i].Id,
                    VisitorId = source.Zakazs[i].VisitorId,
                    VisitorFIO = visitorFIO,
                    SushiId = source.Zakazs[i].SushiId,
                    SushiName = sushiName,
                    CookId = source.Zakazs[i].CookId,
                    CookName = cookFIO,
                    Count = source.Zakazs[i].Count,
                    Sum = source.Zakazs[i].Sum,
                    DateCreate = source.Zakazs[i].DateCreate.ToLongDateString(),
                    DateImplement = source.Zakazs[i].DateImplement?.ToLongDateString(),
                    Status = source.Zakazs[i].Status.ToString()
                });
            }
            return result;
        }

        public void CreateZakaz(ZakazBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                if (source.Zakazs[i].Id > maxId)
                {
                    maxId = source.Visitors[i].Id;
                }
            }
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
            int index = -1;
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                if (source.Zakazs[i].Id == model.Id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            for (int i = 0; i < source.SushiIngredients.Count; ++i)
            {
                if (source.SushiIngredients[i].SushiId == source.Zakazs[index].SushiId)
                {
                    int countOnStorages = 0;
                    for (int j = 0; j < source.StorageIngredients.Count; ++j)
                    {
                        if (source.StorageIngredients[j].IngredientId == source.SushiIngredients[i].IngredientId)
                        {
                            countOnStorages += source.StorageIngredients[j].Count;
                        }
                    }
                    if (countOnStorages < source.SushiIngredients[i].Count * source.Zakazs[index].Count)
                    {
                        for (int j = 0; j < source.Ingredients.Count; ++j)
                        {
                            if (source.Ingredients[j].Id == source.SushiIngredients[i].IngredientId)
                            {
                                throw new Exception("Не достаточно ингредиента " + source.Ingredients[j].IngredientName +
                                    ", требуется " + source.SushiIngredients[i].Count + ", в наличии " + countOnStorages);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < source.SushiIngredients.Count; ++i)
            {
                if (source.SushiIngredients[i].SushiId == source.Zakazs[index].SushiId)
                {
                    int countOnStorages = source.SushiIngredients[i].Count * source.Zakazs[index].Count;
                    for (int j = 0; j < source.StorageIngredients.Count; ++j)
                    {
                        if (source.StorageIngredients[j].IngredientId == source.SushiIngredients[i].IngredientId)
                        {
                            if (source.StorageIngredients[j].Count >= countOnStorages)
                            {
                                source.StorageIngredients[j].Count -= countOnStorages;
                                break;
                            }
                            else
                            {
                                countOnStorages -= source.StorageIngredients[j].Count;
                                source.StorageIngredients[j].Count = 0;
                            }
                        }
                    }
                }
            }
            source.Zakazs[index].CookId = model.CookId;
            source.Zakazs[index].DateImplement = DateTime.Now;
            source.Zakazs[index].Status = ZakazStatus.Выполняется;
        }

        public void FinishZakaz(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                if (source.Visitors[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Zakazs[index].Status = ZakazStatus.Готов;
        }

        public void PayZakaz(int id)
        {
            int index = -1;
            for (int i = 0; i < source.Zakazs.Count; ++i)
            {
                if (source.Visitors[i].Id == id)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Zakazs[index].Status = ZakazStatus.Оплачен;
        }

        public void PutIngredientOnStorage(StorageIngredientBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.StorageIngredients.Count; ++i)
            {
                if (source.StorageIngredients[i].StorageId == model.StorageId &&
                    source.StorageIngredients[i].IngredientId == model.IngredientId)
                {
                    source.StorageIngredients[i].Count += model.Count;
                    return;
                }
                if (source.StorageIngredients[i].Id > maxId)
                {
                    maxId = source.StorageIngredients[i].Id;
                }
            }
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
