using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TG.API
{
    public static class Swagger
    {
        public async static void UseSwaggerUI(WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}