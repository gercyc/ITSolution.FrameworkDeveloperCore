using AutoMapper;
using ITSolution.Framework.Blazor.Application.Features.Menu.Queries.GetById;
using ITSolution.Framework.Blazor.Application.Features.Menus.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.Menus.Queries.GetAll;
using ITSolution.Framework.Blazor.Domain.Entities.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<AddEditMenuCommand, ApplicationMenu>().ReverseMap();
            CreateMap<GetMenuByIdResponse, ApplicationMenu>().ReverseMap();
            CreateMap<GetAllMenusResponse, ApplicationMenu>().ReverseMap();
        }
    }
}
