using System;

namespace DTS.Utilities
{
    public class ExceptionBase<TI> : Exception where TI : struct
    {
        public ExceptionBase(Exception innerException, TI identifier, string format, params object[] args)
            : base(String.Format(format, args), innerException)
        {
            Identifier = identifier;
        }

        public TI Identifier { get; private set; }
    }
}
