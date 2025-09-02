using System.Text.Json.Serialization;

namespace BlueOval.SAPinterface.HearnS4;


public class InventoryRecordsPayload
{
    [JsonPropertyName("InventoryRecords")]
    public InventoryRecords InventoryRecords { get; set; }
}

public class InventoryRecords
{
    [JsonPropertyName("InventoryRecord")]
    public InventoryRecord[] InventoryRecord { get; set; }
}

public class InventoryRecord
{
    [JsonPropertyName("Plant")]
    public string Plant { get; set; }
    [JsonPropertyName("Sloc")]
    public string Sloc { get; set; }
    [JsonPropertyName("Batch")]
    public string Batch { get; set; }
    [JsonPropertyName("Material")]
    public string Material { get; set; }
    [JsonPropertyName("Qty")]
    public string Qty { get; set; }
    [JsonPropertyName("UnitOfEntry")]
    public string UnitOfEntry { get; set; }
    [JsonPropertyName("ExtPhInvNumber")]
    public string ExtPhInvNumber { get; set; }
}
/*
 {
    "InventoryRecords": {
        "InventoryRecord": [
            {
                "Plant": "5727",
                "Sloc": "2701",
                "Batch": "0000000002",
                "Material": "MD-GCC-001-2504L",
                "Qty": "210",
                "UnitOfEntry": "KG",
                "ExtPhInvNumber": "ExtPhInv0001"
            },
            {
                "Plant": "5727",
                "Sloc": "2701",
                "Batch": "0000000007",
                "Material": "MD-GCC-001",
                "Qty": "10",
                "UnitOfEntry": "KG",
                "ExtPhInvNumber": "ExtPhInv0002"
            }
        ]
    }
}
 */