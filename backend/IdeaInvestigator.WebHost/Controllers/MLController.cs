using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using IdeaInvestigator.BusinessLogic.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using IdeaInvestigator.BusinessLogic.Services.Implementations;

namespace IdeaInvestigator.WebHost.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/ml")]
    public class MLController : ControllerBase
    {
        private readonly OpenAI_API.OpenAIAPI apiClient;
        private readonly IIdeaService ideaService;
        private readonly IUserService userService;
        private readonly IProductService productService;

        public MLController(IUserService userService, IIdeaService ideaService, OpenAI_API.OpenAIAPI apiClient, IProductService productService)
        {
            this.ideaService = ideaService;
            this.apiClient = apiClient;
            this.userService = userService;
            this.productService = productService;
        }
        
        [HttpGet("gen-posts")]
        public async Task<ActionResult<List<string>>> GenSocialMediaPostsAsync([FromQuery] Guid ideaId, AuthedUser authedUser)
        {
            var idea = await ideaService.GetIdeaByIdAsync(ideaId);

            if (idea == null)
                return NotFound();

            if (idea.CreatorId != authedUser.UserId)
                return Unauthorized();

            var chat = apiClient.Chat.CreateConversation();
            chat.Model = OpenAI_API.Models.Model.GPT4;
            chat.RequestParameters.Temperature = 0;
            chat.RequestParameters.ResponseFormat = OpenAI_API.Chat.ChatRequest.ResponseFormats.JsonObject;
            
            chat.AppendSystemMessage("You are a social media manager that makes social media posts for different companies. Your responce should be just the asked number of example posts for the coresponding social media.");
            // return model response as json object 
            chat.AppendUserInput($"This is the idea of my company: {idea.Topic}\n The target audience is people ages: {idea.TargetAudience}\n The budget for ad campaigns is: {idea.Budget}\n The relevant tags to use are: {idea.Tags}\nI advertize on {idea.AdvertPlatforms}. Give me example posts for this idea, one for each social media I use");

            var response = await chat.GetResponseFromChatbotAsync();
            Dictionary<string, string> responseDict = new Dictionary<string, string>
            {
                { "response", response }
            };
            return Ok(responseDict);
        }

        [HttpGet("gen-advice")]
        public async Task<ActionResult<string>> GenMarketingAdviceAsync([FromQuery] Guid ideaId, AuthedUser authedUser)
        {
            var idea = await ideaService.GetIdeaByIdAsync(ideaId);
            if (idea == null)
                return NotFound();

            if (idea.CreatorId != authedUser.UserId)
                return Unauthorized();

            var chat = apiClient.Chat.CreateConversation();
            chat.Model = OpenAI_API.Models.Model.GPT4;
            chat.RequestParameters.Temperature = 0;
            chat.RequestParameters.ResponseFormat = OpenAI_API.Chat.ChatRequest.ResponseFormats.JsonObject;
            
            chat.AppendSystemMessage("You are a marketing advisor for different companies. People give you their ideas and you give them advice on how to improve their marketing strategy. You give a 1 paragraph response containing information like should they change their target audiandce, invest more in ads, and where they should advertise.");
            // return model response as json object 
            chat.AppendUserInput($"This is the idea of my company: {idea.Topic}\n My current target audience is people with ages: {idea.TargetAudience}\n The budget for ad campaigns is: {idea.Budget}\n The relevant tags I use are: {idea.Tags}\nSuggest 3 twiter posts for this idea\n I am advertizing on {idea.AdvertPlatforms} and I have {idea.NumberOfCampaigns} campaigns running. What advice do you have for me?");

            var response = await chat.GetResponseFromChatbotAsync();
            Dictionary<string, string> responseDict = new Dictionary<string, string>
            {
                { "response", response }
            };
            return Ok(responseDict);
        }
        [HttpGet("cat-idea")] 
        public  async Task<ActionResult<string>> CategorizeIdeaAsync([FromQuery] Guid ideaId, AuthedUser authedUser)
        {
            var idea = await ideaService.GetIdeaByIdAsync(ideaId);
            if (idea == null)
                return NotFound();

            if (idea.CreatorId != authedUser.UserId)
                return Unauthorized();

            var chat = apiClient.Chat.CreateConversation();
            chat.Model = OpenAI_API.Models.Model.GPT4;
            chat.RequestParameters.Temperature = 0;
            chat.RequestParameters.ResponseFormat = OpenAI_API.Chat.ChatRequest.ResponseFormats.JsonObject;
            
            chat.AppendSystemMessage("You are a bussiness interpreter for different companies. People give you their ideas and you categorize their product into one of the following categories: Toys & Games, Hobbies, Games & Accessories, Arts & Crafts, Remote & App Controlled Vehicles & Parts, Toy Figures & Playsets, Dress Up & Pretend Play, Remote & App Controlled Vehicle Parts, Stuffed Animals & Plush Toys, Board Games, Sports & Outdoor Play, Home & Kitchen, Puzzles, Dolls & Accessories, Party Supplies, Jigsaw Puzzles, Learning & Education, Sports & Outdoors, Action Figures, Play Vehicles, Accessories, Novelty & Gag Toys, Card Games, Baby & Toddler Toys, Dolls, Trains & Accessories, Models & Model Kits, Pretend Play, Stuffed Animals & Teddy Bears, Building Toys, Craft Kits, Sports & Fitness, Model Kits, Stickers, Home Décor, Playsets, Pools & Water Toys, Costumes, Building Sets, Outdoor Recreation, Furniture, Baby Products, Leisure Sports & Game Room, Wall Décor, Nursery, Kids' Electronics, Playsets & Vehicles, Puppets & Puppet Theaters, Bedding, Décor, Science Kits & Toys, Kids' Bedding, Hobby Building Tools & Hardware, Kids' Furniture, Power Plant & Driveline Systems, Die-Cast Vehicles, Cycling, Remote & App Controlled Vehicles, Train Cars, Play Trains & Railway Sets, Hand Puppets, Plush Figures, Figure Kits, Doll Accessories, Toy Vehicles, Standard Playing Card Decks, Party Favors, Play Sets & Playground Equipment, Paints, Game Accessories, Kids' Bikes & Accessories, Counting & Math Toys, Fan Shop, Brain Teasers, Collectible Toys, Event & Party Supplies, Kids' Room Décor, Kitchen Toys, Window Treatments, Vehicle Playsets, Train Sets, Quadcopters & Multirotors, Office Products, Office & School Supplies, Window Stickers & Films, Window Films, Airplane & Jet Kits, Electronic Learning Toys, Toys & Game Room, Kitchen & Dining, Musical Instruments, Scenery, Blasters & Foam Play, Servos & Parts, Collectible Display & Storage, Drawing & Painting Supplies, Outdoor Games & Activities, Pre-Built & Diecast Models, Pool Rafts & Inflatable Ride-ons, Sheets & Pillowcases\n You give a comma seperated list of categories to which the product belongs.");
            // return model response as json object 

            chat.AppendUserInput($"This is the idea of my company: {idea.Topic}\nTo what categories does this idea belong?");

            var response = await chat.GetResponseFromChatbotAsync();
            Dictionary<string, string> responseDict = new Dictionary<string, string>
            {
                { "response", response }
            };
            return Ok(responseDict);
        }
        [HttpGet("find-competitors")]
        public async Task<ActionResult<string>> FindCompetitorsAsync([FromQuery] Guid ideaId, AuthedUser authedUser)
        {
            var idea = await ideaService.GetIdeaByIdAsync(ideaId);
            if (idea == null)
                return NotFound();

            if (idea.CreatorId != authedUser.UserId)
                return Unauthorized();
           string? categories = await ideaService.GetIdeaCategoriesAsync(ideaId);
            if(categories == null)
                categories = (await CategorizeIdeaAsync(ideaId, authedUser)).Value; 

            if(categories == null)
                return BadRequest();
            var products = await productService.MatchProductsByAtLeastOneCategoryAsync(categories.Split(",").ToList()); 
            var chat = apiClient.Chat.CreateConversation();
            chat.Model = OpenAI_API.Models.Model.GPT4;
            chat.RequestParameters.Temperature = 0;
            chat.RequestParameters.ResponseFormat = OpenAI_API.Chat.ChatRequest.ResponseFormats.JsonObject;
            
            chat.AppendSystemMessage("You are a bussiness assistent for different companies. People give you their ideas and a list of products that could potentionally be their competition. Your task is to analize the descriptions of the idea and the products and figure out how similar the product is to our idea. You give a comma seperated list of persentages of similarity to the client's idea: 50, 39, 70, ...");
            // return model response as json object 
            string similarity = "";
            for(int i = 0; i < products.Count; i+=10)
            {
                string prodBatch = "";
                for(int j = i; j < i+10 && j < products.Count; j++)
                {
                    prodBatch += $"{j}. {products[j].Description}\n";       
                }
                chat.AppendUserInput(prodBatch);
                similarity += await chat.GetResponseFromChatbotAsync();
            }
            List<int> percentages = similarity.Split(",").ToList().Select(int.Parse).ToList();
            var result = percentages
                .Select((v, i) => new { v, i })
                .OrderByDescending(x => x.v)
                .ThenByDescending(x => x.i)
                .Take(3)
                .ToArray();

            chat.AppendUserInput($"This is the idea of my company: {idea.Topic}\nI will be giving you the product descriptions in batches of 10. Here is the first batch:");

            List<Dictionary<string, string>> responseDict = 
            [
            new Dictionary<string, string>
            {
                { "image", products[result[0].i].Image },
                { "name", products[result[0].i].Name },
                { "description", products[result[0].i].Description }
            }, 
            new Dictionary<string, string>
            {
                { "image", products[result[1].i].Image },
                { "name", products[result[1].i].Name },
                { "description", products[result[1].i].Description }
            }, 
            new Dictionary<string, string>
            {
                { "image", products[result[2].i].Image },
                { "name", products[result[2].i].Name },
                { "description", products[result[2].i].Description }
            }];
            return Ok(responseDict);

        }
    }
}
