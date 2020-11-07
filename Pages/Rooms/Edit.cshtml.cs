using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesRoomReservations.Data;
using RazorPagesRoomReservations.Models;
using RazorPagesRoomReservations.Services;

namespace RazorPagesRoomReservations.Pages.Rooms
{
    public class EditModel : PageModel
    {
        private readonly RoomService _roomService;

        public EditModel(RoomService roomService) => _roomService = roomService;

        [BindProperty]
        public Room Room { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }


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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _roomService.UpdateAsync(Room, Image);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_roomService.Exists(Room.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
