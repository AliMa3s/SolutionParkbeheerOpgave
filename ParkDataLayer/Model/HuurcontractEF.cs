using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model {
    public class HuurcontractEF {
        [Key]
        [Column(TypeName = "NVARCHAR(25)")] //OR u can use maxlength
        public string HuurcontractId { get; set; }

        public int HuurperiodeId { get; set; }
        [Required]
        public HuurperiodeEF Huurperiode { get; set; }

        public HuurderEF Huurder { get; set; }

        public HuisEF Huis { get; set; }

        public HuurcontractEF() {
        }

        public HuurcontractEF(string huurcontractId, HuurperiodeEF huurperiode, HuurderEF huurder, HuisEF huis) {
            HuurcontractId = huurcontractId;
            Huurperiode = huurperiode;
            Huurder = huurder;
            Huis = huis;
        }

        public HuurcontractEF(string huurcontractId, int huurperiodeId, HuurperiodeEF huurperiode, HuurderEF huurder, HuisEF huis) {
            HuurcontractId = huurcontractId;
            HuurperiodeId = huurperiodeId;
            Huurperiode = huurperiode;
            Huurder = huurder;
            Huis = huis;
        }
    }
}
