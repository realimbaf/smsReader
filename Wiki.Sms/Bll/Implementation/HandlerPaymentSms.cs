using System;
using System.Linq;
using System.Threading.Tasks;
using CarParts.Common.Log;
using CarParts.Data.Componets;
using Wiki.Sms.Bll.Contracts;
using Wiki.Sms.Dal.Repository;

namespace Wiki.Sms.Bll.Implementation
{
    public class HandlerPaymentSms : ISmsHandler
    {
        private readonly ISmsReaderRepository _repository;
        private readonly ISmsParser _parser;
        private static readonly FileLogger _logger  = new FileLogger("PaymentHandler");

        public string Name { get { return "HandlerPayment"; } }
        public HandlerPaymentSms()
        {           
            _repository = new SmsReaderRepository(Constants.Constants.CONNECTIONSTRING);
            _parser = new SmsParser();
        }

        public async Task Handle()
        {
            try
            {
                var messages = _repository.GetUnhandledMessages();
                if (messages.Any())
                {
                    foreach (var message in messages)
                    {
                        try
                        {
                            _logger.WriteEvent("handle unhandled message.Id in Database :{0} ", message.ID);
                            await _parser.ParseAndDo(message);
                            _repository.MarkMessageAsProcessed(message.ID);
                        }
                        catch (LafDbException exception)
                        {
                            _logger.WriteError("Payment http client error.Error message id in Database: " + message.ID, exception);
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
       
    }
}
