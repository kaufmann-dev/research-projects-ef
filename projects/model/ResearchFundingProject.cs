using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projects.model {
    [Table("RESEARCH_FUNDING_PROJECT")]
    public class ResearchFundingProject : AProject {

        [Column("IS_FWF_FUNDED", TypeName = "TINYINT")]
        [Required]
        public bool IsFWFFunded { get; set; }

        [Column("IS_FFG_FUNDED", TypeName = "TINYINT")]
        [Required]
        public bool IsFFGFunded { get; set; }

        [Column("IS_EU_FUNDED", TypeName = "TINYINT")]
        [Required]
        public bool IsEUFunded { get; set; }
        
    }
}