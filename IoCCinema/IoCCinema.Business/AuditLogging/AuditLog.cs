using System;

namespace IoCCinema.Business.AuditLogging
{
	public class AuditLog
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public DateTime ChangeTime { get; set; }
		public string Changes { get; set; }
	}
}
