using System;

using TagSDK.Commands;
using TagSDK.Pipeline;

namespace TagSDK.Pipeline.Interfaces
{
    interface IPipelineBehavior<TIn,TOut>
    {
        IFilter<RequestCommand<TIn, TOut>, ResponseCommand<TOut>> GetPipiline();
    }
}
