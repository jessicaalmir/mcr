using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcr.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace mcr.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
        private bool _disposed = false;

        private IRepository<int, Client> _clientRepository;
        private IRepository<int, Encoder> _encoderRepository;
        private IRepository<int, Event> _eventRepository;
        private IRepository<int, Feed> _feedRepository;
        private IRepository<int, Signal> _signalRepository;
        private IRepository<int, Source> _sourceRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IRepository<int, Client> ClientRepository
        {
            get
            {
                _clientRepository ??= new Repository<int, Client>(_context);
                return _clientRepository;
            }
        }

        public IRepository<int, Encoder> EncoderRepository
        {
            get
            {
                _encoderRepository ??= new Repository<int, Encoder>(_context);
                return _encoderRepository;
            }
        }

        public IRepository<int, Event> EventRepository
        {
            get
            {
                _eventRepository ??= new Repository<int, Event>(_context);
                return _eventRepository;
            }
        }

        public IRepository<int, Feed> FeedRepository
        {
            get
            {
                _feedRepository ??= new Repository<int, Feed>(_context);
                return _feedRepository;
            }
        }
        public IRepository<int, Signal> SignalRepository
        {
            get
            {
                _signalRepository ??= new Repository<int, Signal>(_context);
                return _signalRepository;
            }
        }
        public IRepository<int, Source> SourceRepository
        {
            get
            {
                _sourceRepository ??= new Repository<int, Source>(_context);
                return _sourceRepository;
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return false;
            }
        }

        #region IDisposable
        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
             try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }
    }
}