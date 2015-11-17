using IoCCinema.Business;
using IoCCinema.Business.Notifications;
using IoCCinema.DataAccess;
using IoCCinema.DataAccess.AuditLogging;
using IoCCinema.DataAccess.Business;
using System;
using System.Web;

namespace IoCCinema.CompositionRoot
{
    public class PerRequestStore : IDisposable
    {
        public const string StoreKey = "PerRequestStore";

        public Lazy<CinemaContext> Context { get; private set; }
        public Lazy<IRoomRepository> RoomRepository { get; private set; }
        public Lazy<IUserRepository> UserRepository { get; private set; }
        public Lazy<ITemplateRepository> TemplateRepository { get; private set; }
        public Lazy<INotificationRepository> NotificationRepository { get; private set; }
        public Lazy<AuditLogger> AuditLogger { get; private set; }

        private PerRequestStore()
        {
            Context = new Lazy<CinemaContext>(() => new CinemaContext());
            RoomRepository = new Lazy<IRoomRepository>(() => new EfRoomRepository(Context.Value));
            UserRepository = new Lazy<IUserRepository>(() => new EfUserRepository(Context.Value));
            TemplateRepository = new Lazy<ITemplateRepository>(() => new EfTemplateRepository());
            NotificationRepository = new Lazy<INotificationRepository>(() => new EfNotificationRepository(Context.Value));
            AuditLogger = new Lazy<AuditLogger>(() => new AuditLogger(new ContextUserProvider(), Context.Value));
        }

        public static PerRequestStore Current
        {
            get
            {
                if (HttpContext.Current.Items[StoreKey] == null)
                {
                    HttpContext.Current.Items[StoreKey] = new PerRequestStore();
                }

                return HttpContext.Current.Items[StoreKey] as PerRequestStore;
            }
        }

        public static void DisposeCurrent()
        {
            var store = HttpContext.Current.Items[StoreKey] as PerRequestStore;
            if (store != null)
            {
                store.Dispose();
                HttpContext.Current.Items.Remove(StoreKey);
            }
        }

        public void Dispose()
        {
            if (Context.IsValueCreated)
            {
                Context.Value.Dispose();
            }
        }
    }
}