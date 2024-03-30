using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaInvestigator.Data.Models
{
    public class Idea
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CreatorId { get; set; }
        public string Topic { get; set; } = null!;
        public string AdvertPlatforms { get; set; } = null!;
        public string TargetAudience { get; set; } = null!;
        public int Budget { get; set; }
        public string Tags { get; set; } = null!;
        public int NumberOfCampaigns { get; set; }

        [ForeignKey(nameof(CreatorId))] public User User { get; init; }
    }
}
