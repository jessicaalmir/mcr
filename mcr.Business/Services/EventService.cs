using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using mcr.Business.IServices;
using mcr.Data;
using mcr.Data.DTO;
using mcr.Data.Models;

namespace mcr.Business.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseMessage<Event>> CreateEvent(Event newEvent)
        {
            var _newEvent = new Event
            {
                Code = newEvent.Code,
                Name = newEvent.Name,
                Duration = newEvent.Duration,
                Date = newEvent.Date,
                TxStart = newEvent.TxStart,
                TxEnd = newEvent.TxEnd,
                IntTxStart = newEvent.IntTxStart,
                IntTxEnd = newEvent.IntTxEnd,
                Note = newEvent.Note,
                Status = 1,
                EncoderId = newEvent.EncoderId,
                Feeds = new List<Feed>()
            };

            try
            {
                var encoder = await _unitOfWork.EncoderRepository.FindAsync(newEvent.EncoderId);
                if(encoder==null){
                    return Utilities.BuildResponse<Event>(HttpStatusCode.BadRequest, BaseMessageStatus.BAD_REQUEST_400);
                } 
                await _unitOfWork.EventRepository.AddAsync(_newEvent);
                foreach(var feed in newEvent.Feeds){
                   var newFeed = new Feed{
                    SourceId = feed.SourceId,
                    SignalId = feed.SignalId,
                    EventId = _newEvent.Id
                   };                    
                    _newEvent.Feeds.Add(newFeed);
                    await _unitOfWork.FeedRepository.AddAsync(newFeed);
                }
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return Utilities.BuildResponse<Event>(HttpStatusCode.InternalServerError, $"{BaseMessageStatus.INTERNAL_SERVER_ERROR_500} | {ex.Message}");
            }
            return Utilities.BuildResponse(HttpStatusCode.OK, BaseMessageStatus.OK_200, new List<Event>() { newEvent });
        }

        public async Task<Event> DeleteEvent(int Id)
        {

            var _event = await _unitOfWork.EventRepository.FindAsync(Id);
            _event.Status = 2;
            await _unitOfWork.EventRepository.Update(_event);
            return _event;
        }

        public async Task<IEnumerable<Event>> GetEventsByDate(DateOnly date)
        {
            return await _unitOfWork.EventRepository.GetAllAsync(
                new List<Expression<Func<Event, object>>>
                    {
                        e => e.Feeds // Include the Feeds navigation property
                    },x => x.Date==date);
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync(bool areReferences)
        {
            IEnumerable<Event> events;
            if(areReferences)
            {
                events = await _unitOfWork.EventRepository.GetAllAsync(
                    new List<Expression<Func<Event, object>>>
                    {
                        e => e.Feeds // Include the Feeds navigation property
                    }, 
                    null,
                    x=>x.OrderBy(x=>x.Id),
                    new Encoder().GetType().Name);
            }else{
                events = await _unitOfWork.EventRepository.GetAllAsync(); 
            }
           return events;
        }

        public async Task<Event> GetById(int Id)
        {
            return await _unitOfWork.EventRepository.FindAsync(Id);
        }

        public async Task<Event> UpdateEvent(Event updatedEvent)
        {
            await _unitOfWork.EventRepository.Update(updatedEvent);
            await _unitOfWork.SaveAsync();
            return updatedEvent;
        }
    }
}