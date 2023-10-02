using Microsoft.EntityFrameworkCore;
using PublisherDomain;
using System.Reflection.Emit;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PubDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
                          ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ArtistCover>().HasKey(e => new { e.ArtistId, e.CoverId });

            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        static void SeedData(ModelBuilder modelBuilder)
        {
            List<Artist> artists = new()
            {
                new Artist(){Id=1,FirstName="Mohamed", LastName="Samir"},
                new Artist(){Id=2,FirstName="Amr", LastName="Elsyliny"},
                new Artist(){Id=3,FirstName="Mustafa", LastName="El Khouly"},
            };

            modelBuilder.Entity<Artist>().HasData(artists);

            List<Cover> covers = new()
            {
                new Cover(){Id=1, DesignIdea="Ya b3eed" ,DigitalOnly=true},
                new Cover(){Id=2, DesignIdea="Ya wa74ny" ,DigitalOnly=false},
                new Cover(){Id=3, DesignIdea="Ya 3yoon" ,DigitalOnly=true},
            };

            modelBuilder.Entity<Cover>().HasData(covers);
            
                
            //List<ArtistCover> artistCovers = new()
            //{
            //    new ArtistCover(){ArtistId=1, CoverId=2},
            //    new ArtistCover(){ArtistId=1, CoverId=1},
            //    new ArtistCover(){ArtistId=2, CoverId=1},
            //    new ArtistCover(){ArtistId=1, CoverId=3},
            //    new ArtistCover(){ArtistId=3, CoverId=2},
            //    new ArtistCover(){ArtistId=2, CoverId=3},
            //    new ArtistCover(){ArtistId=3, CoverId=3},
            //};


            //modelBuilder.Entity<ArtistCover>().HasData(artistCovers);
        }
    }
}