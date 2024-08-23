public class FineCalculator
{
    private const double CostPerDay = 0.5;
    public static bool IsOverdue(BorrowingRecord record)
    {
        if (record.ReturnDate.HasValue)
        {
            return false;
        }
        return DateTime.Now > record.DueDate;
    }

    public static double CalculateFine(BorrowingRecord record)
    {
        if (record.ReturnDate.HasValue)
        {
            return 0.0;
        }

        if (IsOverdue(record))
        {
            return 0.0;
        }

        int overdueDays = (DateTime.Now - record.DueDate).Days;
        return overdueDays * CostPerDay;
    }
}
