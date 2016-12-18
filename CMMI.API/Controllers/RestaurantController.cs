using CMMI.API.Data;
using CMMI.Models;
using CMMI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CMMI.API.Controllers
{
    [Authorize]
    public class RestaurantController : BaseAPIController
    {
        [HttpGet]
        [Route("api/restaurants/city/{city}")]
        public async Task<IHttpActionResult> Get(string city)
        {
            try
            {
                var result = await RestaurantRepository.ListByCity(city);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/restaurants/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var result = await RestaurantRepository.Get(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("api/restaurants")]
        public async Task<IHttpActionResult> Post(RestaurantDetailModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Id = await RestaurantRepository.Create(model);
                    if (model.Id > 0)
                        return Created<RestaurantDetailModel>("api/restaurants/" + model.Id, model);
                    else
                        return BadRequest("Restaurant already exists");
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

    }
}
