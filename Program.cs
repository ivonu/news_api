using news_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o => {
   o.EnableAnnotations();
});

builder.Services.AddScoped<INewsService, GNewsService>(
    serviceProvider => new GNewsService(builder.Configuration["apiKey"])
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
