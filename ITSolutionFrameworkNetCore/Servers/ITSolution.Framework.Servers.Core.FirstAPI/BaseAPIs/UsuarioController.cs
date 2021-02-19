using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSolution.Framework.Core.BaseClasses.Identity;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using ITSolution.Framework.Servers.Core.FirstAPI.Data;
using ITSolution.Framework.Servers.Core.FirstAPI.Model;
using ITSolution.Framework.Servers.Core.FirstAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ITSolution.Framework.Servers.Core.FirstAPI.BaseAPIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DbAccessContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsuarioController(ItsDbContextOptions iTsDbContextOptions, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = new DbAccessContext(iTsDbContextOptions);
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return _context.UsuarioRep.GetAll();  //_userManager.Users.AsEnumerable();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Usuario Get(int id)
        {
            return _context.UsuarioRep.FirstOrDefault(id);
        }

        // POST api/values
        [HttpPost]
        public void CreateUser([FromBody] Usuario value)
        {
            _context.UsuarioRep.Create(value, true);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario value)
        {
            _context.UsuarioRep.Create(value, true);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Usuario del = _context.UsuarioRep.FirstOrDefault(id);
            _context.UsuarioRep.Delete(del);
        }
    }
}
