using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers {
    public class MapHuurperiode {
        public static Huurperiode MapToDomain(HuurperiodeEF db) {
            try {
                return new Huurperiode(db.StartDatum,db.Aantaldagen);
            } catch (Exception ex) {
                throw new MapperException("MapHuurperiode - MapToDomain", ex);
            }
        }

        public static HuurperiodeEF MapToDB(Huurperiode db) {
            try {
                return new HuurperiodeEF(db.StartDatum, db.EindDatum, db.Aantaldagen);
            } catch (Exception ex) {
                throw new MapperException("MapToDB - MapToDomain", ex);
            }
        }
    }
}
