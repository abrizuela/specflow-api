using System.Collections.Generic;

namespace SpecFlowAPI
{
	public class PlaylistReponseModel
	{
		public string Href { get; set; } = string.Empty;
		public IList<PlaylistModel>? Items { get; set; }
		public int Total { get; set; }
	}
}
