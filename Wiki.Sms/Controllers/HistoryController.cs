using System.Collections.Generic;
using System.Web.Http;
using Wiki.Sms.Common.Model;
using Wiki.Sms.Dal.Repository;

namespace Wiki.Sms.Controllers
{
    [RoutePrefix("api/history")]
    public class HistoryController : ApiController
    {
        private readonly ISmsReaderRepository _repository;

        public HistoryController()
        {
            _repository = new SmsReaderRepository(Constants.Constants.CONNECTIONSTRING);
        }
        [HttpGet]
        [Route("")]
        public List<SmsMessage> GetSmsMessages()
        {
            return _repository.GetSmsMessages();
        }
    }
}
