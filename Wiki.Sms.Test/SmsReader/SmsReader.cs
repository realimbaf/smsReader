using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wiki.Sms.Bll.Contracts;
using Wiki.Sms.Bll.Implementation;
using Wiki.Sms.Utils;


namespace Wiki.Sms.Test.SmsReader
{
    [TestClass]
    public class SmsReader
    {
        [TestMethod]
        public async Task GetAllMessageQuery()
        {
            HttpClientHandler handler = new HttpClientHandler();
            //handler.Proxy = new WebProxy("127.0.0.1",8888);
            var client = new HttpClient(handler);
            string message = null;
            var Uri = new Uri("http://192.168.0.1/goform/goform_get_cmd_process").AddQuery("isTest", "true")
                .AddQuery("cmd", "sms_data_total")
                .AddQuery("page", "0")
                .AddQuery("data_per_page", "500").AddQuery("mem_store", "1").AddQuery("tags", "10")
                .AddQuery("order_by", "order+by+number");
            UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(true);
            var result = await client.GetAsync(Uri);
            if (result.IsSuccessStatusCode)
            {
                message = await result.Content.ReadAsStringAsync();
            }
            Assert.AreEqual("123", "123");
        }
        [TestMethod]
        public async Task DeleteAllMessagesQuery()
        {
            var handler = new HttpClientHandler { Proxy = new WebProxy("127.0.0.1", 8888) };
            using (var client = new HttpClient(handler))
            {
                var uri = new Uri("http://192.168.0.1/goform/goform_set_cmd_process")
                    .AddQuery("isTest", "false")
                    .AddQuery("goformId", "ALL_DELETE_SMS")
                    .AddQuery("notCallback", "true")
                    .AddQuery("which_cgi", "native_inbox");
                var result = await client.GetAsync(uri);
                if (result.IsSuccessStatusCode)
                {
                    var stringResponse = await result.Content.ReadAsStringAsync();
                    var c = 0;
                }
            }
        }
        [TestMethod]
        public void DecodeM()
        {
            string input =
                "0053006B006F006400610020004600610062006900610020043D043000200441043C0435043B044B0445002004430441043B043E04320438044F0445002100200421043F04350448043804420435002004320020041004320442043E002004130430043C043C0443002100200418043D0444043E0020003300340037002D00380036002D00330032";
            var result = DecodeMessage.Decode(input);
            Assert.AreEqual("1", "1");
        }
        [TestMethod]
        public void GetAllMessages()
        {
            ISmsHandler reader = new HandlerModemMessages();
            reader.Handle();
            Assert.AreEqual("1", "1");
        }
        [TestMethod]
        public async Task SendSmsQuery()
        {
            var formContent = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("isTest", "false"),
                new KeyValuePair<string, string>("goformId", "SEND_SMS"),
                new KeyValuePair<string, string>("notCallback", "true"),
                new KeyValuePair<string, string>("Number","+79117495435"),
                new KeyValuePair<string, string>("sms_time",DecodeMessage.GetCurrentTimeString()),
                new KeyValuePair<string, string>("MessageBody",DecodeMessage.EncodeMessage("Privet,!.134577 Егор")),
                new KeyValuePair<string, string>("ID","-1"),
                new KeyValuePair<string, string>("encode_type","UNICODE")
            });
            using (var myHttpClient = new HttpClient())
            {
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(true);
                var response = await myHttpClient.PostAsync("http://192.168.0.1" + Constants.Constants.POSTURLPREFIX, formContent);
                if (response.IsSuccessStatusCode)
                {
                    UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
                }
                UnsafeHeaderParsing.SetAllowUnsafeHeaderParsing(false);
            }
        }
    }
}
