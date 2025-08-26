using api_cinema_challenge.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace api_cinema_challenge.Data
{
    public class CinemaContext : IdentityUserContext<ApplicationUser>
    {
        private string _connectionString;
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customers
            Customer customer = new Customer
            {
                Id = 1,
                Name = "Jonatan",
                Email = "jonnabr@hotmail.com",
                Phone = "+4793277670",
                CreatedAt = new DateTime(2025, 01, 20, 12, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 15, 00, 00, DateTimeKind.Utc)
            };

            Customer customer2 = new Customer
            {
                Id = 2,
                Name = "Isabel",
                Email = "Isabel@hotmail.com",
                Phone = "+4792088620",
                CreatedAt = new DateTime(2025, 01, 20, 12, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 15, 00, 00, DateTimeKind.Utc)
            };

            Customer customer3 = new Customer
            {
                Id = 3,
                Name = "Marius",
                Email = "marius@hotmail.com",
                Phone = "+4798765432",
                CreatedAt = new DateTime(2025, 01, 19, 12, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 17, 00, 00, DateTimeKind.Utc)
            };

            Customer customer4 = new Customer
            {
                Id = 4,
                Name = "Emma",
                Email = "emma@hotmail.com",
                Phone = "+4791234567",
                CreatedAt = new DateTime(2025, 01, 18, 12, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 18, 00, 00, DateTimeKind.Utc)
            };

            // Movies
            Movie movie1 = new Movie
            {
                Id = 1,
                Title = "Inception",
                Rating = "PG-13",
                Description = "A skilled thief is offered a chance to have his past crimes forgiven.",
                RuntimeMins = 148,
                CreatedAt = new DateTime(2025, 01, 11, 10, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc)
            };

            Movie movie2 = new Movie
            {
                Id = 2,
                Title = "The Matrix",
                Rating = "R",
                Description = "A computer hacker learns about the true nature of reality.",
                RuntimeMins = 136,
                CreatedAt = new DateTime(2025, 01, 09, 14, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc)
            };

            Movie movie3 = new Movie
            {
                Id = 3,
                Title = "Interstellar",
                Rating = "PG-13",
                Description = "Explorers travel through a wormhole in space.",
                RuntimeMins = 169,
                CreatedAt = new DateTime(2025, 01, 13, 09, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc)
            };

            Movie movie4 = new Movie
            {
                Id = 4,
                Title = "The Dark Knight",
                Rating = "PG-13",
                Description = "Batman faces the Joker in Gotham City.",
                RuntimeMins = 152,
                CreatedAt = new DateTime(2025, 01, 06, 16, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc)
            };

            // Screenings
            Screening screening1 = new Screening
            {
                Id = 1,
                ScreenNumber = 1,
                Capacity = 100,
                StartsAt = new DateTime(2025, 01, 23, 18, 30, 00, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 01, 20, 11, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc),
                MovieId = 1
            };

            Screening screening2 = new Screening
            {
                Id = 2,
                ScreenNumber = 2,
                Capacity = 80,
                StartsAt = new DateTime(2025, 01, 24, 20, 00, 00, DateTimeKind.Utc),
                CreatedAt = new DateTime(2025, 01, 19, 11, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc),
                MovieId = 2
            };

            // Tickets
            Ticket ticket1 = new Ticket
            {
                Id = 1,
                NumSeats = 2,
                CreatedAt = new DateTime(2025, 01, 20, 13, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc),
                CustomerId = 1,
                ScreeningId = 1
            };

            Ticket ticket2 = new Ticket
            {
                Id = 2,
                NumSeats = 1,
                CreatedAt = new DateTime(2025, 01, 20, 14, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc),
                CustomerId = 2,
                ScreeningId = 1
            };

            Ticket ticket3 = new Ticket
            {
                Id = 3,
                NumSeats = 3,
                CreatedAt = new DateTime(2025, 01, 19, 15, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc),
                CustomerId = 3,
                ScreeningId = 2
            };

            Ticket ticket4 = new Ticket
            {
                Id = 4,
                NumSeats = 2,
                CreatedAt = new DateTime(2025, 01, 19, 16, 00, 00, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2025, 01, 21, 12, 00, 00, DateTimeKind.Utc),
                CustomerId = 4,
                ScreeningId = 2
            };

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Screening)
                .WithMany()
                .HasForeignKey(t => t.ScreeningId);

            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Movie)
                .WithMany()
                .HasForeignKey(s => s.MovieId);

            modelBuilder.Entity<Ticket>().HasData(new[] { ticket1, ticket2, ticket3, ticket4 });
            modelBuilder.Entity<Customer>().HasData([customer, customer2, customer3, customer4]);
            modelBuilder.Entity<Screening>().HasData(new[] { screening1, screening2 });
            modelBuilder.Entity<Movie>().HasData(new[] { movie1, movie2, movie3, movie4 });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
