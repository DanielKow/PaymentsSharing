using MediatR;
using Microsoft.AspNetCore.Components;

namespace PaymentsSharing.Payments;

internal class AddPaymentFormViewModel(ISender sender, NavigationManager navigationManager)
{
    public uint Amount { get; set; }
    public uint? AmountForMeat { get; set; }
    public string Description { get; set; } = "";

    public async Task Save()
    {
        await sender.Send(new AddPayment(
            Amount,
            AmountForMeat,
            Description));

        navigationManager.NavigateTo("/");
    }
}