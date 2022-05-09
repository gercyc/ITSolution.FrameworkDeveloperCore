using ITSolution.Framework.Common.Abstractions.EntityFramework;
using ITSolution.Framework.Servers.Core.FirstAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ITSolution.Framework.Common.BaseClasses.AbstractEntities;

namespace ITSolution.Framework.Servers.Core.FirstAPI.BaseAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly DbAccessContext _context;
        public MenuController(ItsDbContextOptions iTsDbContextOptions)
        {
            _context = new DbAccessContext(iTsDbContextOptions);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<ApplicationMenu> Get()
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
        public void Post([FromBody] ApplicationMenu value)
        {
            _context.MenuRep.Create(value, true);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ApplicationMenu value)
        {
            _context.MenuRep.Create(value, true);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            ApplicationMenu del = _context.MenuRep.FirstOrDefault(id);
            _context.MenuRep.Delete(del);
        }
    }
}
