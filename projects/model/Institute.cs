using System.ComponentModel.DataAnnotations.Schema;

namespace projects.model {
    public class Institute : AFacility {

        [ForeignKey("FACULTY_ID")]
        public Faculty Faculty { get; set; }
        
    }
}