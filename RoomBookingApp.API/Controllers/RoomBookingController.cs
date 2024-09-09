using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoomBookingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomBookingController : ControllerBase
    {
        private IRoomBookingRequestProcessor _roomBookingProcessor;

        public RoomBookingController(IRoomBookingRequestProcessor roomBookingProcessor)
        {
            _roomBookingProcessor = roomBookingProcessor;
        }
        [HttpPost]
        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _roomBookingProcessor.BookRoom(request);

                if(result.Flag == Core.BookingResultFlag.success)
                {
                    return Ok(result);
                }

                ModelState.AddModelError(nameof(request.Date), "No room available for given date");
            }

            return BadRequest(ModelState);
        }
    }
}

