using System;

namespace DTS.Manager.Domain.Security
{
	public class Role
	{		
		public short RoleId { get; set; }		
		public string Name { get; set; }		
		public bool IsExternal { get; set; }		
		public bool IsDeleted { get; set; }		
		public DateTime CreatedDateTime { get; set; }		
		public DateTime CreatedUser { get; set; }		
		public DateTime UpdatedDateTime { get; set; }		
		public DateTime UpdatedUser { get; set; }
	}
}