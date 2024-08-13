using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Discount.Context;
using MultiShop.Discount.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Service Registration
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IDiscountService, DiscountService>();
#endregion

#region Authentication
//JwtBearer token geçerliliğini kontrol eden pakettir.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //Authority JwtBearer'ı kiminle kullanıcağını belirtir. IdentityServerUrl appsettings.json'dan gelir.
    //Discount mikro servisi ayağa kalkarken IdentityServer mikro servisi de ayağa kalkar.
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "ResourceDiscount";//Config tarafında dinleyici olan key ResourceDiscount ApiResource setlenir.
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
