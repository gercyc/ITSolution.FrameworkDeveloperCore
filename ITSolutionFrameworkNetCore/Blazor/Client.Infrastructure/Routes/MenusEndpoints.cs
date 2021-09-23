namespace ITSolution.Framework.Blazor.Client.Infrastructure.Routes
{
    public static class MenusEndpoints
    {
        public static string ExportFiltered(string searchString)
        {
            return $"{Export}?searchString={searchString}";
        }

        public static string Export = "api/v1/menu/export";

        public static string GetAll = "api/v1/menu";
        public static string Delete = "api/v1/menu";
        public static string Save = "api/v1/menu";
        public static string GetCount = "api/v1/menu/count";
    }
}