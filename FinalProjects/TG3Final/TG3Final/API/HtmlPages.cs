using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TG.DataBase;

namespace TG.API
{
    public static class HtmlPages
    {
        public static void Html(WebApplication app)
        {
            app.MapGet("/", async context => await ServeFileAsync(context, "html/home.html"));
            app.MapGet("/auth", async context => await ServeFileAsync(context, "html/login.html"));
            app.MapGet("/exit", async context => await ServeFileAsync(context, "html/exit.html"));
            app.MapGet("/exit2", async context => await ServeFileAsync(context, "html/exit2.html"));
            app.MapGet("/search", async context => await ServeFileAsync(context, "html/search.html"));
            app.MapGet("/login", async context => await ServeFileAsync(context, "html/login.html"));
            app.MapGet("/profile", async context => await ServeFileAsync(context, "html/profile.html"));
            app.MapGet("/registry", async context => await ServeFileAsync(context, "html/registry.html"));
            app.MapGet("/route-planner", async context => await ServeFileAsync(context, "html/route-planner.html"));
            app.MapGet("/history", async context => await ServeFileAsync(context, "html/history.html"));
            app.MapGet("/add-in-db", async context => await ServeFileAsync(context, "html/add-in-db.html"));
        }

        private static async Task ServeFileAsync(HttpContext context, string filePath)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.SendFileAsync(filePath);
        }
    }
}