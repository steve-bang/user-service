using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Steve.ManagerHero.Middlewares;
using Steve.ManagerHero.UserService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program));
builder.AddCoreServices()
        .AddAuthenticationService();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.MapControllers();

// Config Pipeline middlewares
// We need to add the middlewares in the order of execution
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<IpRestrictionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

