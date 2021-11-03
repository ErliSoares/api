using System;
using Application.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    public class BaseControllerApi : ControllerBase
    {
        protected virtual CreatedResult Created<TEntity>(TEntity entity, int key)
        {
            return base.Created(GetAbsoluteUriWithOutQuery(Request) + "/" + key, entity);
        }

        protected ObjectResult InternalServerError(Exception error)
        {
            return StatusCode(500, new ErrorInternal(error));
        }

        protected static Uri GetAbsoluteUriWithOutQuery(HttpRequest request)
        {
            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Host = request.Host.Host;
            if (request.Host.Port.HasValue)
            {
                uriBuilder.Port = request.Host.Port.Value;
            }

            return uriBuilder.Uri;
        }
    }
}
