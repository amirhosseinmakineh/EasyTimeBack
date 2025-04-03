using EasyTime.Application.Contract.IServices;
using EasyTime.Application.Contract.Mapper;
using EasyTime.Application.Generator;
using EasyTime.Application.Services;
using EasyTime.InfraStracure.Context;
using EasyTime.InfraStracure.Repositories;
using EasyTime.Model.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bamdad API",
        Version = "v1",
        Description = "Description for the API goes here.",
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddScoped(typeof(IBaseService<,,>), typeof(BaseService<,,>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddDbContext<EasyTimeContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EasyTime"));
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
