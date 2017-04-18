using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CarParts.Data.Componets;
using Wiki.Sms.Common.Model;

namespace Wiki.Sms.Dal.Repository
{
    public class SmsReaderRepository : ISmsReaderRepository
    {
        private readonly string _connectionString;

        public SmsReaderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private IWikiDbCommand GetCommand(string sql)
        {
            var cmd = new LafSqlCommand(sql, new SqlConnection(_connectionString))
            {
                CommandType = CommandType.StoredProcedure,
                UseTransaction = true,
                IsolationLevel = IsolationLevel.ReadCommitted
            };
            return cmd;
        }

        public void AddSmsMessage(SmsMessage message)
        {
            using (var cmd = GetCommand("insert_message"))
            {
                cmd.AddParameter("Number", message.Number);
                cmd.AddParameter("Text", message.Text);
                cmd.AddParameter("ReceiveDate", message.ReceiveDate);
                cmd.AddParameter("SendDate", message.SendDate);
                cmd.AddParameter("CreateDate", message.CreateDate);
                cmd.AddParameter("Direction", message.Direction);
                cmd.AddParameter("Status", message.Status);
                cmd.Execute();          
            }
        }

        public void UpdateSmsMessage(string number)
        {
            using (var cmd = GetCommand("update_sms_message"))
            {
                cmd.AddParameter("number", number);
                cmd.Execute();
            }
        }

        public void AddMessageToQueue(Message message)
        {
            using (var cmd = GetCommand("insert_queue"))
            {
                cmd.AddParameter("Number", message.Number);
                cmd.AddParameter("Text", message.Text);
                cmd.AddParameter("CreateDate", message.CreateDate);
                cmd.Execute();
            }
        }

        public List<SmsMessage> GetUnhandledMessages()
        {
            var messages = new List<SmsMessage>();
            using (var cmd = GetCommand("get_unhandled_message"))
            {
                cmd.ExecuteReader(x =>
                {
                    messages.Add(new SmsMessage(
                        x.GetValue<string>("Number"),
                        x.GetValue<string>("Text"),
                        x.GetValue<DateTime>("CreateDate"),
                        x.GetValue<Direction>("Direction"),
                        x.GetValue<Status>("Status"),
                        x.GetValue<DateTime>("SendDate"),
                        x.GetValue<DateTime>("ReceiveDate"),
                        x.GetValue<bool>("IsProcessed")
                        )
                    {
                        ID = x.GetValue<int>("ID")
                    });
                });
            }
            return messages;
        }

        public List<Message> GetMessagesFromQueue()
        {
            var messages = new List<Message>();
            using (var cmd = GetCommand("get_messages_from_queue"))
            {
                cmd.ExecuteReader(x =>
                {
                    messages.Add(new Message(             
                        x.GetValue<string>("Number"),
                        x.GetValue<string>("Text"),
                        x.GetValue<DateTime>("CreateDate"))
                    {
                        ID = x.GetValue<int>("ID")
                    });
                });
            }
            return messages;
        }

        public List<SmsMessage> GetSmsMessages()
        {
            var messages = new List<SmsMessage>();
            using (var cmd = GetCommand("get_sms_messages"))
            {
                cmd.ExecuteReader(x =>
                {
                    messages.Add(new SmsMessage(
                        x.GetValue<string>("Number"),
                        x.GetValue<string>("Text"),
                        x.GetValue<DateTime>("CreateDate"),
                        x.GetValue<Direction>("Direction"),
                        x.GetValue<Status>("Status"),
                        x.GetValue<DateTime>("SendDate"),
                        x.GetValue<DateTime>("ReceiveDate"),
                        x.GetValue<bool>("IsProcessed")
                        )
                    {
                        ID = x.GetValue<int>("ID")
                    });
                });
            }
            return messages;
        }

        public void MarkMessageAsProcessed(int messageId)
        {
            using (var cmd = GetCommand("handle_sms_message_by_payment"))
            {
                cmd.AddParameter("Id", messageId);
                cmd.Execute();
            }
        }

        public void TransferMessageFromQueue(int messageId)
        {
            using (var cmd = GetCommand("transfer_queue_to_messages"))
            {
                cmd.AddParameter("smsId", messageId);
                cmd.Execute();
            }
        }
    }
}
