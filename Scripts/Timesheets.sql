USE [DBSyf]
GO

/****** Object:  Table [dbo].[PipelineTargets]    Script Date: 2020/03/16 19:18:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Timesheets](
	[Id] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NULL,
	[PersonName] [varchar](50) NULL,
	[TimesheetDate] [datetime] NOT NULL,
	[TimesheetEmployeeHours] [decimal](18, 6) NULL,
	[Deleted] [bit] NOT NULL,
	[CreateUser] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyUser] [nvarchar](100) NULL,
	[ModifyDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Timesheets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Timesheets] ADD  CONSTRAINT [DF_Timesheets_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO




