using System.Collections.Generic;
using Wiki.Sms.Common.Model;

namespace Wiki.Sms.Dal.Repository
{
    public interface ISmsReaderRepository
    {
        void AddSmsMessage(SmsMessage message);
        void UpdateSmsMessage(string number);
        void AddMessageToQueue(Message message);
        List<SmsMessage> GetUnhandledMessages();
        List<Message> GetMessagesFromQueue();
        List<SmsMessage> GetSmsMessages();
        void MarkMessageAsProcessed(int messageId);
        void TransferMessageFromQueue(int messageId);
    }
}
