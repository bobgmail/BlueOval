using System.Text.Json.Serialization;

namespace BlueOval.SAPinterface.HearnS4;


public class GoodsMovements
{
    [JsonPropertyName("PostingDate")]
    public DateTime PostingDate { get; set; }

    [JsonPropertyName("GoodsMovementCode")]
    public string GoodsMovementCode { get; set; }

    public To_MaterialDocumentItem to_MaterialDocumentItem { get; set; }
}

public class To_MaterialDocumentItem
{
    public Result[] results { get; set; }
}

public class Result
{
    [JsonPropertyName("Material")]
    public string Material { get; set; }
    [JsonPropertyName("Plant")]
    public string Plant { get; set; }
    [JsonPropertyName("StorageLocation")]
    public string StorageLocation { get; set; }
    [JsonPropertyName("GoodsMovementType")]
    public string GoodsMovementType { get; set; }
    [JsonPropertyName("PurchaseOrder")]
    public string PurchaseOrder { get; set; }
    [JsonPropertyName("PurchaseOrderItem")]
    public string PurchaseOrderItem { get; set; }
    [JsonPropertyName("GoodsMovementRefDocType")]
    public string GoodsMovementRefDocType { get; set; }
    [JsonPropertyName("QuantityInEntryUnit")]
    public string QuantityInEntryUnit { get; set; }
    [JsonPropertyName("Supplier")]
    public string Supplier { get; set; }
    [JsonPropertyName("EntryUnit")]
    public string EntryUnit { get; set; }
}
//response result in JSON format


public class GoodsMovementResponse
{
    public D d { get; set; }
}

public class D
{
    public __Metadata __metadata { get; set; }
    [JsonPropertyName("MaterialDocumentYear")]
    public string MaterialDocumentYear { get; set; }
    [JsonPropertyName("MaterialDocument")]
    public string MaterialDocument { get; set; }
    [JsonPropertyName("InventoryTransactionType")]
    public string InventoryTransactionType { get; set; }
    [JsonPropertyName("DocumentDate")]
    public DateTime DocumentDate { get; set; }
    [JsonPropertyName("PostingDate")]
    public DateTime PostingDate { get; set; }
    [JsonPropertyName("CreationDate")]
    public DateTime CreationDate { get; set; }
    [JsonPropertyName("CreationTime")]
    public string CreationTime { get; set; }
    [JsonPropertyName("CreatedByUser")]
    public string CreatedByUser { get; set; }
    [JsonPropertyName("MaterialDocumentHeaderText")]
    public string MaterialDocumentHeaderText { get; set; }
    [JsonPropertyName("ReferenceDocument")]
    public string ReferenceDocument { get; set; }
    [JsonPropertyName("VersionForPrintingSlip")]
    public string VersionForPrintingSlip { get; set; }
    [JsonPropertyName("ManualPrintIsTriggered")]
    public string ManualPrintIsTriggered { get; set; }
    [JsonPropertyName("CtrlPostgForExtWhseMgmtSyst")]
    public string CtrlPostgForExtWhseMgmtSyst { get; set; }
    [JsonPropertyName("GoodsMovementCode")]
    public string GoodsMovementCode { get; set; }
    public To_MaterialDocumentItem to_MaterialDocumentItem { get; set; }
}

public class __Metadata
{
    public string id { get; set; }
    public string uri { get; set; }
    public string type { get; set; }
}

//public class To_MaterialDocumentItem
//{
//    public object[] results { get; set; } //dynamic type to hold results
//}

/*
 {
    "PostingDate": "2025-06-26T00:00:00",
    "GoodsMovementCode": "01",
    "to_MaterialDocumentItem": {
        "results": [
            {
                "Material": "TG11",
                "Plant": "17AA",
                "StorageLocation": "17AA",
                "GoodsMovementType": "101",
                "PurchaseOrder": "4500000201",
                "PurchaseOrderItem": "30",
                "GoodsMovementRefDocType": "B",
                "QuantityInEntryUnit": "2",
                "EntryUnit": "PC"
            }
        ]
    }
}

 */