USE [SmsReader]
GO

/****** Object:  Table [dbo].[Message]    Script Date: 19.01.2017 12:19:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](256) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[ReceiveDate] [datetime2](7) NOT NULL,
	[DateOfAddition] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


USE [SmsReader]
GO

/****** Object:  Table [dbo].[SmsOutputQueue]    Script Date: 23.01.2017 15:45:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SmsOutputQueue](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SmsOutput] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


