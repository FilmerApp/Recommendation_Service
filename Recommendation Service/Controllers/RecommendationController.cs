using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic_Layer.Interfaces;

namespace Recommendation_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendation recommendation;

        public RecommendationController(IRecommendation _recommendation)
        {
            recommendation = _recommendation;
        }

        [HttpGet]
        [Route("GetRecommendations")]
        public IActionResult GetRecommendations(int userId, int amount)
        {
            List<int> result = recommendation.MakeRecommendationList(userId, amount);
            return Ok(result);
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok("Endpoint working!");
        }
    }
}
