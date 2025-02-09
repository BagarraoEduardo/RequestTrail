using Trailing;
using CallerApi.Integration.Extensions;
using CallerApi.Business.Extensions;
using CallerApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.SetupTrail(builder.Configuration);
builder.Services.SetupIntegration(builder.Configuration);
builder.Services.SetupBusiness();
builder.Services.SetupPresentation(builder.Configuration, builder.Host);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<TrailMiddleware>();

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