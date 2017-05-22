using System;
using System.Collections.Generic;
using System.Text;

namespace InstaSharper.Classes
{
    public class Pagination
    {
        public string NextId { get; }
        public Pagination(string nextId)
        {
            NextId = nextId;
        }
        
    }
}
