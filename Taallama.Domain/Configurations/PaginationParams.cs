namespace Taallama.Domain.Configurations
{
    public class PaginationParams
    {
        private const short maxPageSize = 50;
        private int pageSize;

        public int PageSize { get => pageSize; set => pageSize = value > maxPageSize ? maxPageSize : value; }
        public int PageIndex { get; set; }
    }
}