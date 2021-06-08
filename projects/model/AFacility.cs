using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projects.model {
    [Table("FACILITIES")]
    public abstract class AFacility {

        [Column("FACILITY_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("FACILITY_CODE", TypeName = "VARCHAR(7)")]
        public string FacilityCode { get; set; }

        [Required]
        [Column("FACILITY_TITLE", TypeName = "VARCHAR(100)")]
        public string FacilityTitle { get; set; }
        
    }
}