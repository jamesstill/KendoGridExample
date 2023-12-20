namespace WidgetApi.Models
{
    public class KendoParameters
    {
        public long PageNumber { get; set; }    
        public long PageSize { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
    }
}
