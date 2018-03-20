using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;

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
            List<StorageViewModel> result = new List<StorageViewModel>();
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageIngredientViewModel> StorageIngredients = new List<StorageIngredientViewModel>();
                for (int j = 0; j < source.StorageIngredients.Count; ++j)
                {
                    if (source.StorageIngredients[j].StorageId == source.Storages[i].Id)
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
                        StorageIngredients.Add(new StorageIngredientViewModel
                        {
                            Id = source.StorageIngredients[j].Id,
                            StorageId = source.StorageIngredients[j].StorageId,
                            IngredientId = source.StorageIngredients[j].IngredientId,
                            IngredientName = ingredientName,
                            Count = source.StorageIngredients[j].Count
                        });
                    }
                }
                result.Add(new StorageViewModel
                {
                    Id = source.Storages[i].Id,
                    StorageName = source.Storages[i].StorageName,
                    StorageIngredients = StorageIngredients
                });
            }
            return result;
        }

        public StorageViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                List<StorageIngredientViewModel> StorageIngredients = new List<StorageIngredientViewModel>();
                for (int j = 0; j < source.StorageIngredients.Count; ++j)
                {
                    if (source.StorageIngredients[j].StorageId == source.Storages[i].Id)
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
                        StorageIngredients.Add(new StorageIngredientViewModel
                        {
                            Id = source.StorageIngredients[j].Id,
                            StorageId = source.StorageIngredients[j].StorageId,
                            IngredientId = source.StorageIngredients[j].IngredientId,
                            IngredientName = ingredientName,
                            Count = source.StorageIngredients[j].Count
                        });
                    }
                }
                if (source.Storages[i].Id == id)
                {
                    return new StorageViewModel
                    {
                        Id = source.Storages[i].Id,
                        StorageName = source.Storages[i].StorageName,
                        StorageIngredients = StorageIngredients
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StorageBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id > maxId)
                {
                    maxId = source.Storages[i].Id;
                }
                if (source.Storages[i].StorageName == model.StorageName)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            source.Storages.Add(new Storage
            {
                Id = maxId + 1,
                StorageName = model.StorageName
            });
        }

        public void UpdElement(StorageBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Storages[i].StorageName == model.StorageName &&
                    source.Storages[i].Id != model.Id)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Storages[index].StorageName = model.StorageName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.StorageIngredients.Count; ++i)
            {
                if (source.StorageIngredients[i].StorageId == id)
                {
                    source.StorageIngredients.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Storages.Count; ++i)
            {
                if (source.Storages[i].Id == id)
                {
                    source.Storages.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
