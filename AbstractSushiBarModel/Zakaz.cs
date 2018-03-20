using System;

namespace AbstractSushiBarModel
{
    public class Zakaz
    {
        public int Id { get; set; }

        public int VisitorId { get; set; }

        public int SushiId { get; set; }

        public int? CookId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public ZakazStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }
    }
}
