using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework with In-Memory Database for simplicity
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseInMemoryDatabase("TodoDb"));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:8100", "http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Todo List API",
        Version = "v1.0",
        Description = "A Todo List API built with ASP.NET Core and Entity Framework Core",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Todo API Support",
            Email = "support@todoapi.com"
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // Set XML documentation path
    var xmlFile = "TodoApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }

    // Enable annotations
    options.EnableAnnotations();
    
    // Set tag sorting
    options.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] });
    options.DocInclusionPredicate((name, api) => true);
    
    // Set model examples
    options.SchemaFilter<SwaggerSchemaExampleFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo List API v1.0");
        options.RoutePrefix = "swagger";
        options.DocumentTitle = "Todo List API Documentation";
        options.DefaultModelsExpandDepth(-1);
        options.DisplayRequestDuration();
        options.EnableFilter();
        options.ShowExtensions();
        options.EnableDeepLinking();
        options.ShowCommonExtensions();
        options.EnableValidator();
        
        // Custom CSS
        options.InjectStylesheet("/swagger-ui/custom.css");
        
        // Set default model expand depth
        options.DefaultModelExpandDepth(2);
    });
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

// Initialize database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
    context.Database.EnsureCreated();
}

app.Run();
