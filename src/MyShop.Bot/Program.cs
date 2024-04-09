using MyShop.Bot.BackgroundServices;
using Telegram.Bot.Polling;
using Telegram.Bot;
using MyShop.Bot.Services.Handlers;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);


builder.Services.AddHostedService<BotBackgroundService>();
builder.Services.AddSingleton(new TelegramBotClient(builder.Configuration["TelegramBotAPIKey"]));
builder.Services.AddSingleton<IUpdateHandler, UpdateHandlerService>();



builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
