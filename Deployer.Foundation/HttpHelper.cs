using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Deployer.Foundation
{
    public class HttpHelper
    {
        public static ActionResult ReturnByLogicResponse<T>(StatusResponse<T> response)
        {
            switch (response.Status)
            {
                case StatusResponseType.Ok:
                    return new OkObjectResult(response);
                case StatusResponseType.NotFound:
                    return new NotFoundResult();
                case StatusResponseType.AccesDenied:
                case StatusResponseType.BadRequest:
                    return new BadRequestResult();
            }

            return new BadRequestResult();
        }

    }
}
