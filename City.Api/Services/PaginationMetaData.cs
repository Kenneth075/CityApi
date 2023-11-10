namespace City.Api.Services
{
    public class PaginationMetaData
    {
        public int TotalItemsCount { get; set; }
        public int TotalPageCount { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }

        public PaginationMetaData(int totalItemsCount, int pageSize, int currentPage)
        {
            TotalItemsCount = totalItemsCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPageCount = (int)Math.Ceiling(totalItemsCount /(double) PageSize);
        }
    }
}
