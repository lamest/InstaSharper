using System;
using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaMediaList : List<InstaMedia>,IPaginationData
    {
        public int Pages { get; set; } = 0;
        public int PageSize { get; set; } = 0;

        public Pagination Pagination { get; internal set; }
    }
}