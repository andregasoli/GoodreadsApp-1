using Newtonsoft.Json;

namespace GoodreadsApp.Domain.Models
{
    public class Genre
    {
        public string Name { get; set; }
        
        [JsonIgnore]
        public string Path { get; set; }
    }
}
