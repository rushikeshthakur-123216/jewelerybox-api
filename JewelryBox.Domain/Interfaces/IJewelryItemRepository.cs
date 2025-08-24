using JewelryBox.Domain.Entities;

namespace JewelryBox.Domain.Interfaces
{
    public interface IJewelryItemRepository
    {
        Task<JewelryItem?> GetByIdAsync(int id);
        Task<IEnumerable<JewelryItem>> GetByUserIdAsync(int userId);
        Task<IEnumerable<JewelryItem>> GetByJewelryBoxIdAsync(int jewelryBoxId);
        Task<JewelryItem> CreateAsync(JewelryItem jewelryItem);
        Task<JewelryItem> UpdateAsync(JewelryItem jewelryItem);
        Task DeleteAsync(int id);
    }
}
