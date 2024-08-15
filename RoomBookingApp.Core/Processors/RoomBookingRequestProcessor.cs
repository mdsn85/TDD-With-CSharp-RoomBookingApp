using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Services;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            if (bookingRequest == null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);
            if (availableRooms.Any())
            {
                _roomBookingService.Save(CreateRoomBookingObject<RoomBooking>(bookingRequest));
            }


            return CreateRoomBookingObject<RoomBookingResult>(bookingRequest);
        }

        private static T CreateRoomBookingObject<T>(RoomBookingRequest bookingRequest)
            where T : RoomBookingBase, new()
        {
            return new T
            {
                FullName = bookingRequest.FullName,
                Email = bookingRequest.Email,
                Date = bookingRequest.Date
            };
        }
    }
}