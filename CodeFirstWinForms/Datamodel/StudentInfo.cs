using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstWinForms.Datamodel
{
    public class StudentInfo
    {
        [Key]
        public int id { get; set; }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String phonenumber { get; set; }
        public String city { get; set; }
        public String typeValue { get; set; }
        public int recordCount { get; set; }

        [ForeignKey("Country")]
        public int countryID { get; set; }
        public Country Country { get; set; }
    }
}
