using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PeopleList.Model;

namespace PeopleList.Pages.PeopleList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public People People { get; set; }

        public async Task OnGet(int Id)
        {
            People = await _db.People.FindAsync(Id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var PeopleFromDb = await _db.People.FindAsync(People.Id);
                PeopleFromDb.Name = People.Name;
                PeopleFromDb.Number = People.Number;
                PeopleFromDb.Email = People.Email;
                PeopleFromDb.Address = People.Address;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
