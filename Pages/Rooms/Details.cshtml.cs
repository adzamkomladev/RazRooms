using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesRoomReservations.Data;
using RazorPagesRoomReservations.Models;

namespace RazorPagesRoomReservations.Pages.Rooms
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesRoomReservations.Data.RazorPagesRoomReservationsContext _context;

        public DetailsModel(RazorPagesRoomReservations.Data.RazorPagesRoomReservationsContext context)
        {
            _context = context;
        }

        public Room Room { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room = await _context.Room.FirstOrDefaultAsync(m => m.ID == id);

            if (Room == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
