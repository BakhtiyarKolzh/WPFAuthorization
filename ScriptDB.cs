
//USE[AuthorizationDB]
//GO

///****** Object:  Table [dbo].[Users]    Script Date: 02.12.2022 18:49:29 ******/
//SET ANSI_NULLS ON
//GO

//SET QUOTED_IDENTIFIER ON
//GO

//CREATE TABLE [dbo].[Users] (

//    [id][int] IDENTITY(1, 1) NOT NULL,

//    [IsActivated] [bit] NOT NULL,

//    [FirstName] [nvarchar] (20) NOT NULL,

//    [LastName] [nvarchar] (20) NOT NULL,

//    [Email] [varchar] (30) NOT NULL,

//    [Password] [varchar] (30) NULL,
//	[SerialNumber][varchar] (30) NULL,
//	[Date][datetime] NOT NULL,

//    [Guid] [uniqueidentifier] NOT NULL,
//PRIMARY KEY CLUSTERED 
//(

//    [id] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY],
// CONSTRAINT[DF_Users_Email_Unique] UNIQUE NONCLUSTERED
//(
//[Email] ASC
//)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
//) ON[PRIMARY]
//GO
//ALTER TABLE [dbo].[Users] ADD DEFAULT((1)) FOR[IsActivated]
//GO
//ALTER TABLE [dbo].[Users] ADD CONSTRAINT[DF_Users_Password]  DEFAULT (floor(rand(checksum(newid()))*(((999999) - (100000)) + (1)) + (100000))) FOR[Password]
//GO
//ALTER TABLE [dbo].[Users] ADD CONSTRAINT[DF_Users_Date]  DEFAULT (getdate()) FOR[Date]
//GO
//ALTER TABLE [dbo].[Users] ADD DEFAULT(newid()) FOR[Guid]
//GO


