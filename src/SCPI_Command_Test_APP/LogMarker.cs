using CommunityToolkit.Mvvm.Messaging.Messages;
using CommunityToolkit.Mvvm.Messaging;
using NLog;

namespace SCPI_Command_Test_APP
{
    public class LogMarker
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message)
        {
            logger.Info(message);
            WeakReferenceMessenger.Default.Send(new ValueChangedMessage<string>(message), "LogMessage"); ;

        }
    }
}
