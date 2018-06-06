using System.Runtime.Serialization;

namespace AbstractSushiBarService.ViewModels
{
    [DataContract]
    public class CookViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string CookFIO { get; set; }
    }
}
