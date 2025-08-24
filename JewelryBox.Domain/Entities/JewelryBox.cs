using System.ComponentModel.DataAnnotations;

namespace JewelryBox.Domain.Entities
{
    public class JewelryBox
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign key
        public int UserId { get; set; }
        
        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual ICollection<JewelryItem> JewelryItems { get; set; } = new List<JewelryItem>();
    }
}
