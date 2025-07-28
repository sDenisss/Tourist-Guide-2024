using Microsoft.Extensions.FileProviders;
using TG.DataBase;
using Microsoft.EntityFrameworkCore;
using TG.Users;
using System.Text.Json;
using TG.Attractions;
using TG.API;
using TG.History;
using Microsoft.VisualBasic;

namespace TG.Commands.StartCommands
{
    public class StartApi
    {
        public static IConfiguration configuration;

        public static void Start(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var conn = builder.Configuration.GetConnectionString("DefaultConnection");

            

            // builder.Services.AddDbContext<UserHistory>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<AttractionDbContext>();
            // builder.Services.AddTransient<UserDbContext>();
            builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlite(conn));
            builder.Services.AddSingleton<DatabaseHelper>();
            // builder.Services.AddSingleton<DataBaseManager>();
            builder.Services.AddSingleton<DatabaseUsers>();
            var app = builder.Build();
            var historyPath = builder.Configuration.GetValue<string>("History:FilePath");

            HtmlPages.Html(app);
            Swagger.UseSwaggerUI(app);


            DatabaseHelper? d = new DatabaseHelper(configuration);
            DatabaseUsers? du = new DatabaseUsers(configuration);
            d.InitializeDatabase();
            d.InitializeDatabaseUserHistory();
            du.InitializeUsersDatabase();

            // DataBaseManager dbm = new DataBaseManager(configuration);
            // dbm.DataBaseM();
            // DatabaseUsers du = new DatabaseUsers();
            // du.InitializeUsersDatabase();

            AttractionsActions.Acts(app);
            AuthActions.Acts(app);
            HistoryActions.Acts(app, configuration);

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Assets")),
                RequestPath = "/Assets"
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Frontend")),
                RequestPath = "/Frontend"
            });

            app.UseHttpsRedirection();
            app.Run();

        }
    }
}