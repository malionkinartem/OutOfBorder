using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;
using NLog;

namespace OOB.ServiceApi.Attributes
{
    public class GlobalErrorHandlerAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _errorHandlerType;

        public GlobalErrorHandlerAttribute()
        {
        }

        public GlobalErrorHandlerAttribute(Type errorHandlerType)
        {
            _errorHandlerType = errorHandlerType;            
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler;

            try
            {
                errorHandler = (IErrorHandler)Activator.CreateInstance(_errorHandlerType);
            }
            catch (MissingMethodException e)
            {
                throw new ArgumentException(
                    $"The errorHandlerType specified in the ErrorBehaviorAttribute constructor must have a public empty constructor. {e}");
            }
            catch (InvalidCastException e)
            {
                throw new ArgumentException(
                    $"The errorHandlerType specified in the ErrorBehaviorAttribute constructor must implement System.ServiceModel.Dispatcher.IErrorHandler. {e}");
            }

            foreach (ChannelDispatcherBase channelDispatcherBase in 
            serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher =
                        channelDispatcherBase as ChannelDispatcher;
                channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }
    }
}