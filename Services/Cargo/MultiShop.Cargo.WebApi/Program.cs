using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.BusinessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Context Registration
builder.Services.AddDbContext<CargoContext>();
#endregion

#region Service Registration
builder.Services.AddScoped<ICargoCompanyDal, EfCargoCompanyDal>();
builder.Services.AddScoped<ICargoCustomerDal, EfCargoCustomerDal>();
builder.Services.AddScoped<ICargoDetailDal, EfCargoDetailDal>();
builder.Services.AddScoped<ICargoOperationDal, EfCargoOperationDal>();

builder.Services.AddScoped<ICargoCompanyService, CargoCompanyManager>();
builder.Services.AddScoped<ICargoCustomerService, CargoCustomerManager>();
builder.Services.AddScoped<ICargoDetailService, CargoDetailManager>();
builder.Services.AddScoped<ICargoOperationService, CargoOperationManager>();
#endregion

#region Authentication
//JwtBearer token geçerliliğini kontrol eden pakettir.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //Authority JwtBearer'ı kiminle kullanıcağını belirtir. IdentityServerUrl appsettings.json'dan gelir.
    //Cargo mikro servisi ayağa kalkarken IdentityServer mikro servisi de ayağa kalkar.
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "ResourceCargo";//Config tarafında dinleyici olan key ResourceCargo ApiResource olarak setlenir.
    options.RequireHttpsMetadata = false;//IdentityServerUrl http olduğu için false set edildi.
});
#endregion

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
