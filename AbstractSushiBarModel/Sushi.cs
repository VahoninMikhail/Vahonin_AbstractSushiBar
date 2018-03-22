using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbstractSushiBarModel
{
    public class Sushi
    {
        public int Id { get; set; }

        [Required]
        public string SushiName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("SushiId")]
        public virtual List<Zakaz> Zakazs { get; set; }

        [ForeignKey("SushiId")]
        public virtual List<SushiIngredient> SushiIngredients { get; set; }
    }
}
