using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetWeb3Bourse.Models {
    public class Bourse {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string nom { get; set; }
        public double valeur { get; set; }
        public double variation { get; set; }
        public ICollection<Evenement>? Evenements { get; set; }

    }
}
