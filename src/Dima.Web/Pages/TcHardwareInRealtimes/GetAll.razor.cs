using Dima.Core.Handlers;
using Dima.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Dima.Core.Requests.TcHardwareInRealtime;
using Dima.Core.Dtos;

namespace Dima.Web.Pages.TcHardwareInRealtimes;

public partial class GetAllTcHardwareInRealtimePage : ComponentBase
{
    #region Properties

    private Timer _timer = null!;
    public bool IsBusy { get; set; } = false;
    public List<TcHardwareInRealtimeDto> TcHardwareInRealtimeDtoList { get; set; } = [];

    public bool _expanded = true;   // collapse
    public bool darkIsVisible;

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    [Inject]
    public ITcHardwareInRealtimeHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        await LoadDataAsync();
        IsBusy = false;

        _timer = new Timer(callback: UpdateData, null, 0, 2000);
    }

    #endregion

    #region Methods

    public void OnExpandCollapseClick()
    {
        _expanded = !_expanded;
    }

    private async void UpdateData(object state)
    {
        await LoadDataAsync();
        await InvokeAsync(StateHasChanged);
    }

    private async Task LoadDataAsync()
    {
        try
        {
            var request = new GetAllTcHardwareInRealtimeRequest();

            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
            {
                TcHardwareInRealtimeDtoList = result.Data ?? [];
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    #endregion


}