using System.Globalization;

namespace PaymentsSharing.Time;

public readonly record struct MonthAndYear
{
    public MonthAndYear(int month, int year)
    {
        if (month is < 1 or > 12)
        {
            throw new ArgumentOutOfRangeException(nameof(month));
        }

        Month = month;
        Year = year;
    }
    
    public MonthAndYear(DateTime dateTime)
    {
        Month = dateTime.Month;
        Year = dateTime.Year;
    }

    public int Month { get; } = DateTime.Now.Month;

    public int Year { get; } = DateTime.Now.Year;

    public static MonthAndYear Now => new(DateTime.Now);
    
    public MonthAndYear Previous => Month == 1 ? new MonthAndYear(12, Year - 1) : new MonthAndYear(Month - 1, Year);
    public MonthAndYear Next => Month == 12 ? new MonthAndYear(1, Year + 1) : new MonthAndYear(Month + 1, Year);
    
    public override string ToString() => $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month)} {Year}";
}