using WebApplication_Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationModel>(options => options.UseSqlServer(connection));
 

builder.Services.AddMvc();

var app = builder.Build();

app.UseStaticFiles( );
/*app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files")),
    RequestPath = "/Files"
});*/


app.MapControllers();
app.Run();
