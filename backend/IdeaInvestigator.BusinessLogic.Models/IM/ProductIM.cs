using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IdeaInvestigator.BusinessLogic.Models.IM
{
    public class ProductIM
    {
        [Required]
        [StringLength(320, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 2)]
        public string Description { get; set; }

        [Required]
        [StringLength(320, MinimumLength = 2)]
        public string Category { get; set; }

        [Required]
        [StringLength(2048, MinimumLength = 2)]
        public string Image { get; set; }
    }
}
