using System;
namespace RoomBookingApp.Domain.BaseModels
{
    //no one can take instance
	public abstract class RoomBookingBase
	{
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime Date { get; set; }
    }
}

