using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace TagSDK.Pipeline
{
    public interface IFilter<TIn,TOut>
    {
        void Register(IFilter<TIn, TOut> filter);
        Task<TOut> Execute(TIn context);
    }

    public abstract class Filter<TIn, TOut> : IFilter<TIn, TOut>
    {
        private IFilter<TIn, TOut> next;
        protected abstract Task<TOut> Execute(TIn context, Func<TIn, Task<TOut>> next);

        public void Register(IFilter<TIn, TOut> filter)
        {
            if (next == null)
            {
                next = filter;
            }
            else
            {
                next.Register(filter);
            }
        }

        Task<TOut> IFilter<TIn, TOut>.Execute(TIn context)
        {
            return Execute(context, ctx => next == null
                  ? Task.FromResult(default(TOut))
                  : next.Execute(ctx));
        }
    }

    

    public class PipelineBuilder<T,TOut>
    {
        private readonly List<Func<IFilter<T, TOut>>> filters = new List<Func<IFilter<T, TOut>>>();

        public PipelineBuilder<T, TOut> Register(Func<IFilter<T, TOut>> filter)
        {
            filters.Add(filter);
            return this;
        }

        public PipelineBuilder<T, TOut> Register(IFilter<T, TOut> filter)
        {
            filters.Add(() => filter);
            return this;
        }

        public IFilter<T, TOut> Build()
        {
            var root = filters.First().Invoke();

            foreach (var filter in filters.Skip(1))
            {
                root.Register(filter.Invoke());
            }



            return root;
        }
    }
}
