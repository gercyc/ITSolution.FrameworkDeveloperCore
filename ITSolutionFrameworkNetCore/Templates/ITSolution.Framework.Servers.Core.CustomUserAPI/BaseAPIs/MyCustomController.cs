using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSolution.Framework.Core.CustomUserAPI.Data;
using ITSolution.Framework.Core.CustomUserAPI.Model;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using ITSolution.Framework.Servers.Core.CustomUserAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace ITSolution.Framework.Servers.Core.FirstAPI.BaseAPIs
{
    /// <summary>
    /// Customize or create new APIs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MyCustomController : ControllerBase
    {
        private readonly DBAccessContext _context;
        public MyCustomController(ITSDbContextOptions iTSDbContextOptions)
        {
            _context = new DBAccessContext(iTSDbContextOptions);
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return _context.CountryRep.GetAll();
        }


        //TODO:
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Country> Get(int id)
        {
            return _context.CountryRep.FirstOrDefault(id);
        }

        //TODO:
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        //TODO:
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        //TODO:
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
