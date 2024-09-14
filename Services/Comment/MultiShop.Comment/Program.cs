using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Comment.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Context Registration
builder.Services.AddDbContext<CommentContext>();
#endregion

#region Authentication
//JwtBearer token geçerliliğini kontrol eden pakettir.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //Authority JwtBearer'ı kiminle kullanıcağını belirtir. IdentityServerUrl appsettings.json'dan gelir.
    //Comment mikro servisi ayağa kalkarken IdentityServer mikro servisi de ayağa kalkar.
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "ResourceComment";//Config tarafında dinleyici olan key ResourceComment ApiResource setlenir.
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
