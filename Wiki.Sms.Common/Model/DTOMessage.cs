using System;

namespace Wiki.Sms.Common.Model
{
    public class Message
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }

        public Message()
        {
            
        }
        public Message(string number, string text, DateTime createDate)
        {
            Number = number;
            Text = text;
            CreateDate = createDate;
        }
    }

    public class SmsMessage : Message
    {
        public Direction Direction { get; set; }
        public Status Status { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime ReceiveDate { get; set; }
        public bool IsProcessed { get; set; }

        public SmsMessage(string number, string text, DateTime createDate, Direction direction, Status status, DateTime sendDate, DateTime receiverDate, bool isProcesses) 
            : base(number, text, createDate)
        {
            Direction = direction;
            Status = status;
            SendDate = sendDate;
            ReceiveDate = receiverDate;
            IsProcessed = isProcesses;
        }
    }

    public enum Direction
    {
        input = 1,
        output = 2
    }

    public enum Status
    {
        input = 1,
        receivedOutput = 2,
        confirmedOutput = 3
    }
}
