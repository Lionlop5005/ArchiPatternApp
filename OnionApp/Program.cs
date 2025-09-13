using Microsoft.EntityFrameworkCore;

const string CorsPolicyName = "AllowReactApp";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// CORSの設定を追加
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName,
        policy => policy
            .WithOrigins("http://localhost:3000", "http://localhost:3001", "http://127.0.0.1:3000") // 両方許可
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// コントローラー + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnionApp API V1");
        c.RoutePrefix = string.Empty;
    });
}


// ▼ ここから順序が重要 ▼
app.UseHttpsRedirection();

app.UseRouting();          // ルーティング設定
app.UseCors(CorsPolicyName); // CORSはRoutingの後
app.UseAuthorization();

app.MapControllers();      // ルーティングの最終設定
// ▲ ここまで順序が重要 ▲

app.Run();

