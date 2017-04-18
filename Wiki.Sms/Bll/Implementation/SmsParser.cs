using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wiki.Payment.Client;
using Wiki.Payment.Common.POCO;
using Wiki.Payment.Common.POCO.DTO;
using Wiki.Service.Common.Clients;
using Wiki.Service.Configuration;
using Wiki.Sms.Bll.Contracts;
using Wiki.Sms.Common.Model;

namespace Wiki.Sms.Bll.Implementation
{
    public class SmsParser : ISmsParser
    {
        private const PaymentType TYPEPAYMENT = PaymentType.auto;
        private const string PATTERN = @"(?<code>^[0-9]{1,5})\s+(?<sum>[0-9]{1,9})";

        public async Task ParseAndDo(SmsMessage message)
        {
           var result = Regex.Match(message.Text, PATTERN);
            if (result.Success)
            {
                var payment = new DTOInsertPayment()
                {
                    Type = TYPEPAYMENT,
                    ClientCode = int.Parse(result.Groups["code"].Value),
                    Price = decimal.Parse(result.Groups["sum"].Value)
                };
                await SendPaymentIfValid(new Tuple<DTOInsertPayment,string>(payment,message.Number));
            }          
        }

        private static async Task<ReturnModel> SendPaymentIfValid(Tuple<DTOInsertPayment,string> content)
        {
            var manager = await GetAllowedContact(content.Item2);
            if (manager == null) return null;
            var credentials = DiscoveryFactory.GetDefaultCreditenals();
            using (var client = new PaymentClient(ConfigurationContainer.Configuration["DiscoveryUrl"], credentials.ClientId,credentials.ClientSecret))
            {
                content.Item1.OperatorId = manager.Id;
                return await client.AddPayment(content.Item1);
            }
        }

        private static async Task<DTOManager> GetAllowedContact(string phone)
        {
            List<DTOManager> managers;
            var credentials = DiscoveryFactory.GetDefaultCreditenals();
            using ( var client = new PaymentClient(ConfigurationContainer.Configuration["DiscoveryUrl"], credentials.ClientId, credentials.ClientSecret))
            {
                managers = await client.GetAllowedManagers();
            }
            return managers.FirstOrDefault(x => x.Phone == phone);
        }
    }
}

