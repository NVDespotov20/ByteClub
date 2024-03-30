using IdeaInvestigator.BusinessLogic.Models.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaInvestigator.BusinessLogic.Models.IM
{
    public class IdeaIM
    {
        [Required]
        [StringLength(320, MinimumLength = 2)]
        public string Topic { get; set; } = string.Empty;

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public string AdvertPlatforms { get; set; } = string.Empty;

        [Required]
        [StringLength(320, MinimumLength = 2)]
        public string TargetAudience { get; set; } = string.Empty;

        [Required]
        public int Budget { get; set; } = 0;

        [Required]
        public string Tags { get; set; } = string.Empty;

        [Required]
        public int NumberOfCampaigns { get; set; } = 0;
    }
}
