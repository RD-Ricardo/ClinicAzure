using ClinicAzure.Api.Configurations;
using ClinicAzure.Api.Filters;
using ClinicAzure.Api.Middlewares;
using ClinicAzure.Application;
using ClinicAzure.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AuthenticationFilter>();
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins",
        policy =>
        {
            policy.WithOrigins(builder.Configuration["ApplicationCors"]!)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services
    .AddLogConfiguration(builder.Configuration)
    .AddAuthenticationConfiguration(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Local")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseHttpLogging();

app.UseMiddleware<LoggingMiddleware>();

app.Run();
