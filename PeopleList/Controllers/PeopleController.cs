using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleList.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleList.Controllers
{
    [Route("api/people")]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PeopleController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.People.ToList<People>() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var personFromDb = await _db.People.FirstOrDefaultAsync(u => u.Id == id);
            if (personFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.People.Remove(personFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });

        }
    }
}
