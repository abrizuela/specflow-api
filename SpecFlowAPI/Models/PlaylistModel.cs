namespace SpecFlowAPI
{
	public class PlaylistModel
	{
		public bool Collaborative { get; set; }
		public string Description { get; set; } = string.Empty;
		public string Href { get; set; } = string.Empty;
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public bool Public { get; set; }
	}
}
