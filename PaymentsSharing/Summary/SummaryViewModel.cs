using PaymentsSharing.Payments;
using PaymentsSharing.Persons;
using PaymentsSharing.Time;

namespace PaymentsSharing.Summary;

internal class SummaryViewModel
{
    public SummaryViewModel(Payments.Payments payments, Persons.Persons persons)
    {
        var summaryRows = new List<SummaryRow>();

        foreach (Person person in persons.Everyone)
        {
            Payment[] paymentsForPerson = payments.FromMonth(person, MonthAndYear.Now).ToArray();
            decimal amount = paymentsForPerson.Sum(payment => payment.Amount);
            decimal? amountForMeat = paymentsForPerson.Sum(payment => payment.AmountForMeat);
            string howMuch = $"{amount:0.00 PLN}{amountForMeat?.ToString(" + 0.00 PLN for meat") ?? ""}";
            var summaryRow = new SummaryRow(person.Name, howMuch);
            summaryRows.Add(summaryRow);
        }
        
        SummaryRows = summaryRows;
    }
    
    public IEnumerable<SummaryRow> SummaryRows { get; }
    public string CurrentMonthAndYearTitle => DateTime.Now.ToString("MMMM yyyy");

}