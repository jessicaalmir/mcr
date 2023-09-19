using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcr.Data.Models;

namespace mcr.Data
{
    public interface IUnitOfWork
    {
        IRepository<int,Client> ClientRepository{ get; }
        IRepository<int, Encoder> EncoderRepository{ get; }
        IRepository<int, Event> EventRepository{ get; }
        IRepository<int, Feed> FeedRepository{ get; }
        IRepository<int, Signal> SignalRepository{ get; }
        IRepository<int, Source> SourceRepository{ get; }
        //IRepository<int, AppUser> AppUserRepository{ get; }

        public bool HasChanges();

        Task SaveAsync();
    }
}