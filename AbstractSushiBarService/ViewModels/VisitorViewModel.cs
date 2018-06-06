using System.Runtime.Serialization;

namespace AbstractSushiBarService.ViewModels
{
    [DataContract]
    public class VisitorViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string VisitorFIO { get; set; }
    }
}
