using api_cinema_challenge.Models;
using api_cinema_challenge.Data;
using Microsoft.EntityFrameworkCore;

namespace api_cinema_challenge.Repository
{
    public class Repository : IRepository
    {
        private CinemaContext _db;
        public Repository(CinemaContext db)
        {
            _db = db;
        }

        // Customers
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _db.Customers.FindAsync(id);
        }

        public async Task<ICollection<Customer>> GetCustomers()
        {
            return await _db.Customers.ToListAsync();
        }
        
        public async Task<Customer> CreateCustomer(Customer model)
        {
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<Customer> UpdateCustomer(int id, Customer model)
        {
            var entity = await GetCustomerById(id);
            entity = model;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var entity = await GetCustomerById(id);
            if (entity == null) return entity;
            _db.Customers.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        // Movies
        public async Task<Movie> GetMovieById(int id)
        {
            return await _db.Movies.FindAsync(id);
        }

        public async Task<ICollection<Movie>> GetMovies()
        {
            return await _db.Movies.ToListAsync();
        }
        
        public async Task<Movie> CreateMovie(Movie model)
        {
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        public async Task<Movie> UpdateMovie(int id, Movie model)
        {
            var entity = await GetMovieById(id);
            entity = model;
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<Movie> DeleteMovie(int id)
        {
            var entity = await GetMovieById(id);
            if (entity == null) return entity;
            _db.Movies.Remove(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        // Screenings
        public async Task<ICollection<Screening>> GetScreenings(int movieId)
        {
            return await _db.Screenings.Where(s => s.MovieId == movieId).ToListAsync();
        }
        public async Task<Screening> CreateScreening(int movieId, Screening model)
        {
            await _db.Screenings.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }

        // Tickets
        public async Task<ICollection<Ticket>> GetTickets(int customerId, int screeningId)
        {
            return await _db.Tickets
                .Where(t => t.CustomerId == customerId && t.ScreeningId == screeningId)
                .ToListAsync();
        }

        public async Task<Ticket> CreateTicket(Ticket model)
        {
            await _db.AddAsync(model);
            await _db.SaveChangesAsync();
            return model;
        }
    }
}
