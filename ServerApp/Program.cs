using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

// Add services for CORS and MemoryCache (Performance Optimization)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCors("AllowBlazorClient");

// Endpoint returning structured JSON with caching
app.MapGet("/api/productlist", (IMemoryCache cache) =>
{
    const string cacheKey = "productListCache";

    if (!cache.TryGetValue(cacheKey, out Product[]? products))
    {
        products = new[]
        {
            new Product(1, "Laptop", 1200.50, 25, new Category(101, "Electronics")),
            new Product(2, "Headphones", 50.00, 100, new Category(102, "Accessories")),
            new Product(3, "Mechanical Keyboard", 89.99, 45, new Category(102, "Accessories"))
        };

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        cache.Set(cacheKey, products, cacheOptions);
    }

    return Results.Ok(products);
});

app.Run();

// Data models for clean JSON serialization
public record Category(int Id, string Name);
public record Product(int Id, string Name, double Price, int Stock, Category Category);
