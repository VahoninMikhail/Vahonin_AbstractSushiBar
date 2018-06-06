using System.Runtime.Serialization;

namespace AbstractSushiBarService.BindingModels
{
    [DataContract]
    public class SushiIngredientBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int SushiId { get; set; }

        [DataMember]
        public int IngredientId { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
