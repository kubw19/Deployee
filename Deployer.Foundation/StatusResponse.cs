using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployer.Foundation
{
    public class StatusResponse<T>
    {
        public T Value { get; set; }
        public StatusResponseType Status { get; set; }
        public StatusResponse(T value, StatusResponseType status)
        {
            Value = value;
            Status = status;
        }
        public StatusResponse(T value)
        {
            Value = value;
            Status = StatusResponseType.Ok;
        }
        public StatusResponse(StatusResponseType status)
        {
            Status = StatusResponseType.Ok;
        }

        public StatusResponse<T> NotFound()
        {
            Status = StatusResponseType.NotFound;
            return this;
        }
        public StatusResponse<T> BadRequest()
        {
            Status = StatusResponseType.BadRequest;
            return this;
        }
        public StatusResponse<T> AccessDenied()
        {
            Status = StatusResponseType.AccesDenied;
            return this;
        }
    }

    public class StatusResponse : StatusResponse<bool>
    {
        public StatusResponse() : base(StatusResponseType.Ok)
        {
            Value = true;
        }
        public StatusResponse(StatusResponseType status) : base(status)
        {
            Status = status;
            Value = true;
        }
    }

    public enum StatusResponseType
    {
        Ok,
        NotFound,
        BadRequest,
        AccesDenied
    }
}
