USE [master]
GO
/****** Object:  Database [BankDatabase]    Script Date: 7/29/2018 8:17:45 PM ******/
CREATE DATABASE [BankDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BankDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BankDatabase.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'BankDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\BankDatabase_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BankDatabase] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BankDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BankDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BankDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BankDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BankDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BankDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [BankDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BankDatabase] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [BankDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BankDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BankDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BankDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BankDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BankDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BankDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BankDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BankDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BankDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BankDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BankDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BankDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BankDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BankDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BankDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BankDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BankDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [BankDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BankDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BankDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BankDatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [BankDatabase]
GO
/****** Object:  StoredProcedure [dbo].[AddingAccountDetails]    Script Date: 7/29/2018 8:17:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddingAccountDetails] (
			@AccountNumber int,
            @Balance int,
			@BranchName char(10),
			@AccountType char(10)
			) 
			AS
            insert into dbo.AccountDetails(AccountNumber,Balance,BranchName,AccountType)values(@AccountNumber,@Balance,@BranchName,@AccountType);

GO
/****** Object:  Table [dbo].[AccountDetails]    Script Date: 7/29/2018 8:17:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountDetails](
	[AccountNumber] [int] NOT NULL,
	[Balance] [int] NOT NULL,
	[BranchName] [char](10) NOT NULL,
	[AccountType] [char](10) NOT NULL,
 CONSTRAINT [PK_AccountDetails] PRIMARY KEY CLUSTERED 
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 7/29/2018 8:17:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[AccountNumber] [int] NOT NULL,
	[CustomerName] [nchar](20) NOT NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [BankDatabase] SET  READ_WRITE 
GO
