using Dima.Core.Handlers;
using Dima.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Dima.Core.Requests.TrafficController;

namespace Dima.Web.Pages.TrafficControllers;

public partial class GetAllTrafficControllerPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<TrafficController> TrafficControllers { get; set; } = [];

    #endregion

    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    [Inject]
    public ITrafficControllerHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllTrafficControllerRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                TrafficControllers = result.Data ?? [];
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

    public async void OnDeleteButtonClickedAsync(long id, string name)
    {
        var result = await Dialog.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir o Controlador de Tráfego {name} será removido. Deseja continuar?",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, name);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteTrafficControllerRequest
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
            TrafficControllers.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Controlador de Tráfego {title} removida", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}