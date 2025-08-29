using System.Text.Json.Serialization;

namespace BlueOvalBatteryPark.SAPinterface.HearnS4;



public class OutboundDeliveryPayload
{
    [JsonPropertyName("A_InbDeliveryHeader")]
    public A_Inbdeliveryheader A_InbDeliveryHeader { get; set; }
}

public class A_Inbdeliveryheader
{
    [JsonPropertyName("A_InbdeliveryHeaderType")]
    public A_InbdeliveryHeaderType A_InbDeliveryHeaderType { get; set; }
    [JsonPropertyName("PackagingInformation")]
    public PackagingInformation PackagingInformation { get; set; }
}

public class A_InbdeliveryHeaderType
{
    [JsonPropertyName("DeliveryDocument")]
    public string DeliveryDocument { get; set; }
    [JsonPropertyName("Control")]
    public string Control { get; set; }                             //ORI:Original，CHG：Change，DEL：Delete
    public To_DeliveryDocumentItem to_DeliveryDocumentItem { get; set; }
}

public class To_DeliveryDocumentItem
{
    [JsonPropertyName("A_Inbdeliveryitemtype")]
    public A_InbdeliveryItemType A_InbDeliveryItemType { get; set; }
}

public class A_InbdeliveryItemType
{
    [JsonPropertyName("DeliveryDocumentItem")]
    public string DeliveryDocumentItem { get; set; }
    [JsonPropertyName("ActualDeliveryQuantity")]
    public string ActualDeliveryQuantity { get; set; }      // This is the Picking qty
    [JsonPropertyName("BatchBySupplier")]
    public string BatchBySupplier { get; set; }
    [JsonPropertyName("Batch")]
    public string Batch { get; set; }
    [JsonPropertyName("ExternalID")]
    public string ExternalID { get; set; }
}

public class PackagingInformation
{
    [JsonPropertyName("DeliveryDocumentItemLabels")]
    public DeliveryDocumentItemLabels DeliveryDocumentItemLabels { get; set; }
}

public class DeliveryDocumentItemLabels
{
    [JsonPropertyName("DeliveryDocumentItemNumber")]
    public string DeliveryDocumentItemNumber { get; set; }
    [JsonPropertyName("Stickers")]
    public Sticker[] Stickers { get; set; }
}

public class Sticker
{
    [JsonPropertyName("SerialNumber")]
    public string SerialNumber { get; set; }
    [JsonPropertyName("Level")]
    public string Level { get; set; }
    [JsonPropertyName("LastSerialNumber")]
    public string LastSerialNumber { get; set; }
    [JsonPropertyName("ActualDeliveredQtyInBaseUnit")]
    public string ActualDeliveredQtyInBaseUnit { get; set; }
    [JsonPropertyName("ActualDeliveryQuantity")]
    public string ActualDeliveryQuantity { get; set; }
    [JsonPropertyName("Container")]
    public string Container { get; set; }
    [JsonPropertyName("GrossWeight")]
    public string GrossWeight { get; set; }
    [JsonPropertyName("LineFeedLoc")]
    public string LineFeedLoc { get; set; }
    [JsonPropertyName("SupplierArea")]
    public string SupplierArea { get; set; }
    [JsonPropertyName("Madein")]
    public string MadeIn { get; set; }
    [JsonPropertyName("CustomerCode")]
    public string CustomerCode { get; set; }
    [JsonPropertyName("Customername")]
    public string CustomerName { get; set; }
    [JsonPropertyName("DockCode")]
    public string DockCode { get; set; }
}

