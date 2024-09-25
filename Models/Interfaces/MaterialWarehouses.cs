namespace StockTrack_API.Models.Interfaces
{
    public class MaterialWarehouses : BaseModel
    {
        public int MaterialId { get; set; }
        public Material? Material { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}