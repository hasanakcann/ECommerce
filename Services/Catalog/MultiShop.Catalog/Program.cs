using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Services.CategoryServices;
using MultiShop.Catalog.Services.FeatureServices;
using MultiShop.Catalog.Services.FeatureSliderServices;
using MultiShop.Catalog.Services.OfferDiscountServices;
using MultiShop.Catalog.Services.ProductDetailServices;
using MultiShop.Catalog.Services.ProductImageServices;
using MultiShop.Catalog.Services.ProductServices;
using MultiShop.Catalog.Services.SpecialOfferServices;
using MultiShop.Catalog.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Service Registration
//Singleton Nedir?
//Singleton bir tasarım şablonudur. Bellekte bir nesneden sadece bir tane olabilir. Her istek geldiğinde o nesne(aynı nesne) verilir. Program sonlanana kadar bellekte varlığını korur.

//Scoped Nedir?
//Scoped bir nevi Prototype tasarım şablonudur. AddScoped nesne örneği oluşturur. Her istek geldiğinde istek başına bir tane nesne üretilir. Her seferinde yeni nesne üretildiği için performansı olumsuz etkileyebilir. İstek tamamlanmadan başka bir istek gelirse yenisi yaratılmadan zaten var olan verilir.

//Transient Nedir? 
//Transient bir nevi Prototype tasarım şablonudur. Her istek geldiğinde istek başına bir tane nesne üretilir. Her seferinde yeni nesne üretildiği için performansı olumsuz etkileyebilir. İstek tamamlanmadan başka bir istek gelirse yenisi yaratılır eskisi verilmez. Transient ile Scoped farkı budur.

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IOfferDiscountService, OfferDiscountService>();

//AutoMapper kullanarak DTO sınıflarını oluşturduğumuzda, client tarafında göstermek istediğimiz alanları sınırlandırarak, gerçek nesnemizin güvenliğini sağlamış oluruz.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddScoped<IDatabaseSettings>(serviceProvider =>
{
    return serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
#endregion

#region Authentication
//JwtBearer token geçerliliğini kontrol eden pakettir.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //Authority JwtBearer'ı kiminle kullanıcağını belirtir. IdentityServerUrl appsettings.json'dan gelir.
    //Catalog mikro servisi ayağa kalkarken IdentityServer mikro servisi de ayağa kalkar.
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "ResourceCatalog";//Config tarafında dinleyici olan key ResourceCatalog ApiResource setlenir.
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
