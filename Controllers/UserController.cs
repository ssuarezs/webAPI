using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using newWebAPI.Context;
using newWebAPI.Models;
using newWebAPI.Services;
//using newWebAPI.Models;

namespace newWebAPI.Controllers
{
    public class TModel {}

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApiAppContext apiContext;

        public UserController(ApiAppContext context)
        {
            apiContext = context;
            apiContext.Database.EnsureCreated();
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<User>> Get()
        {
            return apiContext.Users.Include(p => p.UserRoles).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetById(string id)
        {
            Guid.TryParse(id, out var userId);

            if(userId != Guid.Empty)
            {
                var userFound = apiContext.Users.FirstOrDefault(p => p.UserId == userId);
                if(userFound != null)
                {
                    return Ok(userFound);
                }
                else
                {
                    return NotFound();
                }
            }
            else 
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task Post([FromBody] User newUser)
        {
            newUser.UserId = Guid.NewGuid();
            newUser.DateCreated = DateTime.Now;

            apiContext.Add(newUser);
            await apiContext.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] User updatedUser)
        {
            Guid.TryParse(id, out var userId);

            if(userId != Guid.Empty)
            {
                var userFound = apiContext.Users.FirstOrDefault(p => p.UserId == userId);
                if(userFound != null)
                {
                    userFound.Name = updatedUser.Name;
                    userFound.LastName = updatedUser.LastName;
                    userFound.Active = updatedUser.Active;
                }

                apiContext.Users.Update(userFound);
                await apiContext.SaveChangesAsync();
            }
        }

        [HttpDelete("{id}")]
        public async void Delete(string id)
        {
            Guid.TryParse(id, out var userId);

            if(userId != Guid.Empty)
            {
                var userFound = apiContext.Users.FirstOrDefault(p => p.UserId == userId);
                apiContext.Users.Remove(userFound);
                await apiContext.SaveChangesAsync();
            }
        }

        [HttpGet]
        [Route("GetRoles")]
        public ActionResult<IEnumerable<UserRole>> GetRoles()
        {
            return apiContext.UserRoles.Include(p => p.User).ToList();
        }
    }
}