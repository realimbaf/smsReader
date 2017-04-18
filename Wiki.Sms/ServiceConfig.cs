using System.Web.Http;
using Owin;
using Wiki.Service.Configuration;
using Wiki.Sms.Bll.Implementation.SmsScheduler;

namespace Wiki.Sms
{
    public class ServiceConfig : IServiceConfig
    {
        public void Init(IAppBuilder app, HttpConfiguration config)
        {
            
        }

        public void Start()
        {
            var scheduler = new SmsScheduler();
            scheduler.Start();
        }

        public void Stop()
        {
            
        }
    }
}
