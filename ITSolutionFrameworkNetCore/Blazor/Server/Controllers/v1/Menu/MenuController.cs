using ITSolution.Framework.Blazor.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ITSolution.Framework.Blazor.Application.Features.Menus.Queries.GetAll;
using ITSolution.Framework.Blazor.Application.Features.Menus.Commands.AddEdit;
using ITSolution.Framework.Blazor.Application.Features.Menu.Queries.GetById;
using ITSolution.Framework.Blazor.Application.Features.Menu.Commands.Delete;
using ITSolution.Framework.Blazor.Application.Features.Menu.Queries.Export;

namespace ITSolution.Framework.Blazor.Server.Controllers.v1.Catalog
{
    public class MenuController : BaseApiController<MenuController>
    {
        /// <summary>
        /// Get All Menus
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Brands.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _mediator.Send(new GetAllMenusQuery());
            return Ok(brands);
        }

        /// <summary>
        /// Get a Menu By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Brands.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _mediator.Send(new GetMenuByIdResponse() { Id = id });
            return Ok(brand);
        }

        /// <summary>
        /// Create/Update a Menu
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Brands.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditMenuCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Menu
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Brands.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteMenuCommand { Id = id }));
        }

        /// <summary>
        /// Search Menus and Export to Excel
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.Brands.Export)]
        [HttpGet("export")]
        public async Task<IActionResult> Export(string searchString = "")
        {
            return Ok(await _mediator.Send(new ExportMenuQuery(searchString)));
        }
    }
}