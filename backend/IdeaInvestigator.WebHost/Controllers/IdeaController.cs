using IdeaInvestigator.BusinessLogic.Models.IM;
using IdeaInvestigator.BusinessLogic.Models.VM;
using IdeaInvestigator.BusinessLogic.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace IdeaInvestigator.WebHost.Controllers
{
    [Authorize]
    [Route("api/ideas")]
    [ApiController]
    public class IdeaController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IIdeaService ideaService;

        public IdeaController(IIdeaService ideaService)
        {
            this.ideaService = ideaService;
        }


        [HttpGet("history")]
        public async Task<ActionResult<IdeaVM>> GetAllIdeaTopicsByUserAsync(IAuthedUser authedUser)
        {
            var topics = await ideaService.GetAllIdeaTopicsByUserAsync(authedUser.UserId);
            
            if(topics == null)
                return NotFound();

            return Ok(topics);
        }

        [HttpPost("newIdea")]
        public async Task<ActionResult> CreateNewIdeaAsync([FromBody] IdeaIM ideaInput, IAuthedUser authedUser)
        {
            var user = await ideaService.CreateNewIdeaAsync(ideaInput, authedUser.UserId);

            return Ok();
        }
    }
}
