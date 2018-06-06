using System.Runtime.Serialization;

namespace AbstractSushiBarService.BindingModels
{
    [DataContract]
    public class ZakazBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int VisitorId { get; set; }

        [DataMember]
        public int SushiId { get; set; }

        [DataMember]
        public int? CookId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }
    }
}
