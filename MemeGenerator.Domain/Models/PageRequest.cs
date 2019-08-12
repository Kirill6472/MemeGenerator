using System;
using MemeGenerator.Domain.Entities;

namespace MemeGenerator.Domain.Models
{
    public class PageRequest
    {
        public PageRequest(int pageNumber, int pageSize, Func<MemeImage, object> orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            OrderBy = orderBy;
            Skip = (PageNumber - 1) * PageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Func<MemeImage, object> OrderBy { get; set; }
        public int Skip { get; set; }
    }
}
