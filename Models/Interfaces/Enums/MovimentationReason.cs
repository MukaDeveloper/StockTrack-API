namespace StockTrack_API.Models
{
    public enum MovimentationReason
    {
        // Entry reasons
        INSERTION = 1,
        EDIT = 2,
        RETURN_FROM_LOAN = 3,
        RETURN_FROM_MAINTENANCE = 4,

        // Exit reasons
        DISPOSAL = 5,
        LOAN = 6,
        SENT_TO_MAINTENANCE = 7,
        REMOVED = 8,
        
        // Other reason
        OTHER = 9,
    }
}
