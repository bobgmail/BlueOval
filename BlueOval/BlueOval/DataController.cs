
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/data")]
public class DataController : ControllerBase
{
    private readonly DapperContext repo;
    private readonly IWebHostEnvironment env; 
    private readonly PdfUtility pdfTool;
    private IAccountLogic accountLogic;

    public DataController(DapperContext repository, IWebHostEnvironment environment, PdfUtility pdfUtility, IAccountLogic accountLogic)
    {
        repo = repository;
        env = environment;
        pdfTool = pdfUtility;
        accountLogic = accountLogic;
    }   
    
    // A private record type used for cleanly deserializing the JSON body of a POST request.
    // This is a good practice to avoid complex parameter binding in the action method itself.
    public record InsertOrderRequest(EquipmentOrderInfo OrderInfo, List<PartCycle> OrderDetails);



    [HttpGet("insequence/bol/{bolNumber}")]
    public Task<List<BolInfo>> GetInsequenceBolInfo(string bolNumber)
        => repo.GetInsequenceBolInfo(bolNumber);

    [HttpGet("insequence/rack")]
    public Task<List<RackInfo>> GetInsequenceRackInfo([FromQuery] string vin)
        => repo.GetInsequenceRackInfo(vin);

    [HttpGet("insequence/scan")]
    public Task<List<ScanInfo>> GetInsequenceScanInfo([FromQuery] string vin, [FromQuery] int type)
        => repo.GetInsequenceScanInfo(vin.Trim(), type);

    [HttpGet("insequence/scan/report")]
    public Task<List<ScanInfo>> GetInsequenceScanReport([FromQuery] DateTime dts, [FromQuery] DateTime dte, [FromQuery] int type)
        => repo.GetInsequenceScanReport(dts, dte, type);

    [HttpGet("insequence/rack/report")]
    public Task<List<RackInfo>> GetInsequenceRackReport([FromQuery] DateTime dts, [FromQuery] DateTime dte)
        => repo.GetInsequenceRackReport(dts, dte);

    #region Label Print Endpoints

    [HttpGet("labels/bols")]
    public Task<List<LabelPrintBolInfo>> GetLabelPrintBols()
        => repo.GetLabelPrintBolsAsync();

    [HttpGet("labels/info/{receivingId:int}")]
    public List<LabelPrintInfo> GetLabelPrintInfos(int receivingId)
        => repo.GetLabelPrintInfos(receivingId);

    #endregion

    #region Inbound/Outbound Endpoints

    [HttpGet("inbound")]
    public Task<List<PalletInfo>> GetInboundWithTimeRange([FromQuery] DateTime s, [FromQuery] DateTime e, [FromQuery] int mode)
        => repo.GetInboundWithTimeRange(s, e, mode);

    [HttpGet("inbound/battery")]
    public Task<List<PalletInfo>> GetInboundWithTimeRangeBattery([FromQuery] DateTime s, [FromQuery] DateTime e, [FromQuery] int mode)
        => repo.GetInboundWithTimeRangeBattery(s, e, mode);

    [HttpGet("inbound/summary")]
    public Task<List<InfoSummary>> GetInboundPartsSummary([FromQuery] DateTime s, [FromQuery] DateTime e, [FromQuery] int mode)
        => repo.GetInboundPartsSummary(s, e, mode);

    [HttpGet("outbound")]
    public Task<List<PalletInfo>> GetOutboundWithTimeRange([FromQuery] DateTime s, [FromQuery] DateTime e, [FromQuery] int mode)
        => repo.GetOutboundWithTimeRange(s, e, mode);

    [HttpGet("outbound/battery")]
    public Task<List<PalletInfo>> GetOutboundWithTimeRangeBattery([FromQuery] DateTime s, [FromQuery] DateTime e, [FromQuery] int mode)
        => repo.GetOutboundWithTimeRangeBattery(s, e, mode);

    [HttpGet("outbound/summary")]
    public Task<List<InfoSummary>> GetOutboundPartsSummary([FromQuery] DateTime s, [FromQuery] DateTime e, [FromQuery] int mode)
        => repo.GetOutboundPartsSummary(s, e, mode);

    #endregion

    #region Skid History Endpoints

    [HttpGet("history/receiving/{skidNo}")]
    public Task<List<ReceiveOrder>> GetReceivingAsync(string skidNo)
        => repo.GetReceivingAsync(skidNo);

    [HttpGet("history/picking/{skidNo}")]
    public Task<List<PickingLoadTransactions>> GetPickingLoadAsync(string skidNo)
        => repo.GetPickingLoadAsync(skidNo);

    [HttpGet("history/shipping/{skidNo}")]
    public Task<List<ShippingLoadTransactions>> GetShippingAsync(string skidNo)
        => repo.GetShippingAsync(skidNo);

    [HttpGet("history/movements/{skidNo}")]
    public Task<List<InventoryMovementDetails>> GetMovementsAsync(string skidNo)
        => repo.GetMovementsAsync(skidNo);

    #endregion

    #region Inventory Cycle Endpoints

    [HttpGet("inventory/parts-definitions")]
    public Task<Dictionary<int, string>> GetPartsDef()
        => repo.GetPartsDef();

    [HttpPost("inventory/cycle/ctrl")]
    // 2. Add the [FromBody] attribute to tell ASP.NET Core where to find the data.
    public async Task<ActionResult<List<PartCycleCtrl>>> GetInventoryCycleInfoCtrl([FromBody] List<PartCycle> sortedList)
    {
        // Now, the 'sortedList' variable inside this method will contain the
        // exact list that you sent from the client.

        // You can now use this list to perform your business logic.
        // For example, passing it to a repository method:
        var result = await repo.GetInventoryCycleInfoCtrl(sortedList);
        if (result == null)
        {
            return NotFound("Could not process the provided cycle list.");
        }

        return Ok(result);
    }


    [HttpGet("inventory/cycle")]
    public Task<List<PartCycle>> GetInventoryCycleInfo()
        => repo.GetInventoryCycleInfo();

    [HttpGet("inventory/cycle-by-part")]
    public Task<List<PartCycle>> GetInventoryCycleInfoPart([FromQuery] string partNo)
        => repo.GetInventoryCycleInfoPart(partNo);

    [HttpGet("inventory/cycle/battery")]
    public Task<List<PartCycle>> GetInventoryCycleInfoBattery()
        => repo.GetInventoryCycleInfoBattery();

    [HttpGet("inventory/cycle/battery-for-order")]
    public Task<List<PartCycle>> GetInventoryCycleInfoBatteryForOrder()
        => repo.GetInventoryCycleInfoBatteryForOrder();

    #endregion

    #region Orders Endpoints

    [HttpGet("orders")]
    public Task<List<EquipmentOrderInfo>> GetEquipmentOrderInfos([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int mode)
        => repo.GetEquipmentOrderInfos(startDate, endDate, mode);

    [HttpGet("orders/details/{orderId:int}")]
    public Task<List<EquipmentOrderDetail>> GetEquipmentOrderDetails(int orderId)
        => repo.GetEquipmentOrderInfos(orderId);

    [HttpGet("orders/is-pallet-ordered/{palletQrCode}")]
    public bool IsPalletOrdered(string palletQrCode)
        => repo.IsPalletOrdered(palletQrCode);

    [HttpPost("orders/insert")]
    public async Task<IActionResult> InsertOrder([FromBody] InsertOrderRequest req)
    {
        var (ok, id) = await repo.InsertOrderRequest(req.OrderInfo, req.OrderDetails);
        if (!ok)
        {
            // Returns a 400 Bad Request if the transaction failed.
            return BadRequest(new { message = "Failed to insert order. Check logs for details." });
        }
        // Returns a 200 OK with the new order ID in the body.
        return Ok(new { id });
    }

    #endregion

    #region Users & Permissions Endpoints

    [HttpGet("users/has-permission/{userId:int}")]
    public Task<bool> HasPermission(int userId)
        => repo.HasPermission(userId);

    [HttpGet("users/info/netcore/{userName}")]
    public Task<AspNetUsers?> GetUserInfoFromNetCore(string userName)
        => repo.GetUserInfoFromNetCore(userName);

    [HttpGet("users/info/his/{userName}")]
    public Task<AspNetUsers?> GetUserInfoFromHis(string userName)
        => repo.GetUserInfoFromHIS(userName);

    [HttpGet("users/roles/{userId}")]
    public Task<List<string>> GetUserRoles(string userId)
        => repo.GetUserRoles(userId);

    [HttpGet("users/employee-id/{userId}")]
    public Task<int> GetEmployeeId(string userId)
        => repo.GetEmplyeeID(userId);

    #endregion

    #region Location Endpoints

    [HttpGet("locations/battery-counts")]
    public Task<List<LocationInfo>> GetLocationInfoListBattery()
        => repo.GetLocationInfoListBattery();

    #endregion

   

    [HttpPost("labels/print")]
    public async Task<IActionResult> CreateAndPrintLabel([FromBody] PrintLabelRequest request)
    {
        try
        {

            string pdfName = request.ReceivingId.ToString().PadLeft(6, '0') + ".PDF";
            string labelsFolderPath = Path.Combine(env.WebRootPath, "Labels");

            if (!Directory.Exists(labelsFolderPath))
            {
                Directory.CreateDirectory(labelsFolderPath);
            }
            string fullFilePath = Path.Combine(labelsFolderPath, pdfName);
            // Delete the old file if it exists
            if (System.IO.File.Exists(fullFilePath))
            {
                System.IO.File.Delete(fullFilePath);
            }



            pdfTool.CreateLabelPDF(request.ReceivingId, request.Bol, pdfName, repo);

            var relativePath = $"Labels/{pdfName}";

            return Ok(new { FilePath = relativePath });
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, "An error occurred while creating the PDF.");
        }
    }
}

