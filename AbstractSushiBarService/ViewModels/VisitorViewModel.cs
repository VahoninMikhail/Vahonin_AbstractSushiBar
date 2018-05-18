using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AbstractSushiBarService.ViewModels
{
    [DataContract]
    public class VisitorViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DataMember]
        public string VisitorFIO { get; set; }

        [DataMember]
        public List<MessageInfoViewModel> Messages { get; set; }
    }
}
