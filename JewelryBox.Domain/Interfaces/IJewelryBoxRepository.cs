using JewelryBox.Domain.Entities;

namespace JewelryBox.Domain.Interfaces
{
    public interface IJewelryBoxRepository
    {
        Task<Entities.JewelryBox?> GetByIdAsync(int id);
        Task<IEnumerable<Entities.JewelryBox>> GetByUserIdAsync(int userId);
        Task<Entities.JewelryBox> CreateAsync(Entities.JewelryBox jewelryBox);
        Task<Entities.JewelryBox> UpdateAsync(Entities.JewelryBox jewelryBox);
        Task DeleteAsync(int id);
    }
}
