using Microsoft.AspNetCore.Mvc;
using BackendAPI.Services;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        // GET: api/campaigns
        [HttpGet]
        public ActionResult<List<Campaign>> GetCampaigns()
        {
            try
            {
                var campaigns = _campaignService.GetAllCampaigns();
                return Ok(campaigns);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/campaigns/{id}/leads
        [HttpGet("{campaignId}/leads")]
        public ActionResult<int> GetLeadsForCampaign(int campaignId)
        {
            try
            {
                var leads = _campaignService.GetLeadsForCampaign(campaignId);
                return Ok(leads);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/campaigns/lead-rate
        [HttpGet("lead-rate")]
        public ActionResult<double> GetLeadRateWithEmail()
        {
            try
            {
                var leadRate = _campaignService.GetLeadStatistics();
                return Ok(leadRate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/campaigns/average-funded-rate
        [HttpGet("average-funded-rate")]
        public ActionResult<double> GetAverageFundedRate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            Console.WriteLine("contacted GetAverageFundedRate");
            try
            {
                var fundedRate = _campaignService.GetAverageFundedRate(startDate, endDate);
                return Ok(fundedRate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
