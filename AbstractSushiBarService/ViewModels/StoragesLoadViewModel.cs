using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractSushiBarService.ViewModels
{
    [DataContract]
    public class StoragesLoadViewModel
    {
        [DataMember]
        public string StorageName { get; set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public List<StoragesIngredientLoadViewModel> Ingredients { get; set; }
    }

    [DataContract]
    public class StoragesIngredientLoadViewModel
    {
        [DataMember]
        public string IngredientName { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
