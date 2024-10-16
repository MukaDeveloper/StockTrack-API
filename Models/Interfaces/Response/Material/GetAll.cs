namespace StockTrack_API.Models.Interfaces.Response.Material
{
    public class GetAllRes
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string ImageURL { get; set; } = string.Empty;

        public string Manufacturer { get; set; } = string.Empty;
        public int RecordNumber { get; set; }
        public string MaterialType { get; set; } = string.Empty;
        public float Quantity => Status.Sum(s => s.Quantity);
        public string Measure { get; set; } = string.Empty;
        public List<MaterialStatusDto> Status { get; set; } = [];
        public List<int> MaterialWarehouses { get; set; } = [];
    }

    public class MaterialStatusDto
    {
        public string Status { get; set; } = string.Empty;
        public float Quantity { get; set; }
    }
}