using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RazorPagesRoomReservations.Data;
using RazorPagesRoomReservations.Models;

namespace RazorPagesRoomReservations.Services
{
    public class RoomService
    {
        private readonly RazorPagesRoomReservationsContext _context;
        private readonly UploadFileService _uploadFileService;

        public RoomService(
            RazorPagesRoomReservationsContext context,
            UploadFileService uploadFileService)
        {
            _context = context;
            _uploadFileService = uploadFileService;
        }

        public Task<List<Room>> FindAllAsync() => _context.Room.ToListAsync();

        public bool Exists(int id) => _context.Room.Any(e => e.ID == id);

        public async Task<Room> FindOneByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            var room = await _context.Room.FirstOrDefaultAsync(m => m.ID == id);

            if (room == null)
            {
                throw new NullReferenceException();
            }

            return room;
        }

        public async Task DeleteOneById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            var room = await FindOneByIdAsync(id);

            if (room != null)
            {
                _context.Room.Remove(room);
                await _context.SaveChangesAsync();
                _uploadFileService.DeleteFile(room.Image);
            }
        }

        public async Task CreateAsync(Room room, IFormFile image)
        {
            room.Image = await _uploadFileService.UploadFileAsync(image, "Images", "Rooms");

            _context.Room.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Room room, IFormFile image)
        {
            var currentRoom = await _context.Room.Where(m => m.ID == room.ID)
                                          .Select(m => new Room { Image = m.Image })
                                          .FirstOrDefaultAsync();

            if (currentRoom == null)
            {
                throw new NullReferenceException();
            }

            if (image != null)
            {
                room.Image = await _uploadFileService.UploadFileAsync(image, "Images", "Rooms");
            }
            else
            {
                room.Image = currentRoom.Image;
            }

            _context.Attach(room).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            if (image != null && currentRoom.Image != null)
            {
                _uploadFileService.DeleteFile(currentRoom.Image);
            }
        }

    }
}