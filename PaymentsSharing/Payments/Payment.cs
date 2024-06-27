namespace PaymentsSharing.Payments;

internal readonly record struct Payment(
    DateTime CreatedAt,
    uint Amount,
    uint? AmountForMeat = null,
    string Description = "");