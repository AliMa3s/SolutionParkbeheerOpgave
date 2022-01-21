using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories {
    public class HuurderRepositoryEF : IHuurderRepository {
        private readonly ParkbeheerContext _ctx;

        private void SaveAndClear() {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
        public HuurderRepositoryEF(string connectionString) {

            _ctx = new ParkbeheerContext(connectionString);
        }
        public Huurder GeefHuurder(int id) {
            try {
                return MapHuurder.MapToDomain(_ctx.Huurders.Where(h => h.HuurderId == id).Include(h => h.Contactgegevens).AsNoTracking().FirstOrDefault());
            } catch (Exception ex) {

                throw new RepositoryException("GeefHuurder - ", ex);
            }
        }

        public List<Huurder> GeefHuurders(string naam) {
            try {
                return _ctx.Huurders.Include(h=>h.Contactgegevens).Where(h => h.Naam == naam).Select(h => MapHuurder.MapToDomain(h)).AsNoTracking().ToList();
            } catch (Exception ex) {

                throw new RepositoryException("GeefHuurders - ", ex);
            }
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact) {
            try {
                return _ctx.Huurders.Any(h => h.Naam == naam && h.Contactgegevens == MapContactgegevens.MapToDB(contact));
            } catch (Exception ex) {

                throw new RepositoryException("HeeftHuurder - ", ex);
            }
        }

        public bool HeeftHuurder(int id) {
            try {
                return _ctx.Huurders.Any(h => h.HuurderId == id);
            } catch (Exception ex) {

                throw new RepositoryException("HeeftHuurder - id ", ex);
            }
        }

        public void UpdateHuurder(Huurder huurder) {
            try {
                _ctx.Huurders.Update(MapHuurder.MapToDB(huurder));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepositoryException("UpdateHuurder ", ex);
            }
        }

        public Huurder VoegHuurderToe(Huurder h) {
            try {
                _ctx.Huurders.Add(MapHuurder.MapToDB(h));
                SaveAndClear();
                return h;
            } catch (Exception ex) {

                throw new RepositoryException("VoegHuurderToe ", ex);
            }
        }
    }
}
