using MediatR;
using PaymentsSharing.Time;

namespace PaymentsSharing.Payments;

internal class Payments
{
    private readonly List<Payment> _payments = [];

    public void Add(Payment payment)
    {
        _payments.Add(payment);
    }
    
    public void Remove(Payment payment)
    {
        _payments.Remove(payment);
    }
    
    public bool Contains(Payment payment)
    {
        return _payments.Contains(payment);
    }
    
    public IEnumerable<Payment> FromCurrentMonth => _payments.Where(payment => payment.CreatedAt.Month == DateTime.Now.Month && payment.CreatedAt.Year == DateTime.Now.Year);
    
    public IEnumerable<Payment> FromMonth(MonthAndYear monthAndYear)
    {
        return _payments.Where(payment => payment.CreatedAt.Month == monthAndYear.Month
                                          && payment.CreatedAt.Year == monthAndYear.Year);
    }
}