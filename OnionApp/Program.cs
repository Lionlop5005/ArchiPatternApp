using Microsoft.EntityFrameworkCore;

const string CorsPolicyName = "AllowReactApp";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// CORS�̐ݒ��ǉ�
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName,
        policy => policy
            .WithOrigins("http://localhost:3000", "http://localhost:3001", "http://127.0.0.1:3000") // ��������
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// �R���g���[���[ + Swagger
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


// �� �������珇�����d�v ��
app.UseHttpsRedirection();

app.UseRouting();          // ���[�e�B���O�ݒ�
app.UseCors(CorsPolicyName); // CORS��Routing�̌�
app.UseAuthorization();

app.MapControllers();      // ���[�e�B���O�̍ŏI�ݒ�
// �� �����܂ŏ������d�v ��

app.Run();

