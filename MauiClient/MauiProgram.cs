using Grpc.Net.Client;
using GrpcShared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc.Client;

namespace MauiClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<ScheduleOMaticClient>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

public class ScheduleOMaticClient : IDisposable
{
#if DEBUG
    //private const string GrpcApiAddress = "http://localhost:8585";
    private const string GrpcApiAddress = "https://grpcmauiapi.azurewebsites.net:8585";
#else
    private const string GrpcApiAddress = "http://localhost:5000";
#endif

    private GrpcChannel channel;
    public IScheduleOMaticService Client { get; }

    public ScheduleOMaticClient(IConfiguration config)
    {
        GrpcClientFactory.AllowUnencryptedHttp2 = true;
        channel = GrpcChannel.ForAddress(GrpcApiAddress);
        Client = channel.CreateGrpcService<IScheduleOMaticService>();
    }

    public void Dispose()
    {
        channel.Dispose();
    }
}