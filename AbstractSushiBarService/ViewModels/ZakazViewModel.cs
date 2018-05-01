using System.Runtime.Serialization;

namespace AbstractSushiBarService.ViewModels
{
    [DataContract]
    public class ZakazViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int VisitorId { get; set; }

        [DataMember]
        public string VisitorFIO { get; set; }

        [DataMember]
        public int SushiId { get; set; }

        [DataMember]
        public string SushiName { get; set; }

        [DataMember]
        public int? CookId { get; set; }

        [DataMember]
        public string CookName { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string DateCreate { get; set; }

        [DataMember]
        public string DateImplement { get; set; }
    }
}
