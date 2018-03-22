using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AbstractSushiBarService.ImplementationsBD
{
    public class BaseServiceBD : IBaseService
    {
        private AbstractDbContext context;

        public BaseServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<ZakazViewModel> GetList()
        {
            List<ZakazViewModel> result = context.Zakazs
                .Select(rec => new ZakazViewModel
                {
                    Id = rec.Id,
                    VisitorId = rec.VisitorId,
                    SushiId = rec.SushiId,
                    CookId = rec.CookId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    DateImplement = rec.DateImplement == null ? "" :
                                        SqlFunctions.DateName("dd", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.DateImplement.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.DateImplement.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    VisitorFIO = rec.Visitor.VisitorFIO,
                    SushiName = rec.Sushi.SushiName,
                    CookName = rec.Cook.CookFIO
                })
                .ToList();
            return result;
        }

        public void CreateZakaz(ZakazBindingModel model)
        {
            context.Zakazs.Add(new Zakaz
            {
                VisitorId = model.VisitorId,
                SushiId = model.SushiId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = ZakazStatus.Принят
            });
            context.SaveChanges();
        }

        public void TakeZakazInWork(ZakazBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var sushiIngredients = context.SushiIngredients
                                                .Include(rec => rec.Ingredient)
                                                .Where(rec => rec.SushiId == element.SushiId);
                    // списываем
                    foreach (var sushiIngredient in sushiIngredients)
                    {
                        int countOnStorages = sushiIngredient.Count * element.Count;
                        var storageIngredients = context.StorageIngredients
                                                    .Where(rec => rec.IngredientId == sushiIngredient.IngredientId);
                        foreach (var storageIngredient in storageIngredients)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (storageIngredient.Count >= countOnStorages)
                            {
                                storageIngredient.Count -= countOnStorages;
                                countOnStorages = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnStorages -= storageIngredient.Count;
                                storageIngredient.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnStorages > 0)
                        {
                            throw new Exception("Не достаточно ингредиента " +
                                sushiIngredient.Ingredient.IngredientName + " требуется " +
                                sushiIngredient.Count + ", не хватает " + countOnStorages);
                        }
                    }
                    element.CookId = model.CookId;
                    element.DateImplement = DateTime.Now;
                    element.Status = ZakazStatus.Выполняется;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishZakaz(int id)
        {
            Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ZakazStatus.Готов;
            context.SaveChanges();
        }

        public void PayZakaz(int id)
        {
            Zakaz element = context.Zakazs.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = ZakazStatus.Оплачен;
            context.SaveChanges();
        }

        public void PutIngredientOnStorage(StorageIngredientBindingModel model)
        {
            StorageIngredient element = context.StorageIngredients
                                                .FirstOrDefault(rec => rec.StorageId == model.StorageId &&
                                                                    rec.IngredientId == model.IngredientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.StorageIngredients.Add(new StorageIngredient
                {
                    StorageId = model.StorageId,
                    IngredientId = model.IngredientId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }
    }
}
