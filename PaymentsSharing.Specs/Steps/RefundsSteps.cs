using MediatR;
using NSubstitute;
using PaymentsSharing.Payments;
using PaymentsSharing.Persons;
using PaymentsSharing.Refunds;
using TechTalk.SpecFlow;

namespace PaymentsSharing.Specs.Steps;

[Binding]
public class RefundsSteps
{
    private readonly IPublisher _publisherMock = Substitute.For<IPublisher>();
    private readonly Payments.Payments _payments;
    private readonly Refunds.Refunds _refunds;

    private readonly IEnumerable<Person> _persons =
    [
        new Person("Natalia", false),
        new Person("Mikołaj", true),
        new Person("Andrzej", true)
    ];

    public RefundsSteps()
    {
        _payments = new Payments.Payments(_publisherMock);
        _refunds = new Refunds.Refunds(_payments);
    }

    [Given(@"(.*) has paid (\d+) PLN")]
    public async Task GivenPersonHasPaidPln(string payer, uint amount)
    {
        await _payments.Add(new Payment(DateTime.Now, _persons.Where(person => person.Name == payer), _persons, amount));
    }

    [Given(@"(.*) has paid (\d+) PLN for meat")]
    public async Task GivenPersonHasPaidPlnForMeat(string payer, uint amount)
    {
        await _payments.Add(new Payment(DateTime.Now, _persons.Where(person => person.Name == payer), _persons, 0, amount));
    }

    [Given(@"(.*) has paid (\d+) PLN and (\d+) PLN for meat")]
    public async Task GivenPersonHasPaidPlnAndPlnForMeat(string payer, uint amount, uint amountForMeat)
    {
        await _payments.Add(new Payment(DateTime.Now, _persons.Where(person => person.Name == payer), _persons, amount,
            amountForMeat));
    }

    [Given(@"(.*) and (.*) have paid (\d+) PLN")]
    public async Task GivenPersonsHavePaidPln(string firstPayer, string secondPayer, uint amount)
    {
        await _payments.Add(new Payment(DateTime.Now,
            _persons.Where(person => person.Name == firstPayer || person.Name == secondPayer),
            _persons, amount));
    }

    [Given(@"(.*) has paid (\d+) PLN for (.*) and (.*)")]
    public async Task GivenPersonHasPaidPlnForConsumers(string payer, uint amount, string firstConsumer,
        string secondConsumer)
    {
        await _payments.Add(new Payment(DateTime.Now, _persons.Where(row => row.Name == payer),
            _persons.Where(person => person.Name == firstConsumer || person.Name == secondConsumer), amount));
    }

    [When("refund is recalculated")]
    public void WhenRefundIsRecalculated()
    {
        _refunds.Recalculate();
    }

    [Then(@"(.*) should return (\d+) PLN to (.*)")]
    public void ThenPersonShouldReturnPlnToAnotherPerson(string from, uint amount, string to)
    {
        _refunds.Should().Contain(new Refund(from, to).WithAmount(amount));
    }
}