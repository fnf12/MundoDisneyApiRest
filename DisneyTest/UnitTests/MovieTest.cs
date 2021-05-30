using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MundoDisneyApiRest.Controllers;
using MundoDisneyApiRest.DTOs;
using MundoDisneyApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyTest.UnitTests
{
    [TestClass]
    public class MovieTest:BaseTests
    {
        [TestMethod]
        public async Task GetAllMovieTVs()
        {
            // Arranger
            var pruebaBD = "pruebabd";
            var context = MakeContext(pruebaBD);

            context.MovieTVs.Add(new MovieTV() { Imagen = "link:image.jpg", Titulo = "toystory", FechaCreacion = new DateTime(1995, 11, 22) });
            context.MovieTVs.Add(new MovieTV() { Imagen = "link:image.jpg", Titulo = "mulan", FechaCreacion = new DateTime(1998, 6, 19) });

            await context.SaveChangesAsync();

            var context2 = MakeContext(pruebaBD);

            // Act
            var controller = new MovieTVsController(context2);
            var response = await controller.GetMovieTVs(null, null, null);

            // Assert
            var MovieTVs = response.Value;
            Assert.AreEqual(2, MovieTVs.Count());
        }

        [TestMethod]
        public async Task GetMovieTVByIdNonExistent()
        {
            // Arranger
            var pruebaBD = Guid.NewGuid().ToString();
            var context = MakeContext(pruebaBD);

            // Act
            var controller = new MovieTVsController(context);
            var response = await controller.GetMovieTV(1);

            // Assert
            var okResult = response.Result as StatusCodeResult;

            Assert.AreEqual(404, okResult.StatusCode);

        }

        [TestMethod]
        public async Task GetMovieTVByIdExistent()
        {
            // Arranger
            var pruebaBD = Guid.NewGuid().ToString();
            var context = MakeContext(pruebaBD);

            context.MovieTVs.Add(new MovieTV() { Imagen = "link:image.jpg", Titulo = "toystory", FechaCreacion = new DateTime(1995, 11, 22) });
            await context.SaveChangesAsync();

            // Act
            var context2 = MakeContext(pruebaBD);
            var controller = new MovieTVsController(context2);
            var id = 1;

            // Assert
            var response = await controller.GetMovieTV(id);
            var result = response.Value;
            Assert.AreEqual(id, result.IdMovieTV);
        }

        [TestMethod]
        public async Task CreateMovieTV()
        {
            // Arranger
            var pruebaBD = Guid.NewGuid().ToString();
            var context = MakeContext(pruebaBD);

            var woody = new MoviePostDto() { Imagen = "link:image.jpg", Titulo = "toystory", FechaCreacion = new DateTime(1995, 11, 22) };

            // Act
            var controller = new MovieTVsController(context);


            // Assert
            await controller.PostMovieTV(woody);

            var count = context.MovieTVs.Count();

            Assert.AreEqual(1, count);

        }
    }
}
