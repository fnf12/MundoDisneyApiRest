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
    public class CharactersTest: BaseTests
    {
        [TestMethod]
        public async Task GetAllCharacters()
        {
            // Arranger
            var pruebaBD = "pruebabd";
            var context = MakeContext(pruebaBD);

            context.Characters.Add(new Character() { Imagen = "link:image.jpg", Nombre = "woody", Edad = 25, Peso = 90, Historia = "vaquero del oeste" });
            context.Characters.Add(new Character() { Imagen = "link:image.jpg", Nombre = "buzz", Edad = 30, Peso = 120, Historia = "Heroe del espacio" });
            await context.SaveChangesAsync();

            var context2 = MakeContext(pruebaBD);

            // Act
            var controller = new CharactersController(context2);
            var response = await controller.GetCharacters(null,null,null);

            // Assert
            var characters = response.Value;
            Assert.AreEqual(2, characters.Count());
        }

        [TestMethod]
        public async Task GetCharacterByIdNonExistent()
        {
            // Arranger
            var pruebaBD = Guid.NewGuid().ToString();
            var context = MakeContext(pruebaBD);

            // Act
            var controller = new CharactersController(context);
            var response = await controller.GetCharacter(1);

            // Assert
            var okResult = response.Result as StatusCodeResult;

            Assert.AreEqual(404, okResult.StatusCode);

        }

        [TestMethod]
        public async Task GetCharacterByIdExistent()
        {
            // Arranger
            var pruebaBD = Guid.NewGuid().ToString();
            var context = MakeContext(pruebaBD);

            context.Characters.Add(new Character() { Imagen= "link:image.jpg", Nombre = "woody2",Edad=25,Peso=90,Historia="vaquero del oeste"});
            await context.SaveChangesAsync();

            // Act
            var context2 = MakeContext(pruebaBD);
            var controller = new CharactersController(context2);
            var id = 1;

            // Assert
            var response = await controller.GetCharacter(id);
            var result = response.Value;
            Assert.AreEqual(id, result.IdCharacter);
        }

        [TestMethod]
        public async Task CreateCharacter()
        {
            // Arranger
            var pruebaBD = Guid.NewGuid().ToString();
            var context = MakeContext(pruebaBD);

            var woody = new CharacterPostDto() { Imagen = "link:image.jpg", Nombre = "woody2", Edad = 25, Peso = 90, Historia = "vaquero del oeste" };

            // Act
            var controller = new CharactersController(context);
            

            // Assert
            await controller.PostCharacter(woody);

            var count = context.Characters.Count();

            Assert.AreEqual(1, count);

        }
    }
}
