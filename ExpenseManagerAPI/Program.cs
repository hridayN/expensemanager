using ExpenseManagerAPI.Database;
using ExpenseManagerAPI.Interface;
using ExpenseManagerAPI.Services;
using ExpenseManagerAPI.Settings;
using ExpenseManagerAPI.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers with convertible options
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Here DateOnlyJsonConverter is used to convert DateTime to DateOnly
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

// Add Swagger generator
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddScoped<DBHelper>();
builder.Services.AddScoped<IPaymentMethod, PaymentMethodManager>();
builder.Services.AddScoped<IExpenseManager, ExpenseManager>();
builder.Services.AddScoped<IDBService, DBService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    // To serve the Swagger UI at the app's root (https://localhost:<port>/), set the RoutePrefix property to an empty string
    options.RoutePrefix = string.Empty;
});
    ;
app.UseAuthorization();

app.MapControllers();

app.Run();
