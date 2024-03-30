using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaInvestigator.Data.Models
{
    public class Idea
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Topic { get; set; } = null!;
        public string AdvertPlatforms { get; set; } = null!;
        public string TargetAudience { get; set; } = null!;
        public int Budget { get; set; }
        public string Tags { get; set; } = null!;
        public int NumberOfCampaigns { get; set; }
        public string Suggestion { get; set; } = null!;
    }
}
