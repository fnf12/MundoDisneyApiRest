using Microsoft.EntityFrameworkCore;
using MundoDisneyApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisneyTest
{
    public class BaseTests
    {
        protected DisneyContext MakeContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<DisneyContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbcontext = new DisneyContext(opciones);
            return dbcontext;
        }
    }
}
