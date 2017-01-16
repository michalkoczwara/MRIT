USE [OpenMongo]
GO
/****** Object:  Table [dbo].[tblRansomRecords]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblRansomRecords](
	[IP] [varchar](500) NULL,
	[Port] [varchar](20) NULL,
	[DatabaseName] [varchar](200) NULL,
	[Email] [varchar](500) NULL,
	[BitcoinWallet] [varchar](500) NULL,
	[RansomNote] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblRansomSchemas]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRansomSchemas](
	[DatabaseName] [nvarchar](500) NULL,
	[CollectionName] [nvarchar](500) NULL,
	[BitcoinField] [nvarchar](500) NULL,
	[EmailField] [nvarchar](500) NULL,
	[NotesField] [nvarchar](500) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblShodanRecords]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblShodanRecords](
	[IP] [nvarchar](500) NULL,
	[Port] [nvarchar](20) NULL,
	[Banner] [nvarchar](max) NULL,
	[Timestamp] [nvarchar](500) NULL,
	[Hostnames] [nvarchar](max) NULL,
	[Country] [nvarchar](500) NULL,
	[City] [nvarchar](500) NULL,
	[OperatingSystem] [nvarchar](500) NULL,
	[Organization] [nvarchar](500) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[AddRansomDemand]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddRansomDemand]
	-- Add the parameters for the stored procedure here
	@IP varchar(500),
	@Port varchar(20),
	@DatabaseName varchar(200),
	@Email varchar(500),
	@BitcoinWallet varchar(500),
	@RansomNote varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO tblRansomRecords(IP, Port, DatabaseName, Email, BitcoinWallet, RansomNote)
	VALUES(@IP, @Port, @DatabaseName, @Email, @BitcoinWallet, @RansomNote)
		
END

GO
/****** Object:  StoredProcedure [dbo].[AddShodanRecord]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddShodanRecord]
	@Ip nvarchar(500), 
	@Port nvarchar(20), 
	@Banner nvarchar(MAX), 
	@Timestamp nvarchar(500), 
	@Hostnames nvarchar(MAX), 
	@Country nvarchar(500), 
	@City nvarchar(500),
    @OperatingSystem nvarchar(500), 
	@Organization nvarchar(500)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO 
		tblShodanRecords([IP],[Port],[Banner],[Timestamp],[Hostnames],[Country],[City],[OperatingSystem],[Organization]) 
	VALUES(@Ip,@Port,@Banner,@Timestamp,@Hostnames,@Country,@City,@OperatingSystem,@Organization);
END

GO
/****** Object:  StoredProcedure [dbo].[CheckIfMongoInstanceExists]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckIfMongoInstanceExists] 
	-- Add the parameters for the stored procedure here
	@Ip varchar(500),
	@Port varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT COUNT(IP) FROM tblShodanRecords WHERE IP = @Ip AND Port = @Port;
END

GO
/****** Object:  StoredProcedure [dbo].[CheckIfRansomDemandExists]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CheckIfRansomDemandExists]
	-- Add the parameters for the stored procedure here
	@IP varchar(500) NULL,
	@Port varchar(20) NULL,
	@DatabaseName varchar(200) NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		COUNT(IP) 
	FROM
		tblRansomRecords
	WHERE
		IP = @IP
		AND Port = @Port
		AND DatabaseName = @DatabaseName
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllIps]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllIps]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		IP, Port
	FROM
		tblShodanRecords
	WHERE
		IP in (SELECT DISTINCT(IP) FROM tblDatabases)
	ORDER BY IP DESC
END


GO
/****** Object:  StoredProcedure [dbo].[GetRansomSchemas]    Script Date: 1/16/2017 4:12:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRansomSchemas]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		DatabaseName,
		CollectionName,
		BitcoinField,
		EmailField,
		NotesField
	FROM
		tblRansomSchemas
	ORDER BY
		DatabaseName,CollectionName
END

GO
