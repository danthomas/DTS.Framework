using System;

namespace DTS.Manager.Domain.Scheduling
{
	public class Calendar
	{		
		public short CalendarId { get; set; }		
		public string Name { get; set; }		
		public bool IsActive { get; set; }		
		public bool IsDeleted { get; set; }		
		public DateTime CreatedDateTime { get; set; }		
		public DateTime CreatedUser { get; set; }		
		public DateTime UpdatedDateTime { get; set; }		
		public DateTime UpdatedUser { get; set; }
	}
}