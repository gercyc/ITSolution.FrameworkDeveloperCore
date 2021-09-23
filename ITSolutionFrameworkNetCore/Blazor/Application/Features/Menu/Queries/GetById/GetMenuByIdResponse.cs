namespace ITSolution.Framework.Blazor.Application.Features.Menu.Queries.GetById
{
    public class GetMenuByIdResponse
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public int? ParentMenu { get; set; }
        public string MenuAction { get; set; }
    }
}