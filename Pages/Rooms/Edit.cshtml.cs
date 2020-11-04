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
        private readonly RazorPagesRoomReservationsContext _context;
        private readonly UploadFileService _uploadFileService;

        public EditModel(RazorPagesRoomReservationsContext context, UploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

        [BindProperty]
        public Room Room { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }


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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var room = await _context.Room.Where(m => m.ID == Room.ID)
                                          .Select(m => new Room { Image = m.Image })
                                          .FirstOrDefaultAsync();

            if (room == null)
            {
                return NotFound();
            }

            if (Image != null)
            {
                Room.Image = await _uploadFileService.UploadFileAsync(Image, "Images", "Rooms");
            }
            else
            {
                Room.Image = room.Image;
            }

            _context.Attach(Room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                if (Image != null && room.Image != null)
                {
                    _uploadFileService.DeleteFile(room.Image);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(Room.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RoomExists(int id)
        {
            return _context.Room.Any(e => e.ID == id);
        }
    }
}
