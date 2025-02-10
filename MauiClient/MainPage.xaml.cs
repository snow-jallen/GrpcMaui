using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grpc.Net.Client;
using GrpcShared;
using ProtoBuf.Grpc.Client;

namespace MauiClient;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

public partial class MainPageViewModel(ScheduleOMaticClient client) : ObservableObject
{
    [ObservableProperty]
    private ScheduleResponse response;

    [RelayCommand]
    private async Task Schedule()
    {
        Response = await client.Client.Schedule(new ScheduleRequest
        {
            BandName = "The Beatles",
            PerformanceDate = DateTime.UtcNow,
            Venue = "The Cavern Club"
        });
    }

    [ObservableProperty]
    private ModifyScheduleResponse modifyResponse;

    [RelayCommand]
    private async Task ModifySchedule()
    {
        ModifyResponse = await client.Client.ModifySchedule(new ModifyScheduleRequest
        {
            BookingId = response.BookingId,
            NewPerformanceDate = DateTime.UtcNow.AddDays(7),
            NewVenue = "Shea Stadium"
        });
    }
}