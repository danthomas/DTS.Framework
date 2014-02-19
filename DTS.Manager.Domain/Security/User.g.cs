using System;

namespace DTS.Manager.Domain.Security
{
	public class User
	{		
		public short UserId { get; set; }		
		public string Username { get; set; }		
		public string Email { get; set; }		
		public string FirstName { get; set; }		
		public string MiddleName { get; set; }		
		public string LastName { get; set; }		
		public string PreferredName { get; set; }		
		public bool IsActive { get; set; }		
		public bool IsDeleted { get; set; }		
		public DateTime CreatedDateTime { get; set; }		
		public DateTime CreatedUser { get; set; }		
		public DateTime UpdatedDateTime { get; set; }		
		public DateTime UpdatedUser { get; set; }
	}
}