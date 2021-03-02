using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PeopleList.Model;

namespace PeopleList.Pages.PeopleList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<People> People { get; set; }

        public async Task OnGet()
        {
            People = await _db.People.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int Id)
        {
            var people = await _db.People.FindAsync(Id);

            if (people == null)
            {
                return NotFound();
            }

            _db.People.Remove(people);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
