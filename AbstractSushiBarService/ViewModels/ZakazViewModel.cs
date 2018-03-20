namespace AbstractSushiBarService.ViewModels
{
    public class ZakazViewModel
    {
        public int Id { get; set; }

        public int VisitorId { get; set; }

        public string VisitorFIO { get; set; }

        public int SushiId { get; set; }

        public string SushiName { get; set; }

        public int? CookId { get; set; }

        public string CookName { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateImplement { get; set; }
    }
}
