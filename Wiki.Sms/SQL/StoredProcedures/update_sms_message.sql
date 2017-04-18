USE [SmsReader]
GO
/****** Object:  StoredProcedure [dbo].[update_sms_message]    Script Date: 23.01.2017 17:14:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE  [dbo].[update_sms_message]  
@number nvarchar(50)
AS
BEGIN
UPDATE dbo.SmsMessage
		SET [Status]=3,
			[ReceiveDate] = GETDATE()
where id in		
(
	Select top(1) id 
	From dbo.SmsMessage 
	where ReceiveDate is Null and Direction=2 and Status=2 and Number=@number 
	order by SendDate 
) 
END 