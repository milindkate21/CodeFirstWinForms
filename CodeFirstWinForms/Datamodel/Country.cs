using System.ComponentModel.DataAnnotations;

namespace CodeFirstWinForms.Datamodel
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
