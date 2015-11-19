using IoCCinema.Presentation;
using System.Web.Mvc;

namespace IoCCinema.Controllers
{
    public class AuditController : Controller
    {
        private readonly IAuditViewRepository _auditRepository;

        public AuditController(IAuditViewRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var data = _auditRepository.GetAuditEntriesForUser(int.Parse(User.Identity.Name));
            return View(data);
        }
    }
}