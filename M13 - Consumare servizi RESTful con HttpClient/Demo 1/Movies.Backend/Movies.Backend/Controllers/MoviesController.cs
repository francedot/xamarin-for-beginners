using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Movies.Backend.Models;
using Newtonsoft.Json;

namespace Movies.Backend.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        // GET api/movies
        [HttpGet]
        public IActionResult Get()
        {
            string content;

            try
            {
                content = JsonConvert.SerializeObject(Movies);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Content(content, "application/json");
        }

        // GET api/movies/2
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id >= Movies.Count)
            {
                return BadRequest();
            }

            string content;

            try
            {
                content = JsonConvert.SerializeObject(Movies.ElementAt(id));
            }
            catch (Exception)
            {
                return NotFound(id);
            }

            return Content(content, "application/json");
        }

        // POST api/movies
        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest();
            }

            List<Movie> movies;
            try
            {
                movies = JsonConvert.DeserializeObject<List<Movie>>(value);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
            try
            {
                Movies = movies;
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Created(new Uri(Request.GetDisplayUrl()), value);
        }

        // PUT api/movies/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest();
            }

            if (id >= Movies.Count)
            {
                return BadRequest();
            }

            Movie movie;
            try
            {
                movie = JsonConvert.DeserializeObject<Movie>(value);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            try
            {
                var newMovies = Movies;
                newMovies.Insert(id, movie);
                Movies = newMovies;
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        // DELETE api/movies/2
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id >= Movies.Count)
            {
                return BadRequest();
            }

            try
            {
                var newMovies = Movies;
                newMovies.RemoveAt(id);
                Movies = newMovies;
            }
            catch (Exception)
            {
                StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        #region Helper Methods

        public IList<Movie> Movies
        {
            get => ReadMoviesFile();
            set => WriteMoviesFile(value);
        }

        private string ReadMoviesContent()
        {
            string content;
            using (var reader = System.IO.File.OpenText("Assets/movies.json"))
            {
                content = reader.ReadToEnd();
            }

            return content;
        }

        private IList<Movie> ReadMoviesFile()
        {
            return JsonConvert.DeserializeObject<List<Movie>>(ReadMoviesContent());
        }

        private void WriteMoviesFile(IList<Movie> movies)
        {
            try
            {
                var content = JsonConvert.SerializeObject(movies);
                System.IO.File.WriteAllText("Assets/movies.json", content.Replace(@"\""", @"\\\"""));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        #endregion
    }
}
