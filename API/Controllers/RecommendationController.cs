using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic_Layer.Interfaces;
using DAL.Model;

namespace Recommendation_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendation recommendation;

        public RecommendationController(IRecommendation? _recommendation = null)
        {
            recommendation = _recommendation;
        }

        [HttpGet]
        [Route("GetRecommendations")]
        public IActionResult GetRecommendations(int userId, int amount)
        {
            //This is an object from the DAL, which creates a dependency you obviously don't want. 
            //It would be better to use a DTO or use a different method for obtaining film details
            List<Film> result = recommendation.MakeRecommendationList(userId, amount);
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
