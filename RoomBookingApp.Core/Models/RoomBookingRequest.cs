﻿using RoomBookingApp.Domain.BaseModels;

namespace RoomBookingApp.Core.Models
{
    public class RoomBookingRequest:RoomBookingBase
    {
        public int RoomId { get; set; }

    }
}