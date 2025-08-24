using System.ComponentModel.DataAnnotations;
using JewelryBox.Domain.Enums;

namespace JewelryBox.Domain.Entities
{
    public class JewelryItem
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public JewelryType Type { get; set; }
        
        public JewelryMaterial Material { get; set; }
        
        [StringLength(50)]
        public string? Brand { get; set; }
        
        public decimal? PurchasePrice { get; set; }
        
        public DateTime? PurchaseDate { get; set; }
        
        [StringLength(100)]
        public string? Location { get; set; }
        
        public bool IsFavorite { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign keys
        public int UserId { get; set; }
        public int? JewelryBoxId { get; set; }
        
        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual JewelryBox? JewelryBox { get; set; }
    }
}
