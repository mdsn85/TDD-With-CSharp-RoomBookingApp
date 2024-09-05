using System;
using RoomBookingApp.Core.Services;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Persistence.Repositories
{
	public class RoomBookingService: IRoomBookingService
    {
        private readonly RoomBookingAppDbContext _dbContext;

        public RoomBookingService(RoomBookingAppDbContext context)
		{
            this._dbContext = context;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime date)
        {

            //var bookedRooms = _dbContext.RoomBookings
            //    .Where(q => q.Date == date).Select(q => q.RoomId)
            //    .ToList();

            //var availableRoom = _dbContext.Rooms.Where(q => bookedRooms.Contains(q.Id) == false).ToList();

            var availableRoom = _dbContext.Rooms.Where(q => q.RoomBookings.Any(x => x.Date == date)==false).ToList();
            return availableRoom;
        }

        public void Save(RoomBooking roomBooking)
        {
            _dbContext.Add(roomBooking);
            _dbContext.SaveChanges();
        }
    }
}

