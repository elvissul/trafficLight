using TrafficLight.Api.HubConfig;
using TrafficLight.Api.Services;
using TrafficLight.Api.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITrafficLightService, TrafficLightService>();
builder.Services.AddSingleton<ITrafficLightManager, TrafficLightManager>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Open", builder => builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
});
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Open");
app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<TrafficLightHub>("/trafficLight");
});
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
