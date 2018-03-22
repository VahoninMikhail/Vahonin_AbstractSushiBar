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
    public class SushiServiceBD : ISushiService
    {
        private AbstractDbContext context;

        public SushiServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<SushiViewModel> GetList()
        {
            List<SushiViewModel> result = context.Sushis
                .Select(rec => new SushiViewModel
                {
                    Id = rec.Id,
                    SushiName = rec.SushiName,
                    Price = rec.Price,
                    SushiIngredients = context.SushiIngredients
                            .Where(recPC => recPC.SushiId == rec.Id)
                            .Select(recPC => new SushiIngredientViewModel
                            {
                                Id = recPC.Id,
                                SushiId = recPC.SushiId,
                                IngredientId = recPC.IngredientId,
                                IngredientName = recPC.Ingredient.IngredientName,
                                Count = recPC.Count
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public SushiViewModel GetElement(int id)
        {
            Sushi element = context.Sushis.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new SushiViewModel
                {
                    Id = element.Id,
                    SushiName = element.SushiName,
                    Price = element.Price,
                    SushiIngredients = context.SushiIngredients
                            .Where(recPC => recPC.SushiId == element.Id)
                            .Select(recPC => new SushiIngredientViewModel
                            {
                                Id = recPC.Id,
                                SushiId = recPC.SushiId,
                                IngredientId = recPC.IngredientId,
                                IngredientName = recPC.Ingredient.IngredientName,
                                Count = recPC.Count
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(SushiBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Sushi element = context.Sushis.FirstOrDefault(rec => rec.SushiName == model.SushiName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть суши с таким названием");
                    }
                    element = new Sushi
                    {
                        SushiName = model.SushiName,
                        Price = model.Price
                    };
                    context.Sushis.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupIngredients = model.SushiIngredients
                                                .GroupBy(rec => rec.IngredientId)
                                                .Select(rec => new
                                                {
                                                    IngredientId = rec.Key,
                                                    Count = rec.Sum(r => r.Count)
                                                });
                    // добавляем компоненты
                    foreach (var groupIngredient in groupIngredients)
                    {
                        context.SushiIngredients.Add(new SushiIngredient
                        {
                            SushiId = element.Id,
                            IngredientId = groupIngredient.IngredientId,
                            Count = groupIngredient.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdElement(SushiBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Sushi element = context.Sushis.FirstOrDefault(rec =>
                                        rec.SushiName == model.SushiName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть суши с таким названием");
                    }
                    element = context.Sushis.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.SushiName = model.SushiName;
                    element.Price = model.Price;
                    context.SaveChanges();

                    // обновляем существуюущие компоненты
                    var compIds = model.SushiIngredients.Select(rec => rec.IngredientId).Distinct();
                    var updateIngredients = context.SushiIngredients
                                                    .Where(rec => rec.SushiId == model.Id &&
                                                        compIds.Contains(rec.IngredientId));
                    foreach (var updateIngredient in updateIngredients)
                    {
                        updateIngredient.Count = model.SushiIngredients
                                                        .FirstOrDefault(rec => rec.Id == updateIngredient.Id).Count;
                    }
                    context.SaveChanges();
                    context.SushiIngredients.RemoveRange(
                                        context.SushiIngredients.Where(rec => rec.SushiId == model.Id &&
                                                                            !compIds.Contains(rec.IngredientId)));
                    context.SaveChanges();
                    // новые записи
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
                        SushiIngredient elementPC = context.SushiIngredients
                                                .FirstOrDefault(rec => rec.SushiId == model.Id &&
                                                                rec.IngredientId == groupIngredient.IngredientId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupIngredient.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.SushiIngredients.Add(new SushiIngredient
                            {
                                SushiId = model.Id,
                                IngredientId = groupIngredient.IngredientId,
                                Count = groupIngredient.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Sushi element = context.Sushis.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.SushiIngredients.RemoveRange(
                                            context.SushiIngredients.Where(rec => rec.SushiId == id));
                        context.Sushis.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

