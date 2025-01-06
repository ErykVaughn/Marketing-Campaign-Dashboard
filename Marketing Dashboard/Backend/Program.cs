using BackendAPI.Services;
using BackendAPI.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register CampaignService with scoped lifetime
builder.Services.AddScoped<ICampaignService, CampaignService>();
builder.Services.AddScoped<IAudienceService, AudienceService>();
builder.Services.AddScoped<IResponseService, ResponseService>();


// Register MySqlDatabaseWrapper as IDatabaseWrapper
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IDatabaseWrapper>(provider =>
    new MySqlDatabaseWrapper(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
