using System;


namespace InventoryDTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }  // Assuming the ID is auto-generated upon insertion, so it's optional when creating a new item.
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int StorageLocation { get; set; }
        public string DateAdded { get; set; } // Using DateTime type for date columns
        public string Expiry { get; set; }  // Nullable DateTime since expiry can be optional (REAL type in SQLite typically means a floating-point number, so if Expiry is meant to be a date, then consider changing this in your SQLite schema.)
        public int PackSizeID { get; set; } // This assumes PackSizeID is required. If it can be null, use "int?" instead.

        public override string ToString()
        {
            return $"ItemName: {ItemName}, Quantity: {Quantity}, StorageLocation: {StorageLocation}, DateAdded: {DateAdded}, Expiry: {Expiry}, PackSizeID: {PackSizeID}";
        }
    }
}

