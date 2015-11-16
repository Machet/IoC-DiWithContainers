namespace IoCCinema.Business
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string MobilePhone { get; set; }
        public bool ContactByEmailAllowed { get; set; }
        public bool ContactBySmslAllowed { get; set; }
    }
}
