using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjetWeb3Bourse.Models {
    public class Bourse {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [DisplayName("Nom")]
        public string nom { get; set; }
        [DisplayName("Valeur")]
        public double valeur { get; set; }
        [DisplayName("Variation")]
        public double variation { get; set; }
        public ICollection<Evenement>? Evenements { get; set; }

    }
}
