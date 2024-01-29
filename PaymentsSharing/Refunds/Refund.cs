namespace PaymentsSharing.Refunds;

internal readonly record struct Refund(string From, string To, decimal Amount);