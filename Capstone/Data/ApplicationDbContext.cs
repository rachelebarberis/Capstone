using Capstone.Models;
using Capstone.Models.Carrello;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Paese> Paesi { get; set; }
        public DbSet<Itinerario> Itinerari { get; set; }
        public DbSet<ItinerarioGiorno> ItinerarioGiorni { get; set; }
        public DbSet<FasciaDiPrezzo> FasceDiPrezzo { get; set; }
        public DbSet<ItinerarioFasciaPrezzo> ItinerarioFascePrezzo { get; set; }
        public DbSet<Carrello> Carrelli { get; set; }
        public DbSet<CarrelloItem> CarrelloItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<ApplicationRole> ApplicationRoles { get; set; }

    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationUser).WithMany(u => u.UserRoles).HasForeignKey(a => a.UserId);

        modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationRole).WithMany(r => r.UserRoles).HasForeignKey(a => a.RoleId);
            modelBuilder.Entity<Paese>()
        .HasKey(p => p.Id);

            modelBuilder.Entity<Paese>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Itinerario
            modelBuilder.Entity<Itinerario>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<Itinerario>()
                .Property(i => i.Nome)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Itinerario>()
                .HasOne(i => i.Paese)
                .WithMany(p => p.Itinerari)
                .HasForeignKey(i => i.PaeseId);

            // ItinerarioGiorno
            modelBuilder.Entity<ItinerarioGiorno>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<ItinerarioGiorno>()
                .HasOne(g => g.Itinerario)
                .WithMany(i => i.ItinerarioGiorni)
                .HasForeignKey(g => g.ItinerarioId);

            modelBuilder.Entity<ItinerarioGiorno>()
                .Property(g => g.Titolo)
                .IsRequired()
                .HasMaxLength(200);

            // FasciaDiPrezzo
            modelBuilder.Entity<FasciaDiPrezzo>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<FasciaDiPrezzo>()
                .Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // ItinerarioFasciaPrezzo
            modelBuilder.Entity<ItinerarioFasciaPrezzo>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ItinerarioFasciaPrezzo>()
                .HasOne(p => p.Itinerario)
                .WithMany(i => i.FasceDiPrezzo)
                .HasForeignKey(p => p.ItinerarioId);

            modelBuilder.Entity<ItinerarioFasciaPrezzo>()
                .HasOne(p => p.FasciaDiPrezzo)
                .WithMany(f => f.Itinerari)
                .HasForeignKey(p => p.FasciaDiPrezzoId);

            modelBuilder.Entity<ItinerarioFasciaPrezzo>()
                .Property(p => p.Prezzo)
                .HasColumnType("decimal(10,2)");

            // Carrello
            modelBuilder.Entity<Carrello>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Carrello>()
                .Property(c => c.UserId)
                .IsRequired();

            // CarrelloItem
            modelBuilder.Entity<CarrelloItem>()
                .HasKey(ci => ci.Id);

            modelBuilder.Entity<CarrelloItem>()
                .HasOne(ci => ci.Carrello)
                .WithMany(c => c.CarrelloItems)
                .HasForeignKey(ci => ci.CarrelloId);

            modelBuilder.Entity<CarrelloItem>()
                .HasOne(ci => ci.Itinerario)
                .WithMany()
                .HasForeignKey(ci => ci.ItinerarioId);

            modelBuilder.Entity<CarrelloItem>()
                .HasOne(ci => ci.FasciaDiPrezzo)
                .WithMany()
                .HasForeignKey(ci => ci.FasciaDiPrezzoId);

            modelBuilder.Entity<CarrelloItem>()
                .Property(ci => ci.Prezzo)
                .HasColumnType("decimal(10,2)");

            var adminId = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid().ToString();

        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = adminId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = adminId
            },
             new ApplicationRole
             {
                 Id = userId,
                 Name = "User",
                 NormalizedName = "USER",
                 ConcurrencyStamp = userId
             }

        );
            modelBuilder.Entity<Paese>().HasData(
       new Paese { Id = 1, Nome = "Thailandia" },
       new Paese { Id = 2, Nome = "Cina" },
       new Paese { Id = 3, Nome = "Giappone" }
   );

            // Fasce di Prezzo
            modelBuilder.Entity<FasciaDiPrezzo>().HasData(
                new FasciaDiPrezzo { Id = 1, Nome = "Economica" },
                new FasciaDiPrezzo { Id = 2, Nome = "Standard" },
                new FasciaDiPrezzo { Id = 3, Nome = "Lusso" }
            );

            // Itinerari
            modelBuilder.Entity<Itinerario>().HasData(
                new Itinerario { Id = 1, Nome = "Tour in Thailandia", Durata = 9, PaeseId = 1 },
                new Itinerario { Id = 2, Nome = "Tour in Cina", Durata = 12, PaeseId = 2 },
                new Itinerario { Id = 3, Nome = "Tour in Giappone", Durata = 15, PaeseId = 3 }
            );

            // Giorni degli itinerari (dettaglio per ogni giorno)
            modelBuilder.Entity<ItinerarioGiorno>().HasData(
                // Giorni per il tour in Thailandia (9 giorni)
                new ItinerarioGiorno { Id = 1, Giorno = 1, Titolo = "Arrivo in Thailandia", Descrizione = "Arrivo all'aeroporto internazionale di Bangkok e trasferimento in hotel.", ItinerarioId = 1 },
                new ItinerarioGiorno { Id = 2, Giorno = 2, Titolo = "Visita al Tempio", Descrizione = "Visita al Tempio del Buddha di Smeraldo e alla Grande Sala del Trono.", ItinerarioId = 1 },
                new ItinerarioGiorno { Id = 3, Giorno = 3, Titolo = "Mercati galleggianti", Descrizione = "Esplorazione dei famosi mercati galleggianti e pranzo tipico.", ItinerarioId = 1 },
                new ItinerarioGiorno { Id = 4, Giorno = 4, Titolo = "Tour della città", Descrizione = "Visita al Palazzo Reale e al Museo Nazionale.", ItinerarioId = 1 },
                new ItinerarioGiorno { Id = 5, Giorno = 5, Titolo = "Visita a Ayutthaya", Descrizione = "Escursione a Ayutthaya, la storica capitale del regno del Siam.", ItinerarioId = 1 },
                new ItinerarioGiorno { Id = 6, Giorno = 6, Titolo = "Isola di Ko Samui", Descrizione = "Arrivo a Ko Samui per una giornata di relax sulle spiagge esotiche.", ItinerarioId = 1 },
                new ItinerarioGiorno { Id = 7, Giorno = 7, Titolo = "Snorkeling e visite alle isole", Descrizione = "Giornata dedicata allo snorkeling e visita delle isole vicine a Ko Samui.", ItinerarioId = 1 },
                new ItinerarioGiorno { Id = 8, Giorno = 8, Titolo = "Visita a Chiang Mai", Descrizione = "Trasferimento a Chiang Mai e visita ai templi locali.", ItinerarioId = 1 },
                new ItinerarioGiorno { Id = 9, Giorno = 9, Titolo = "Partenza", Descrizione = "Tempo libero per shopping e ritorno all'aeroporto per il volo di ritorno.", ItinerarioId = 1 },

                // Giorni per il tour in Cina (12 giorni)
                new ItinerarioGiorno { Id = 10, Giorno = 1, Titolo = "Arrivo in Cina", Descrizione = "Arrivo a Pechino, trasferimento in hotel e visita alla Piazza Tiananmen.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 11, Giorno = 2, Titolo = "Città Proibita", Descrizione = "Visita alla Città Proibita e al Tempio del Cielo.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 12, Giorno = 3, Titolo = "Grande Muraglia", Descrizione = "Escursione alla Grande Muraglia Cinese.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 13, Giorno = 4, Titolo = "Pechino Moderna", Descrizione = "Visita alla zona moderna di Pechino, comprensiva di un tour del quartiere commerciale.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 14, Giorno = 5, Titolo = "Xian e l'Esercito di Terracotta", Descrizione = "Volo per Xian e visita all'Esercito di Terracotta.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 15, Giorno = 6, Titolo = "Visita a Chengdu", Descrizione = "Arrivo a Chengdu e visita al centro di ricerca per la protezione del panda gigante.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 16, Giorno = 7, Titolo = "Tour di Chengdu", Descrizione = "Visita a un monastero buddista e passeggiata nei parchi locali.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 17, Giorno = 8, Titolo = "Pechino Antica", Descrizione = "Visita ai templi e mercati storici della vecchia Pechino.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 18, Giorno = 9, Titolo = "Viaggio a Shanghai", Descrizione = "Partenza per Shanghai e visita al Bund e alla zona moderna della città.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 19, Giorno = 10, Titolo = "Shanghai Antica", Descrizione = "Visita ai templi antichi e al Giardino Yu.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 20, Giorno = 11, Titolo = "Shopping a Shanghai", Descrizione = "Giornata dedicata allo shopping nei quartieri più famosi di Shanghai.", ItinerarioId = 2 },
                new ItinerarioGiorno { Id = 21, Giorno = 12, Titolo = "Partenza", Descrizione = "Trasferimento all'aeroporto per il volo di ritorno.", ItinerarioId = 2 },

                // Giorni per il tour in Giappone (15 giorni)
                new ItinerarioGiorno { Id = 22, Giorno = 1, Titolo = "Arrivo in Giappone", Descrizione = "Arrivo a Tokyo e sistemazione in hotel.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 23, Giorno = 2, Titolo = "Tempio di Asakusa", Descrizione = "Visita al Tempio di Senso-ji e al mercato di Nakamise.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 24, Giorno = 3, Titolo = "Visita al Palazzo Imperiale", Descrizione = "Visita al Palazzo Imperiale e al parco circostante.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 25, Giorno = 4, Titolo = "Escursione a Nikko", Descrizione = "Visita al Santuario Toshogu e al Parco Nazionale di Nikko.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 26, Giorno = 5, Titolo = "Visita a Kyoto", Descrizione = "Visita ai templi di Kyoto, incluso il famoso Tempio d’Oro.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 27, Giorno = 6, Titolo = "Nara", Descrizione = "Escursione a Nara, famosa per il Parco dei Cervi e il Tempio Todai-ji.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 28, Giorno = 7, Titolo = "Osaka", Descrizione = "Esplorazione di Osaka, con visita al Castello di Osaka e al quartiere Dotonbori.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 29, Giorno = 8, Titolo = "Hiroshima", Descrizione = "Visita al Parco della Pace e al Museo della Pace di Hiroshima.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 30, Giorno = 9, Titolo = "Miyajima", Descrizione = "Escursione all'isola di Miyajima, famosa per il Torii galleggiante.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 31, Giorno = 10, Titolo = "Kobe", Descrizione = "Visita alla città di Kobe, famosa per la carne di manzo Kobe.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 32, Giorno = 11, Titolo = "Hakone", Descrizione = "Visita alla zona termale di Hakone e alle sue splendide vedute del Monte Fuji.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 33, Giorno = 12, Titolo = "Tokyo Disneyland", Descrizione = "Giornata dedicata a Tokyo Disneyland.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 34, Giorno = 13, Titolo = "Shopping a Tokyo", Descrizione = "Giornata di shopping a Shibuya e Shinjuku.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 35, Giorno = 14, Titolo = "Escursione al Monte Fuji", Descrizione = "Escursione al Monte Fuji e visita ai suoi laghi.", ItinerarioId = 3 },
                new ItinerarioGiorno { Id = 36, Giorno = 15, Titolo = "Partenza", Descrizione = "Trasferimento all'aeroporto di Tokyo per il volo di ritorno.", ItinerarioId = 3 }
            );
            modelBuilder.Entity<ItinerarioFasciaPrezzo>().HasData(
    // Itinerario 1 - Tour in Thailandia
    new ItinerarioFasciaPrezzo { Id = 1, ItinerarioId = 1, FasciaDiPrezzoId = 1, Prezzo = 999.99m }, // Economica
    new ItinerarioFasciaPrezzo { Id = 2, ItinerarioId = 1, FasciaDiPrezzoId = 2, Prezzo = 1499.99m }, // Standard
    new ItinerarioFasciaPrezzo { Id = 3, ItinerarioId = 1, FasciaDiPrezzoId = 3, Prezzo = 1999.99m }, // Lusso

    // Itinerario 2 - Tour in Cina
    new ItinerarioFasciaPrezzo { Id = 4, ItinerarioId = 2, FasciaDiPrezzoId = 1, Prezzo = 1200.00m }, // Economica
    new ItinerarioFasciaPrezzo { Id = 5, ItinerarioId = 2, FasciaDiPrezzoId = 2, Prezzo = 1700.00m }, // Standard
    new ItinerarioFasciaPrezzo { Id = 6, ItinerarioId = 2, FasciaDiPrezzoId = 3, Prezzo = 2200.00m }, // Lusso

    // Itinerario 3 - Tour in Giappone
    new ItinerarioFasciaPrezzo { Id = 7, ItinerarioId = 3, FasciaDiPrezzoId = 1, Prezzo = 1500.00m }, // Economica
    new ItinerarioFasciaPrezzo { Id = 8, ItinerarioId = 3, FasciaDiPrezzoId = 2, Prezzo = 2000.00m }, // Standard
    new ItinerarioFasciaPrezzo { Id = 9, ItinerarioId = 3, FasciaDiPrezzoId = 3, Prezzo = 2500.00m }  // Lusso

);
        }
    }
}
    
