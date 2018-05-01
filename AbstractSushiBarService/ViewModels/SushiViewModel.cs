using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractSushiBarService.ViewModels
{
    [DataContract]
    public class SushiViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SushiName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<SushiIngredientViewModel> SushiIngredients { get; set; }
    }
}
