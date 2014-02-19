using System;

namespace DTS.Manager.Domain.Scheduling
{
	public class EntryType
	{		
		public byte EntryTypeId { get; set; }		
		public string Code { get; set; }		
		public string Name { get; set; }		
		public short ConversionFactor { get; set; }
	}
}