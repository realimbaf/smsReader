using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wiki.Sms.Common.Model;
using Wiki.Sms.Utils;

namespace Wiki.Sms.Providers
{
    public class BeelineHttpClient
    {
        public async Task<DTOBeelineMessages> GetAllMessages()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(Constants.Constants.BASEMODEMURL + Constants.Constants.GETURLPREFIX)
                   .AddQuery("isTest", "false")
                   .AddQuery("cmd", "sms_data_total")
                   .AddQuery("page", "0")
                   .AddQuery("data_per_page", "500")
                   .AddQuery("mem_store", "1")
                   .AddQuery("tags", "10")
                   .AddQuery("order_by", "order+by+number");
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(true);
                var result = await client.GetAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    var stringResponse = await result.Content.ReadAsStringAsync();
                    //TODOs :  use custom converter.
                    var jsonResponse =  JsonConvert.DeserializeObject<DTOBeelineMessages>(stringResponse);
                    foreach (var message in jsonResponse.messages)
                    {
                        message.content = DecodeMessage.Decode(message.content);
                    }
                    UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                    return jsonResponse;
                }
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                return null;
            }
        }
        public async Task<bool> DeleteAllMessages()
        {
            var allMessages = await GetAllMessages();
            if (allMessages != null)
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("isTest", "false"),
                            new KeyValuePair<string, string>("goformId", "DELETE_SMS"),
                            new KeyValuePair<string, string>("notCallback", "true"),
                            new KeyValuePair<string, string>("msg_id", string.Join(";",allMessages.messages.Select(x=>x.id)))
                        });
                    UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(true);
                    var result = await client.PostAsync(Constants.Constants.BASEMODEMURL + Constants.Constants.POSTURLPREFIX, formContent);
                    if (result.IsSuccessStatusCode)
                    {
                        await result.Content.ReadAsStringAsync();
                        UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                        return true;
                    }
                    UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                    return false;
                }
            }
            UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
            return false;
        }

        public async Task<bool> DeleteMessage(string id)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("isTest", "false"),
                new KeyValuePair<string, string>("goformId", "DELETE_SMS"),
                new KeyValuePair<string, string>("notCallback", "true"),
                new KeyValuePair<string, string>("msg_id", id)
            });
            using (var myHttpClient = new HttpClient())
            {
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(true);
                var response = await myHttpClient.PostAsync(Constants.Constants.BASEMODEMURL + Constants.Constants.POSTURLPREFIX, formContent);
                if (response.IsSuccessStatusCode)
                {
                    UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                    return true;
                }
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                return false;
            }                   
        }

        public async Task<bool> SendSms(Message message)
        {
             var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("isTest", "false"),
                new KeyValuePair<string, string>("goformId", "SEND_SMS"),
                new KeyValuePair<string, string>("notCallback", "true"),
                new KeyValuePair<string, string>("Number",message.Number), 
                new KeyValuePair<string, string>("sms_time",DecodeMessage.GetCurrentTimeString()),
                new KeyValuePair<string, string>("MessageBody",DecodeMessage.EncodeMessage(message.Text)),
                new KeyValuePair<string, string>("ID","-1"),   
                new KeyValuePair<string, string>("encode_type","UNICODE")
            });
            using (var myHttpClient = new HttpClient())
            {
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(true);
                var response = await myHttpClient.PostAsync(Constants.Constants.BASEMODEMURL + Constants.Constants.POSTURLPREFIX, formContent);
                if (response.IsSuccessStatusCode)
                {
                    UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                    return true;
                }
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                return false;
            }
        }

        public async Task<DTOBeelineStatus> GetReportAboutReceived()
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(Constants.Constants.BASEMODEMURL + Constants.Constants.GETURLPREFIX)
                    .AddQuery("isTest", "false")
                    .AddQuery("cmd", "sms_status_rpt_data")
                    .AddQuery("page", "0")
                    .AddQuery("data_per_page", "10");
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(true);
                var result = await client.GetAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    var stringResponse = await result.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<DTOBeelineStatus>(stringResponse);
                    UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                    return jsonResponse;
                }
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                return null;
            }
        }
    }
}
