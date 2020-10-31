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
    public class IndexModel : PageModel
    {
        private readonly RazorPagesRoomReservations.Data.RazorPagesRoomReservationsContext _context;

        public IndexModel(RazorPagesRoomReservations.Data.RazorPagesRoomReservationsContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; }

        public async Task OnGetAsync()
        {
            Room = await _context.Room.ToListAsync();
        }
    }
}
