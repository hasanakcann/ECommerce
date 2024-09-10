using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

#region Authentication
//JwtBearer token geçerliliğini kontrol eden pakettir.
builder.Services.AddAuthentication().AddJwtBearer("OcelotAuthenticationScheme", options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];//Authority JwtBearer'ı kiminle kullanıcağını belirtir. IdentityServerUrl appsettings.json'dan gelir.
    options.MapInboundClaims = false; //User.Claims'da sub değeri için .Net 8.0 versiyonunda MapInboundClaims'ı false set etmek gerekiyor. 
    options.Audience = "ResourceOcelot";//Identity Server Config tarafında dinleyici olan key ResourceOcelot ApiResource olarak setlenir.
    options.RequireHttpsMetadata = false;//IdentityServerUrl http olduğu için false set edildi.
});
#endregion

#region Configuration
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Services.AddOcelot(configuration);
#endregion

var app = builder.Build();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
