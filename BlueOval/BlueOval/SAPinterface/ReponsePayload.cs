namespace BlueOvalBatteryPark.SAPinterface;

//return result in JSON format

public class ReturnClass
{
    public Response response { get; set; }
}

public class Response
{
    public Return Return { get; set; }
}

public class Return
{
    //[JsonPropertyName("returnCode")]
    public string returnCode { get; set; } = "";
    public string returnDesc { get; set; } = "";
}
