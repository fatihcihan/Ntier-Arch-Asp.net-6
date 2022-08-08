using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// ioc yapisi olarak autofac kullandigimizi burada belirtiyoruz (service-manager)

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())  
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

// Add services to the container.

// burayi business'a tasidik. Dependency resolvers -> autofac

//builder.Services.AddSingleton<IProductService, ProductManager>();       // IoC -> servise denk gelen manager'i ver ge� (tek instance)
//builder.Services.AddSingleton<IProductDal, EfProductDal>();             // soyutu istiyoruz bize somutu veriyor gibi dusunulebilir

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
