using MediatR;
using Microsoft.AspNetCore.Components;
using PaymentsSharing.Persons;

namespace PaymentsSharing.Payments;

internal class AddPaymentFormViewModel
{
    private readonly CurrentPerson _currentPerson;
    private readonly Persons.Persons _persons;
    private readonly ISender _sender;
    private readonly NavigationManager _navigationManager;

    public AddPaymentFormViewModel(CurrentPerson currentPerson, Persons.Persons persons, ISender sender,
        NavigationManager navigationManager)
    {
        _currentPerson = currentPerson;
        _persons = persons;
        _sender = sender;
        _navigationManager = navigationManager;
        Payers = _currentPerson.IsSignedIn ? [_currentPerson.Person] : [];
        Consumers = _persons.Everyone;
    }

    public IEnumerable<Person> Payers { get; set; }
    public IEnumerable<Person> Consumers { get; set; }
    public uint Amount { get; set; }
    public uint? AmountForMeat { get; set; }
    public string Description { get; set; } = "";
    public Person? CurrentPerson => _currentPerson.Person;
    public bool IsCurrentPersonEatMeat => _currentPerson.Person.IsMeatEater;
    public IEnumerable<Person> AllPersons => _persons.Everyone;
    public string SelectedPayers => string.Join(", ", Payers.Select(payer => payer.Name));
    public string SelectedConsumers => string.Join(", ", Consumers.Select(consumer => consumer.Name));

    public void UpdateAmount(uint amount)
    {
        Amount = amount;
    }

    public void UpdateAmountForMeat(uint? amountForMeat)
    {
        AmountForMeat = amountForMeat;
    }

    public async Task Save()
    {
        await _sender.Send(new AddPayment(
            Payers,
            Consumers,
            Amount,
            AmountForMeat,
            Description));

        _navigationManager.NavigateTo("/");
    }
}