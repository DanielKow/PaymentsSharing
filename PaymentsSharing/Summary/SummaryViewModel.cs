using PaymentsSharing.Payments;
using PaymentsSharing.Persons;
using PaymentsSharing.Time;

namespace PaymentsSharing.Summary;

internal class SummaryViewModel(CurrentPerson currentPerson, Payments.Payments payments)
{
    public string Amount => payments.FromMonth(currentPerson.Person, MonthAndYear.Now).Sum(payment => payment.Amount)
        .ToString("0 PLN");

    public string AmountForMeat => payments.FromMonth(currentPerson.Person, MonthAndYear.Now)
        .Sum(payment => payment.AmountForMeat ?? 0).ToString("0 PLN");

    public IEnumerable<string> AmountsPerPerson()
    {
        var summariesPerPerson = new List<SummaryPerPerson>();

        foreach (Payment payment in payments.FromCurrentMonth)
        {
            string payer = string.Join('+', payment.Payers.Select(payer => payer.Name).OrderByDescending(name => name));

            SummaryPerPerson? summaryPerPerson = summariesPerPerson.FirstOrDefault(summary => summary.Person == payer);

            if (summaryPerPerson is null)
            {
                summaryPerPerson = new SummaryPerPerson(payer);
                summariesPerPerson.Add(summaryPerPerson);
            }

            summaryPerPerson.AddAmount(payment.Amount);

            if (payment.AmountForMeat is not null)
            {
                summaryPerPerson.AddAmountForMeat(payment.AmountForMeat ?? 0);
            }
        }

        return summariesPerPerson.Select(summaryPerPerson => summaryPerPerson.ToString());
    }
}