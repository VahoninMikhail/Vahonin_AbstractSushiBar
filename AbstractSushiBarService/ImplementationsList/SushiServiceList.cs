using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
            List<SushiViewModel> result = source.Sushis
                .Select(rec => new SushiViewModel
                {
                    Id = rec.Id,
                    SushiName = rec.SushiName,
                    Price = rec.Price,
                    SushiIngredients = source.SushiIngredients
                            .Where(recSI => recSI.SushiId == rec.Id)
                            .Select(recSI => new SushiIngredientViewModel
                            {
                                Id = recSI.Id,
                                SushiId = recSI.SushiId,
                                IngredientId = recSI.IngredientId,
                                IngredientName = source.Ingredients
                                    .FirstOrDefault(recI => recI.Id == recSI.IngredientId)?.IngredientName,
                                Count = recSI.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public SushiViewModel GetElement(int id)
        {
            Sushi element = source.Sushis.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SushiViewModel
                {
                    Id = element.Id,
                    SushiName = element.SushiName,
                    Price = element.Price,
                    SushiIngredients = source.SushiIngredients
                            .Where(recSI => recSI.SushiId == element.Id)
                            .Select(recSI => new SushiIngredientViewModel
                            {
                                Id = recSI.Id,
                                SushiId = recSI.SushiId,
                                IngredientId = recSI.IngredientId,
                                IngredientName = source.Ingredients
                                        .FirstOrDefault(recI => recI.Id == recSI.IngredientId)?.IngredientName,
                                Count = recSI.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SushiBindingModel model)
        {
            Sushi element = source.Sushis.FirstOrDefault(rec => rec.SushiName == model.SushiName);
            if (element != null)
            {
                throw new Exception("Уже есть суши с таким названием");
            }
            int maxId = source.Sushis.Count > 0 ? source.Sushis.Max(rec => rec.Id) : 0;
            source.Sushis.Add(new Sushi
            {
                Id = maxId + 1,
                SushiName = model.SushiName,
                Price = model.Price
            });
            int maxSIId = source.SushiIngredients.Count > 0 ?
                                    source.SushiIngredients.Max(rec => rec.Id) : 0;
            var groupIngredients = model.SushiIngredients
                                        .GroupBy(rec => rec.IngredientId)
                                        .Select(rec => new
                                        {
                                            IngredientId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            foreach (var groupIngredient in groupIngredients)
            {
                source.SushiIngredients.Add(new SushiIngredient
                {
                    Id = ++maxSIId,
                    SushiId = maxId + 1,
                    IngredientId = groupIngredient.IngredientId,
                    Count = groupIngredient.Count
                });
            }
        }

        public void UpdElement(SushiBindingModel model)
        {
            Sushi element = source.Sushis.FirstOrDefault(rec =>
                                        rec.SushiName == model.SushiName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть суши с таким названием");
            }
            element = source.Sushis.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.SushiName = model.SushiName;
            element.Price = model.Price;

            int maxSIId = source.SushiIngredients.Count > 0 ? source.SushiIngredients.Max(rec => rec.Id) : 0;
            var ingrIds = model.SushiIngredients.Select(rec => rec.IngredientId).Distinct();
            var updateIngredients = source.SushiIngredients
                                            .Where(rec => rec.SushiId == model.Id &&
                                           ingrIds.Contains(rec.IngredientId));
            foreach (var updateIngredient in updateIngredients)
            {
                updateIngredient.Count = model.SushiIngredients
                                                .FirstOrDefault(rec => rec.Id == updateIngredient.Id).Count;
            }
            source.SushiIngredients.RemoveAll(rec => rec.SushiId == model.Id &&
                                       !ingrIds.Contains(rec.IngredientId));
            var groupIngredients = model.SushiIngredients
                                        .Where(rec => rec.Id == 0)
                                        .GroupBy(rec => rec.IngredientId)
                                        .Select(rec => new
                                        {
                                            IngredientId = rec.Key,
                                            Count = rec.Sum(r => r.Count)
                                        });
            foreach (var groupIngredient in groupIngredients)
            {
                SushiIngredient elementSI = source.SushiIngredients
                                        .FirstOrDefault(rec => rec.SushiId == model.Id &&
                                                        rec.IngredientId == groupIngredient.IngredientId);
                if (elementSI != null)
                {
                    elementSI.Count += groupIngredient.Count;
                }
                else
                {
                    source.SushiIngredients.Add(new SushiIngredient
                    {
                        Id = ++maxSIId,
                        SushiId = model.Id,
                        IngredientId = groupIngredient.IngredientId,
                        Count = groupIngredient.Count
                    });
                }
            }
        }

        public void DelElement(int id)
        {
            Sushi element = source.Sushis.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.SushiIngredients.RemoveAll(rec => rec.SushiId == id);
                source.Sushis.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
