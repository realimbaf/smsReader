using System;
using System.Collections.Generic;
using Wiki.Service.Common.Clients;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wiki.Core.Exceptions;
using Wiki.Sms.Common.Model;

namespace Wiki.Sms.Client
{
    public class SmsClient : ServiceClientBase
    {
        public SmsClient(string discoveryAdress, string clientId, string clientSecret) : base(discoveryAdress, clientId, clientSecret)
        { }

        public SmsClient() : this(ConfigurationManager.AppSettings["DiscoveryUrl"],
                                  ConfigurationManager.AppSettings["ClientId"],
                                  ConfigurationManager.AppSettings["ClientSecret"])
        { }

        public SmsClient(string discoveryUrl) : this(discoveryUrl,
                                                     ConfigurationManager.AppSettings["ClientId"],
                                                     ConfigurationManager.AppSettings["ClientSecret"])
        { }

        public override string ServiceId { get { return "Wiki.Sms"; } }

        public async Task<List<SmsMessage>> GetAll()
        {
            var cl = this.GetClient();
            const string requestUri = "api/history";
            var result = await cl.GetAsync(requestUri);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SmsMessage>>(content);

        }

        public async Task AddNewSms(Message message)
        {
            var cl = this.GetClient();
            const string requestUri = "api/sms";
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
          {
                { "Text", message.Text},
                { "Number", message.Number},
                { "CreateDate", DateTime.Now.ToString(CultureInfo.InvariantCulture)}
            });
            var result = await cl.PostAsync(requestUri,formContent);
            if (!result.IsSuccessStatusCode)
            {
                var msg = await result.Content.ReadAsStringAsync();
                throw new WikiApiException(result.StatusCode, msg);
            }
        }
    }


}
