using Capstone.Models;
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
        public DbSet<Partenza> Partenze { get; set; }
        public DbSet<Carrello> Carrelli { get; set; }
        public DbSet<CarrelloItem> CarrelloItems { get; set; }
        public DbSet<Recensione> Recensioni {  get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<ApplicationRole> ApplicationRoles { get; set; }

    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationUser).WithMany(u => u.UserRoles).HasForeignKey(a => a.UserId);

        modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationRole).WithMany(r => r.UserRoles).HasForeignKey(a => a.RoleId);
            modelBuilder.Entity<Paese>()
        .HasKey(p => p.IdPaese);

            modelBuilder.Entity<Paese>()
                .Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Itinerario
            modelBuilder.Entity<Itinerario>()
                .HasKey(i => i.IdItinerario);

            modelBuilder.Entity<Itinerario>()
                .Property(i => i.NomeItinerario)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Itinerario>()
                .HasOne(i => i.Paese)
                .WithMany(p => p.Itinerari)
                .HasForeignKey(i => i.PaeseId);

            // ItinerarioGiorno
            modelBuilder.Entity<ItinerarioGiorno>()
                .HasKey(g => g.IdItinerarioGiorno);

            modelBuilder.Entity<ItinerarioGiorno>()
                .HasOne(g => g.Itinerario)
                .WithMany(i => i.ItinerarioGiorni)
                .HasForeignKey(g => g.IdItinerario);

            modelBuilder.Entity<ItinerarioGiorno>()
                .Property(g => g.Titolo)
                .IsRequired()
                .HasMaxLength(200);

            // FasciaDiPrezzo
            modelBuilder.Entity<FasciaDiPrezzo>()
                .HasKey(f => f.IdFasciaDiPrezzo);

            modelBuilder.Entity<FasciaDiPrezzo>()
                .Property(f => f.Nome)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<ItinerarioFasciaPrezzo>()
                .HasKey(p => p.IdItinerarioFasciaPrezzo);

            modelBuilder.Entity<ItinerarioFasciaPrezzo>()
                .HasOne(p => p.Itinerario)
                .WithMany(i => i.ItinerarioFascePrezzo)
                .HasForeignKey(p => p.IdItinerario);

            modelBuilder.Entity<ItinerarioFasciaPrezzo>()
                .HasOne(p => p.FasciaDiPrezzo)
                .WithMany(f => f.Itinerari)
                .HasForeignKey(p => p.IdFasciaDiPrezzo);

            modelBuilder.Entity<ItinerarioFasciaPrezzo>()
                .Property(p => p.Prezzo)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<ApplicationUser>()
        .HasOne(a => a.Carrello)
        .WithOne(c => c.User)
        .HasForeignKey<Carrello>(c => c.UserId)
        .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Carrello>()
                .HasMany(c => c.CarrelloItems)
                .WithOne()
                .HasForeignKey("IdCarrello")
                .OnDelete(DeleteBehavior.Cascade);

            // CarrelloItem
            modelBuilder.Entity<CarrelloItem>()
                .HasKey(ci => ci.IdCarrelloItem);

            modelBuilder.Entity<CarrelloItem>()
                .HasOne(ci => ci.Carrello)
                .WithMany(c => c.CarrelloItems)
                .HasForeignKey(ci => ci.IdCarrello);

            modelBuilder.Entity<CarrelloItem>()
                .HasOne(ci => ci.Itinerario)
                .WithMany()
                .HasForeignKey(ci => ci.IdItinerario)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<CarrelloItem>()
                .HasOne(ci => ci.ItinerarioFasciaPrezzo)
                .WithMany()
                .HasForeignKey(ci => ci.IdItinerarioFasciaPrezzo)
                  .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CarrelloItem>()
                .HasOne(ci => ci.Partenza)
                .WithMany()  
                 .HasForeignKey(ci => ci.IdPartenza)
                 
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CarrelloItem>()
                .Property(ci => ci.Prezzo)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Recensione>()
        .HasOne(r => r.User)  
        .WithMany(u => u.Recensioni)  
        .HasForeignKey(r => r.UserId) 
        .OnDelete(DeleteBehavior.Cascade); 

   
            modelBuilder.Entity<Recensione>()
                .HasOne(r => r.Itinerario)  
                .WithMany(i => i.Recensioni)  
                .HasForeignKey(r => r.IdItinerario) 
                .OnDelete(DeleteBehavior.Cascade);

          

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
       new Paese { IdPaese = 1, Nome = "Thailandia" },
       new Paese { IdPaese = 2, Nome = "Cina" },
       new Paese { IdPaese = 3, Nome = "Giappone" }
   );

            // Fasce di Prezzo
            modelBuilder.Entity<FasciaDiPrezzo>().HasData(
                new FasciaDiPrezzo { IdFasciaDiPrezzo = 1, Nome = "Economica" },
                new FasciaDiPrezzo { IdFasciaDiPrezzo = 2, Nome = "Standard" },
                new FasciaDiPrezzo { IdFasciaDiPrezzo = 3, Nome = "Lusso" }
            );

            // Itinerari
            modelBuilder.Entity<Itinerario>().HasData(
     new Itinerario
     {
         IdItinerario = 1,
         NomeItinerario = "Tour in Thailandia",
         Durata = 9,
         PaeseId = 1,
         ImmagineUrl = "https://hips.hearstapps.com/cosmopolitan-it/assets/17/42/1508506981-thailandia-bangkok-cosa-visitare.jpg"
     },
     new Itinerario
     {
         IdItinerario = 2,
         NomeItinerario = "Tour in Cina",
         Durata = 12,
         PaeseId = 2,
         ImmagineUrl = "https://www.wwf.ch/sites/default/files/styles/page_cover_large_16_9/public/2017-02/Die-grosse-Mauer-in-China.jpg?h=6d1dd041&itok=4DpZ4xH4"
     },
     new Itinerario
     {
         IdItinerario = 3,
         NomeItinerario = "Tour in Giappone",
         Durata = 15,
         PaeseId = 3,
         ImmagineUrl = "https://img.freepik.com/foto-gratuito/la-fioritura-dei-ciliegi-in-primavera-la-pagoda-chureito-e-il-monte-fuji-al-tramonto-in-giappone_335224-215.jpg"
     }
 );


            // Giorni degli itinerari (dettaglio per ogni giorno)
            modelBuilder.Entity<ItinerarioGiorno>().HasData(
                // Giorni per il tour in Thailandia (9 giorni)
                new ItinerarioGiorno { IdItinerarioGiorno = 1, Giorno = 1, Titolo = "Arrivo in Thailandia", Descrizione = "Arrivo all'aeroporto internazionale di Bangkok e trasferimento in hotel.", IdItinerario = 1 },
                new ItinerarioGiorno { IdItinerarioGiorno = 2, Giorno = 2, Titolo = "Visita al Tempio", Descrizione = "Visita al Tempio del Buddha di Smeraldo e alla Grande Sala del Trono.", IdItinerario= 1 },
                new ItinerarioGiorno { IdItinerarioGiorno = 3, Giorno = 3, Titolo = "Mercati galleggianti", Descrizione = "Esplorazione dei famosi mercati galleggianti e pranzo tipico.", IdItinerario = 1 },
                new ItinerarioGiorno { IdItinerarioGiorno = 4, Giorno = 4, Titolo = "Tour della città", Descrizione = "Visita al Palazzo Reale e al Museo Nazionale.", IdItinerario = 1 },
                new ItinerarioGiorno { IdItinerarioGiorno = 5, Giorno = 5, Titolo = "Visita a Ayutthaya", Descrizione = "Escursione a Ayutthaya, la storica capitale del regno del Siam.", IdItinerario = 1 },
                new ItinerarioGiorno { IdItinerarioGiorno = 6, Giorno = 6, Titolo = "Isola di Ko Samui", Descrizione = "Arrivo a Ko Samui per una giornata di relax sulle spiagge esotiche.", IdItinerario = 1 },
                new ItinerarioGiorno { IdItinerarioGiorno = 7, Giorno = 7, Titolo = "Snorkeling e visite alle isole", Descrizione = "Giornata dedicata allo snorkeling e visita delle isole vicine a Ko Samui.", IdItinerario = 1 },
                new ItinerarioGiorno { IdItinerarioGiorno = 8, Giorno = 8, Titolo = "Visita a Chiang Mai", Descrizione = "Trasferimento a Chiang Mai e visita ai templi locali.", IdItinerario = 1 },
                new ItinerarioGiorno { IdItinerarioGiorno = 9, Giorno = 9, Titolo = "Partenza", Descrizione = "Tempo libero per shopping e ritorno all'aeroporto per il volo di ritorno.", IdItinerario = 1 },

                // Giorni per il tour in Cina (12 giorni)
                new ItinerarioGiorno { IdItinerarioGiorno = 10, Giorno = 1, Titolo = "Arrivo in Cina", Descrizione = "Arrivo a Pechino, trasferimento in hotel e visita alla Piazza Tiananmen.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 11, Giorno = 2, Titolo = "Città Proibita", Descrizione = "Visita alla Città Proibita e al Tempio del Cielo.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 12, Giorno = 3, Titolo = "Grande Muraglia", Descrizione = "Escursione alla Grande Muraglia Cinese.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 13, Giorno = 4, Titolo = "Pechino Moderna", Descrizione = "Visita alla zona moderna di Pechino, comprensiva di un tour del quartiere commerciale.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno  = 14, Giorno = 5, Titolo = "Xian e l'Esercito di Terracotta", Descrizione = "Volo per Xian e visita all'Esercito di Terracotta.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 15, Giorno = 6, Titolo = "Visita a Chengdu", Descrizione = "Arrivo a Chengdu e visita al centro di ricerca per la protezione del panda gigante.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 16, Giorno = 7, Titolo = "Tour di Chengdu", Descrizione = "Visita a un monastero buddista e passeggiata nei parchi locali.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 17, Giorno = 8, Titolo = "Pechino Antica", Descrizione = "Visita ai templi e mercati storici della vecchia Pechino.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 18, Giorno = 9, Titolo = "Viaggio a Shanghai", Descrizione = "Partenza per Shanghai e visita al Bund e alla zona moderna della città.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 19, Giorno = 10, Titolo = "Shanghai Antica", Descrizione = "Visita ai templi antichi e al Giardino Yu.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 20, Giorno = 11, Titolo = "Shopping a Shanghai", Descrizione = "Giornata dedicata allo shopping nei quartieri più famosi di Shanghai.", IdItinerario = 2 },
                new ItinerarioGiorno { IdItinerarioGiorno = 21, Giorno = 12, Titolo = "Partenza", Descrizione = "Trasferimento all'aeroporto per il volo di ritorno.", IdItinerario = 2 },

                // Giorni per il tour in Giappone (15 giorni)
                new ItinerarioGiorno { IdItinerarioGiorno = 22, Giorno = 1, Titolo = "Arrivo in Giappone", Descrizione = "Arrivo a Tokyo e sistemazione in hotel.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 23, Giorno = 2, Titolo = "Tempio di Asakusa", Descrizione = "Visita al Tempio di Senso-ji e al mercato di Nakamise.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 24, Giorno = 3, Titolo = "Visita al Palazzo Imperiale", Descrizione = "Visita al Palazzo Imperiale e al parco circostante.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 25, Giorno = 4, Titolo = "Escursione a Nikko", Descrizione = "Visita al Santuario Toshogu e al Parco Nazionale di Nikko.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 26, Giorno = 5, Titolo = "Visita a Kyoto", Descrizione = "Visita ai templi di Kyoto, incluso il famoso Tempio d’Oro.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 27, Giorno = 6, Titolo = "Nara", Descrizione = "Escursione a Nara, famosa per il Parco dei Cervi e il Tempio Todai-ji.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 28, Giorno = 7, Titolo = "Osaka", Descrizione = "Esplorazione di Osaka, con visita al Castello di Osaka e al quartiere Dotonbori.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 29, Giorno = 8, Titolo = "Hiroshima", Descrizione = "Visita al Parco della Pace e al Museo della Pace di Hiroshima.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 30, Giorno = 9, Titolo = "Miyajima", Descrizione = "Escursione all'isola di Miyajima, famosa per il Torii galleggiante.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 31, Giorno = 10, Titolo = "Kobe", Descrizione = "Visita alla città di Kobe, famosa per la carne di manzo Kobe.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 32, Giorno = 11, Titolo = "Hakone", Descrizione = "Visita alla zona termale di Hakone e alle sue splendide vedute del Monte Fuji.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 33, Giorno = 12, Titolo = "Tokyo Disneyland", Descrizione = "Giornata dedicata a Tokyo Disneyland.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 34, Giorno = 13, Titolo = "Shopping a Tokyo", Descrizione = "Giornata di shopping a Shibuya e Shinjuku.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 35, Giorno = 14, Titolo = "Escursione al Monte Fuji", Descrizione = "Escursione al Monte Fuji e visita ai suoi laghi.", IdItinerario = 3 },
                new ItinerarioGiorno { IdItinerarioGiorno = 36, Giorno = 15, Titolo = "Partenza", Descrizione = "Trasferimento all'aeroporto di Tokyo per il volo di ritorno.", IdItinerario = 3 }
            );
            modelBuilder.Entity<ItinerarioFasciaPrezzo>().HasData(
    // Itinerario 1 - Tour in Thailandia
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 1, IdItinerario = 1, IdFasciaDiPrezzo = 1, Prezzo = 999.99m }, // Economica
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 2, IdItinerario = 1, IdFasciaDiPrezzo = 2, Prezzo = 1499.99m }, // Standard
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 3, IdItinerario = 1, IdFasciaDiPrezzo = 3, Prezzo = 1999.99m }, // Lusso

    // Itinerario 2 - Tour in Cina
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 4, IdItinerario = 2, IdFasciaDiPrezzo = 1, Prezzo = 1200.00m }, // Economica
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 5, IdItinerario = 2, IdFasciaDiPrezzo = 2, Prezzo = 1700.00m }, // Standard
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 6, IdItinerario = 2, IdFasciaDiPrezzo = 3, Prezzo = 2200.00m }, // Lusso

    // Itinerario 3 - Tour in Giappone
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 7, IdItinerario = 3, IdFasciaDiPrezzo = 1, Prezzo = 1500.00m }, // Economica
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 8, IdItinerario = 3, IdFasciaDiPrezzo = 2, Prezzo = 2000.00m }, // Standard
    new ItinerarioFasciaPrezzo { IdItinerarioFasciaPrezzo = 9, IdItinerario = 3, IdFasciaDiPrezzo = 3, Prezzo = 2500.00m }  // Lusso

);
            modelBuilder.Entity<Partenza>().HasData(
    new Partenza { IdPartenza = 1, IdItinerario = 1, DataPartenza = new DateOnly(2025, 6, 10), PostiDisponibili = 15, Stato = "Disponibile" },
    new Partenza { IdPartenza = 2, IdItinerario = 1, DataPartenza = new DateOnly(2025, 7, 20), PostiDisponibili = 10, Stato = "Disponibile" },
    new Partenza { IdPartenza = 3, IdItinerario = 2, DataPartenza = new DateOnly(2025, 8, 5), PostiDisponibili = 5, Stato = "Sold Out" }
);

            modelBuilder.Entity<ApplicationUser>().HasData(
       new ApplicationUser
       {
           Id = "1",
           UserName = "user1@example.com",
           Email = "user1@example.com",
           FirstName = "Mario",
           LastName = "Rossi",
           NormalizedUserName = "USER1@EXAMPLE.COM",
           NormalizedEmail = "USER1@EXAMPLE.COM",
           PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password123!")

       },
       new ApplicationUser
       {
           Id = "2",
           UserName = "user2@example.com",
           Email = "user2@example.com",
           FirstName = "Luca",
           LastName = "Bianchi",
           NormalizedUserName = "USER2@EXAMPLE.COM",
           NormalizedEmail = "USER2@EXAMPLE.COM",
           PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(null, "Password123!")
       }
   );
           


            modelBuilder.Entity<Recensione>().HasData(
       new Recensione
       {
           IdRecensione = 1,
           Commento = "Un tour fantastico, lo consiglio a tutti!",
           Valutazione = 5,
           CreatedAt = DateOnly.FromDateTime(DateTime.Now),
           UserId = "1",  
           IdItinerario = 1 
       },
       new Recensione
       {
           IdRecensione = 2,
           Commento = "Ottimo, ma il prezzo potrebbe essere più basso.",
           Valutazione = 4,
           CreatedAt = DateOnly.FromDateTime(DateTime.Now),
           UserId = "2",  
           IdItinerario = 2  
       }
   );
        }
    }
}
    
