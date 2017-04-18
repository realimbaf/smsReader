using System;
using System.Threading.Tasks;
using CarParts.Common.Log;
using CarParts.Data.Componets;
using Wiki.Sms.Bll.Contracts;
using Wiki.Sms.Dal.Repository;
using Wiki.Sms.Providers;

namespace Wiki.Sms.Bll.Implementation
{
    public class HandlerQueueMessages : ISmsHandler
    {
        private readonly BeelineHttpClient _client;
        private readonly ISmsReaderRepository _repository;
        private static readonly FileLogger _logger = new FileLogger("QueueMessages");

        public string Name { get { return "HandlerQueue"; } }
        public HandlerQueueMessages()
        {
            _client = new BeelineHttpClient();
            _repository = new SmsReaderRepository(Constants.Constants.CONNECTIONSTRING);
        }

        public async Task Handle()
        {
            try
            {
                var messages = _repository.GetMessagesFromQueue();
                if (messages.Count > 0)
                {
                    foreach (var message in messages)
                    {
                        try
                        {
                             await _client.SendSms(message);
                            _repository.TransferMessageFromQueue(message.ID);
                        }
                        catch (LafDbException exception)
                        {
                            _logger.WriteError("Database error.Error transfer message id: " + message.ID, exception);
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
                _logger.WriteError("Error", exception);
            }


        }

    }
}
