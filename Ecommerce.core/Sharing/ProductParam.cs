namespace Ecommerce.core.Sharing
{
    public class ProductParam
    {
        public string? sort { get; set; }
        public int? categoryId { get; set; }
        public string? search { get; set; }
        public int maxPageSize { get; set; } = 6;
        private int _pageSize = 3;

        public int pageSize
        {
            get { return _pageSize ; }
            set { _pageSize  = value > maxPageSize ? maxPageSize : value; }
        }
        public int pageNumber { get; set; } = 1;

    }
}
