using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Dima.Core.Requests.TcHardwareInRealtime;

namespace Dima.Web.Pages.TcHardwareInRealtimes;

public partial class CreateTcHardwareInRealtimePage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreateTcHardwareInRealtimeRequest InputModel { get; set; } = new();

    #endregion

    #region Services

    [Inject]
    public ITcHardwareInRealtimeHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    protected override void OnInitialized()
    {
        // Preenche os campos com valores aleatorios apenas para desenvolvimento.
        // Essa page não existirá no projeto final para o cliente!
        InputModel.TrafficControllerId = 1;
        InputModel.VoltageAcIn = GenerateRandomVoltage(min: 200, max: 250);
        InputModel.Voltage9vIn = GenerateRandomVoltage(min: 8, max: 10);
        InputModel.DoorOpen = true;
        InputModel.FlashingYellowOn = false;
        InputModel.CpuConnected = true;
    }

    #endregion

    #region Methods

    private static double GenerateRandomVoltage(double min, double max)
    {
        var random = new Random();
        return Math.Round(random.NextDouble() * (max - min) + min, 2);
    }


    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        await Task.Delay(500);

        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/hardware/adicionar", forceLoad: true);
            }
            else
                Snackbar.Add(result.Message, Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}