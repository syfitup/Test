USE [DBSyf]
GO

/****** Object:  Table [dbo].[Departments]    Script Date: 2020/03/16 20:19:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubDepartments](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[BusinessUnitId] [uniqueidentifier] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[CreateUser] [nvarchar](100) NULL,
	[CreateDate] [datetime] NOT NULL,
	[ModifyUser] [nvarchar](100) NULL,
	[ModifyDate] [datetime] NOT NULL,
	[KpiSequenceId] [uniqueidentifier] NULL,
	[IdeaSequenceId] [uniqueidentifier] NULL,
	[Code] [nvarchar](40) NULL,
	[MeetingScheduleId] [uniqueidentifier] NULL,
	[OwnerPersonId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SubDepartments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SubDepartments] ADD  CONSTRAINT [DF_SubDepartments_Id2]  DEFAULT (newsequentialid()) FOR [Id]
GO
