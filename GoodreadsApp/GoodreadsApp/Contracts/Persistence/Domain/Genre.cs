using Newtonsoft.Json;

namespace GoodreadsApp.Contracts.Persistence.Domain
{
    public class Genre
    {
        public string Name { get; set; }
        
        [JsonIgnore]
        public string Path { get; set; }
    }
}
