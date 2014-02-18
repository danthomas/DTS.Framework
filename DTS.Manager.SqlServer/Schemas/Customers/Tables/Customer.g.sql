create table [Customers].[Customer]
(
[CustomerId] smallint NOT NULL IDENTITY (1, 1)
, [Code] varchar(5) NOT NULL
, [Name] varchar(50) NOT NULL
, CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerId])
, CONSTRAINT [AK_Customer_Code] UNIQUE ([Code])
, CONSTRAINT [Code_MinLength] CHECK (LEN([Code]) > 3)
)