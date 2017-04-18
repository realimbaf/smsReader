USE [SmsReader]
GO
/****** Object:  StoredProcedure [dbo].[get_messages_from_queue]    Script Date: 23.01.2017 18:06:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE  [dbo].[get_messages_from_queue]  
AS
BEGIN
SELECT [ID],[Number],[Text],[CreateDate] FROM dbo.SmsOutputQueue

END 