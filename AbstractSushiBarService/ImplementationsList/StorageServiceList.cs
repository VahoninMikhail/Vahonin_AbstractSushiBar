using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractSushiBarService.ImplementationsList
{
    public class StorageServiceList : IStorageService
    {
        private InformationList source;

        public StorageServiceList()
        {
            source = InformationList.GetInstance();
        }

        public List<StorageViewModel> GetList()
        {
            List<StorageViewModel> result = source.Storages
                .Select(rec => new StorageViewModel
                {
                    Id = rec.Id,
                    StorageName = rec.StorageName,
                    StorageIngredients = source.StorageIngredients
                            .Where(recSI => recSI.StorageId == rec.Id)
                            .Select(recSI => new StorageIngredientViewModel
                            {
                                Id = recSI.Id,
                                StorageId = recSI.StorageId,
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

        public StorageViewModel GetElement(int id)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StorageViewModel
                {
                    Id = element.Id,
                    StorageName = element.StorageName,
                    StorageIngredients = source.StorageIngredients
                            .Where(recSI => recSI.StorageId == element.Id)
                            .Select(recSI => new StorageIngredientViewModel
                            {
                                Id = recSI.Id,
                                StorageId = recSI.StorageId,
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

        public void AddElement(StorageBindingModel model)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.StorageName == model.StorageName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            int maxId = source.Storages.Count > 0 ? source.Storages.Max(rec => rec.Id) : 0;
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdElement(StorageBindingModel model)
        {
            Storage element = source.Storages.FirstOrDefault(rec =>
                                        rec.StorageName == model.StorageName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = source.Storages.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StorageName = model.StorageName;
        }

        public void DelElement(int id)
        {
            Storage element = source.Storages.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.StorageIngredients.RemoveAll(rec => rec.StorageId == id);
                source.Storages.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
