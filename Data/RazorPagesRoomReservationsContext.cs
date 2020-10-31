using Microsoft.EntityFrameworkCore;
using RazorPagesRoomReservations.Models;

namespace RazorPagesRoomReservations.Data
{
    public class RazorPagesRoomReservationsContext : DbContext
    {
        public RazorPagesRoomReservationsContext(
            DbContextOptions<RazorPagesRoomReservationsContext> options
            ) : base(options)
        {
        }

        public DbSet<Room> Room { get; set; }
    }
}