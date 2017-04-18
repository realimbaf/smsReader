using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wiki.Sms.Common.Model;
using Wiki.Sms.Dal.Repository;

namespace Wiki.Sms.Test.Repository
{
    /// <summary>
    /// Summary description for RepositoryTest
    /// </summary>
    [TestClass]
    public class RepositoryTest
    {

        private readonly ISmsReaderRepository _repo;

        public RepositoryTest()
        {
            _repo = new SmsReaderRepository(@"Data Source=192.168.0.208;Initial Catalog=SmsReader;Integrated Security=false;
                                            User ID=sa;Password=masterkey;");
        }
        [TestMethod]
        public void add_message_to_queue()
        {
            _repo.AddMessageToQueue(new Message("79113343434", "dsdsdsdsd", DateTime.Now));
            Assert.AreEqual("1", "1");
        }
        [TestMethod]
        public void get_all_queue()
        {
            var result = _repo.GetMessagesFromQueue();
            Assert.AreEqual("1", "1");
        }
        [TestMethod]
        public void transfer_from_queue()
        {
            _repo.TransferMessageFromQueue(3);
            Assert.AreEqual("1", "1");
        }
        [TestMethod]
        public void update_sms()
        {

            _repo.UpdateSmsMessage("79113343434");
        }
    }
}
