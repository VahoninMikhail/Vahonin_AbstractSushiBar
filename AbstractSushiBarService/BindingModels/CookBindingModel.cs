using System.Runtime.Serialization;

namespace AbstractSushiBarService.BindingModels
{
    [DataContract]
    public class CookBindingModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CookFIO { get; set; }
    }
}
