﻿using System.Data.Entity;

using IoCCinema.Business.AuditLogging;
using IoCCinema.Business;
using IoCCinema.Business.Notifications;

namespace IoCCinema.DataAccess
{
    public class CinemaContext : DbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Seanse> Seanses { get; set; }
        public DbSet<SeatAssignment> SeatAssignments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
