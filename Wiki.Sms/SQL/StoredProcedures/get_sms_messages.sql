USE [SmsReader]
GO
/****** Object:  StoredProcedure [dbo].[get_messages_from_queue]    Script Date: 25.01.2017 14:04:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE  [dbo].[get_sms_messages]  
AS
BEGIN
SELECT [ID]
      ,[Number]
      ,[Text]
      ,[SendDate]
      ,[ReceiveDate]
      ,[CreateDate]
      ,[Direction]
      ,[Status]
  FROM [SmsReader].[dbo].[SmsMessage]

END 