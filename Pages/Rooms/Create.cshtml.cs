using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesRoomReservations.Data;
using RazorPagesRoomReservations.Models;

namespace RazorPagesRoomReservations.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesRoomReservations.Data.RazorPagesRoomReservationsContext _context;

        public CreateModel(RazorPagesRoomReservations.Data.RazorPagesRoomReservationsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Room.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
