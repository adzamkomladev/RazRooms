using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesRoomReservations.Services;
using RazorPagesRoomReservations.Models;

namespace RazorPagesRoomReservations.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        private readonly RoomService _roomService;

        public DeleteModel(RoomService roomService) => _roomService = roomService;

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                await _roomService.DeleteOneById(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
