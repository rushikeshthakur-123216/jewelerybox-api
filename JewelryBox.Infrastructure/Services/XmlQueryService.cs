using System.Xml;

namespace JewelryBox.Infrastructure.Services
{
    public class XmlQueryService : IQueryService
    {
        private readonly Dictionary<string, Dictionary<string, string>> _queries = new();

        public XmlQueryService()
        {
            LoadQueries();
        }

        public string GetQuery(string category, string operation)
        {
            if (_queries.TryGetValue(category, out var categoryQueries) &&
                categoryQueries.TryGetValue(operation, out var query))
            {
                return query;
            }

            throw new ArgumentException($"Query not found for category: {category}, operation: {operation}");
        }

        private void LoadQueries()
        {
            var queryFiles = new[] { "UserQueries.xml" };

            foreach (var file in queryFiles)
            {
                var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Queries", file);
                if (File.Exists(filePath))
                {
                    LoadQueriesFromFile(filePath);
                }
            }
        }

        private void LoadQueriesFromFile(string filePath)
        {
            var doc = new XmlDocument();
            doc.Load(filePath);

            var root = doc.DocumentElement;
            if (root == null) return;

            foreach (XmlNode categoryNode in root.ChildNodes)
            {
                if (categoryNode.NodeType != XmlNodeType.Element) continue;

                var categoryName = categoryNode.Name;
                var categoryQueries = new Dictionary<string, string>();

                foreach (XmlNode operationNode in categoryNode.ChildNodes)
                {
                    if (operationNode.NodeType != XmlNodeType.Element) continue;

                    var operationName = operationNode.Name;
                    var query = operationNode.InnerText.Trim();
                    categoryQueries[operationName] = query;
                }

                _queries[categoryName] = categoryQueries;
            }
        }
    }
}
