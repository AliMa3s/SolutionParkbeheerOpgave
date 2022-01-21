using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers {
    public class MapContactgegevens {
        public static Contactgegevens MapToDomain(ContactgegevensEF db) {
            try {
                return new Contactgegevens(db.Email, db.Tel, db.Adres);
            } catch (Exception ex) {

                throw new MapperException("MapToDomain - MapContactgegevens", ex);
            }
        }

        public static ContactgegevensEF MapToDB(Contactgegevens db) {
            try {
                return new ContactgegevensEF(db.Email, db.Adres, db.Tel);
            } catch (Exception ex) {

                throw new MapperException("MapToDB - MapContactgegevens", ex);
            }
        }
    }
}
