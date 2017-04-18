USE [SmsReader]
GO
/****** Object:  StoredProcedure [dbo].[transfer_queue_to_messages]    Script Date: 23.01.2017 16:14:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE  [dbo].[transfer_queue_to_messages]  
@smsId int
AS
BEGIN
INSERT INTO [dbo].SmsMessage
           ([ID_Sms]
		   ,[Number]
           ,[Text]
           ,[SendDate]
		   ,[CreateDate]
		   ,[Direction]
		   ,[Status] )
      SELECT 
	    NULL
		,[Number]
		,[Text]
		,GETDATE()
		,[CreateDate]
		,2
		,2
		FROM [dbo].SmsOutputQueue
		where id=@smsId
					  	 

delete FROM [dbo].SmsOutputQueue where id=@smsId

END 

