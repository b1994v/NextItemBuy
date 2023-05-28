namespace NextItemBuy.Services.Model
{
    public class BaseSearchModel
    {
        public string Key { get; set; }
        public bool? Active { get; set; }
        public int Page {get; set;}
        public int PageSize { get; set; }
    }
}
