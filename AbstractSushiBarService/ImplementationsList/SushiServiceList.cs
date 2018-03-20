using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractSushiBarService.ImplementationsList
{
    public class SushiServiceList : ISushiService
    {
        private InformationList source;

        public SushiServiceList()
        {
            source = InformationList.GetInstance();
        }

        public List<SushiViewModel> GetList()
        {
            List<SushiViewModel> result = new List<SushiViewModel>();
            for (int i = 0; i < source.Sushis.Count; ++i)
            {
                List<SushiIngredientViewModel> sushiIngredients = new List<SushiIngredientViewModel>();
                for (int j = 0; j < source.SushiIngredients.Count; ++j)
                {
                    if (source.SushiIngredients[j].SushiId == source.Sushis[i].Id)
                    {
                        string ingredientName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.SushiIngredients[j].IngredientId == source.Ingredients[k].Id)
                            {
                                ingredientName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        sushiIngredients.Add(new SushiIngredientViewModel
                        {
                            Id = source.SushiIngredients[j].Id,
                            SushiId = source.SushiIngredients[j].SushiId,
                            IngredientId = source.SushiIngredients[j].IngredientId,
                            IngredientName = ingredientName,
                            Count = source.SushiIngredients[j].Count
                        });
                    }
                }
                result.Add(new SushiViewModel
                {
                    Id = source.Sushis[i].Id,
                    SushiName = source.Sushis[i].SushiName,
                    Price = source.Sushis[i].Price,
                    SushiIngredients = sushiIngredients
                });
            }
            return result;
        }

        public SushiViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Sushis.Count; ++i)
            {
                List<SushiIngredientViewModel> sushiIngredients = new List<SushiIngredientViewModel>();
                for (int j = 0; j < source.SushiIngredients.Count; ++j)
                {
                    if (source.SushiIngredients[j].SushiId == source.Sushis[i].Id)
                    {
                        string ingredientName = string.Empty;
                        for (int k = 0; k < source.Ingredients.Count; ++k)
                        {
                            if (source.SushiIngredients[j].IngredientId == source.Ingredients[k].Id)
                            {
                                ingredientName = source.Ingredients[k].IngredientName;
                                break;
                            }
                        }
                        sushiIngredients.Add(new SushiIngredientViewModel
                        {
                            Id = source.SushiIngredients[j].Id,
                            SushiId = source.SushiIngredients[j].SushiId,
                            IngredientId = source.SushiIngredients[j].IngredientId,
                            IngredientName = ingredientName,
                            Count = source.SushiIngredients[j].Count
                        });
                    }
                }
                if (source.Sushis[i].Id == id)
                {
                    return new SushiViewModel
                    {
                        Id = source.Sushis[i].Id,
                        SushiName = source.Sushis[i].SushiName,
                        Price = source.Sushis[i].Price,
                        SushiIngredients = sushiIngredients
                    };
                }
            }

            throw new Exception("Элемент не найден");
        }

        public void AddElement(SushiBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Sushis.Count; ++i)
            {
                if (source.Sushis[i].Id > maxId)
                {
                    maxId = source.Sushis[i].Id;
                }
                if (source.Sushis[i].SushiName == model.SushiName)
                {
                    throw new Exception("Уже есть суши с таким названием");
                }
            }
            source.Sushis.Add(new Sushi
            {
                Id = maxId + 1,
                SushiName = model.SushiName,
                Price = model.Price
            });
            int maxSIId = 0;
            for (int i = 0; i < source.SushiIngredients.Count; ++i)
            {
                if (source.SushiIngredients[i].Id > maxSIId)
                {
                    maxSIId = source.SushiIngredients[i].Id;
                }
            }
            for (int i = 0; i < model.SushiIngredients.Count; ++i)
            {
                for (int j = 1; j < model.SushiIngredients.Count; ++j)
                {
                    if (model.SushiIngredients[i].IngredientId ==
                        model.SushiIngredients[j].IngredientId)
                    {
                        model.SushiIngredients[i].Count +=
                            model.SushiIngredients[j].Count;
                        model.SushiIngredients.RemoveAt(j--);
                    }
                }
            }
            for (int i = 0; i < model.SushiIngredients.Count; ++i)
            {
                source.SushiIngredients.Add(new SushiIngredient
                {
                    Id = ++maxSIId,
                    SushiId = maxId + 1,
                    IngredientId = model.SushiIngredients[i].IngredientId,
                    Count = model.SushiIngredients[i].Count
                });
            }
        }

        public void UpdElement(SushiBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Sushis.Count; ++i)
            {
                if (source.Sushis[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Sushis[i].SushiName == model.SushiName &&
                    source.Sushis[i].Id != model.Id)
                {
                    throw new Exception("Уже есть суши с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Sushis[index].SushiName = model.SushiName;
            source.Sushis[index].Price = model.Price;
            int maxSIId = 0;
            for (int i = 0; i < source.SushiIngredients.Count; ++i)
            {
                if (source.SushiIngredients[i].Id > maxSIId)
                {
                    maxSIId = source.SushiIngredients[i].Id;
                }
            }
            for (int i = 0; i < source.SushiIngredients.Count; ++i)
            {
                if (source.SushiIngredients[i].SushiId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.SushiIngredients.Count; ++j)
                    {
                        if (source.SushiIngredients[i].Id == model.SushiIngredients[j].Id)
                        {
                            source.SushiIngredients[i].Count = model.SushiIngredients[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        source.SushiIngredients.RemoveAt(i--);
                    }
                }
            }
            for (int i = 0; i < model.SushiIngredients.Count; ++i)
            {
                if (model.SushiIngredients[i].Id == 0)
                {
                    for (int j = 0; j < source.SushiIngredients.Count; ++j)
                    {
                        if (source.SushiIngredients[j].SushiId == model.Id &&
                            source.SushiIngredients[j].IngredientId == model.SushiIngredients[i].IngredientId)
                        {
                            source.SushiIngredients[j].Count += model.SushiIngredients[i].Count;
                            model.SushiIngredients[i].Id = source.SushiIngredients[j].Id;
                            break;
                        }
                    }
                    if (model.SushiIngredients[i].Id == 0)
                    {
                        source.SushiIngredients.Add(new SushiIngredient
                        {
                            Id = ++maxSIId,
                            SushiId = model.Id,
                            IngredientId = model.SushiIngredients[i].IngredientId,
                            Count = model.SushiIngredients[i].Count
                        });
                    }
                }
            }
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.SushiIngredients.Count; ++i)
            {
                if (source.SushiIngredients[i].SushiId == id)
                {
                    source.SushiIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Sushis.Count; ++i)
            {
                if (source.Sushis[i].Id == id)
                {
                    source.Sushis.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
