using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model {
    public class HuurperiodeEF {
        [Key]
        public int HuurperiodeId { get; set; }
        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        [Required]
        public int Aantaldagen { get; set; }

        public HuurperiodeEF() {

        }

        public HuurperiodeEF(int huurperiodeId, DateTime startDatum, DateTime eindDatum, int aantaldagen) {
            HuurperiodeId = huurperiodeId;
            StartDatum = startDatum;
            EindDatum = eindDatum;
            Aantaldagen = aantaldagen;
        }

        public HuurperiodeEF(DateTime startDatum, DateTime eindDatum, int aantaldagen) {
            StartDatum = startDatum;
            EindDatum = eindDatum;
            Aantaldagen = aantaldagen;
        }

        public HuurperiodeEF(DateTime startDatum, int aantaldagen) {
            StartDatum = startDatum;
            Aantaldagen = aantaldagen;
        }

        public HuurperiodeEF(int huurperiodeId, DateTime startDatum, int aantaldagen) {
            HuurperiodeId = huurperiodeId;
            StartDatum = startDatum;
            Aantaldagen = aantaldagen;
        }
    }
}
