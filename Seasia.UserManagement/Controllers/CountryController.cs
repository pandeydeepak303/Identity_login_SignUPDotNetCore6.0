using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seasia.UserManagement.MigrationProject;
using Seasia.UserManagement.MigrationProject.Entitites;

namespace Seasia.UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly SeasiaUserContext _context;

        public CountryController(SeasiaUserContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        //{
        //    return await _context.Countries.ToListAsync();
        //}

    }
}
