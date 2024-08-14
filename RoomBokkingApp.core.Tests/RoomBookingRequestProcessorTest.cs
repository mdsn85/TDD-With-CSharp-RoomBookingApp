using System;
using RoomBokkingApp.Core.Models;
using Shouldly;
namespace RoomBokkingApp.Core
{
	public class RoomBookingRequestProcessorTest
	{ 
		[Fact]
		public void Should_Return_Booking_Response_With_Request_Values()
		{

			//arrange sample data
			var request = new RoomBookingRequest
			{
				FullName = "Test Name",
				Email = "test@gmail.com",
				Date = new DateTime(2024, 10, 20)
			};

			// arrange object of what we want to test 
			var processor = new Processors.RoomBookingRequestProcessor();

			//Act
			//call the method and try get the result
			RoomBookingResult result =  processor.BookRoom(request);


			//check that result has all the values that were in request  
			//Assert
			Assert.NotNull(result);

			Assert.Equal(request.FullName, result.FullName);
			Assert.Equal(request.Email, result.Email);
			Assert.Equal(request.Date, request.Date);


            result.ShouldNotBeNull();
			result.FullName.Equals(request.FullName);


        }

    }
}

