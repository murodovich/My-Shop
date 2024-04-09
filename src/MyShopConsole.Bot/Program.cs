using System.Text.Json;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Domain.Entities;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;

public class Program
{
    private static readonly HttpClient _client = new HttpClient();

    public static ITelegramBotClient _botClient;

    public static async Task Main(string[] args)
    {
        _botClient = new TelegramBotClient("6426194164:AAGxzroLFjYCgAB6y_6eZXSpDuxL8K3aJfw");

        _botClient.OnMessage += Bot_OnMessage;
        _botClient.OnCallbackQuery += Bot_OnCallbackQuery;

        _botClient.StartReceiving();

        Console.ReadLine();
    }

    public static async void Bot_OnMessage(object? sender, MessageEventArgs e)
    {
        string text = e.Message.Text;

        if (text == "/start")
        {
            await _botClient.SendTextMessageAsync(
            chatId: e.Message.Chat.Id,
            text: "Ozingizga keraklisini tanlang ",
            replyMarkup: await SendInlineKeyboard());
        }
    }

    

    public static async Task<InlineKeyboardMarkup> SendInlineKeyboard()
    {
        var response = await GetProductsFromApiAsync();

        var inlineKeyboard = new List<List<InlineKeyboardButton>>();

        foreach (var i in response)
        {
            var row = new List<InlineKeyboardButton>(2);
            row.Add(new InlineKeyboardButton { Text = i.SortNumber.ToString(), CallbackData = i.Id.ToString() });
            inlineKeyboard.Add(row);
        }
        return new InlineKeyboardMarkup(inlineKeyboard);
    }

    private static async Task<List<Product>> GetProductsFromApiAsync()
    {
        var response = await _client.GetAsync("https://localhost:7026/api/Product/GetAll");
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<Product>>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return products;
    }

    public static async void Bot_OnCallbackQuery(object? sender, CallbackQueryEventArgs e)
    {
        string callbackData = e.CallbackQuery.Data;

        var products = await GetProductsFromApiAsync();

        var product = products.FirstOrDefault(x => x.Id == int.Parse(callbackData));

        using (var videoStream = System.IO.File.OpenRead("\\Users\\sarva\\OneDrive\\Рабочий стол\\My Shop\\My Shop\\MyShop\\wwwroot\\media\\videos\\" + product.VideoPath))
        {
            var videoInputFile = new InputOnlineFile(videoStream, Path.GetFileName("\\Users\\sarva\\OneDrive\\Рабочий стол\\My Shop\\My Shop\\MyShop\\wwwroot\\media\\videos\\" + product.VideoPath));
            var message = await _botClient.SendVideoAsync(e.CallbackQuery.Message.Chat.Id, videoInputFile);
            
        }

    }


    //public static async Task SendFilmListAsync(string messageText, ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    //{
    //    var root = await ApiBroker.GetFilmListAsync(messageText);
    //    var listOfSearch = root.Search;
    //    if (listOfSearch == null)
    //    {
    //        await botClient.SendTextMessageAsync(
    //            chatId: update.Message.Chat.Id,
    //            text: "Bu nomli kino topilmadi uzur",
    //            replyToMessageId: update.Message.MessageId,
    //            cancellationToken: cancellationToken
    //            );

    //        return;
    //    }

    //    var page = root.PageNumber;
    //    var searchKey = root.SearchKey;

    //    int count = 0;
    //    var listOfInlineKeyboardButton = new List<List<InlineKeyboardButton>>();
    //    var inlineKeyBoardButtonsRow = new List<InlineKeyboardButton>();


    //    //agar 5dan kichik bo'lsa bir qator
    //    if (listOfSearch.Count <= 5)
    //    {
    //        inlineKeyBoardButtonsRow = new List<InlineKeyboardButton>();
    //        for (int j = 1; j <= listOfSearch.Count; j++)
    //        {
    //            inlineKeyBoardButtonsRow.Add(InlineKeyboardButton.WithCallbackData($"{count + 1}", $"{listOfSearch[count].imdbID}"));
    //            count++;
    //        }
    //        listOfInlineKeyboardButton.Add(inlineKeyBoardButtonsRow);
    //    }
    //    else//agar 5dan katta bo'lsa 2 qator
    //    {
    //        inlineKeyBoardButtonsRow = new List<InlineKeyboardButton>();
    //        for (int j = 1; j <= 5; j++)
    //        {
    //            inlineKeyBoardButtonsRow.Add(InlineKeyboardButton.WithCallbackData($"{count + 1}", $"{listOfSearch[count].imdbID}"));
    //            count++;
    //        }
    //        listOfInlineKeyboardButton.Add(inlineKeyBoardButtonsRow);

    //        inlineKeyBoardButtonsRow = new List<InlineKeyboardButton>();
    //        for (int j = 1; j <= listOfSearch.Count - 5; j++)
    //        {
    //            inlineKeyBoardButtonsRow.Add(InlineKeyboardButton.WithCallbackData($"{count + 1}", $"{listOfSearch[count].imdbID}"));
    //            count++;
    //        }
    //        listOfInlineKeyboardButton.Add(inlineKeyBoardButtonsRow);
    //    }

    //    //pagination uchun yana bir qator 
    //    //shu yerga paginationni oxirgi pagemi yo'qmi biladigan code yozish kerak 
    //    listOfInlineKeyboardButton.Add(new List<InlineKeyboardButton>()
    //    {
    //        InlineKeyboardButton.WithCallbackData($"⬅️", $"page={page-1} {searchKey}"),
    //        InlineKeyboardButton.WithCallbackData($"➡️", $"page={page+1} {searchKey}"),
    //    });

    //    var inlineKeyboard = new InlineKeyboardMarkup(listOfInlineKeyboardButton);

    //    var TitleOfFilms = $"<b>Films page:{page} <i>(search:{searchKey} total result:{root.totalResults})</i>:</b>";
    //    for (int i = 0; i < listOfSearch.Count; i++)
    //    {
    //        TitleOfFilms += $"\n{i + 1}.<i>{listOfSearch[i].Title}-{listOfSearch[i].Year}</i>";
    //    }

    //    await botClient.SendTextMessageAsync(
    //        chatId: update.Message.Chat.Id,
    //        text: TitleOfFilms,
    //        parseMode: ParseMode.Html,
    //        replyMarkup: inlineKeyboard,
    //        cancellationToken: cancellationToken);
    //}
}