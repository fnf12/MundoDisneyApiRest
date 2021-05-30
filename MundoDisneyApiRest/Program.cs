using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MundoDisneyApiRest.Models;
using System;
using System.Linq;

namespace MundoDisneyApiRest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            using (var context = new DisneyContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                string[] generos = {"drama","accion","animado","romance","aventura","ciencia ficcion","comedia","crimen","documental",
                    "familia","fantasia","misterio","musical","politica","terror" };

                var genres = generos.Select(x => new Genre
                {
                    Nombre = x
                }).ToList();

                var woody = new Character() { Imagen = "link:image.jpg", Nombre = "woody", Edad = 25, Peso = 90, Historia = "vaquero del oeste" };
                var buzz = new Character() { Imagen = "link:image.jpg", Nombre = "buzz lightyear", Edad = 30, Peso = 120, Historia = "Heroe del espacio" };
                var mulann = new Character() { Imagen = "link:image.jpg", Nombre = "fa mulan", Edad = 20, Peso = 60, Historia = "Chica samurai" };
                var mushu = new Character() { Imagen = "link:image.jpg", Nombre = "mushu", Edad = 365, Peso = 10, Historia = "Dragon rojo" };
                var gaston = new Character() { Imagen = "link:image.jpg", Nombre = "gaston", Edad = 23, Peso = 100, Historia = "un villano de la peli" };
                var principe = new Character() { Imagen = "link:image.jpg", Nombre = "principe adam", Edad = 23, Peso = 90, Historia = "un principe cualquiera" };
                var simba = new Character() { Imagen = "link:image.jpg", Nombre = "simba", Edad = 10, Peso = 500, Historia = "el nuevo rey de la selva" };
                var scar = new Character() { Imagen = "link:image.jpg", Nombre = "scar", Edad = 20, Peso = 400, Historia = "villano y hrno de mufasa" };

                var toystory = new MovieTV() { Titulo = "toystory 1", Genres = { genres[1] , genres[2], genres[6] }, Characters = { woody, buzz }, FechaCreacion = new DateTime(1995,11,22) };
                var mulan = new MovieTV() { Titulo = "mulan", Genres = { genres[1], genres[2], genres[0]}, Characters = { mulann, mushu }, FechaCreacion = new DateTime(1998, 6, 19) };
                var LaBeyBes = new MovieTV() { Titulo = "la bella y la bestia", Genres = { genres[1], genres[2], genres[0], genres[3] }, Characters = { gaston, principe}, FechaCreacion = new DateTime(1991, 11, 13) };
                var reyleon = new MovieTV() { Titulo = "el rey leon", Genres = { genres[1], genres[2], genres[6], genres[4] }, Characters = { simba, scar }, FechaCreacion = new DateTime(1994, 6, 22) };

                foreach (Genre gen in genres)
                {
                    context.AddRange(gen);
                }
                context.AddRange(toystory,mulan,LaBeyBes,reyleon);
                context.SaveChanges();
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
