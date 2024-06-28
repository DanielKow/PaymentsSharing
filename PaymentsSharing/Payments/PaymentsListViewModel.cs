using MediatR;
using MudBlazor;
using PaymentsSharing.Time;

namespace PaymentsSharing.Payments;

internal class PaymentsListViewModel(Payments payments, ISender sender, IDialogService dialogService)
{
    public MonthAndYear MonthAndYear { get; set; } = MonthAndYear.Now;
    
    public IEnumerable<Payment> Payments =>
        payments.FromMonth(MonthAndYear).OrderBy(payment => payment.CreatedAt);

    public string Total => Payments.Sum(payment => payment.Amount + (payment.AmountForMeat ?? 0)).ToString("0 PLN");

    public string When(Payment payment) => payment.CreatedAt.ToString("dd.MM.yy");

    public string ForWhat(Payment payment) => payment.Description;

    public async Task RemovePayment(Payment payment)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };
        IDialogReference? dialog = await dialogService.ShowAsync<RemovePaymentDialog>("Confirm payment removal", options);
        DialogResult? result = await dialog.Result;

        if (!result.Canceled)
        {
            await sender.Send(new RemovePayment(payment));
        }
    }
}