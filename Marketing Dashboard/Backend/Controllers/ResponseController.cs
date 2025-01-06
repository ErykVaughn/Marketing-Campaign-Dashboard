using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Services;
using System.Collections.Generic;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseService _responseService;

        public ResponseController(IResponseService responseService)
        {
            _responseService = responseService;
        }

        [HttpGet]
        public ActionResult<List<Response>> GetAllResponses()
        {
            var responses = _responseService.GetAllResponses();
            return Ok(responses);
        }

        [HttpGet("{campaignId}")]
        public ActionResult<List<Response>> GetResponsesByCampaignId(int campaignId)
        {
            var responses = _responseService.GetResponsesByCampaignId(campaignId);
            return Ok(responses);
        }
    }
}
