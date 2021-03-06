﻿using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;

namespace AbstractSushiBarService.ImplementationsList
{
    public class IngredientServiceList : IIngredientService
    {
        private InformationList source;

        public IngredientServiceList()
        {
            source = InformationList.GetInstance();
        }

        public List<IngredientViewModel> GetList()
        {
            List<IngredientViewModel> result = new List<IngredientViewModel>();
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                result.Add(new IngredientViewModel
                {
                    Id = source.Ingredients[i].Id,
                    IngredientName = source.Ingredients[i].IngredientName
                });
            }
            return result;
        }

        public IngredientViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == id)
                {
                    return new IngredientViewModel
                    {
                        Id = source.Ingredients[i].Id,
                        IngredientName = source.Ingredients[i].IngredientName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(IngredientBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id > maxId)
                {
                    maxId = source.Ingredients[i].Id;
                }
                if (source.Ingredients[i].IngredientName == model.IngredientName)
                {
                    throw new Exception("Уже есть ингредиент с таким названием");
                }
            }
            source.Ingredients.Add(new Ingredient
            {
                Id = maxId + 1,
                IngredientName = model.IngredientName
            });
        }

        public void UpdElement(IngredientBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Ingredients[i].IngredientName == model.IngredientName &&
                    source.Ingredients[i].Id != model.Id)
                {
                    throw new Exception("Уже есть ингредиент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Ingredients[index].IngredientName = model.IngredientName;
        }

        public void DelElement(int id)
        {
            for (int i = 0; i < source.Ingredients.Count; ++i)
            {
                if (source.Ingredients[i].Id == id)
                {
                    source.Ingredients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
