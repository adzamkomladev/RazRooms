using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesRoomReservations.Services;
using RazorPagesRoomReservations.Models;

namespace RazorPagesRoomReservations.Pages.Rooms
{
    public class DetailsModel : PageModel
    {
        private readonly RoomService _roomService;

        public DetailsModel(RoomService roomService) => _roomService = roomService;

        public Room Room { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                Room = await _roomService.FindOneByIdAsync(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
