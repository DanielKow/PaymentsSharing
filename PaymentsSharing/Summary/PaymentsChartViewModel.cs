using MudBlazor;
using PaymentsSharing.Payments;
using PaymentsSharing.Time;

namespace PaymentsSharing.Summary;

internal class PaymentsChartViewModel
{
    public IEnumerable<string> XAxisLabels { get; }
    public IEnumerable<ChartSeries> Series { get; }
    
    private const int MonthsToShow = 6;
    
    public PaymentsChartViewModel(Payments.Payments payments)
    {
        DateTime sixMonthsAgo = DateTime.Now.AddMonths(-MonthsToShow + 1);
        var monthAndYear = new MonthAndYear(sixMonthsAgo);
        List<string> xAxisLabels = [];
        List<double> amounts = [];
        List<double> amountsForMeat = []; 
        
        for (var i = 0; i < MonthsToShow; i++)
        {
            IReadOnlyCollection<Payment> paymentsFromMonth = payments.FromMonth(monthAndYear).ToList();
            amounts.Add(paymentsFromMonth.Sum(payment => payment.Amount));
            amountsForMeat.Add(paymentsFromMonth.Sum(payment => payment.AmountForMeat ?? 0));
            xAxisLabels.Add(monthAndYear.ToShortString());
            monthAndYear = monthAndYear.Next;
        }
        
        XAxisLabels = xAxisLabels;
        Series = new []
        {
            new ChartSeries {Name = "Amount", Data = amounts.ToArray()},
            new ChartSeries {Name = "Amount for meat", Data = amountsForMeat.ToArray()}
        };
    }
    
}