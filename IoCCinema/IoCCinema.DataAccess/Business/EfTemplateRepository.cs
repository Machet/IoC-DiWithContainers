using IoCCinema.Business.Notifications;

namespace IoCCinema.DataAccess.Business
{
    public class EfTemplateRepository : ITemplateRepository
    {
        public string GetHtmlTemplate()
        {
            return "You have reserved seat <b>{1}</b> in row <b>{0}</b>";
        }

        public string GetPlainTextTemplate()
        {
            return "You have reserved seat {1} in row {0}";
        }
    }
}
