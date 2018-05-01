using System.Runtime.Serialization;

namespace AbstractSushiBarService.ViewModels
{
    [DataContract]
    public class VisitorZakazsModel
    {
        [DataMember]
        public string VisitorName { get; set; }

        [DataMember]
        public string DateCreate { get; set; }

        [DataMember]
        public string SushiName { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public string Status { get; set; }
    }
}
