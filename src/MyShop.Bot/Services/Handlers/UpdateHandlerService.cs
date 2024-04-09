using Application.Services.Contracts.Products;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace MyShop.Bot.Services.Handlers
{
    public class UpdateHandlerService : IUpdateHandler
    {
        private IProductService _productService;
        private readonly IServiceScopeFactory _scopeFactory;



        public UpdateHandlerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }



        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateAsyncScope();
            _productService = scope.ServiceProvider.GetService<IProductService>();

            var products = await _productService.GetAllAsync();
            //foreach (var product in products)
            //{
            //    var message = product.SortNumber;
            //    if (update.Message.Text == $"{message}")
            //    {


            //        var videoPath = product.VideoPath.Replace("/", "\\");

            //        string path = $"C:\\Users\\sarva\\OneDrive\\Рабочий стол\\My Shop\\My Shop\\MyShop\\wwwroot{videoPath}";

            //        try
            //        {
            //            byte[] readText = System.IO.File.ReadAllBytes(path);
            //            using (Stream stream = new MemoryStream(readText))
            //            {

            //                stream.Seek(0, SeekOrigin.Begin);

            //                await botClient.SendVideoAsync(chatId: update.Message.Chat.Id, video: InputFile.FromStream(stream));
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine($"An error occurred: {ex.Message}");
            //        }
            //    }
            //}






        }
    }
}
