using System;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Core.Services;
using Shouldly;
using Moq;
using RoomBookingApp.Core.Domain;

namespace RoomBookingApp.Core
{
	public class RoomBookingRequestProcessorTest
	{
		private RoomBookingRequestProcessor _processor;
		private RoomBookingRequest _request;
		private Mock<IRoomBookingService> _roomBookingServiceMock;
		private object request;
		private List<Room> _availableRooms;

		public RoomBookingRequestProcessorTest()
		{


			//arrange sample data
			_request = new RoomBookingRequest
			{
				FullName = "Test Name",
				Email = "test@gmail.com",
				Date = new DateTime(2024, 10, 20)
			};
			_availableRooms = new List<Room>() { new Room()};

			_roomBookingServiceMock = new Mock<IRoomBookingService>();
			_roomBookingServiceMock.Setup(q => q.GetAvailableRooms(_request.Date))
				.Returns(_availableRooms);
			// arrange object of what we want to test 
			_processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
		}


		[Fact]
		public void Should_Return_Booking_Response_With_Request_Values()
		{


			//Act
			//call the method and try get the result
			RoomBookingResult result = _processor.BookRoom(_request);


			//check that result has all the values that were in request  
			//Assert
			Assert.NotNull(result);

			Assert.Equal(_request.FullName, result.FullName);
			Assert.Equal(_request.Email, result.Email);
			Assert.Equal(_request.Date, _request.Date);


			result.ShouldNotBeNull();
			result.FullName.Equals(_request.FullName);
			result.Email.Equals(_request.Email);
			result.Date.Equals(_request.Date);

		}


		[Fact]
		public void Should_Throw_Exception_For_Null_Request()
		{
			var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));
			//var exception2 = Assert.Throws<ArgumentNullException>(() => _processor.BookRoom(null));

			exception.ParamName.ShouldBe("bookingRequest");
		}

		[Fact]
		public void Should_Save_RoomBooking_Request()
		{
			//get value of whatever was being passed in during the test
			RoomBooking savedBooking = null;
			_roomBookingServiceMock
				.Setup(q => q.Save(It.IsAny<RoomBooking>()))
				.Callback<RoomBooking>(booking =>
				{
					savedBooking = booking;
				});

			_processor.BookRoom(_request);

			_roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Once);


			savedBooking.ShouldNotBeNull();
			savedBooking.FullName.ShouldBe(_request.FullName);
			savedBooking.Email.ShouldBe(_request.Email);
			savedBooking.Date.ShouldBe(_request.Date);


		}

		[Fact]
		public void Should_Not_Save_RoomBooking_Request_If_None_Available()
		{
			_availableRooms.Clear();
			_processor.BookRoom(_request);
            //Expected invocation on the mock should never have been performed,
            _roomBookingServiceMock.Verify(q => q.Save(It.IsAny<RoomBooking>()), Times.Never);

		}
	}
}

