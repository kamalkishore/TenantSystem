using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TenantSystem.Api.Models;

namespace TenantSystem.Api.Controllers
{
    public class BaseController : ApiController
    {
        protected new IHttpActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected new IHttpActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }

        protected IHttpActionResult Error(string errorMessage)
        {
            return BadRequest(result.ErrorMessage);
        }
    }
}
