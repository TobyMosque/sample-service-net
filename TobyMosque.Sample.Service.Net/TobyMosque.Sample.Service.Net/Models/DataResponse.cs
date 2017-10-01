using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Models
{
    [DataContract]
    public class DataResponseError
    {
        internal DataResponseError()
        {

        }
        [DataMember(Order = 1)]
        public string ErrorMessage { get; set; }
        [DataMember(Order = 2)]
        public string[] Properties { get; set; }
    }

    [DataContract]
    public class DataResponse<T>
    {
        [DataMember(Order = 1)]
        public int ResponseCode { get; set; }
        [DataMember(Order = 2)]
        public string Message { get; set; }
        [DataMember(Order = 3)]
        public List<DataResponseError> Errors { get; set; } = new List<DataResponseError>();
        [DataMember(Order = 4)]
        public T Data { get; set; }

        public void AddError(string errorMessage, params string[] properties)
        {
            var error = new DataResponseError();
            error.ErrorMessage = errorMessage;
            error.Properties = properties;
            this.Errors.Add(error);
        }

        internal string GetMessageFromErrors()
        {
            var message = string.Empty;
            if (this.Errors.Any())
            {
                message = String.Join("<br />" + Environment.NewLine, this.Errors.Select(m => {
                    return String.Join("<br />" + Environment.NewLine, m.Properties.Select(e => $"{e} => {m.ErrorMessage}"));
                }));
            }
            return message;
        }
    }
}
