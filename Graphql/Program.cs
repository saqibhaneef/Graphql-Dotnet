using Graphql.Data;
using Graphql.GraphQL.Mutations;
using Graphql.GraphQL.Types;
using Graphql.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, ProductService>();
//InMemory Database
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("GraphQLDemo"));

builder.Services.AddGraphQLServer()
    .AddQueryType<ProductQueryTypes>()    
    .AddMutationType<ProductMutations>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData.Initialize(services);
}
//GraphQL
app.MapGraphQL();

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
