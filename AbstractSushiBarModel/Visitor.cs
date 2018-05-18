using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractSushiBarModel
{
    public class Visitor
    {
        public int Id { get; set; }

        [Required]
        public string VisitorFIO { get; set; }

        public string Mail { get; set; }

        [ForeignKey("VisitorId")]
        public virtual List<Zakaz> Zakazs { get; set; }

        [ForeignKey("VisitorId")]
        public virtual List<MessageInfo> MessageInfos { get; set; }
    }
}
