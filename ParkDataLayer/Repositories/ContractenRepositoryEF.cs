using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using System;
using System.Collections.Generic;
using ParkDataLayer.Model;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ParkDataLayer.Repositories {
    public class ContractenRepositoryEF : IContractenRepository {

        private readonly ParkbeheerContext _ctx;

        private void SaveAndClear() {
            _ctx.SaveChanges();
            _ctx.ChangeTracker.Clear();
        }
        public ContractenRepositoryEF(string connectionString) {
            _ctx = new ParkbeheerContext(connectionString);
        }
        public void AnnuleerContract(Huurcontract contract) {
            try {
                _ctx.HuurContracten.Remove(new HuurcontractEF() { HuurcontractId = contract.Id });
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepositoryException("AnnuleerContract - ", ex);
            }
        }

        public Huurcontract GeefContract(string id) {
            try {
                return MapHuurcontract.MapToDomain(_ctx.HuurContracten.Where(h=> h.HuurcontractId == id)
                    .Include(h=>h.Huis).ThenInclude(h => h.Park).Include(h => h.Huurperiode)
                    .Include(h => h.Huurder).ThenInclude(h=> h.Contactgegevens).AsNoTracking().FirstOrDefault());
            } catch (Exception ex) {

                throw new RepositoryException("GeefContract - ", ex);
            }
        }

        public List<Huurcontract> GeefContracten(DateTime dtBegin, DateTime? dtEinde) {
            try {
                if (dtEinde == null) {
                    return _ctx.HuurContracten.Include(h => h.Huis).ThenInclude(h => h.Park)
                        .Include(h => h.Huurperiode).Include(h => h.Huurder).ThenInclude(h => h.Contactgegevens)
                        .Where(h => h.Huurperiode.StartDatum >= dtBegin)
                        .Select(h => MapHuurcontract.MapToDomain(h)).AsNoTracking().ToList();
                }
                return _ctx.HuurContracten.Include(h => h.Huis).ThenInclude(h => h.Park).Include(h => h.Huurperiode).Include(h => h.Huurder).ThenInclude(h => h.Contactgegevens)
                    .Where(h => h.Huurperiode.StartDatum >= dtBegin && h.Huurperiode.EindDatum <= dtEinde)
                    .Select(h => MapHuurcontract.MapToDomain(h)).AsNoTracking().ToList();
            } catch (Exception ex) {

                throw new RepositoryException("GeefContracten - ", ex);
            }
        }

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid) {
            try {
                return _ctx.HuurContracten.Any(h => h.Huurperiode.StartDatum == startDatum && h.Huurder.HuurderId == huurderid && h.Huis.HuisId == huisid);
            } catch (Exception ex) {

                throw new RepositoryException("HeeftContract - ", ex);
            }
        }

        public bool HeeftContract(string id) {
            try {
                return _ctx.HuurContracten.Any(h => h.HuurcontractId == id);
            } catch (Exception ex) {

                throw new RepositoryException("HeeftContract - ", ex);
            }
        }

        public void UpdateContract(Huurcontract contract) {
            try {
                _ctx.HuurContracten.Update(MapHuurcontract.MapToDB(contract));
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepositoryException("UpdateContract - ", ex);
            }
        }

        public void VoegContractToe(Huurcontract contract) {
            try {
                HuurcontractEF huurcontractEF = MapHuurcontract.MapToDB(contract,_ctx);
                _ctx.HuurContracten.Add(huurcontractEF);
                SaveAndClear();
            } catch (Exception ex) {

                throw new RepositoryException("VoegContractToe - ", ex);
            }
        }
    }
}
