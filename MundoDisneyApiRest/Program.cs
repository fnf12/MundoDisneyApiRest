using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MundoDisneyApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

                var woody = new Character() { Nombre = "woody" };
                var buzz = new Character() { Nombre = "buzz lightyear" };
                var mulann = new Character() { Nombre = "fa mulan" };
                var mushu = new Character() { Nombre = "mushu" };
                var gaston = new Character() { Nombre = "gaston" };
                var principe = new Character() { Nombre = "principe adam" };
                var simba = new Character() { Nombre = "simba" };
                var scar = new Character() { Nombre = "scar" };

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
