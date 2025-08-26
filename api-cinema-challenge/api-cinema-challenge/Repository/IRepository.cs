using api_cinema_challenge.Models;

namespace api_cinema_challenge.Repository
{
    public interface IRepository
    {
        // Customers
        Task<ICollection<Customer>> GetCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(int id, Customer customer);
        Task<Customer> DeleteCustomer(int id);

        // Movies
        Task<ICollection<Movie>> GetMovies();
        Task<Movie> GetMovieById(int id);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(int id, Movie movie);
        Task<Movie> DeleteMovie(int id);

        // Screenings
        Task<ICollection<Screening>> GetScreenings(int movieId);
        Task<Screening> CreateScreening(int movieId, Screening screening);

        // Tickets
        Task<ICollection<Ticket>> GetTickets(int customerId, int screeningId);
        Task<Ticket> CreateTicket(Ticket ticket);
    }
}
