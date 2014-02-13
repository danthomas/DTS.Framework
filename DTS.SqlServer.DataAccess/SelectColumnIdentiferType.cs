using System;

namespace DTS.SqlServer.DataAccess
{
    [Flags]
    public enum SelectColumnIdentiferType
    {
        Schema = 1,
        Object = 2,
        Name = 4,
        Alias = 8,
        ObjectName = Object | Name,
        SchemaObjectName = Schema | Object | Name,
        NameAlias = Name | Alias,
        ObjectNameAlias = Object | Name | Alias,
        SchemaObjectNameAlias = Schema | Object | Name | Alias
    }
}