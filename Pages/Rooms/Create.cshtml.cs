using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesRoomReservations.Data;
using RazorPagesRoomReservations.Models;
using RazorPagesRoomReservations.Services;

namespace RazorPagesRoomReservations.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly RazorPagesRoomReservationsContext _context;
        private readonly UploadFileService _uploadFileService;

        public CreateModel(RazorPagesRoomReservationsContext context, UploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

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

            Room.Image = await _uploadFileService.UploadFileAsync(Image, "Images", "Rooms");

            _context.Room.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
