using System;
using System.Collections.Generic;

namespace InstaSharper.Classes.Models
{
    public class InstaFeed:IPaginationData
    {
        public int MediaItemsCount => Medias.Count;
        public int StoriesItemsCount => Stories.Count;
        public Pagination Pagination { get; internal set; }
        public List<InstaMedia> Medias { get; set; } = new List<InstaMedia>();
        public List<InstaStory> Stories { get; set; } = new List<InstaStory>();

        public int Pages { get; set; } = 0;

        
    }
}