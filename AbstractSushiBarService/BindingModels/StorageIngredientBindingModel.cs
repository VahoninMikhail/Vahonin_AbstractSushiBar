using System.Runtime.Serialization;

namespace AbstractSushiBarService.BindingModels
{
    [DataContract]
    public class StorageIngredientBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StorageId { get; set; }

        [DataMember]
        public int IngredientId { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
