using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers {
    public class MapPark {
        public static Park MapToDomain(ParkEF db) {
            try {
                return new Park(db.ParkId,db.Naam,db.Locatie);
            } catch (Exception ex) {
                throw new MapperException("MapPark - MapToDomain", ex);
            }
        }

        public static ParkEF MapToDB(Park db) {
            try {
                return new ParkEF(db.Id, db.Naam, db.Locatie, MapHuis.MapToDB(db.Huizen));
            } catch (Exception ex) {
                throw new MapperException("MapPark - MapToDB", ex);
            }
        }
    }
}
