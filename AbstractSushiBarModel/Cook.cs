using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractSushiBarModel
{
    public class Cook
    {
        public int Id { get; set; }

        [Required]
        public string CookFIO { get; set; }

        [ForeignKey("CookId")]
        public virtual List<Zakaz> Zakazs { get; set; }
    }
}
