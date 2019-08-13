using System;

namespace MemeGenerator.Domain.Models
{
    public class PageRequest<T>
    {
        public PageRequest(int pageNumber, int pageSize, Func<T, object> orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            OrderBy = orderBy;
            Skip = (PageNumber - 1) * PageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Func<T, object> OrderBy { get; set; }
        public int Skip { get; set; }
    }
}
