using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers {
    public class MapHuurcontract {
        public static Huurcontract MapToDomain(HuurcontractEF db) {
            try {
                if (db == null) {
                    return null;
                }
                return new Huurcontract(db.HuurcontractId,MapHuurperiode.MapToDomain(db.Huurperiode), MapHuurder.MapToDomain(db.Huurder), MapHuis.MapToDomain(db.Huis));
            } catch (Exception ex) {

                throw new MapperException("MapToDomain - MapHuurContract", ex);
            }
        }

        public static HuurcontractEF MapToDB(Huurcontract cnt, ParkbeheerContext ctx) {
            try {
                HuisEF huisEF = ctx.Huizen.Find(cnt.Huis.Id);
                if (huisEF == null) {
                    huisEF = MapHuis.MapToDB(cnt.Huis, cnt.Huurder, ctx);
                }
                HuurderEF huurderEF = ctx.Huurders.Find(cnt.Huurder.Id);
                if (huurderEF == null) {
                    huurderEF = MapHuurder.MapToDB(cnt.Huurder);
                }
                return new HuurcontractEF(cnt.Id, MapHuurperiode.MapToDB(cnt.Huurperiode), huurderEF, huisEF);
            } catch (Exception ex) {

                throw new MapperException("MapToDB - MapHuurContract", ex);
            }
        }
        public static HuurcontractEF MapToDB(Huurcontract cnt) {
            try {
                return new HuurcontractEF(cnt.Id, MapHuurperiode.MapToDB(cnt.Huurperiode), MapHuurder.MapToDB(cnt.Huurder), MapHuis.MapToDB(cnt.Huis));
            } catch (Exception ex) {

                throw new MapperException("MapToDB - MapHuurContract", ex);
            }
        }
    }
}
