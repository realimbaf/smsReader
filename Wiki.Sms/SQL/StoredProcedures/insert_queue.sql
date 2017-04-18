USE [SmsReader]
GO
/****** Object:  StoredProcedure [dbo].[insert_queue]    Script Date: 23.01.2017 18:06:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE  [dbo].[insert_queue] 
	@Number nvarchar(50),   
    @Text nvarchar(256),
	@CreateDate datetime
AS
BEGIN
	SET NOCOUNT ON;

INSERT INTO [dbo].[SmsOutputQueue]
           ([Number]
           ,[Text]
           ,[CreateDate])
     VALUES(
           @Number ,   
		   @Text,
           @CreateDate)
END
