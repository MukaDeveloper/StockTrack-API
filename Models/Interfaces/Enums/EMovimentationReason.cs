namespace StockTrack_API.Models.Interfaces.Enums
{
    public enum EMovimentationReason
    {
        // Entry reasons
        INSERTION,
        EDIT,
        RETURN_FROM_LOAN,
        RETURN_FROM_MAINTENANCE,

        // Exit reasons
        DISPOSAL,
        LOAN,
        SENT_TO_MAINTENANCE,
        REMOVED,
        
        // Other reason
        OTHER,
    }
}
