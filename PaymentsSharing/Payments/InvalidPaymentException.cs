namespace PaymentsSharing.Payments;

internal class InvalidPaymentException(string reason) : Exception($"Invalid payment. Reason: {reason}")
{
    public string Reason => reason;
}
