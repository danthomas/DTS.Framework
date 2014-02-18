using System;

namespace DTS.Manager.Domain.Companies
{
	public class Company
	{		
		public short CompanyId { get; set; }		
		public string Code { get; set; }		
		public bool IsInternal { get; set; }		
		public string Name { get; set; }		
		public bool IsActive { get; set; }		
		public bool IsDeleted { get; set; }		
		public DateTime CreatedDateTime { get; set; }		
		public DateTime CreatedUser { get; set; }		
		public DateTime UpdatedDateTime { get; set; }		
		public DateTime UpdatedUser { get; set; }
	}
}