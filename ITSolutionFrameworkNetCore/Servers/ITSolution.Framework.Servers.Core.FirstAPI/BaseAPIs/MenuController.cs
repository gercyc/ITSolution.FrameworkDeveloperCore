using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSolution.Framework.Core.BaseClasses.CommonEntities;
using ITSolution.Framework.Core.BaseClasses.Identity;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using ITSolution.Framework.Servers.Core.FirstAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace ITSolution.Framework.Servers.Core.FirstAPI.BaseAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly DbAccessContext _context;
        public static List<ApplicationMenu> MenuTree { get; set; }
        public MenuController(ItsDbContextOptions iTsDbContextOptions, [FromServices] UserManager<ApplicationUser> userManager, [FromServices] SignInManager<ApplicationUser> signInManager, [FromServices] IHttpContextAccessor accessor)
        {
            _context = new DbAccessContext(iTsDbContextOptions, userManager, signInManager, accessor);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ApplicationMenu> Get()
        {
            if (MenuTree != null && MenuTree.Count() > 0)
                return MenuTree;

            var menuset = _context.MenuRep.GetAll();
            var mn = menuset.ToList();

            foreach (var item in menuset.Where(m => m.MenuPai == null))
            {
                item.ChildMenus = new List<ApplicationMenu>();

                foreach (var childMenu in mn.Where(m => m.MenuPai == item.Id))
                {
                    item.ChildMenus.Add(childMenu);
                }
            }

            menuset = menuset.Where(m => m.MenuPai == null);
            MenuTree = menuset.ToList();
            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return menuset;
        }


        #region for DevExpress/DevExtreme methods

        [HttpPost("dxPost")]
        public async Task<IActionResult> dxPost(string values)
        {
            var model = new ApplicationMenu();
            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.MenuRep.CreateAsync(model, true);

            return Ok();
        }

        [HttpPut("dxPut")]
        public async Task<IActionResult> dxPut([FromForm] string key, [FromForm] string values)
        {
            var model = await _context.MenuRep.FirstOrDefaultAsync(key);
            if (model == null)
                return StatusCode(409, "Menu not found");

            JsonConvert.PopulateObject(values, model);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.MenuRep.UpdateAsync(model, true);
            return Ok();
        }

        [HttpDelete("dxDelete")]
        public async Task dxDelete([FromForm] string key)
        {
            var model = await _context.MenuRep.FirstOrDefaultAsync(key);
            await _context.MenuRep.DeleteAsync(model, true);
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
        #endregion

        #region standard api methods
        
         
        // GET api/values
        [HttpGet("stdGet")]
        public IEnumerable<ApplicationMenu> stdGet()
        {
            return _context.MenuRep.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ApplicationMenu Get(int id)
        {
            return _context.MenuRep.FirstOrDefault(id);
        }

        // POST api/values
        [HttpPost]
        public ApplicationMenu Post([FromBody] ApplicationMenu value)
        {
            _context.MenuRep.Create(value, true);
            return value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ApplicationMenu Put(int id, [FromBody] ApplicationMenu value)
        {
            _context.MenuRep.Create(value, true);
            return value;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ApplicationMenu del = _context.MenuRep.FirstOrDefault(id);
            _context.MenuRep.Delete(del);
        }
         

        #endregion


    }
}
