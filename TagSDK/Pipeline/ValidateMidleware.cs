using System;
using System.Threading.Tasks;
using TagSDK.Commands;
using TagSDK.Validators;
using System.Linq;

namespace TagSDK.Pipeline
{

    public class ValidateMidleware<T,T2> : Filter<RequestCommand<T,T2>, ResponseCommand<T2>>
    {
        protected SDKOptions options;
        protected IModelValidator validator;

        public ValidateMidleware(IModelValidator validator)
        {
            this.validator = validator;
        }

        protected override async Task<ResponseCommand<T2>> Execute(RequestCommand<T, T2> context, Func<RequestCommand<T, T2>, Task<ResponseCommand<T2>>> next)
        {
            var failures = validator.Validate<T>(context.Model);

            return failures.Any()
                ? throw new Exception(String.Join(",", failures))
                : await next(context);

        }
    }
}
