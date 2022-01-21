using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model {
    public class ParkEF {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(20)] //you could use this or just put it line 11 after comma its generally the same
        public string ParkId { get; set; }

        [Required, Column(TypeName = "NVARCHAR(250)")]//The column type is specificaly here added  you could just user maxlength(250 same as line 12)
        public string Naam { get; set;}

        [Column(TypeName = "NVARCHAR(500)")]
        public string Locatie { get; set; }

        public List<HuisEF> Huis { get; set; } = new();

        public ParkEF() {

        }

        public ParkEF(string parkId, string naam, string locatie, List<HuisEF> huis) {
            ParkId = parkId;
            Naam = naam;
            Locatie = locatie;
            Huis = huis;
        }

        public ParkEF(string parkId, string naam, string locatie) {
            ParkId = parkId;
            Naam = naam;
            Locatie = locatie;
        }
    }
}
