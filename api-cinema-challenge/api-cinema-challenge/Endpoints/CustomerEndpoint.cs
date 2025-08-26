using api_cinema_challenge.DTOs;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace api_cinema_challenge.Endpoints
{
    public static class CustomerEndpoint
    {
        public static void ConfigureCustomerEndpoint(this WebApplication app)
        {
            var customerGroup = app.MapGroup("customers/").RequireAuthorization();
            customerGroup.MapGet("/", GetCustomers);
            customerGroup.MapGet("/{id}", GetCustomerById);
            customerGroup.MapPost("", CreateCustomer);
            customerGroup.MapPut("", UpdateCustomer);
            customerGroup.MapDelete("", DeleteCustomer);

            // Customers
            customerGroup.MapPost("/{customerId}/screenings/{screeningId}", CreateTicket);
            customerGroup.MapGet("/{customerId}/screenings/{screeningId}", GetTickets);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetCustomers(IRepository repository)
        {
            var entities = await repository.GetCustomers();
            List<Customer> result = new List<Customer>();
            foreach (var entity in entities)
            {
                result.Add(entity);
            }
            return TypedResults.Ok(new
            {
                status = "success",
                data = result
            });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetCustomerById(IRepository repository, int id)
        {
            string statusString = "success";
            var entity = await repository.GetCustomerById(id);
            if (entity == null) statusString = "NotFound";

            return TypedResults.Ok(new
            {
                status = statusString,
                data = entity
            });
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateCustomer(IRepository repository, CustomerPost model)
        {
            Customer customer = new Customer();
            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.Phone = model.Phone;
            customer.CreatedAt = DateTime.UtcNow;
            customer.UpdatedAt = DateTime.UtcNow;

            try
            {
                var entity = await repository.CreateCustomer(customer);
                return TypedResults.Created($"", new
                {
                    status = "success",
                    data = entity
                });
            }
            catch (Exception e)
            {
                return TypedResults.Created($"", new
                {
                    status = "failed",
                    data = new Customer()
                });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateCustomer(IRepository repository, int id, CustomerPut model)
        {
            var customer = await repository.GetCustomerById(id);
            //if (entity == null) return TypedResults.NotFound(new {Error = $"Did not find a customer with id '{id}'."});
            if (customer == null)
            {
                return TypedResults.Ok(new
                {
                    status = "NotFound",
                    data = customer
                });
            }

            if (model.Name != null) customer.Name = model.Name;
            if (model.Email != null) customer.Email = model.Email;
            if (model.Phone != null) customer.Phone = model.Phone;

            customer.UpdatedAt = DateTime.UtcNow;
            var result = await repository.UpdateCustomer(id, customer);

            return TypedResults.Ok(new
            {
                status = "success",
                data = result
            });
        }

        public static async Task<IResult> DeleteCustomer(IRepository repository, int id)
        {
            var entity = await repository.DeleteCustomer(id);
            string statusString = "success";
            if (entity == null) statusString = "NotFound"; // return TypedResults.NotFound(new { Error = $"Did not find a customer with id '{id}'." });

            return TypedResults.Ok(new
            {
                status = statusString,
                data = entity
            });
        }

        // Tickets
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetTickets(IRepository repository, int customerId, int screeningId)
        {
            var entities = await repository.GetTickets(customerId, screeningId);
            List<TicketGet> result = new List<TicketGet>();
            foreach (var entity in entities)
            {
                TicketGet ticket = new TicketGet();
                ticket.Id = entity.Id;
                ticket.numSeats = entity.NumSeats;
                ticket.CreatedAt = entity.CreatedAt;
                ticket.UpdatedAt = entity.UpdatedAt;
                result.Add(ticket);
            }

            return TypedResults.Ok(new
            {
                status = "success",
                data = result
            });
        }

        public static async Task<IResult> CreateTicket(IRepository repository, int customerId, int screeningId, TicketPost model)
        {
            Ticket ticket = new Ticket();
            ticket.CustomerId = customerId;
            ticket.ScreeningId = screeningId;
            ticket.NumSeats = model.numSeats;
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.UpdatedAt = DateTime.UtcNow;

            var entity = await repository.CreateTicket(ticket);

            TicketGet ticketResult = new TicketGet();
            ticketResult.Id = entity.Id;
            ticketResult.numSeats = entity.NumSeats;
            ticketResult.CreatedAt = entity.CreatedAt;
            ticketResult.UpdatedAt = entity.UpdatedAt;

            return TypedResults.Created("", new
            {
                status = "success",
                data = ticketResult
            });
        }
    }
}
