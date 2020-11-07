using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesRoomReservations.Models;
using RazorPagesRoomReservations.Services;

namespace RazorPagesRoomReservations.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly RoomService _roomService;

        public CreateModel(RoomService roomService) => _roomService = roomService;

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _roomService.CreateAsync(Room, Image);
            return RedirectToPage("./Index");
        }
    }
}
