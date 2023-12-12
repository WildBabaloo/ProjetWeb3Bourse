using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models; // Add this for OpenApiInfo
using ProjetWeb3Bourse.Data;
using ProjetWeb3Bourse.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BourseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProjetWeb3BourseContext") ?? throw new InvalidOperationException("Connection string 'ProjetWeb3BourseContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projet Bourse", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable Swagger and Swagger UI in development environment
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1");
    c.RoutePrefix = "swagger"; // This is the route where Swagger UI will be accessible
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints => {
    endpoints.MapHub<BourseHub>("/BourseHub");
    endpoints.MapControllers();
    endpoints.MapGet("/", context => {
        context.Response.Redirect("/Bourses");
        return Task.CompletedTask;
    });
});

app.Run();
