USE [master]
GO
/****** Object:  Database [OpenMongo]    Script Date: 1/19/2017 1:51:38 AM ******/
CREATE DATABASE [OpenMongo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OpenMongo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\OpenMongo.mdf' , SIZE = 3772416KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OpenMongo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\OpenMongo_log.ldf' , SIZE = 4632576KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OpenMongo] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OpenMongo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OpenMongo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OpenMongo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OpenMongo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OpenMongo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OpenMongo] SET ARITHABORT OFF 
GO
ALTER DATABASE [OpenMongo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OpenMongo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OpenMongo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OpenMongo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OpenMongo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OpenMongo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OpenMongo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OpenMongo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OpenMongo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OpenMongo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OpenMongo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OpenMongo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OpenMongo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OpenMongo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OpenMongo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OpenMongo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OpenMongo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OpenMongo] SET RECOVERY FULL 
GO
ALTER DATABASE [OpenMongo] SET  MULTI_USER 
GO
ALTER DATABASE [OpenMongo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OpenMongo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OpenMongo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OpenMongo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [OpenMongo] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'OpenMongo', N'ON'
GO
USE [OpenMongo]
GO
/****** Object:  User [OpenMongoUser]    Script Date: 1/19/2017 1:51:38 AM ******/
CREATE USER [OpenMongoUser] FOR LOGIN [OpenMongoUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [OpenMongoUser]
GO
/****** Object:  Table [dbo].[tblRansomRecords]    Script Date: 1/19/2017 1:51:38 AM ******/
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
/****** Object:  Table [dbo].[tblRansomSchemas]    Script Date: 1/19/2017 1:51:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblRansomSchemas](
	[DatabaseName] [varchar](500) NULL,
	[CollectionName] [varchar](500) NULL,
	[BitcoinField] [varchar](500) NULL,
	[EmailField] [varchar](500) NULL,
	[NotesField] [varchar](500) NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('PLEASE_READ','PLEASE_READ','Bitcoin Address','Email','Info')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('README_MISSING_DATABASES','README_MISSING_DATABASES','NONE','mail','note')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('READ1','info','NONE','mail','text')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('WARNING','WARNING','NONE','email','note')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('ENCRYPTED','READ_ME','NONE','NONE','WARNING')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('PLEASE_READ_ME','PLEASE_READ_ME','Bitcoin Address','Email','info')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('README_YOU_DB_IS_INSECURE','README_YOU_DB_IS_INSECURE','BTC_ADDRESS','EMAIL','NOTE')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('README','bitcoin','Bitcoin Address','email','message')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('CONTACTME','CONTACTME','NONE','mail','note')
INSERT INTO [dbo].[tblRansomSchemas] (DatabaseName, CollectionName, BitcoinField, EmailField, NotesField)
VALUES('PWNED_SECURE_YOUR_STUFF_SILLY','pwned','btcAddress','email','note')


GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblShodanRecords]    Script Date: 1/19/2017 1:51:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblShodanRecords](
	[IP] [varchar](50) NULL,
	[Port] [varchar](10) NULL,
	[Banner] [varchar](max) NULL,
	[Timestamp] [varchar](50) NULL,
	[Hostnames] [varchar](200) NULL,
	[Country] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[OperatingSystem] [varchar](25) NULL,
	[Organization] [varchar](100) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[AddRansomDemand]    Script Date: 1/19/2017 1:51:38 AM ******/
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
/****** Object:  StoredProcedure [dbo].[AddRansomSchema]    Script Date: 1/19/2017 1:51:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddRansomSchema] @DatabaseName   VARCHAR(500), 
                                 @CollectionName VARCHAR(500), 
                                 @BitcoinField   VARCHAR(500), 
                                 @EmailField     VARCHAR(500), 
                                 @NotesField     VARCHAR(500),
                                 @ID             int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF ((SELECT COUNT(ID) FROM tblRansomSchemas WHERE ID=@ID) < 1) 
        INSERT INTO tblransomschemas 
                    (databasename, 
                     collectionname, 
                     bitcoinfield, 
                     emailfield, 
                     notesfield) 
        VALUES     (@DatabaseName, 
                    @CollectionName, 
                    @BitcoinField, 
                    @EmailField, 
                    @NotesField);
	ELSE
		UPDATE 
			tblRansomSchemas 
		SET
			DatabaseName = @DatabaseName,
			CollectionName = @CollectionName,
			EmailField = @EmailField,
			BitcoinField = @BitcoinField,
			NotesField = @NotesField
		WHERE
			ID = @ID;
END

GO
/****** Object:  StoredProcedure [dbo].[AddShodanRecord]    Script Date: 1/19/2017 1:51:38 AM ******/
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
/****** Object:  StoredProcedure [dbo].[CheckIfMongoInstanceExists]    Script Date: 1/19/2017 1:51:38 AM ******/
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
/****** Object:  StoredProcedure [dbo].[CheckIfRansomDemandExists]    Script Date: 1/19/2017 1:51:38 AM ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteRansomSchema]    Script Date: 1/19/2017 1:51:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteRansomSchema] @ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM 
		tblRansomSchemas 
    WHERE
		[ID] = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllIps]    Script Date: 1/19/2017 1:51:38 AM ******/
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
	ORDER BY IP DESC
END


GO
/****** Object:  StoredProcedure [dbo].[GetIpsAndPorts]    Script Date: 1/19/2017 1:51:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetIpsAndPorts] 
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Ip, Port FROM tblShodanRecords ORDER BY Ip;
END

GO
/****** Object:  StoredProcedure [dbo].[GetRansomSchemas]    Script Date: 1/19/2017 1:51:38 AM ******/
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
		NotesField,
		ID
	FROM
		tblRansomSchemas
	ORDER BY
		DatabaseName,CollectionName, BitcoinField, EmailField, NotesField
END

GO
USE [master]
GO
ALTER DATABASE [OpenMongo] SET  READ_WRITE 
GO

