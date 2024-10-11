using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TEAMLIFTSS.Authorization;
using TEAMLIFTSS.Authorization.Services;
using TEAMLIFTSS.Helper;
using TEAMLIFTSS.Repos;
using TEAMLIFTSS.Services;
using TEAMLIFTSS.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Repository Config
builder.Services.AddTransient<IRefreshService, RefreshService>();
builder.Services.AddTransient<IEmployeeDetailsService, EmployeeDetailsService>();
builder.Services.AddTransient<IAttendanceService, AttendanceService>();
#endregion

#region Database Configurations

builder.Services.AddDbContext<ApplicationDBContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

#endregion

#region CORS Config
builder.Services.AddCors(x => x.AddDefaultPolicy(build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
}));
#endregion

#region JWT Authuntication
var _authKet = builder.Configuration.GetValue<string>("JwtSettings:JwtSecurityKey");
builder.Services.AddAuthentication(item =>
{
    item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authKet == null ? "1234567890abcdefghijkalamopqrstuvwxyz" : _authKet)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero

    };
});
#endregion

#region JwtSetting Config
var _jwtsett = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(_jwtsett);
#endregion

#region AuttoMapper Configuration
var automapper = new MapperConfiguration(x => x.AddProfile(new AutoMapperHelper()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/**************/

app.UseAuthentication();

app.UseCors();

/**************/
app.MapControllers();

app.Run();
