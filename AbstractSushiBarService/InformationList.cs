using AbstractSushiBarModel;
using System.Collections.Generic;

namespace AbstractSushiBarService
{
    class InformationList
    {
        private static InformationList instance;

        public List<Visitor> Visitors { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<Cook> Cooks { get; set; }

        public List<Zakaz> Zakazs { get; set; }

        public List<Sushi> Sushis { get; set; }

        public List<SushiIngredient> SushiIngredients { get; set; }

        public List<Storage> Storages { get; set; }

        public List<StorageIngredient> StorageIngredients { get; set; }

        private InformationList()
        {
            Visitors = new List<Visitor>();
            Ingredients = new List<Ingredient>();
            Cooks = new List<Cook>();
            Zakazs = new List<Zakaz>();
            Sushis = new List<Sushi>();
            SushiIngredients = new List<SushiIngredient>();
            Storages = new List<Storage>();
            StorageIngredients = new List<StorageIngredient>();
        }

        public static InformationList GetInstance()
        {
            if (instance == null)
            {
                instance = new InformationList();
            }

            return instance;
        }
    }
}
