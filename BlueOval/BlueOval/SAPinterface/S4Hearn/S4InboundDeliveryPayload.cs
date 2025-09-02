using BlueOval.SAPinterface.HearnS4;
using System.Text.Json.Serialization;

namespace BlueOval.SAPinterface.S4Hearn;


public class S4InboundDeliveryPayload
{
    [JsonPropertyName("DeliveryHeader")]
    public DeliveryHeader DeliveryHeader { get; set; }
}

public class DeliveryHeader
{
    [JsonPropertyName("DeliveryDocument")]
    public string DeliveryDocument { get; set; }
    [JsonPropertyName("Control")]
    public string Control { get; set; }
    [JsonPropertyName("DeliveryDocumentBySupplier")]
    public string DeliveryDocumentBySupplier { get; set; }
    [JsonPropertyName("DeliveryDocumentType")]
    public string DeliveryDocumentType { get; set; }
    [JsonPropertyName("DeliveryDate")]
    public string DeliveryDate { get; set; }
    [JsonPropertyName("DocumentDate")]
    public string DocumentDate { get; set; }
    [JsonPropertyName("TextElementText")]
    public string TextElementText { get; set; }
    [JsonPropertyName("Party")]
    public Party Party { get; set; }
    [JsonPropertyName("DeliveryItem")]
    public DeliveryItem[] DeliveryItem { get; set; }
}

public class Party
{
    [JsonPropertyName("Supplier")]
    public string Supplier { get; set; }
}

public class DeliveryItem
{
    [JsonPropertyName("DeliveryDocumentItem")]
    public string DeliveryDocumentItem { get; set; }
    [JsonPropertyName("Material")]
    public string Material { get; set; }
    [JsonPropertyName("Batch")]
    public string Batch { get; set; }
    [JsonPropertyName("Plant")]
    public string Plant { get; set; }
    [JsonPropertyName("StorageLocation")]
    public string StorageLocation { get; set; }
    [JsonPropertyName("BatchBySupplier")]
    public string BatchBySupplier { get; set; }
    [JsonPropertyName("ManufactureDate")]
    public string ManufactureDate { get; set; }
    [JsonPropertyName("ShelfLifeExpirationDate")]
    public string ShelfLifeExpirationDate { get; set; }
    [JsonPropertyName("ActualDeliveredQtyInBaseUnit")]
    public string ActualDeliveredQtyInBaseUnit { get; set; }
    [JsonPropertyName("BaseUnit")]
    public string BaseUnit { get; set; }
    [JsonPropertyName("ActualDeliveryQuantity")]
    public string ActualDeliveryQuantity { get; set; }
    [JsonPropertyName("DeliveryQuantityUnit")]
    public string DeliveryQuantityUnit { get; set; }
    [JsonPropertyName("StockType")]
    public string StockType { get; set; }
    [JsonPropertyName("OriSupplier")]
    public string OriSupplier { get; set; }
    [JsonPropertyName("ReferenceSDDocument")]
    public string ReferenceSDDocument { get; set; }
    [JsonPropertyName("ReferenceSDDocumentItem")]
    public string ReferenceSDDocumentItem { get; set; }
    [JsonPropertyName("Sticker")]
    public Sticker[] Stickers { get; set; }
}

