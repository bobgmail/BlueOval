using BlueOvalBatteryPark.SAPinterface.DataConverter;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlueOvalBatteryPark.SAPinterface.S4Hearn;

//for interface with SAP: OutboundDelivery

public class OutboundDeliveryClass
{
    [Required]
    //[ValidateObject] // Add this for nested object validation
    public A_OutbDeliveryHeader A_OutbDeliveryHeader { get; set; }
}

public class A_OutbDeliveryHeader
{
    [Required]
    //[ValidateObject]
    public A_OutbDeliveryHeaderType A_OutbDeliveryHeaderType { get; set; }
}

public class A_OutbDeliveryHeaderType
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipToParty is required")]   // Change this to reject empty strings
    public string DeliveryDocument { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "ShipToParty is required")]   
    public string ShipToParty { get; set; }
    [Required]
    //[ValidateObject]
    public To_DeliveryDocumentItem to_DeliveryDocumentItem { get; set; }
}

public class To_DeliveryDocumentItem
{
    [Required]
    //[ValidateEnumerable] // Add this for array validation
    public A_OutbDeliveryItemType[] A_OutbDeliveryItemType { get; set; }
}

public class A_OutbDeliveryItemType
{
    public string Batch { get; set; } = "";
    public string ManufactureDate { get; set; } = "";

    [Range(0.0, (double)decimal.MaxValue, ErrorMessage = "ActualDeliveryQuantity Error")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal ActualDeliveryQuantity { get; set; } = 0;

    [Range(0.0, (double)decimal.MaxValue, ErrorMessage = "OriginalDeliveryQuantity Error")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal OriginalDeliveryQuantity { get; set; } = 0;

    [Range(0.0, (double)decimal.MaxValue, ErrorMessage = "ActualDeliveredQtyInBaseUnit Error")]
    [JsonConverter(typeof(StringToDecimalConverter))]
    public decimal ActualDeliveredQtyInBaseUnit { get; set; } = 0;

    [Required(AllowEmptyStrings = false, ErrorMessage = "Material is required")]   
    public string Material { get; set; }    // Removed Default Values: For required fields, don't provide default empty strings

    public string ShelfLifeExpirationDate { get; set; } = "";
    public string BatchBySupplier { get; set; } = "";
}


