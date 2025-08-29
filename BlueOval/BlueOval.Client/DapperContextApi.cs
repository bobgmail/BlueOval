using DocumentFormat.OpenXml.Office2016.Excel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json; // Required for JsonElement
using System.Text.Json.Nodes;
using System.Threading.Tasks;
 // Ensure this using statement is correct for your shared project

/// <summary>
/// A client-side service to interact with the backend Data API.
/// This class uses HttpClient to make calls to the corresponding endpoints in DataController.
/// It should be registered as a scoped service in the Blazor application's Program.cs.
/// </summary>
public class DapperContextApi
{
    private readonly HttpClient _http;

    public DapperContextApi(HttpClient http) => _http = http;

    #region Insequence Endpoints

    public Task<List<BolInfo>?> GetInsequenceBolInfo(string bolNumbers)
    {
        // Safely parse the input string to an integer before making the API call.
        if (!int.TryParse(bolNumbers.Trim(), out var bol))
        {
            // Return an empty list if parsing fails to avoid unnecessary API calls.
            return Task.FromResult<List<BolInfo>?>(new List<BolInfo>());
        }
        return _http.GetFromJsonAsync<List<BolInfo>>($"api/data/insequence/bol/{bol}");
    }

    public Task<List<RackInfo>?> GetInsequenceRackInfo(string vin)
        => _http.GetFromJsonAsync<List<RackInfo>>($"api/data/insequence/rack?vin={Uri.EscapeDataString(vin)}");

    public Task<List<ScanInfo>?> GetInsequenceScanInfo(string vin, int type)
        => _http.GetFromJsonAsync<List<ScanInfo>>($"api/data/insequence/scan?vin={Uri.EscapeDataString(vin)}&type={type}");

    public Task<List<ScanInfo>?> GetInsequenceScanReport(DateTime dts, DateTime dte, int type)
        => _http.GetFromJsonAsync<List<ScanInfo>>($"api/data/insequence/scan/report?dts={dts:yyyy-MM-dd}&dte={dte:yyyy-MM-dd}&type={type}");

    public Task<List<RackInfo>?> GetInsequenceRackReport(DateTime dts, DateTime dte)
        => _http.GetFromJsonAsync<List<RackInfo>>($"api/data/insequence/rack/report?dts={dts:yyyy-MM-dd}&dte={dte:yyyy-MM-dd}");

    #endregion

    #region Label Print Endpoints

    public Task<List<LabelPrintBolInfo>?> GetLabelPrintBolsAsync()
        => _http.GetFromJsonAsync<List<LabelPrintBolInfo>>("api/data/labels/bols");

    public Task<List<LabelPrintInfo>?> GetLabelPrintInfos(int receivingId)
        => _http.GetFromJsonAsync<List<LabelPrintInfo>>($"api/data/labels/info/{receivingId}");

    #endregion

    #region Inbound/Outbound Endpoints

    public Task<List<PalletInfo>?> GetInboundWithTimeRange(DateTime s, DateTime e, int mode)
        => _http.GetFromJsonAsync<List<PalletInfo>>($"api/data/inbound?s={s:yyyy-MM-dd}&e={e:yyyy-MM-dd}&mode={mode}");

    public Task<List<PalletInfo>?> GetInboundWithTimeRangeBattery(DateTime s, DateTime e, int mode)
        => _http.GetFromJsonAsync<List<PalletInfo>>($"api/data/inbound/battery?s={s:yyyy-MM-dd}&e={e:yyyy-MM-dd}&mode={mode}");

    public Task<List<InfoSummary>?> GetInboundPartsSummary(DateTime s, DateTime e, int mode)
        => _http.GetFromJsonAsync<List<InfoSummary>>($"api/data/inbound/summary?s={s:yyyy-MM-dd}&e={e:yyyy-MM-dd}&mode={mode}");

    public Task<List<PalletInfo>?> GetOutboundWithTimeRange(DateTime s, DateTime e, int mode)
        => _http.GetFromJsonAsync<List<PalletInfo>>($"api/data/outbound?s={s:yyyy-MM-dd}&e={e:yyyy-MM-dd}&mode={mode}");

    public Task<List<PalletInfo>?> GetOutboundWithTimeRangeBattery(DateTime s, DateTime e, int mode)
        => _http.GetFromJsonAsync<List<PalletInfo>>($"api/data/outbound/battery?s={s:yyyy-MM-dd}&e={e:yyyy-MM-dd}&mode={mode}");

    public Task<List<InfoSummary>?> GetOutboundPartsSummary(DateTime s, DateTime e, int mode)
        => _http.GetFromJsonAsync<List<InfoSummary>>($"api/data/outbound/summary?s={s:yyyy-MM-dd}&e={e:yyyy-MM-dd}&mode={mode}");

    #endregion

    #region Skid History Endpoints
    // For methods returning 'dynamic', we deserialize into a flexible type like JsonElement or Dictionary.
    public Task<List<ReceiveOrder>?> GetReceivingAsync(string skidNo)
        => _http.GetFromJsonAsync<List<ReceiveOrder>>($"api/data/history/receiving/{Uri.EscapeDataString(skidNo)}");

    public Task<List<PickingLoadTransactions>?> GetPickingLoadAsync(string skidNo)
        => _http.GetFromJsonAsync<List<PickingLoadTransactions>>($"api/data/history/picking/{Uri.EscapeDataString(skidNo)}");

    public Task<List<ShippingLoadTransactions>?> GetShippingAsync(string skidNo)
        => _http.GetFromJsonAsync<List<ShippingLoadTransactions>>($"api/data/history/shipping/{Uri.EscapeDataString(skidNo)}");

    public Task<List<InventoryMovementDetails>?> GetMovementsAsync(string skidNo)
        => _http.GetFromJsonAsync<List<InventoryMovementDetails>>($"api/data/history/movements/{Uri.EscapeDataString(skidNo)}");
    #endregion

    #region Inventory Cycle Endpoints

    public async Task<List<PartCycleCtrl>> GetInventoryCycleInfoCtrl(List<PartCycle> SortedList)
    {
        // Use PostAsJsonAsync to send the 'SortedList' in the request body.
        var response = await _http.PostAsJsonAsync("api/data/inventory/cycle/ctrl", SortedList);

        // Ensure the server responded with a success code.
        response.EnsureSuccessStatusCode();

        // Deserialize the response from the server into the list you need.
        return await response.Content.ReadFromJsonAsync<List<PartCycleCtrl>>();
    }

    public Task<Dictionary<int, string>?> GetPartsDef()
    => _http.GetFromJsonAsync<Dictionary<int, string>>("api/data/inventory/parts-definitions");

    public Task<List<PartCycle>?> GetInventoryCycleInfo()
        => _http.GetFromJsonAsync<List<PartCycle>>($"api/data/inventory/cycle");
    public Task<List<PartCycle>?> GetInventoryCycleInfoPart(string partNo)
        => _http.GetFromJsonAsync<List<PartCycle>>($"api/data/inventory/cycle-by-part?partNo={Uri.EscapeDataString(partNo)}");
    public Task<List<PartCycle>?> GetInventoryCycleInfoBattery()
        => _http.GetFromJsonAsync<List<PartCycle>>("api/data/inventory/cycle/battery");
    public Task<List<PartCycle>?> GetInventoryCycleInfoBatteryForOrder()
        => _http.GetFromJsonAsync<List<PartCycle>>("api/data/inventory/cycle/battery-for-order");
    #endregion
    #region Orders Endpoints
    public Task<List<EquipmentOrderInfo>?> GetEquipmentOrderInfos(DateTime startDate, DateTime endDate, int mode)
        => _http.GetFromJsonAsync<List<EquipmentOrderInfo>>($"api/data/orders?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}&mode={mode}");
    
    public Task<List<EquipmentOrderDetail>?> GetEquipmentOrderInfos(int orderId)
        => _http.GetFromJsonAsync<List<EquipmentOrderDetail>>($"api/data/orders/details/{orderId}");
    public Task<bool> IsPalletOrdered(string palletQrCode)
        => _http.GetFromJsonAsync<bool>($"api/data/orders/is-pallet-ordered/{Uri.EscapeDataString(palletQrCode)}");
    public async Task<(bool Success, int ID)> InsertOrderRequest(EquipmentOrderInfo orderInfo, List<PartCycle> orderDetails)
    {
        // The anonymous type here must match the parameter name in the controller action: public async Task<IActionResult> InsertOrder([FromBody] InsertOrderRequest req)
        var response = await _http.PostAsJsonAsync("api/data/orders/insert", new { OrderInfo = orderInfo, OrderDetails = orderDetails });
        if (!response.IsSuccessStatusCode) return (false, 0);

        // The controller returns a JSON object like { "id": 123 }, which we deserialize here.
        var payload = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
        return (true, payload?["id"] ?? 0);
    }

    #endregion

    #region Users & Permissions Endpoints
    public Task<bool> HasPermission(int userId)
        => _http.GetFromJsonAsync<bool>($"api/data/users/has-permission/{userId}");
    public Task<AspNetUsers?> GetUserInfoFromNetCore(string userName)
        => _http.GetFromJsonAsync<AspNetUsers>($"api/data/users/info/netcore/{Uri.EscapeDataString(userName)}");
    public Task<AspNetUsers?> GetUserInfoFromHis(string userName)
        => _http.GetFromJsonAsync<AspNetUsers>($"api/data/users/info/his/{Uri.EscapeDataString(userName)}");
    public Task<List<string>?> GetUserRoles(string userId)
        => _http.GetFromJsonAsync<List<string>>($"api/data/users/roles/{Uri.EscapeDataString(userId)}");
    public Task<int> GetEmployeeId(string userId)
        => _http.GetFromJsonAsync<int>($"api/data/users/employee-id/{Uri.EscapeDataString(userId)}");
    #endregion
    #region Location Endpoints

    public Task<List<LocationInfo>?> GetLocationInfoListBattery()
        => _http.GetFromJsonAsync<List<LocationInfo>>("api/data/locations/battery-counts");
    #endregion

    
 



    public async Task<string?> CreatePdfOnServer(int receivingId, string bol)
    {
        var request = new { ReceivingId = receivingId, Bol = bol };


        var response = await _http.PostAsJsonAsync("api/data/labels/print", request);

      
        if (response.IsSuccessStatusCode)
        {
            
            JsonNode? jsonNode = await response.Content.ReadFromJsonAsync<JsonNode>();

          
            return jsonNode?["FilePath"]?.GetValue<string?>();
        }

        
        return null;
    }


    


  


}