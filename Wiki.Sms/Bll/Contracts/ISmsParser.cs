using System.Threading.Tasks;
using Wiki.Sms.Common.Model;

namespace Wiki.Sms.Bll.Contracts
{
    public interface ISmsParser
    {
        Task ParseAndDo(SmsMessage message);
    }
}
