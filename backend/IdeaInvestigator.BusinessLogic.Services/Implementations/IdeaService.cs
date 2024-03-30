using AutoMapper;
using IdeaInvestigator.BusinessLogic.Models.VM;
using IdeaInvestigator.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaInvestigator.BusinessLogic.Services.Implementations
{
    public class IdeaService
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public IdeaService(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<IdeaVM>?> GetAllIdeasAsync()
        {
            var ideas = await context.Ideas.ToListAsync();

            if (ideas == null)
                return null;

            return mapper.Map<List<IdeaVM>>(ideas);
        }

        public async Task<IdeaVM?> GetIdeaByIdAsync(Guid id)
        {
            var idea = await context.Ideas.FirstOrDefaultAsync(i => i.Id == id);

            if (idea == null)
                return null;

            return mapper.Map<IdeaVM>(idea);
        }
    }
}
