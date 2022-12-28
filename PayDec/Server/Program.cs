using Nethereum.JsonRpc.Client;
using PayDec.Server.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PayDec.Server.Attributes;
using PayDec.Server.Services;
using Microsoft.Extensions.DependencyInjection;
using PayDec.Server.Services.Interfaces;
using Microsoft.AspNetCore.Server.IISIntegration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PayDecContext>( options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORSPolicy",
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration.GetSection("Client:Url").Value)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});


builder.Services.AddTransient<IJwtMaker,JwtMaker>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

builder.Services.AddTransient<IBlockchainTransaction, BlockchainTransaction>();

var app = builder.Build();


//builder.Services.AddTransient<JwtAuthorize>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("CORSPolicy");

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
