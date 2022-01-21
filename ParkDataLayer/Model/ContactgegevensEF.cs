using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model {
    public class ContactgegevensEF {
        [Key]
        public int ContactgegevensId { get; set; }
        
        [Column(TypeName = "NVARCHAR(100)")]
        public string Email { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string Adres { get; set; }

        [Column(TypeName = "NVARCHAR(100)")]
        public string Tel { get; set; }

        public ContactgegevensEF() { }

        public ContactgegevensEF(int contactgegevensId, string email, string adres, string tel) {
            ContactgegevensId = contactgegevensId;
            Email = email;
            Adres = adres;
            Tel = tel;
        }

        public ContactgegevensEF(string email, string adres, string tel) {
            Email = email;
            Adres = adres;
            Tel = tel;
        }
    }
}
