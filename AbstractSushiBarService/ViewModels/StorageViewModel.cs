using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractSushiBarService.ViewModels
{
    [DataContract]
    public class StorageViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string StorageName { get; set; }

        [DataMember]
        public List<StorageIngredientViewModel> StorageIngredients { get; set; }
    }
}
