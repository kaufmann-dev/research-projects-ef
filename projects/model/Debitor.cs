using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projects.model {
    [Table("DEBITORS")]
    public class Debitor {

        [Column("DEBITOR_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("NAME", TypeName = "VARCHAR(100)")]
        public string Name { get; set; }

        [Column("DESCRIPTION", TypeName = "VARCHAR(255)")]
        public string Description { get; set; }
        
    }
}