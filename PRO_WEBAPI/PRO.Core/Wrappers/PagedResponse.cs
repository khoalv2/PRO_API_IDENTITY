using System;
using System.Collections.Generic;
using System.Text;

namespace PRO.Core.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public int TotalRecords { get; set; }
        public PagedResponse(T _data, int _pageSize, int _pageNumber, int _totalRecords)
        {
            Data = _data;
            pageSize = _pageSize;
            pageNumber = _pageNumber;
            Error = null;
            Message = null;
            Successed = true;
            TotalRecords = _totalRecords;
        }
    }
}
