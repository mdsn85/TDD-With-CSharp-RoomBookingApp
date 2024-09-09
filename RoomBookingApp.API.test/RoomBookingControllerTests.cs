using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RoomBookingApp.API.Controllers;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using Shouldly;

namespace RoomBookingApp.API.test
{
	public class RoomBookingControllerTests
	{
		private Mock<IRoomBookingRequestProcessor> _roomBookingProcessor;
		private RoomBookingController _controller;
		private RoomBookingRequest _request;
		private RoomBookingResult _result;

		public RoomBookingControllerTests()
		{

			_roomBookingProcessor = new Mock<IRoomBookingRequestProcessor>();
			_controller = new RoomBookingController(_roomBookingProcessor.Object);
			_request = new RoomBookingRequest();
			_result = new RoomBookingResult();

			_roomBookingProcessor.Setup(x => x.BookRoom(_request)).Returns(_result);

		}

		[Theory]
		[InlineData(1,true,typeof (OkObjectResult), Core.BookingResultFlag.success)]
		[InlineData(0,false,typeof(BadRequestObjectResult), Core.BookingResultFlag.failur)]
        public async void Should_call_booking_method_when_valid(int expectedMethodCalls,
			bool isModelValid, Type excpectedActionResultType, Core.BookingResultFlag? bookingrequestflag)
		{
			//Arrange
			if (!isModelValid)
			{
				_controller.ModelState.AddModelError("Key", "Error Message");
			}


				_result.Flag = (Core.BookingResultFlag)bookingrequestflag;

			//Act
			var result = await _controller.BookRoom(_request);


			//Assert
			result.ShouldBeOfType(excpectedActionResultType);
			_roomBookingProcessor.Verify(r => r.BookRoom(_request), Times.Exactly(expectedMethodCalls));

		}


    }
}

