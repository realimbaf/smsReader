using System;
using System.Net;
using System.Web.Http;
using Wiki.Sms.Common.Model;
using Wiki.Sms.Dal.Repository;

namespace Wiki.Sms.Controllers
{
    [RoutePrefix("api/sms")]
    public class SmsController : ApiController
    {
        private readonly ISmsReaderRepository _repository;

        public SmsController()
        {
            _repository = new SmsReaderRepository(Constants.Constants.CONNECTIONSTRING);
        }

        [HttpPost]
        [Route("")]
        public void AddSms([FromBody]Message message)
        {
            try
            {
                _repository.AddMessageToQueue(message);
            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
             
        }
    }
}
