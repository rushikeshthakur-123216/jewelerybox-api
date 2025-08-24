namespace JewelryBox.Core.Configuration
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string Provider { get; set; } = "PostgreSQL"; // PostgreSQL, Oracle, SQLServer, etc.
    }
}
