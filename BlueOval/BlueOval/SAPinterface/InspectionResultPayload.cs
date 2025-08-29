using System.Text.Json.Serialization;

namespace BlueOvalBatteryPark.SAPinterface;


public class InspectionResultPayload
{
    [JsonPropertyName("InspectionLotUsageDecision")]
    public InspectionLotUsageDecision InspectionLotUsageDecision { get; set; }
}

public class InspectionLotUsageDecision
{
    [JsonPropertyName("Material")]
    public string Material { get; set; }
    [JsonPropertyName("Batch")]
    public string Batch { get; set; }
    [JsonPropertyName("BatchBySupplier")]
    public string BatchBySupplier { get; set; }

    //If InspectionLotUsageDecisionCode = “A1”, then the Lot Results is PASSED, Else Lot Result is FAILED
    [JsonPropertyName("InspectionLotUsageDecisionCode")]
    public string InspectionLotUsageDecisionCode { get; set; }
}
