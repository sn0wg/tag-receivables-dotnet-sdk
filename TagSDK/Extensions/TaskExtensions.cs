using Newtonsoft.Json;
using RestSharp;
using System;
using TagSDK.Commands;
using TagSDK.Exceptions;
using static TagSDK.Exceptions.TagSDKException;

namespace TagSDK.Extensions
{
    internal static class TaskExtensions
    {
        internal static async System.Threading.Tasks.Task<T> MapResponse<T>(
            this System.Threading.Tasks.Task<ResponseCommand<T>> task)
        {
            return await task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    RestSharp.IRestResponse<T> result = t.Result.Response;

                    if (result.IsSuccessful)
                        return result.Data;
                    else
                    {
                        ResponseError responseError = null;
                        if (String.IsNullOrEmpty(result.Content))
                        {
                            responseError = JsonConvert.DeserializeObject<ResponseError>("{}");
                        } else
                        {
                            responseError = JsonConvert.DeserializeObject<ResponseError>(result.Content);
                        }
                        throw new TagSDKException(result.ErrorMessage, result.StatusCode, responseError);
                    }
                }
                else
                    throw t.Exception;
            });
        }

        internal static async System.Threading.Tasks.Task<T> MapResponse<T>(
            this System.Threading.Tasks.Task<IRestResponse<T>> task)
        {
            return await task.ContinueWith(t =>
            {
                if (!t.IsFaulted)
                {
                    IRestResponse<T> result = t.Result;

                    if (result.IsSuccessful)
                        return result.Data;
                    else
                    {
                        var responseError = JsonConvert.DeserializeObject<ResponseError>(result.Content);
                        throw new TagSDKException(result.ErrorMessage, result.StatusCode, responseError);
                    }
                }
                else
                    throw t.Exception;
            });
        }
    }
}
