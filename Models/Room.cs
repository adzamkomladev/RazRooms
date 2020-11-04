using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesRoomReservations.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberOfOccupants { get; set; }
        public string Condition { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}