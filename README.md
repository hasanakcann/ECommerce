=> .Net: 

https://learn.microsoft.com/tr-tr/dotnet/standard/whats-new/whats-new-in-dotnet-standard?tabs=csharp

=> Mikro Servis: 

https://medium.com/@furkanbegen/mikroservis-mimarisi-nedir-ve-avantajlar%C4%B1-nelerdir-1369175cc4e6

=> MongoDb: 

https://medium.com/@berkekurnaz/nedir-bu-mongodb-994a94a9d1df

MongoDb Kurulum: 

https://www.mongodb.com/try/download/community

=> Auto Mapper: 

https://medium.com/@ecanyuksel/automapper-nedir-db05f85facd2

=> Asenkron Programlama: 

https://atarikguney.medium.com/asenkron-asynchronous-programlama-nedir-296230121f9d

=> Dapper: 

https://medium.com/software-development-turkey/micro-orm-lerin-kral%C4%B1-dapper-nedir-ec5838b115ac

=> Onion Mimarisi: 

https://fatih-gunalp.medium.com/1-asp-net-coreda-onion-mimarisine-genel-bak%C4%B1%C5%9F-67940d3c43d8

=> CQRS: 

https://sefikcankanber.medium.com/cqrs-command-query-responsibility-segregation-nedir-16b196376389

=> Mediator: 

https://refactoring.guru/design-patterns/mediator

=> Docker: 

https://aws.amazon.com/tr/docker/

https://medium.com/geeks-of-data/docker-101-yeni-ba%C5%9Flayanlar-i%C3%A7in-ad%C4%B1m-ad%C4%B1m-rehber-253e6b7cb9f6

https://acokgungordu.medium.com/docker-serisi-docker-volumes-1c509f043f98

Docker Kurulum:

https://www.docker.com/products/docker-desktop/

=> Portainer:

https://medium.com/devopsturkiye/docker-ile-portainer-kurulumu-ve-portainera-h%C4%B1zl%C4%B1-bak%C4%B1%C5%9F-2fdcf2b31deb

https://medium.com/devopsturkiye/portainer-ile-sunucular%C4%B1n%C4%B1z-%C3%BCzerinde-container-y%C3%B6netimi-sa%C4%9Flay%C4%B1n-6e39d539d46a#:~:text=Portainer'%C4%B1%20Docker%20%C3%BCzerinde%20%C3%A7al%C4%B1%C5%9Ft%C4%B1rarak,i%C5%9Fleminden%20sonra%20art%C4%B1k%20kullanmaya%20ba%C5%9Flayabiliriz.

Portainer Kurulumu Cmd Komutları:

docker pull portainer/portainer

docker run -d -p 8000:8000 -p 9000:9000 --name portainer --restart always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer

Şifre Sıfırlama Cmd Komutları:

docker container stop portainer

docker run --rm -v portainer_data:/data portainer/helper-reset-password

docker container start portainer

=> Identity Server

https://medium.com/innoviletech/net-core-mikroservis-mimarisinde-identityserver4-framework%C3%BC-932c82e2b88c

IdentityServer4 Kurulumu Cmd Komutları:

dotnet new -i identityserver4.templates

cd C:\Users\akcan\source\repos\MultiShop\IdentityServer

dotnet new is4aspid --name MultiShop.IdentityServer

Do you want to run this action [Y(yes)|N(no)]?
n

Migration - VS Package Manager Console Komutları:

add-migration mig1

update-database

Error:
To change the IDENTITY property of a column, the column needs to be dropped and recreated

Yukarıdaki hata alınırsa proje içerisindeki migrations klasörü silinir ve tekrar sırasıyla add-migration mig1 komutu sonrasında da update-database komutu çalıştılır. Hata düzelecektir.

Identity Server Version Değiştirme:

https://learn.microsoft.com/en-us/answers/questions/1183067/could-not-load-file-or-assembly-system-runtime-ver

=> JWT(Json Web Tokens)

https://tugrulbayrak.medium.com/jwt-json-web-tokens-nedir-nasil-calisir-5ca6ebc1584a








