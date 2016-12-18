using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CMMI.Web;
using System.Threading.Tasks;
using CMMI.Models;
using CMMI.API.Data;

namespace CMMI.API.Controllers
{
    public class ReviewController : BaseAPIController
    {
        [HttpGet]
        [Route("api/reviews/user/{username}")]
        public async Task<IHttpActionResult> Get(string username)
        {
            try
            {
                var reviews = await ReviewsRepository.ListByUsername(username);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/reviews/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var reviews = await ReviewsRepository.Get(id);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/reviews/")]
        public async Task<IHttpActionResult> Post(ReviewDetailModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = await ReviewsRepository.Create(model);
                    return Created<ReviewDetailModel>("api/reviews/" + model.Id, model);

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
                

        [HttpDelete]
        [Route("api/reviews/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await ReviewsRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}