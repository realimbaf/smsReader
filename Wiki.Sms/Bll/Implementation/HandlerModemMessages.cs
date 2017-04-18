using System;
using System.Threading.Tasks;
using CarParts.Common.Log;
using CarParts.Data.Componets;
using Wiki.Sms.Bll.Contracts;
using Wiki.Sms.Common.Model;
using Wiki.Sms.Dal.Repository;
using Wiki.Sms.Providers;
using Wiki.Sms.Utils;

namespace Wiki.Sms.Bll.Implementation
{
    public class HandlerModemMessages : ISmsHandler
    {
        private readonly BeelineHttpClient _client;
        private readonly ISmsReaderRepository _repository;
        private static readonly FileLogger _logger = new FileLogger("HandlerModem");

        public string Name { get { return "HandlerModem"; } }
        public HandlerModemMessages()
        {
            _client = new BeelineHttpClient();
            _repository = new SmsReaderRepository(Constants.Constants.CONNECTIONSTRING);
        }

        public async Task Handle()
        {
            try
            {
                var allMessage = await _client.GetAllMessages();
                if (allMessage != null && allMessage.messages.Length != 0)
                {

                    foreach (var message in allMessage.messages)
                    {
                        try
                        {
                            _logger.WriteEvent("handle message.Id :{0} ", message.id);
                            await HandleMessage(message);
                        }
                        catch (LafDbException exception)
                        {
                            _logger.WriteError("Database error.Error message id: " + message.id, exception);
                        }

                        catch (Exception exception)
                        {
                            _logger.WriteError("Error", exception);
                        }
                    }
                }


                var receivedReport = await _client.GetReportAboutReceived();
                if (receivedReport.messages.Length > 0)
                {
                    foreach (var status in receivedReport.messages)
                    {
                        try
                        {
                            _logger.WriteEvent("Update status sms.Number: " + status.number);
                            _repository.UpdateSmsMessage(status.number);
                        }
                        catch (LafDbException exception)
                        {
                            _logger.WriteError("Database error.Error status.Number : " + status.number, exception);
                        }

                        catch (Exception exception)
                        {
                            _logger.WriteError("Error", exception);
                        }

                    }
                }
            }
            catch (LafDbException exception)
            {
                _logger.WriteError("Database error", exception);
            }

            catch (Exception exception)
            {
                _logger.WriteError("Modem not found. Try reboot the modem and check sim-card in modem", exception);
            }
        }

        private async Task HandleMessage(BeelineMessage message)
        {
            if (message.tag == "0" || message.tag == "1")
            {
                var smsMessage = new SmsMessage(
                    message.number,
                    message.content,
                    DateTime.Now,
                    Direction.input,
                    Status.input,
                    DecodeMessage.ParseDate(message.date),
                    DateTime.Now,
                    false);
                _repository.AddSmsMessage(smsMessage);
            }
            await _client.DeleteMessage(message.id);
        }
    }
}
