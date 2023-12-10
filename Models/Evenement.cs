using System.ComponentModel.DataAnnotations;

namespace ProjetWeb3Bourse.Models {
    public class Evenement {

        public int id { get; set; }
        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        [DataType(DataType.Date)]
        public DateTime heure { get; set; }
        public double valeur { get; set; }
        public double variation { get; set; }

    }
}
