
using Play.Catalog.Service.Entities;
using Play.Common.Repositories;
using Play.Common.Settings;

var builder = WebApplication.CreateBuilder(args);

var serviceSettings = builder.Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

builder.Services.AddControllers(options =>
{
    //methods with async in their name are changedon run time. we need this to use nameof in controllers
    options.SuppressAsyncSuffixInActionNames = false;
});

builder.Services.AddMongo()
    .AddMongoRepository<Item>("items");

/// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
