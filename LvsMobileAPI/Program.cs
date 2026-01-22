using LvsMobileAPI.Authorization;
using LvsMobileAPI.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
//builder.Services.AddAuthentication(x =>
//    {
//        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    }).AddJwtBearer(x =>
//            {
//                x.RequireHttpsMetadata = false;
//                x.SaveToken = true;
//                x.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//                    ValidateIssuer = false,
//                    ValidateAudience = false
//                };
//            });

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});

// configure DI for application services
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IAsnReadService, AsnReadService>();
builder.Services.AddScoped<IAusgangService, AusgangService>();
builder.Services.AddScoped<ICallService, CallService>();
builder.Services.AddScoped<IDamageService, DamageService>();
builder.Services.AddScoped<IEingangService, EingangService>();
builder.Services.AddScoped<IGoodtypeService, GoodtypeService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IMandantService, MandantService>();
builder.Services.AddScoped<IPrintQueueService, PrintQueueService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWorkspaceService, WorkspaceService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();



app.UseAuthorization();
app.UseAuthentication();
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();
//app.UseMvc();
app.MapControllers();
app.Run();
