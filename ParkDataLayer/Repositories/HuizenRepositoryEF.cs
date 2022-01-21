using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Linq;

namespace ParkDataLayer.Repositories {
    public class HuizenRepositoryEF : IHuizenRepository {
        private readonly ParkbeheerContext _ctx;

        private void SaveAndClear() {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
        public HuizenRepositoryEF(string connectionString) {
            _ctx = new ParkbeheerContext(connectionString);
        }

        public Huis GeefHuis(int id) {
            try {
               return MapHuis.MapToDomain(_ctx.Huizen.Where(h=>h.HuisId == id).Include(h=>h.Park).AsNoTracking().FirstOrDefault());
            } catch (Exception ex) {

                throw new RepositoryException("GeefHuis - ", ex);
            }
        }

        public bool HeeftHuis(string straat, int nummer, Park park) {
            try {
                return _ctx.Huizen.Any(h=>h.Straat == straat && h.Nummer == nummer && h.Park == MapPark.MapToDB(park));
            } catch (Exception ex) {

                throw new RepositoryException("HeeftHuis - ", ex);
            }
        }

        public bool HeeftHuis(int id) {
            try {
                return _ctx.Huizen.Any(h => h.HuisId == id);
            } catch (Exception ex) {

                throw new RepositoryException("HeeftHuis - ", ex);
            }
        }

        public void UpdateHuis(Huis huis) {
            try {
                 _ctx.Huizen.Update(MapHuis.MapToDB(huis));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepositoryException("UpdateHuis - ", ex);
            }
        }

        public Huis VoegHuisToe(Huis h) {
            try {
                HuisEF huisEF = MapHuis.MapToDB(h, _ctx);
                _ctx.Huizen.Add(huisEF);
                SaveAndClear();
                h.ZetId(huisEF.HuisId);
                return h;
            } catch (Exception ex) {

                throw new RepositoryException("VoegHuisToe - ", ex);
            }

        }
    }
}
