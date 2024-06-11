using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Dima.Core.Requests.TrafficController;

namespace Dima.Web.Pages.TrafficControllers;

public partial class CreateTrafficControllerPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public CreateTrafficControllerRequest InputModel { get; set; } = new();

    #endregion

    #region Services

    [Inject]
    public ITrafficControllerHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    protected override void OnInitialized()
    {
        InputModel.CodeLocal = "010101";
        InputModel.Name = "Controlador 010101";
        InputModel.Description = "Rua ABX x Avenida XYZ1";        
    }

    #endregion

    #region Methods

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
                NavigationManager.NavigateTo("/controladores");
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