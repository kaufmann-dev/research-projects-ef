using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projects.model {
    [Table("PROJECTS")]
    public abstract class AProject {
        
        [Column("PROJECT_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("TITLE", TypeName = "VARCHAR(100)")]
        public string  Title { get; set; }

        [Required]
        [Column("LEGAL_FOUNDATION", TypeName = "VARCHAR(4)")]
        public ELegalFoundation LegalFoundation { get; set; }

        public override string ToString() {
            return $"{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(LegalFoundation)}: {LegalFoundation}";
        }
    }
}