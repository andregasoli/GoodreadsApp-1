using System;
using System.Collections.Generic;

namespace GoodreadsApp.Domain.Models
{
    public class Book
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Path { get; set; }
        public string BookFormat { get; set; }
        public string PublishedBy { get; set; }
        public string ISBN { get; set; }
        public int NumberOfPages { get; set; }
        public int RatingsCount { get; set; }
        public double AverageRating { get; set; }
        public DateTime PublishingDate { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
