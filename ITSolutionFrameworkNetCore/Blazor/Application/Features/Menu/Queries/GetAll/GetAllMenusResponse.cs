namespace ITSolution.Framework.Blazor.Application.Features.Menus.Queries.GetAll
{
    public class GetAllMenusResponse
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public int? ParentMenu { get; set; }
        public string MenuAction { get; set; }
    }
}