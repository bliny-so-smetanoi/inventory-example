namespace InventoryApp.Models;

public class Item
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Picture { get; set; } = "";
    public int Quantity { get; set; }
    public string RoomNumber { get; set; }
}