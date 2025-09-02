using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BlueOval.SAPinterface.S4Hearn;


public class InspectionResultPayload
{
    [JsonPropertyName("InspectionLotUsageDecision")]
    public InspectionLotUsageDecision InspectionLotUsageDecision { get; set; }
}

public class InspectionLotUsageDecision
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Material is required")]
    [JsonPropertyName("Material")]
    public string Material { get; set; }
    [JsonPropertyName("Batch")]
    public string Batch { get; set; }
    [JsonPropertyName("BatchBySupplier")]
    public string BatchBySupplier { get; set; }

    //If InspectionLotUsageDecisionCode = “A1”, then the Lot Results is PASSED, Else Lot Result is FAILED
    [Required(AllowEmptyStrings = false, ErrorMessage = "InspectionLotUsageDecisionCode is required")]
    [JsonPropertyName("InspectionLotUsageDecisionCode")]
    public string InspectionLotUsageDecisionCode { get; set; }
}
