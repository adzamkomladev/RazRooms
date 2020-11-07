using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesRoomReservations.Models;
using RazorPagesRoomReservations.Services;

namespace RazorPagesRoomReservations.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly RoomService _roomService;

        public IndexModel(RoomService roomService) => _roomService = roomService;

        public IList<Room> Room { get;set; }

        public async Task OnGetAsync() => Room = await _roomService.FindAllAsync();
    }
}
