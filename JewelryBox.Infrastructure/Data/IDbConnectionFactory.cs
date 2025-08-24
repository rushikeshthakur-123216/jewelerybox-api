using System.Data;

namespace JewelryBox.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
