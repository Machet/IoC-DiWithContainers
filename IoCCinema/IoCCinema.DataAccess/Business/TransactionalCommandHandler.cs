﻿using IoCCinema.Business.Commands;

namespace IoCCinema.DataAccess.Business
{
    public class TransactionalCommandHandler<T> : ICommandHandler<T> where T : ICommand
    {
        private ICommandHandler<T> _innerHandler;
        private CinemaContext _context;

        public TransactionalCommandHandler(CinemaContext context, ICommandHandler<T> handler)
        {
            _context = context;
            _innerHandler = handler;
        }

        public void Handle(T command)
        {
            _innerHandler.Handle(command);
            _context.SaveChanges();
        }
    }
}
