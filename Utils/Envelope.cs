namespace StockTrack_API
{
    public class Envelope<T>
    {
        public T Item { get; set; }
        public Envelope(T item) => Item = item;
    }

    public class EnvelopeArray<T>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int ItemsTotal { get; set; }
        public int Total { get; set; }
        public List<T> Items { get; set; }

        public EnvelopeArray(List<T>? items = null)
        {
            Items = items ?? [];
            Offset = 0;
            Limit = 0;
            ItemsTotal = Items.Count;
            Total = Items.Count;
        }
    }

    public class EnvelopeArrayPagination<T>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int ItemsTotal { get; set; }
        public int Total { get; set; }
        public List<T> Items { get; set; }

        public EnvelopeArrayPagination(List<T> items, int offset, int limit, int? total = null)
        {
            Items = items ?? [];
            Offset = offset;
            Limit = limit;
            ItemsTotal = Items.Count;
            Total = total ?? Items.Count;
        }
    }
    public class EnvelopeFactory
    {

        public static Envelope<T> factoryEnvelope<T>(T item) => new Envelope<T>(item);

        public static EnvelopeArray<T> factoryEnvelopeArray<T>(List<T>? items = null) => new EnvelopeArray<T>(items);

        public static EnvelopeArrayPagination<T> factoryEnvelopeArrayPagination<T>(List<T> items, int offset, int limit, int? total = null) => new EnvelopeArrayPagination<T>(items, offset, limit, total);
    }
}
