using mcr.Data;
using NSubstitute;
using mcr.Data.Models;
using mcr.Business.IServices;
using mcr.Business.Services;
using System.Globalization;
using NSubstitute.ExceptionExtensions;

namespace mcr.Tests;

public class EventTests
{
    private readonly IRepository <int, Event> _eventRepository;
    private readonly IRepository <int,Encoder> _encoderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private IEventService _eventService;
    private readonly Event _correctEvent;
    private readonly Event _wrongEvent;
    private const string EVENT_SERVICE_EXCEPTION = "Event Exception Thrown";
    public EventTests()
    {
        _eventRepository = Substitute.For<IRepository<int,Event>>();
        _encoderRepository = Substitute.For<IRepository<int, Encoder>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _eventService = new EventService(_unitOfWork);
        _correctEvent = new Event(){
            Id = 1,
            Code = "PFR12345F",
            Name = "Messe de Jour",
            Duration = TimeOnly.Parse("00:30:00", CultureInfo.InvariantCulture),
            Date = DateOnly.ParseExact("31/08/2023","dd/MM/yyyy", CultureInfo.InvariantCulture),
            TxStart = TimeOnly.Parse("23:00:00", CultureInfo.InvariantCulture),
            TxEnd= TimeOnly.Parse("23:30:00", CultureInfo.InvariantCulture),
            IntTxStart = TimeOnly.Parse("22:00:00", CultureInfo.InvariantCulture),
            IntTxEnd = TimeOnly.Parse("23:30:00", CultureInfo.InvariantCulture),
            Note = "Live",
            Status = 1,
            EncoderId = 3,
            Feeds = new List<Feed>{
                new Feed(){
                    Id = 1,
                    EventId = 1,
                    SignalId = 1,
                    SourceId = 1
                }
            }
        };

        _wrongEvent = new Event(){
            Id = 1,
            Code = "XXXAB!23",
            Name = "Messe de Jour",
            Duration = TimeOnly.Parse("20:30:00", CultureInfo.InvariantCulture),
            Date = DateOnly.ParseExact("31/08/2023", "dd/MM/yyyy",CultureInfo.InvariantCulture),
            TxStart = TimeOnly.Parse("21:00:00", CultureInfo.InvariantCulture),
            TxEnd= TimeOnly.Parse("20:30:00", CultureInfo.InvariantCulture),
            IntTxStart = TimeOnly.Parse("22:00:00", CultureInfo.InvariantCulture),
            IntTxEnd = TimeOnly.Parse("19:30:00", CultureInfo.InvariantCulture),
            Note = "Wrong",
            Status = 4,
            EncoderId = 1,
            Feeds = new List<Feed>{
                new Feed(){
                    EventId = 1,
                    SignalId = 1,
                    SourceId = 1
                }
            }
        };
    }

    [Test]
    public async Task IsEventCreatedCorrectly(){
        //Arrange
        _encoderRepository.FindAsync(3).Returns(Task.FromResult(new Encoder())); 
        _eventRepository.AddAsync(_correctEvent).Returns(Task.FromResult(_correctEvent));
        _unitOfWork.EventRepository.Returns(_eventRepository);
        _unitOfWork.EncoderRepository.Returns(_encoderRepository);
        //Act
        var newEvent = await _eventService.CreateEvent(_correctEvent);
        //Assert
        Assert.That(newEvent.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
    }

    [Test]
    public async Task IsEventWrongReturningBadRequest(){
        //Arrange
        _encoderRepository.FindAsync(3).Returns(Task.FromResult(new Encoder())); 
        _eventRepository.AddAsync(_wrongEvent).Returns(Task.FromResult(_wrongEvent));
        _unitOfWork.EventRepository.Returns(_eventRepository);
        _unitOfWork.EncoderRepository.Returns(_encoderRepository);
        //Act
        var newEvent = await _eventService.CreateEvent(_wrongEvent);
        //Asert
        Assert.That(newEvent.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.BadRequest));
    }

    [Test]
    public async Task IsExceptionHandled(){
        //Arrange
        _encoderRepository.FindAsync(3).Returns(Task.FromResult(new Encoder())); 
        _eventRepository.AddAsync(_correctEvent).ThrowsAsyncForAnyArgs(new Exception(EVENT_SERVICE_EXCEPTION));
        _unitOfWork.EventRepository.Returns(_eventRepository);
        _unitOfWork.EncoderRepository.Returns(_encoderRepository);
        //Act
        var newEvent = await _eventService.CreateEvent(_correctEvent);
        //Assert
        Assert.IsTrue(newEvent.Message.Contains(EVENT_SERVICE_EXCEPTION));
    }
}