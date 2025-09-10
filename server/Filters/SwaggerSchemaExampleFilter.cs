using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Filters;

public class SwaggerSchemaExampleFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(CreateTodoDto))
        {
            schema.Example = new OpenApiObject
            {
                ["title"] = new OpenApiString("Learn Swagger Documentation"),
                ["description"] = new OpenApiString("Add complete documentation and examples to the API")
            };
        }
        else if (context.Type == typeof(UpdateTodoDto))
        {
            schema.Example = new OpenApiObject
            {
                ["title"] = new OpenApiString("Updated Title"),
                ["description"] = new OpenApiString("Updated description"),
                ["isCompleted"] = new OpenApiBoolean(true)
            };
        }
        else if (context.Type == typeof(Todo))
        {
            schema.Example = new OpenApiObject
            {
                ["id"] = new OpenApiInteger(1),
                ["title"] = new OpenApiString("Complete project documentation"),
                ["description"] = new OpenApiString("Write comprehensive Swagger documentation for Todo API"),
                ["isCompleted"] = new OpenApiBoolean(false),
                ["createdAt"] = new OpenApiString("2025-09-10T15:30:00Z"),
                ["updatedAt"] = new OpenApiString("2025-09-10T15:30:00Z")
            };
        }
    }
}
