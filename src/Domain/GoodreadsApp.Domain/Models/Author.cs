using System.Collections.Generic;

namespace GoodreadsApp.Domain.Models
{
    public class Author
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
        public string Website { get; set; }
        public string Path { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
