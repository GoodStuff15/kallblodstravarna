using System.ComponentModel.DataAnnotations;

namespace restortlibrary.Models
{
    public class AccomodationType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal BasePrice { get; set; }
        public ICollection<Accomodation> Accomodations { get; set; } = new List<Accomodation>();
    }
}
