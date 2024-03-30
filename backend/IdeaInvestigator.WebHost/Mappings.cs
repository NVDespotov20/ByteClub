using AutoMapper;
using IdeaInvestigator.BusinessLogic.Models.IM;
using IdeaInvestigator.BusinessLogic.Models.VM;
using IdeaInvestigator.Data.Models;

namespace IdeaInvestigator.WebHost;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<UserIM, User>();
        CreateMap<User, UserVM>();
        CreateMap<Product, ProductVM>();
        CreateMap<Idea, IdeaVM>();
        CreateMap<IdeaIM, Idea>();
    }
}