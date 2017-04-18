using System;
using CarParts.Common.Log;
using Wiki.Core.Scheduler;
using Wiki.Sms.Bll.Contracts;

namespace Wiki.Sms.Bll.Implementation.SmsScheduler
{
    public class SmsScheduler : ShedulerBase
    {
        private readonly ISmsComposite _composite;
        private readonly ISmsHandler _modemSmsHandler;
        private readonly ISmsHandler _paymentSmsHandler;
        private readonly ISmsHandler _queueSmsHandler;

        public SmsScheduler()
        {
            _composite = new SmsComposite();
            _modemSmsHandler = new HandlerModemMessages();
            _paymentSmsHandler = new HandlerPaymentSms();
            _queueSmsHandler = new HandlerQueueMessages();
        }

        protected override int Interval { get { return 15000; } }

        protected override TimeSpan MaxRumTime { get { return TimeSpan.FromMinutes(20); } }


        protected override void TimerTick()
        {
           _composite.Compose(_modemSmsHandler,_paymentSmsHandler,_queueSmsHandler);
        }


        protected override void ProcessError(Exception exception)
        {
            new FileLogger("TimerProcess").WriteError("Error ReadMessages.", exception);
        }
    }
}
