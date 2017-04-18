using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarParts.Common.Log;
using Wiki.Sms.Bll.Contracts;

namespace Wiki.Sms.Bll.Implementation
{
    public class SmsComposite : ISmsComposite
    {
        private readonly IList<Task> _composeTasks;
        private static readonly FileLogger _logger = new FileLogger("sms_scheduler");

        public SmsComposite()
        {
            _composeTasks = new List<Task>();
        }

        public void Compose(params ISmsHandler[] handlers)
        {
            foreach (var handler in handlers)
            {            
                _composeTasks.Add(Task.Factory.StartNew(() =>
                {
                    _logger.WriteEvent("Handler start "+ handler.Name);
                    handler.Handle();
                }));
            }
            Task.WaitAll(_composeTasks.ToArray());
        }
    }
}
