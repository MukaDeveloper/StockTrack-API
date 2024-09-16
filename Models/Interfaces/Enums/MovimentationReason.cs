namespace StockTrack_API.Models
{
    public enum MovimentationReason
    {
        // Entry reasons
        Insertion = 1,
        Edit = 2,
        ReturnFromLoan = 3,
        ReturnFromMaintenance = 4,

        // Exit reasons
        Disposal = 5,
        Loan = 6,
        SentToMaintenance = 7,
        Removed = 8,
        
        // Other reason
        Other = 9,
    }
}
