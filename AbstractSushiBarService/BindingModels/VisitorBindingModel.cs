using System.Runtime.Serialization;

namespace AbstractSushiBarService.BindingModels
{
    [DataContract]
    public class VisitorBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string VisitorFIO { get; set; }
    }
}
