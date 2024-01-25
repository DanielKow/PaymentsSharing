namespace PaymentsSharing.Persons;

internal readonly record struct Person(string Name, bool IsMeatEater)
{
    public static Person Null => new(string.Empty, false);
}