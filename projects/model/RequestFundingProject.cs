using System.ComponentModel.DataAnnotations.Schema;

namespace projects.model {
    [Table("REQUEST_FUNDING_PROJECTS")]
    public class RequestFundingProject : AProject {

        [Column("IS_SMALL_PROJECT", TypeName = "TINYINT")]
        public bool IsSmallProject { get; set; }

        public override string ToString() {
            return $"{base.ToString()}, {nameof(IsSmallProject)}: {IsSmallProject}";
        }
    }
}