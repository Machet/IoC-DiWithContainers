using System;
using System.Collections.Generic;

namespace IoCCinema.Business
{
	public class Movie
	{
		public int MovieId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public TimeSpan Length { get; set; }
		public virtual List<Seanse> Seanses { get; set; }
	}
}
