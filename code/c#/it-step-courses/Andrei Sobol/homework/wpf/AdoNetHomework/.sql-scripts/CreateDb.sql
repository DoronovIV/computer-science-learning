CREATE DATABASE [DoronovAdoNetCoreHomework]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DoronovAdoNetCoreHomework', FILENAME = N'D:\SQL Server\MSSQL15.DORONOVLOCALDB\MSSQL\DATA\DoronovAdoNetCoreHomework.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DoronovAdoNetCoreHomework_log', FILENAME = N'D:\SQL Server\MSSQL15.DORONOVLOCALDB\MSSQL\DATA\DoronovAdoNetCoreHomework_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )

ALTER DATABASE [DoronovAdoNetCoreHomework] SET COMPATIBILITY_LEVEL = 150

ALTER DATABASE [DoronovAdoNetCoreHomework] SET ANSI_NULL_DEFAULT OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET ANSI_NULLS OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET ANSI_PADDING OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET ANSI_WARNINGS OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET ARITHABORT OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET AUTO_CLOSE OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET AUTO_SHRINK OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF)

ALTER DATABASE [DoronovAdoNetCoreHomework] SET AUTO_UPDATE_STATISTICS ON 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET CURSOR_CLOSE_ON_COMMIT OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET CURSOR_DEFAULT  GLOBAL 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET CONCAT_NULL_YIELDS_NULL OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET NUMERIC_ROUNDABORT OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET QUOTED_IDENTIFIER OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET RECURSIVE_TRIGGERS OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET  DISABLE_BROKER 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET DATE_CORRELATION_OPTIMIZATION OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET PARAMETERIZATION SIMPLE 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET READ_COMMITTED_SNAPSHOT OFF 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET  READ_WRITE 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET RECOVERY FULL 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET  MULTI_USER 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET PAGE_VERIFY CHECKSUM  

ALTER DATABASE [DoronovAdoNetCoreHomework] SET TARGET_RECOVERY_TIME = 60 SECONDS 

ALTER DATABASE [DoronovAdoNetCoreHomework] SET DELAYED_DURABILITY = DISABLED 

USE [DoronovAdoNetCoreHomework]

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = Off;

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = Primary;

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = On;

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = Primary;

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = Off;

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = Primary;

USE [DoronovAdoNetCoreHomework]

IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [DoronovAdoNetCoreHomework] MODIFY FILEGROUP [PRIMARY] DEFAULT

