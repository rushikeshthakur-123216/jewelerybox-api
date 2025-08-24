namespace JewelryBox.Infrastructure.Services
{
    public interface IQueryService
    {
        string GetQuery(string category, string operation);
    }
}
