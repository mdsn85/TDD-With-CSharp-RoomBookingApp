using System;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Domain;
using RoomBookingApp.Persistence.Repositories;

namespace RoomBookingApp.Persistence.Test
{
	public class RoomBookingServiceTest
	{
		public RoomBookingServiceTest()
		{
		}

		[Fact]
		public void Should_return_available_rooms()
		{
			//Arrange
			var date = new DateTime(2021, 06, 09);

            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>()
                .UseInMemoryDatabase("AvailableRoomTests")
                .Options;

            using var context = new RoomBookingAppDbContext(dbOptions);
			
				context.Add(new Room{Id = 1, Name = "Room 1" });
                context.Add(new Room { Id = 2, Name = "Room 2" });
                context.Add(new Room { Id = 3, Name = "Room 3" });

                context.Add(new RoomBooking { RoomId = 1, Date = date, Email = "aa@aa.com", FullName= "aa aa" });
                context.Add(new RoomBooking { RoomId = 2, Date = date.AddDays(-1), Email = "bb@bb.com", FullName = "bb bb" });

				context.SaveChanges();

				var roomBookingService = new RoomBookingService(context);

				//Act
				var availableRooms = roomBookingService.GetAvailableRooms(date);

				//Assert
				Assert.Equal(2, availableRooms.Count());

				Assert.Contains(availableRooms, q => q.Id == 2);
				Assert.Contains(availableRooms, q => q.Id == 3);
				Assert.DoesNotContain(availableRooms, q => q.Id == 1);
		}

		[Fact]
		public void Should_save_room_booking()
		{
            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>()
				.UseInMemoryDatabase("ShouldSaveTest")
				.Options;

			var roomBooking = new RoomBooking { RoomId = 1, Date = new DateTime(2021, 06, 09), Email = "aa@aa.com", FullName = "aa aa" };
            using var context = new RoomBookingAppDbContext(dbOptions);
			var roomBookingService = new RoomBookingService(context);
			roomBookingService.Save(roomBooking);

			var bookings = context.RoomBookings.ToList();

			var savedBooking = Assert.Single(bookings);
			Assert.Equal(roomBooking.RoomId, savedBooking.RoomId);
			Assert.Equal(roomBooking.Date, savedBooking.Date);



        }

    }
}

