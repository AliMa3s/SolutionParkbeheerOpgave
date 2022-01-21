using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model {
    public class HuurderEF {
        [Key]
        public int HuurderId { get; set; }

        [Required, Column(TypeName = "NVARCHAR(100)")]
        public string Naam { get; set; }

        public int? ContactgegevensId { get; set; }
        public ContactgegevensEF Contactgegevens { get; set; }

        public HuurderEF() {

        }
        public HuurderEF(int huurderId, string naam, int? contactgegevensId, ContactgegevensEF contactgegevens) {
            HuurderId = huurderId;
            Naam = naam;
            ContactgegevensId = contactgegevensId;
            Contactgegevens = contactgegevens;
        }

        public HuurderEF(int huurderId, string naam, ContactgegevensEF contactgegevens) {
            HuurderId = huurderId;
            Naam = naam;
            Contactgegevens = contactgegevens;
        }
    }
}
