namespace WidgetApi.Models
{
    public class Widget
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Shape { get; set; }
        public string? Color { get; set; }
    }

    // if using sproc approach this is a required wrapper
    public class WidgetList
    {
        public List<Widget> data { get; set; }
    }
}
