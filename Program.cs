using Microsoft.EntityFrameworkCore;
using VendingMachineAPIs.BLL.Mappings;
using VendingMachineAPIs.DAL;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VendingMachineAPIs.DAL.Models;
using VendingMachineAPIs.DAL.Repo;
using VendingMachineAPIs.DAL.UnitIOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VendingMachineAPIs.BLL.Services.UserService;
using VendingMachineAPIs.BLL.Services.ProductService;
using VendingMachineAPIs.BLL.Services.AuthService;
using VendingMachineAPIs.BLL.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Database

var connectionString = builder.Configuration.GetConnectionString("VendingMachineDataBase");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});
    
      
#endregion

#region CORS
var allowPolicy = "AllowPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowPolicy, builder =>
    {
        builder.AllowAnyOrigin().
        AllowAnyHeader().AllowAnyMethod();
    });
});
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion


#region Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region Repos
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
#endregion

#region Authentication and Authorization
// For Identity and for allowing spaces in username
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "http://localhost:4200",
        ValidIssuer = "http://localhost:61955",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"))
    };
});
#endregion

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IToken, TokenService>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(allowPolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
