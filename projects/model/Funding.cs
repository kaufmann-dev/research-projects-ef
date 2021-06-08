using System.ComponentModel.DataAnnotations.Schema;

namespace projects.model {
    
    [Table("FUNDING")]
    public class Funding {

        [Column("PROJECT_ID")]
        public int ProjectId { get; set; }

        [Column("DEBITOR_ID")]
        public int DebitorId { get; set; }

        public Debitor Debitor { get; set; }

        public AProject Project { get; set; }

        [Column("AMOUNT", TypeName = "DECIMAL(10,2)")]
        public float Amount { get; set; }
    }
}