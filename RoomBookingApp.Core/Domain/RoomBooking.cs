using System;
using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Services
{
    public class RoomBooking:RoomBookingBase
    {
        public int RoomId { get; set; }
        public int Id { get; set; }
    }
}