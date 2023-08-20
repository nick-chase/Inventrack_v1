using System;

namespace InventoryDTOs
{
    public class PackSizeDTO
    {
        public int Id { get; set; } // Corresponds to the "id" column in your table.
        public string Unit { get; set; } // Corresponds to the "Unit" column in your table.
        public int PackSize { get; set; } // Corresponds to the "PackSize" column in your table.
    }

}
