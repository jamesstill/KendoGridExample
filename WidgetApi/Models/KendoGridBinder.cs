using Kendo.Mvc.UI;
using Kendo.Mvc;

namespace WidgetApi.Models
{
    public class KendoGridBinder
    {
        public long PageNumber { get; set; }
        public long PageSize { get; set; }
        public long TotalRecordCount { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public string? FilterColumn { get; set; }
        public string? FilterValue { get; set; }

        public KendoGridBinder(DataSourceRequest request)
        {
            // set values with defaults
            PageNumber = request.Skip.ToLong(1);
            PageSize = request.Take.ToLong(25);
            SortColumn = "PersonInstanceId";
            SortDirection = "DESC";

            // -------------------------------------------------------------------
            // NOTE: For the sake of simplicity only one sort column allowed
            // -------------------------------------------------------------------
            SortDescriptor? sortDescriptor = request.Sorts?.FirstOrDefault();
            if (sortDescriptor != null)
            {
                SortColumn = sortDescriptor.Member;
                SortDirection = sortDescriptor.SortDirection == ListSortDirection.Ascending ? "ASC" : "DESC";
            }

            // -------------------------------------------------------------------
            // NOTE: Only one filter allowed and ignoring composite filtering
            // -------------------------------------------------------------------
            IFilterDescriptor? filterDescriptor = request.Filters?.FirstOrDefault();
            if (filterDescriptor != null)
            {
                if (filterDescriptor is not FilterDescriptor)
                {
                    return; // ignore CompositeFilterDescriptor
                }

                var f = (FilterDescriptor)filterDescriptor;
                FilterColumn = f.Member;
                FilterValue = (string)f.Value;
            }
        }
    }
}
