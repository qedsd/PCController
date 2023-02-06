using PCController.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(p =>
{
    // Îª Swagger JSON and UIÉèÖÃxmlÎÄµµ×¢ÊÍÂ·¾¶
    var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
    var xmlPath = Path.Combine(basePath, "PCController.Server.xml");
    p.IncludeXmlComments(xmlPath);
});
builder.Services.AddCors(options =>
{
    options.AddPolicy
        (name: "AllowCors",
            builde =>
            {
                builde.WithOrigins("*", "*", "*")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            }
        );
});

#region Æô¶¯websocket
var wsPort = builder.Configuration.GetSection("WSPort")?.Get<int>();
if (wsPort == null || wsPort == 0)
{
    Console.WriteLine("Î´ÅäÖÃwebsocket¶Ë¿Ú");
    return;
}
else
{
    WebSocketService webSocketService = new WebSocketService((int)wsPort);
}
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
