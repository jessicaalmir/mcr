using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcr.Data.DTO;
using mcr.Data.Models;

namespace mcr.Business.IServices
{
    public interface IEventService
    {
        /// <summary>
        /// Creates a new Event <see cref="Event"/> entity in the database
        /// </summary>
        /// <param name="newEvent"></param> A new Event entity to be created
        /// <returns>The new Event created with an unique Id</returns> 
        Task<BaseMessage<Event>> CreateEvent(Event newEvent);

        /// <summary>
        /// Updates the <see cref="Event"/> entity in the database
        /// </summary>
        /// <param name="updatedEvent"></param> The Event to update
        /// <returns>The new Event with the updated information </returns> 
        Task<Event> UpdateEvent(Event updatedEvent);

        /// <summary>
        /// Get a Event by its Id
        /// </summary>
        /// <param name="Id"></param> The Event Id to be consulted
        /// <returns>A <see cref="Event"/></returns>
        Task<Event> GetById(int Id);
   
        /// <summary>
        /// Get all of the Events from the database
        /// </summary>
        /// <param name="areReferences">Returns associate entities
        /// <returns>A <see cref="List"/> of <see cref="Client"/></returns>
        Task<IEnumerable<Event>> GetAllEventsAsync(bool areReferences);
    
         /// <summary>
        /// Get all of the Events in the database in a specific date
        /// </summary>
        /// <param name="date">Date of the Events
        /// <returns>A <see cref="List"/></returns>
        Task<IEnumerable<Event>> GetEventsByDate(DateOnly date);

        /// <summary>
        /// Delete an Event from the database
        /// </summary>
        /// <param name="Id"></param>Event Id to be deleted
        /// <returns>The Event logically deleted from the database</returns>
        Task<Event> DeleteEvent(int Id);
    }
}