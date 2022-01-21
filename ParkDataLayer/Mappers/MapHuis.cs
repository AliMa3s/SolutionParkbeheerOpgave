using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers {
    public class MapHuis {
        public static Huis MapToDomain(HuisEF db) {
            try {
                if(db.HuurContracten.Count == 0) {
                    return new Huis(db.HuisId, db.Straat, db.Nummer, db.Actief, MapPark.MapToDomain(db.Park));
                }
                return new Huis(db.HuisId, db.Straat, db.Nummer, db.Actief, MapPark.MapToDomain(db.Park));
            } catch (Exception ex) {

                throw new MapperException("MapToDomain - MapHuis", ex);
            }
        }
        public static List<Huis> MapToDomain(ICollection<HuisEF> huizenEFS) {
            try {
                List<Huis> huizen = new();
                foreach (HuisEF huis in huizenEFS) {
                    huizen.Add(MapToDomain(huis));
                }
                return huizen;
            } catch (Exception ex) {

                throw new MapperException("MapToDomain - MapHuis", ex);
            }
        }

        public static HuisEF MapToDB(Huis huis) {
            try {
                return new HuisEF(huis.Id, huis.Straat, huis.Nr, huis.Actief, huis.Park.Id, MapPark.MapToDB(huis.Park));
            } catch (Exception ex) {
                throw new MapperException("MapHuis - MapToDB", ex);
            }
        }
        public static HuisEF MapToDB(Huis huis, Huurder huurder, ParkbeheerContext ctx) {
            try {
                ParkEF parkEF = ctx.Parken.Find(huis.Park.Id);
                if (parkEF == null) {
                    parkEF = MapPark.MapToDB(huis.Park);
                }
                return new HuisEF(huis.Id, huis.Straat, huis.Nr, huis.Actief, parkEF.ParkId, parkEF, MapHuurcontracten.MapToDB(huis.Huurcontracten(huurder)));
            } catch (Exception ex) {
                throw new MapperException("MapHuis - MapToDB", ex);
            }
        }
        public static HuisEF MapToDB(Huis huis, ParkbeheerContext ctx) {
            try {
                ParkEF parkEF = ctx.Parken.Find(huis.Park.Id);
                if (parkEF == null) {
                    parkEF = MapPark.MapToDB(huis.Park);
                }
                return new HuisEF(huis.Id, huis.Straat, huis.Nr, huis.Actief, parkEF.ParkId, parkEF);
            } catch (Exception ex) {
                throw new MapperException("MapHuis - MapToDB", ex);
            }
        }

        public static List<HuisEF> MapToDB(ICollection<Huis> huizen) {
            try {
                List<HuisEF> huizenEF = new();
                foreach (Huis huis in huizen) {
                    huizenEF.Add(MapToDB(huis));
                }
                return huizenEF;
            } catch (Exception ex) {
                throw new MapperException("MapHuis - MapToDB", ex);
            }
        }
        public static List<HuisEF> MapToDB(Func<IReadOnlyList<Huis>> huizen) {
            try {
                IReadOnlyCollection<Huis> huizenDM = huizen.Invoke();
                List<HuisEF> huizenEF = new();
                foreach (Huis huis in huizenDM) {
                    huizenEF.Add(MapToDB(huis));
                }
                return huizenEF;
            } catch (Exception ex) {
                throw new MapperException("MapHuis - MapToDB", ex);
            }
        }
    }
}
