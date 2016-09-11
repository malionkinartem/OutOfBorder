using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using NLog;

namespace OOB.ServiceApi
{
    public class ErrorHandler : IErrorHandler
    {
        private readonly ILogger _logger;

        public ErrorHandler()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            var newEx = new FaultException(
                string.Format(
                    $"Exception caught at Service Application GlobalErrorHandler {0} Method: {1}{2} Message: {3}",
                    Environment.NewLine, error.TargetSite.Name,
                    Environment.NewLine, error.Message));

            MessageFault msgFault = newEx.CreateMessageFault();
            Message.CreateMessage(version, msgFault, newEx.Action);
        }

        public bool HandleError(Exception error)
        {            
            _logger.Log(LogLevel.Error, error);

            return false;
        }
    }
}