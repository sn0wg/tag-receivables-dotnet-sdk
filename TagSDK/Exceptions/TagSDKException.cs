using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TagSDK.Exceptions
{
    [Serializable]
    public class TagSDKException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ResponseError Error { get; set; }

        public TagSDKException() { }

        public TagSDKException(string message)
            : base(message) { }

        public TagSDKException(HttpStatusCode StatusCode)
            : base(StatusCode.ToString()) { 
            this.StatusCode = StatusCode; 
        }

        public TagSDKException(string message, Exception inner)
            : base(message, inner) { }

        public TagSDKException(string message, HttpStatusCode StatusCode, ResponseError error)
            : this(message)
        {
            this.StatusCode = StatusCode;
            this.Error = error;
        }

        public TagSDKException(string message, HttpStatusCode StatusCode)
           : this(message)
        {
            this.StatusCode = StatusCode;
        }
        public class ResponseError
        {
            [JsonProperty("errors")]
            public List<string> Errors { get; set; }

            [JsonProperty("processKey")]
            public string ProcessKey { get; set; }

            [JsonProperty("createdAt")]
            public DateTime? createdAt { get; set; }
        }
    }
}
