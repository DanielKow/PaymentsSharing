using MediatR;
using Microsoft.AspNetCore.Components;

namespace PaymentsSharing.Payments;

internal class AddPaymentFromDateFormViewModel(ISender sender)
{
    public uint Amount { get; set; }
    public uint? AmountForMeat { get; set; }
    public DateTime? Date { get; set; } = DateTime.Now;
    public string Description { get; set; } = "";

    public async Task Save()
    {
        await sender.Send(new AddPayment(
            Date ?? DateTime.Now,
            Amount,
            AmountForMeat,
            Description));
    }
}