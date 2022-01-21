using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer;
using ParkDataLayer.Model;
using ParkDataLayer.Repositories;
using System;
using System.Configuration;

namespace ConsoleApp1 {
    internal class Program {
        static void Main(string[] args) {
            //string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ParkBeheerDB;Integrated Security=True";
            //OR
            string connectionString = ConfigurationManager.ConnectionStrings["ParkbeheerDB"].ConnectionString;
            ParkbeheerContext ctx = new ParkbeheerContext(connectionString);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            Console.WriteLine("Database aangemaakt!");

            BeheerHuizen bh = new BeheerHuizen(new HuizenRepositoryEF(connectionString));
            BeheerHuurders bhuur = new BeheerHuurders(new HuurderRepositoryEF(connectionString));
            BeheerContracten bc = new BeheerContracten(new ContractenRepositoryEF(connectionString));

            //Voorbeeld van Tom
            //Console.WriteLine("parkbeheer!");

            //Park p = new("p2", "Binnenhoeve", "Gent");
            //bh.VoegNieuwHuisToe("parklaan", 1, p);
            //bh.VoegNieuwHuisToe("parklaan", 2, p);
            //bh.VoegNieuwHuisToe("parklaan", 3, p);
            //var x = bh.GeefHuis(1);
            //x.ZetStraat("Kerkstraat");
            //x.ZetNr(11);
            //bh.UpdateHuis(x);
            //bh.ArchiveerHuis(x);
            ////Huis h1 = new Huis();
            //ParkEF pdb = new("p1", "naam", "locatie");
            //HuisEF hdb = new("straat", 5, true);
            //hdb.Park = pdb;
            //ctx.Huizen.Add(hdb);
            //ctx.SaveChanges();
            ////huurder
            //bhuur.VoegNieuweHuurderToe("jos", new Contactgegevens("email1", "tel", "adres"));
            //bhuur.VoegNieuweHuurderToe("jef", new Contactgegevens("email2", "tel", "adres"));

            //Huurperiode hp = new(DateTime.Now, 10);
            //Huurder h = new(2, "Jos", new Contactgegevens("email1", "tel", "adres"));
            //p = new Park("p1", "Buitenhoeve", "Deinze");
            //Huis huis = new(1, "Kerkstraat", 5, true, p);
            //bc.MaakContract("c2", hp, h, huis);

            //var y = bc.GeefContract("c2");
            //var t = bh.GeefHuis(1);
            //Console.WriteLine(t);

            ///*****************************************************/
            Console.WriteLine("**********Beheer Huizen************");
            Console.WriteLine("Huis Toevoegen");
            Park p = new Park("p2", "Binnenhoeve", "Gent");
            bh.VoegNieuwHuisToe("parklaan", 1, p);
            bh.VoegNieuwHuisToe("parklaan", 2, p);
            bh.VoegNieuwHuisToe("parklaan", 3, p);
            Huis h = bh.GeefHuis(1);
            Huis h1 = bh.GeefHuis(2);
            Huis h2 = bh.GeefHuis(3);
            Console.WriteLine($"{h}\n{h1}\n{h2}");
            /*****************************************************/
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nHuis Updaten");
            var x = bh.GeefHuis(1);
            x.ZetNr(11);
            x.ZetStraat("Kerkstraat");
            bh.UpdateHuis(x);
            Console.WriteLine(x);
            /***********************************/
            Console.ResetColor();
            Console.WriteLine("\nHuis Archiveren");
            bh.ArchiveerHuis(x);
            Console.WriteLine(x);

            /***********************************/
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n**********Beheer Huurders************");
            Console.WriteLine("Huurder Toevoegen");
            bhuur.VoegNieuweHuurderToe("jos", new Contactgegevens("jos@jos.be", "04865465", "josstraat 9 Gent"));
            Console.WriteLine(bhuur.GeefHuurder(1));
            bhuur.VoegNieuweHuurderToe("jef", new Contactgegevens("jef@jef.be", "04658468", "jeftstraat 7 Oostende"));
            Console.WriteLine(bhuur.GeefHuurder(2));
            Console.WriteLine("Huurders met de naam (Jos)");
            foreach (Huurder huurder in bhuur.GeefHuurders("jos")) {
                Console.WriteLine(huurder);
            }
            Console.ResetColor();
            Console.WriteLine("\nHuurder Updaten");
            Huurder huur1 = bhuur.GeefHuurder(2);
            huur1.ZetContactgegevens(new Contactgegevens("jefUpdate@jef.be", "111111", "jefstraatUpdate 9 Oostende"));
            bhuur.UpdateHuurder(huur1);
            Console.WriteLine(huur1);
            /***********************************/
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n**********Beheer Contracten************");
            Console.WriteLine("Contract Toevoegen");
            Huurperiode hp = new Huurperiode(DateTime.Now.AddDays(20), 45);
            bc.MaakContract("c1", hp, huur1, x);
            Console.WriteLine(bc.GeefContract("c1"));
            Huurperiode hp1 = new Huurperiode(DateTime.Now, 20);
            bc.MaakContract("c2", hp1, huur1, x);
            Console.WriteLine(bc.GeefContract("c2"));
            Console.ResetColor();
            Console.WriteLine("\nContract Updaten");
            hp1 = new Huurperiode(DateTime.Now, 50);
            Huurcontract hc2 = bc.GeefContract("c2");
            hc2.ZetHuurperiode(hp1);
            bc.UpdateContract(hc2);
            Console.WriteLine(hc2);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nContracten Aanvragen");
            foreach (Huurcontract hc1 in bc.GeefContracten(DateTime.Now, DateTime.Now.AddDays(354))) {
                Console.WriteLine(hc1);
            }
            Console.WriteLine($"\nContracten Aanvragen Alleen startdatum EindDatum==null");
            foreach (Huurcontract huurcontract in bc.GeefContracten(DateTime.Now, null)) {
                Console.WriteLine(huurcontract);
            }
            Console.ResetColor();
            Console.WriteLine("\nAnnuleer Contract");
            Huurcontract hcAnnuleer = bc.GeefContract("c1");
            bc.AnnuleerContract(hcAnnuleer);
            string id = hcAnnuleer.Id;
            hcAnnuleer = bc.GeefContract("c1");
            if (hcAnnuleer == null) {
                Console.WriteLine($"Contract geannuleerd met Id: {id}");
            }



        }
    }
}
