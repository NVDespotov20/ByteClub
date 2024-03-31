using OpenAI_API;

namespace IdeaInvestigator.WebHost
{
    public static class OpenApiExtension
    {
        public static void AddOpenApi(this IServiceCollection services, string apiKey)
        {
            services.AddSingleton(new OpenAIAPI(apiKey));
        }
    }
}