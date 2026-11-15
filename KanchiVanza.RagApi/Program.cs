using SmartRAG.Dashboard;
using SmartRAG.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSmartRag(builder.Configuration, options =>
{
    // These are read from the "SmartRAG" section in appsettings.json
    // The library handles the rest automatically
});
builder.Services.AddSmartRagDashboard(options =>
{
    options.Path = "/smartrag";
    options.EnableInDevelopmentOnly = true;
});
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // This tells Swagger to pick the first action instead of throwing
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseSmartRagDashboard("/smartrag");
//app.MapSmartRagDashboard("/smartrag");
app.MapControllers();

app.Run();
