using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projects.model {
    [Table("SUBPROJECTS")]
    public class Subproject {
    
        [Column("SUBPROJECT_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DESCRIPTION", TypeName = "VARCHAR(255)")]
        public string Description { get; set; }

        [Required]
        [Column("APPLIED_RESEARCH", TypeName = "INT")]
        public int AppliedResearch { get; set; }

        [Required]
        [Column("THEORETICAL_RESEARCH", TypeName = "INT")]
        public int TheoreticalResearch { get; set; }

        [Required]
        [Column("FOCUS_RESEARCH", TypeName = "INT")]
        public int FocusResearch { get; set; }

        public AProject Project { get; set; }

        [Column("PROJECT_ID")]
        public int? ProjectId { get; set; }

        public Institute Institute { get; set; }

        [Column("INSTITUTE_ID")]
        public int? InstituteId { get; set; }
    }
}