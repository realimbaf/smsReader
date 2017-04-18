using System;
using System.Threading.Tasks;
using CarParts.Common.Log;
using CarParts.Data.Componets;

namespace Wiki.Sms.Utils
{
    public class SafelyHandleMessages
    {
        private static readonly FileLogger _logger = new FileLogger("HandleMessage");
        public static async void SafelyHandleMessageAsync(Func<Task> action, string errorMsg , string eventMsg = null)
        {
            try
            {
                if (eventMsg != null)
                {
                    _logger.WriteEvent(eventMsg);
                }
                await action();
            }
            catch (LafDbException exception)
            {
                _logger.WriteError(errorMsg, exception);
            }

            catch (Exception exception)
            {
                _logger.WriteError("Modem not found. Try reboot the modem and check sim-card in modem", exception);
            }
        }
        public static void SafelyHandleMessage(Action action, string errorMsg, string eventMsg = null)
        {
            try
            {
                if (eventMsg != null)
                {
                    _logger.WriteEvent(eventMsg);
                }
                 action();
            }
            catch (LafDbException exception)
            {
                _logger.WriteError(errorMsg, exception);
            }

            catch (Exception exception)
            {
                _logger.WriteError("Modem not found. Try reboot the modem and check sim-card in modem", exception);
            }
        }
    }

}
