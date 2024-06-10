using BusinessLayerr.Interfaces;
using BusinessLayerr.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayerr.Context;
using RepositoryLayerr.Interfaces;
using RepositoryLayerr.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


builder.Services.AddSingleton<BookContext>();

builder.Services.AddControllers();


//User
builder.Services.AddTransient<IUserRL,UserRL>();
builder.Services.AddTransient<IUserBL,UserBL>();

//Book
builder.Services.AddTransient<IBookRL,BookRL>();
builder.Services.AddTransient<IBookBL,BookBL>();

//Feedback
builder.Services.AddTransient<IFeedbackRL,FeedbackRL>();
builder.Services.AddTransient<IFeedbackBL,FeedbackBL>();

//Cart
builder.Services.AddTransient<ICartRL,CartRL>();
builder.Services.AddTransient<ICartBL ,CartBL>();

//Address
builder.Services.AddTransient<IAddressRL,AddressRL>();
builder.Services.AddTransient<IAddressBL,AddressBL>();

//Order
builder.Services.AddTransient<IOrderRL,OrderRL>();
builder.Services.AddTransient<IOrderBL,OrderBL>();

//Whislist
builder.Services.AddTransient<IWishlistRL, WishlistRL>();
builder.Services.AddTransient<IWhislistBL,WhislistBL>();

//Admin
builder.Services.AddTransient<IAdminRL,AdminRL>();
builder.Services.AddTransient<IAdminBL,AdminBL>();

//Review
builder.Services.AddTransient<IReviewRL,ReviewRL>();
builder.Services.AddTransient<IReviewBL,ReviewBL>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
