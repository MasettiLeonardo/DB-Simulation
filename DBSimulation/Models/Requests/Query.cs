namespace DBSimulation.Models.Requests
{
    public class Query
    {
        public string? Content { get; set; } = string.Empty;

        public bool IsSelectQuery()
        {
            return Content!.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase);
        }
    }

}
