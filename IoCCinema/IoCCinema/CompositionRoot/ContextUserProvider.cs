using IoCCinema.Business.Authentication;
using System.Web;

namespace IoCCinema.CompositionRoot
{
    public class ContextUserProvider : ICurrentUserProvider
    {
        private int? _id;

        public int? GetUserId()
        {
            return !string.IsNullOrEmpty(StringId)
                ? int.Parse(StringId)
                : _id;
        }

        public void SetUserId(int id)
        {
            if (string.IsNullOrEmpty(StringId))
            {
                _id = id;
            }
        }

        private string StringId
        {
            get { return HttpContext.Current.User.Identity.Name; }
        }
    }
}