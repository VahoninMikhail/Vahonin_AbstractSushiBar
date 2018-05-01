using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractSushiBarService.BindingModels
{
    [DataContract]
    public class SushiBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SushiName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<SushiIngredientBindingModel> SushiIngredients { get; set; }
    }
}
