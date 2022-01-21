using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers {
    public class MapHuurcontracten {
        public static Dictionary<Huurder,List<Huurcontract>> MapToDomain(List<HuurcontractEF> db) {
            try {
                List<Huurcontract> hc = new();
                foreach (HuurcontractEF EF in db) {
                    hc.Add(MapHuurcontract.MapToDomain(EF));
                }
                Dictionary<Huurder, List<Huurcontract>> huurcontracten = hc.GroupBy(h=> h.Huurder).ToDictionary(h => h.Key, h => h.ToList());
                return huurcontracten;
            } catch (Exception ex) {

                throw new MapperException("MapToDomain - MapHuurcontracten", ex);
            }
        }

        public static List<HuurcontractEF> MapToDB(IReadOnlyList<Huurcontract> db) {
            try {
                List<HuurcontractEF> hc = new();
                foreach (var x in db) {
                    hc.Add(MapHuurcontract.MapToDB(x));
                }
                return hc;

            } catch (Exception ex) {

                throw new MapperException("MapToDB - MapHuurcontracten", ex);
            }
        }
    }
}
