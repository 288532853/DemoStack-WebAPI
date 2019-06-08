
IF  NOT EXISTS (SELECT * FROM sys.objects 
WHERE object_id = OBJECT_ID(N'DS_User') AND type in (N'U'))
BEGIN
	CREATE TABLE [DS_User](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Email] [varchar](55)  NULL,
		[FirstName] [varchar](55)  NULL,
		[LastName] [varchar](55)  NULL,		
		[UserType] [TINYINT]  NULL,
		[Password] [varchar](55)  NULL,		
		[Phone]		[varchar](20) NULL,
		[Address]	[NVARCHAR](127)  NULL,
		[City]		[NVARCHAR](55)   NULL,
		[State]		[NVARCHAR](100)   NULL,
		[Country]	[TINYINT]  DEFAULT(0)NULL,
		[Tokan] [NVARCHAR](MAX) NULL,
		[IsActive]  [bit]  NULL DEFAULT (1),
		[CreatedBy] [INT]  NULL DEFAULT (0),
		[IsDeleted] [TINYINT]  NULL DEFAULT (0),
		[DateCreated] [datetime]  NULL DEFAULT (GETDATE()),
		[DateUpdated] [datetime] NULL,
		CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)
	) ON [PRIMARY]
END

IF NOT EXISTS (SELECT * FROM information_schema.columns WHERE table_name = 'DS_User' and column_name = 'FullName')
	BEGIN
		ALTER TABLE [DS_User]
	ADD FullName AS
			REPLACE(REPLACE(REPLACE((FirstName +' '+ COALESCE(LastName,'')) ,' ','<>'),'><',''),'<>',' ')
	PERSISTED
	END

