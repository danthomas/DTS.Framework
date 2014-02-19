using System;

namespace DTS.Manager.Domain.Scheduling
{
	public class DurationType
	{		
		public byte DurationTypeId { get; set; }		
		public string Code { get; set; }		
		public string Name { get; set; }		
		public short ConversionFactor { get; set; }
	}
}