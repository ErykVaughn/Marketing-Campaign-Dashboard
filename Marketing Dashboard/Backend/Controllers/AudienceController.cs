using Microsoft.AspNetCore.Mvc;
using BackendAPI.Services;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudienceController : ControllerBase
    {
        private readonly IAudienceService _audienceService;

        public AudienceController(IAudienceService audienceService)
        {
            _audienceService = audienceService;
        }

        [HttpGet]
        public ActionResult<List<Audience>> GetAllAudiences()
        {
            var audiences = _audienceService.GetAllAudiences();
            return Ok(audiences);
        }

        [HttpGet("{campaignId}")]
        public ActionResult<List<Audience>> GetAudiencesByCampaignId(int campaignId)
        {
            var audiences = _audienceService.GetAudiencesByCampaignId(campaignId);
            return Ok(audiences);
        }
    }
}

