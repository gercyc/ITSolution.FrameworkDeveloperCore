﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using ITSolution.Framework.Server.Core.BaseClasses.Repository;
using ITSolution.Framework.Servers.Core.FirstAPI.Data;
using ITSolution.Framework.Servers.Core.FirstAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<Menu> Get()
        {
            return _context.MenuRep.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Menu Get(int id)
        {
            return _context.MenuRep.FirstOrDefault(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Menu value)
        {
            _context.MenuRep.Create(value, true);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Menu value)
        {
            _context.MenuRep.Create(value, true);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Menu del = _context.MenuRep.FirstOrDefault(id);
            _context.MenuRep.Delete(del);
        }
    }
}
