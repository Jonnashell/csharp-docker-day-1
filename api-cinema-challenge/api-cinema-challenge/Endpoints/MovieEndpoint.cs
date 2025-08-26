using api_cinema_challenge.DTOs;
using api_cinema_challenge.Models;
using api_cinema_challenge.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace api_cinema_challenge.Endpoints
{
    public static class MovieEndpoint
    {
        public static void ConfigureMovieEndpoint(this WebApplication app)
        {
            var moviesGroup = app.MapGroup("/movies").RequireAuthorization();
            moviesGroup.MapGet("", GetMovies);
            //moviesGroup.MapGet("movies/{id}", GetMovieById);
            moviesGroup.MapPost("", CreateMovie);
            moviesGroup.MapPut("", UpdateMovie);
            moviesGroup.MapDelete("movies", DeleteMovie);

            // Screenings
            moviesGroup.MapGet("/{id}/screenings", GetScreenings);
            moviesGroup.MapPost("/{id}/screenings", CreateScreening);
        }

        public static async Task<IResult> GetMovies(IRepository repository)
        {
            var entities = await repository.GetMovies();
            List<Movie> result = new List<Movie>();
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

        public static async Task<IResult> GetMovieById(IRepository repository, int id)
        {
            var entity = await repository.GetMovieById(id);
            return TypedResults.Ok(entity);
        }

        public static async Task<IResult> CreateMovie(IRepository repository, MoviePost model)
        {
            Movie movie = new Movie();
            movie.Title = model.Title;
            movie.Rating = model.Rating;
            movie.Description = model.Description;
            movie.RuntimeMins = model.RuntimeMins;
            movie.CreatedAt = DateTime.UtcNow;
            movie.UpdatedAt = DateTime.UtcNow;

            try
            {
                var entity = await repository.CreateMovie(movie);
                return TypedResults.Created("", new
                {
                    status = "success",
                    data = entity
                });
            }
            catch
            {
                return TypedResults.Created("", new
                {
                    status = "failed",
                    data = new List<Movie>()
                });
            }
        }

        public static async Task<IResult> UpdateMovie(IRepository repository, int id, MoviePut model)
        {
            var movie = await repository.GetMovieById(id);
            if (movie == null)
            {
                return TypedResults.Created("", new
                {
                    status = "Notfound",
                    data = movie
                });
            }

            if (model.Title != null) movie.Title = model.Title;
            if (model.Rating != null) movie.Rating = model.Rating;
            if (model.Description != null) movie.Description = model.Description;
            if (model.RuntimeMins != 0 && movie.RuntimeMins != model.RuntimeMins) movie.RuntimeMins = model.RuntimeMins;

            movie.UpdatedAt = DateTime.UtcNow;
            var entity = await repository.UpdateMovie(id, movie);
            return TypedResults.Created("", new
            {
                status = "success",
                data = entity
            });
        }

        public static async Task<IResult> DeleteMovie(IRepository repository, int id)
        {
            string statusString = "success";
            var entity = await repository.DeleteMovie(id);
            if (entity == null) statusString = "NotFound";
            return TypedResults.Ok(new
            {
                status = statusString,
                data = entity
            });
        }

        // Screenings
        public static async Task<IResult> GetScreenings(IRepository repository, int id)
        {
            var entities = await repository.GetScreenings(id);
            List<ScreeningGet> result = new List<ScreeningGet>();
            foreach (var entity in entities)
            {
                ScreeningGet screening = new ScreeningGet();
                screening.Id = entity.Id;
                screening.ScreenNumber = entity.ScreenNumber;
                screening.Capacity = entity.Capacity;
                screening.StartsAt = entity.StartsAt;
                screening.CreatedAt = entity.CreatedAt;
                screening.UpdatedAt = entity.UpdatedAt;
                result.Add(screening);
            }
            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateScreening(IRepository repository, int id, ScreeningPost model)
        {
            Screening sc = new Screening();
            sc.ScreenNumber = model.ScreenNumber;
            sc.Capacity = model.Capacity;
            sc.StartsAt = model.StartsAt;
            sc.CreatedAt = DateTime.UtcNow;
            sc.UpdatedAt = DateTime.UtcNow;
            sc.MovieId = id;

            var entity = await repository.CreateScreening(id, sc);

            ScreeningGet screening = new ScreeningGet();
            screening.Id = entity.Id;
            screening.ScreenNumber = entity.ScreenNumber;
            screening.Capacity = entity.Capacity;
            screening.StartsAt = entity.StartsAt;
            screening.CreatedAt = entity.CreatedAt;
            screening.UpdatedAt = entity.UpdatedAt;

            return TypedResults.Created("", new
            {
                status = "success",
                data = screening
            });
        }
    }
}
