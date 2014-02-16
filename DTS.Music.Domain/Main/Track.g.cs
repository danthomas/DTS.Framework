using System;

namespace DTS.Music.Domain.Main
{
	public class Track
	{		
		public int TrackId { get; set; }		
		public string Name { get; set; }		
		public TimeSpan Length { get; set; }
	}
}