using MediatR;
using NSubstitute;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;
using PaymentsSharing.Refunds;
using PaymentsSharing.Time;
using TechTalk.SpecFlow;

namespace PaymentsSharing.Specs.Steps;

[Binding]
public class RefundsSteps
{
    private readonly Payments.Payments _payments;
    private readonly Refunds.Refunds _refunds;
    private IEnumerable<Refund> _currentRefunds = [];

    private readonly IEnumerable<Person> _persons =
    [
        new Person("Natalia", false),
        new Person("Mikołaj", true),
        new Person("Andrzej", true)
    ];

    public RefundsSteps()
    {
        _payments = new Payments.Payments();
        _refunds = new Refunds.Refunds(_payments);
    }

    [Given(@"(.*) has paid (\d+) PLN")]
    public void GivenPersonHasPaidPln(string payer, uint amount)
    {
        _payments.Add(new Payment(DateTime.Now, _persons.Where(person => person.Name == payer), _persons,
            amount));
    }

    [Given(@"(.*) has paid (\d+) PLN for meat")]
    public void GivenPersonHasPaidPlnForMeat(string payer, uint amount)
    {
        _payments.Add(new Payment(DateTime.Now, _persons.Where(person => person.Name == payer), _persons, 0,
            amount));
    }

    [Given(@"(.*) has paid (\d+) PLN and (\d+) PLN for meat")]
    public void GivenPersonHasPaidPlnAndPlnForMeat(string payer, uint amount, uint amountForMeat)
    {
        _payments.Add(new Payment(DateTime.Now, _persons.Where(person => person.Name == payer), _persons, amount,
            amountForMeat));
    }

    [Given(@"(.*) and (.*) have paid (\d+) PLN")]
    public void GivenPersonsHavePaidPln(string firstPayer, string secondPayer, uint amount)
    {
        _payments.Add(new Payment(DateTime.Now,
            _persons.Where(person => person.Name == firstPayer || person.Name == secondPayer),
            _persons, amount));
    }

    [Given(@"(.*) has paid (\d+) PLN for (.*) and (.*)")]
    public void GivenPersonHasPaidPlnForConsumers(string payer, uint amount, string firstConsumer,
        string secondConsumer)
    {
        _payments.Add(new Payment(DateTime.Now, _persons.Where(row => row.Name == payer),
            _persons.Where(person => person.Name == firstConsumer || person.Name == secondConsumer), amount));
    }

    [When("refund is recalculated")]
    public void WhenRefundIsRecalculated()
    {
        _currentRefunds = _refunds.FromMonth(MonthAndYear.Now);
    }

    [Then(@"(.*) should return (\d+) PLN to (.*)")]
    public void ThenPersonShouldReturnPlnToAnotherPerson(string from, uint amount, string to)
    {
        _currentRefunds.Should().Contain(new Refund(from, to).WithAmount(amount));
    }
}