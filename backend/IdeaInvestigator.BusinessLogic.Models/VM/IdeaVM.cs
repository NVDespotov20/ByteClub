using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaInvestigator.BusinessLogic.Models.VM
{
    public class IdeaVM
    {
        public Guid Id { get; set; }

        public string Topic { get; set; } = null!;

        public string AdvertPlatforms { get; set; } = null!;

        public string TargetAudience { get; set; } = null!;

        public int Budget { get; set; } = 0;

        public string Tags { get; set; } = null!;

        public int NumberOfCampaigns { get; set; } = 0;
    }
}
