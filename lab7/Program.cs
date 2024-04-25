var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddJsonConsole());
ILogger logger= loggerFactory.CreateLogger<Program>();

app.Run(async (context) =>
{
    logger.LogInformation($"working with {context.Request.Path}");

   logger.LogDebug($"debug log {context.Request.Path}");

    logger.LogError($"error log {context.Request.Path}");

   logger.LogCritical($"critical log {context.Request.Path}");

    await context.Response.WriteAsync("log");
}

);
app.Map("/hello", (ILogger<Program> logger) =>
{
    logger.LogInformation($"Path: /hello Time: {DateTime.Now.ToLongTimeString()}");
    return "log2";
});




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
