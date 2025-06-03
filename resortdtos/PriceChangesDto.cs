using System.ComponentModel.DataAnnotations;

namespace resortapi.Converters
{
    public class PriceChangesDto
    {
        public int Id { get; set; }
        public float PriceChange { get; set; }
        public string Type { get; set; } = string.Empty;
    }
}