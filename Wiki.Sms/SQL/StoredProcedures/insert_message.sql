USE [SmsReader]
GO
/****** Object:  StoredProcedure [dbo].[insert_message]    Script Date: 23.01.2017 18:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE  [dbo].[insert_message] 
	@Number nvarchar(50),   
    @Text nvarchar(256),
	@SendDate datetime,
	@ReceiveDate datetime,
	@CreateDate datetime,
	@Direction tinyint,
	@Status tinyint
AS
BEGIN
	SET NOCOUNT ON;
INSERT INTO [dbo].[SmsMessage]
           (
		    [Number]
           ,[Text]
           ,[SendDate]
           ,[ReceiveDate]
		   ,[CreateDate]
		   ,[Direction]
		   ,[Status] )
     VALUES(
           @Number, 
           @Text,
           @SendDate, 
           @ReceiveDate,
		   @CreateDate,
		   @Direction,
		   @Status)
END
