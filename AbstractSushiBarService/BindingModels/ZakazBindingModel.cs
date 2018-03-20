namespace AbstractSushiBarService.BindingModels
{
    public class ZakazBindingModel
    {
        public int Id { get; set; }

        public int VisitorId { get; set; }

        public int SushiId { get; set; }

        public int? CookId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }
    }
}
