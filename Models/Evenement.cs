using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetWeb3Bourse.Models {
    public class Evenement {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("Bourse")]
        public int bourseId { get; set; }
        public Bourse? bourse { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime date { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("Heure")]
        public DateTime heure { get; set; }
        [DisplayName("Valeur")]
        public double valeur { get; set; }
        [DisplayName("Variation")]
        public double variation { get; set; }

    }
}
