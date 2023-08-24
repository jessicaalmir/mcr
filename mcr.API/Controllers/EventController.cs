using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcr.Business.IServices;
using mcr.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace mcr.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [Route("GetEvents")]
        public async Task<IActionResult> GetEvents(bool areReferences){
            var events = await _eventService.GetAllEventsAsync(areReferences);
            return Ok(events);
        }

        [HttpGet]
        [Route("GetEventById")]
        public async Task<IActionResult> GetById(int id){
            var events = await _eventService.GetById(id);
            return Ok(events);
        }

        [HttpGet]
        [Route("GetEventsByDate")]
        public async Task<IActionResult> GetEventsByDate(DateOnly date){
            var events = await _eventService.GetEventsByDate(date);
            return Ok(events);
        }

        [HttpPost]
        [Route("CreateEvent")]
        public async Task<IActionResult> CreateEvent(Event newEvent){

            Console.WriteLine( newEvent);
            var result = await _eventService.CreateEvent(newEvent);
            return Ok(result);
        }
    }
}