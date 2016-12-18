using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using CMMI.Logging;

namespace CMMI.Web
{
    /// <summary>
    /// CMMI Base API Class
    /// </summary>
    /// <remarks>
    /// The purpose of this clase is to create a base set of logic and behavior to be consumed by the
    /// implemented API controllers.
    /// </remarks>
    public abstract class BaseAPIController : ApiController
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Logs = new LogManager();
            base.Initialize(controllerContext);
        }

        /// <summary>
        /// Provides access to the Log Manager
        /// </summary>
        protected LogManager Logs
        {
            get; private set;
        }

        /// <summary>
        /// Creates a System.Web.Http.Results.OkResult(200 OK)
        /// </summary>
        /// <returns>System.Web.Http.IHttpActionResult</returns>
        new protected IHttpActionResult Ok()
        {
            //Wrapped so we could add and desired tracking later if needed
            return base.Ok();
        }

        /// <summary>
        ///  Creates an System.Web.Http.Results.OkNegotiatedContentResult<T>"/> with the specified values.
        /// </summary>
        /// <typeparam name="T">The type of content in the entity body.</typeparam>
        /// <param name="content">The content value to negotiate and format in the entity body.</param>
        /// <returns>An System.Web.Http.Results.OkNegotiatedContentResult<T> with the specified values.</returns>
        new protected IHttpActionResult Ok<T>(T content)
        {
            //Wrapped so we could add and desired tracking later if needed
            return base.Ok(content);
        }

        /// <summary>
        /// Creates a System.Web.Http.Results.CreatedNegotiatedContentResult (201 Created) with the specified values.
        /// </summary>
        /// <typeparam name="T">The type of content in the entity body.</typeparam>
        /// <param name="location">The location at which the content has been created.</param>
        /// <param name="content">The content value to negotiate and format in the entity body.</param>
        /// <returns>A System.Web.Http.Results.CreatedNegotiatedContentResult with the specified values.</returns>
        new protected IHttpActionResult Created<T>(string location, T content)
        {

            //Wrapped so we could add and desired tracking later if needed
            return base.Created(location, content);
        }

        /// <summary>
        /// Creates an System.Web.Http.Results.InvalidModelStateResult with the specified model state.
        /// </summary>
        /// <returns>An System.Web.Http.Results.InvalidModelStateResult with the specified model state.</returns>
        new protected IHttpActionResult BadRequest()
        {

            //Wrapped so we could add and desired tracking later if needed
            return base.BadRequest();
        }

        /// <summary>
        /// Creates an System.Web.Http.Results.InvalidModelStateResult with the specified model state.
        /// </summary>
        /// <param name="message">The user-visible error message.</param>
        /// <returns>An System.Web.Http.Results.InvalidModelStateResult with the specified model state.</returns>
        new protected IHttpActionResult BadRequest(string message)
        {

            //Wrapped so we could add and desired tracking later if needed
            return base.BadRequest(message);
        }

        /// <summary>
        /// Creates an System.Web.Http.Results.InvalidModelStateResult with the specified model state.
        /// </summary>
        /// <param name="modelState">The model state to include in the error.</param>
        /// <returns>An System.Web.Http.Results.InvalidModelStateResult with the specified model state.</returns>
        new protected IHttpActionResult BadRequest(System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {

            //Wrapped so we could add and desired tracking later if needed
            return base.BadRequest(modelState);
        }

        /// <summary>
        ///     Creates an System.Web.Http.Results.ExceptionResult (500 Internal Server Error)
        ///     with the specified exception.    
        /// </summary>
        /// <param name="ex">The exception to include in the error.</param>
        /// <returns>An System.Web.Http.Results.ExceptionResult with the specified exception.</returns>
        new protected IHttpActionResult InternalServerError(Exception ex)
        {
            Logs.LogException(ex);
            return base.InternalServerError(ex);
        }
    }
}
