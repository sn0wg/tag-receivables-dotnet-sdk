using TagSDK.Pipeline;
using TagSDK.Commands;
using System;
using Microsoft.Extensions.DependencyInjection;
using TagSDK.Pipeline.Interfaces;

namespace TagSDK.Services
{
    public abstract class BaseService
    {
        protected readonly IServiceProvider serviceProvider;
        protected readonly SDKOptions options;

        public BaseService(IServiceProvider serviceProvider, SDKOptions options)
        {
            this.serviceProvider = serviceProvider;
            this.options = options;
        }

        public IFilter<RequestCommand<object, TOut>, ResponseCommand<TOut>> GetPipeline<TOut>() =>
            this.GetPipeline<object,TOut>();

        public IFilter<RequestCommand<TIn, TOut>, ResponseCommand<TOut>> GetPipeline<TIn, TOut>() =>
            this.serviceProvider.GetService<IPipelineBehavior<TIn, TOut>>().GetPipiline();
    }
}
