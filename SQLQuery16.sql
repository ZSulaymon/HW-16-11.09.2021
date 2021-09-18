USE [AcademyHW]
GO

/****** Object:  Table [dbo].[Person]    Script Date: 18.09.2021 23:27:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NOT NULL
) ON [PRIMARY]
GO


