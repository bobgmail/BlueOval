

using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;


public enum HisSiteType
{
    Canton,
    Louisville,
    Louisville2,
    Louisville3,
    Windsor1,
    Windsor2,
    Romulus1,
    Romulus2,
    Cleveland,
    Memphis,
    Oakville,
    Strongsville,
    Taylor,
    Taylor2,
    BattleCreek,
}

public class ScanInfo
{
    public string VinNumber { get; set; }
    public string ProcessSequence { get; set; }
    public string PartNumber { get; set; }
    public string PartDesc { get; set; }
    public string MbModuleType { get; set; }
    public string MbSupplierCode { get; set; }

    public int    ScanType { get; set; }
    public string ScanData { get; set; }
    public string DcTagDesc { get; set; }
    public string ProductionLine { get; set; }
    public DateTime ProcessDate { get; set; }
}

public class RackInfo
{
    public string VinNumber { get; set; }
    public string ProcessSequence { get; set; }
    public string PartNumber { get; set; }
    public string PartDesc { get; set; }
    public string MbModuleType { get; set; }
    public string MbSupplierCode { get; set; }

    public int SlotPosition { get; set; }
    public string RackGroup { get; set; }
    public string BOLNumber { get; set; }
    public DateTime RackDate { get; set; }
}


public class BolInfox
{
    public int BOLNumber { get; set; }
    public int BOLDetailId { get; set; }
    public string PartNumber { get; set; }
    public int PartQty { get; set; }
    public DateTime ProcessDate { get; set; }
}

public partial class BolInfo
{
    public int Id { get; set; }

    public int ShippingId { get; set; }
    [Required]
    public int ShipperID { get; set; }
    [Required]

    public int CarrierId { get; set; }

    [Required]
    public int ConsigneeId { get; set; }

    [Required]
    public string TrailerNo { get; set; }

    public DateTime CreateDate { get; set; }

    public int UserId { get; set; }

    //for Display
    public string Shipper { get; set; }
    public string Carrier { get; set; }
    public string Consignee { get; set; }
    public bool IsSelectedForDel { get; set; }

    public string GetBolPdfFileName()
    {
        return "BL" + Id.ToString().PadLeft(6, '0') + ".PDF";
    }
    public string BolNo()
    {
        return "BL" + Id.ToString().PadLeft(6, '0');
    }

    public string ShippingNo()
    {
        return "SH" + ShippingId.ToString().PadLeft(6, '0');
    }
}
public enum CustomerType
{
    Shipper = 1,
    Carrier = 2,
    Consignee = 3
}
public partial class Customer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Customer Type is required")]
    public CustomerType? CustType { get; set; }

    [Required(ErrorMessage = "Customer Name is required")]
    public string CustName { get; set; }
    [Required(ErrorMessage = "Address is required")]
    public string Addr { get; set; }

    public string City { get; set; }

    public string Province { get; set; }

    public string PostCode { get; set; }
    public string Phone { get; set; }

    public DateTime CreateDate { get; set; }

    public int UserId { get; set; }
    //for UI operating
    public bool IsSelectedForDel { get; set; }

    public string GetCustomerType()
    {
        switch (CustType)
        {
            case CustomerType.Shipper:
                return "Shipper";
                break;
            case CustomerType.Carrier:
                return "Carrier";
                break;
            case CustomerType.Consignee:
                return "Consignee";
        }
        return "";
    }
}
public partial class InventoryPartsWithLocation
{
    public int Id { get; set; }

    public int CtrlId { get; set; }

    public int OpType { get; set; }

    public int PartId { get; set; }

    public string PartNo { get; set; }

    public string SkidNo { get; set; }

    public int Qty { get; set; }

    public DateTime CreateDate { get; set; }

    public int LocId { get; set; }

    public string LocName { get; set; }

    public int UserId { get; set; }

    //PartInfo for BOL
    public string Info { get; set; } = "";

    public double? PartWeight { get; set; }

}

public class Country
{
    public string? Name { get; set; }
    public string? Capital { get; set; }
    public string? Population { get; set; }
    public string? Area_KM_Squared { get; set; }
}

public class LabelPrintInfo
{
    public int ReceivingID { get; set; }
    public int ReceivingPartID { get; set; }
    public string? PartNumber { get; set; }
    public int BinQuantity { get; set; }
    //Battle Creek
    public string? ContainerNo { get; set; }
    public string? PalletNo { get; set; }
    public string? ContainerQRCode { get; set; }
    public string? PalletQRCode { get; set; }
}

public class LabelPrintBolInfo
{
    public int ReceivingID { get; set; }
    public int LabelsCount { get; set; }
    public string?  BOL { get; set; }
    public string? SupplierName { get; set; }
    public string? CustomerName { get; set; }
    public string? CarrierName { get; set; }
    public string? TrailerNo { get; set; }
    public DateTime ReceivingDate { get; set; }
 
}


public class EquipmentOrderInfo
{
    public int ID { get; set; }
    [Required]
    public string? Installer { get; set; }
    [Required]
    public string? Requestor { get; set; }
    [Required]
    public string? DockDoor { get; set; }

    public string? DocumentNo { get; set; }

    public DateTime RevisedDate { get; set; }
    public string? RevisonLevel { get; set; }
    [Required]
    public DateTime? DeliveryDate { get; set; } = null;
    [Required]
    //public TimeSpan? DeliveryTime { get; set; } = null;
    public string? DeliveryTime { get; set; }
    
    [Required]
    public DateTime CreateTime { get; set; }

    public int CreatedBy { get; set; }

    //public string GetTimeDisplay()
    //{
    //    DateTime time = DateTime.Today.Add(DeliveryTime.Value);
    //    string formatted = time.ToString("hh:mm tt"); // "02:30 PM"
    //    return formatted;
    //}
    // added on 2025-07-24
    public string InstallerContact { get; set; } = "";
    public string InstallerPhone { get; set; } = "";
}

public class EquipmentOrderDetail
{
    public int ID { get; set; }
    public int OrderID { get; set; }

    public string? ContainerNo { get; set; }
    public string? ContainerQRCode { get; set; }
    public string? PalletNo { get; set; }
    public string? PalletQRCode { get; set; }
    public string? WhseSite { get; set; }
    public bool ContainerSelected { get; set; }
    public bool PalletSelected { get; set; }
    public DateTime CreateTime { get; set; }
    public string GetContainerSelected()
    {
        return ContainerSelected ? "X" : "";
    }

    public string GetPalletSelected()
    {
        return PalletSelected ? "X" : "";
    }
}

public class LocationInfo
{
    public int Id { get; set; }
    public string LocationType { get; set; } = string.Empty;
    public string LocationCode { get; set; } = string.Empty;
    public int LocationCount { get; set; }
    public HashSet<string> LocationUsed { get; set; } = new();

    public string PercentUsed()
    {
         if (LocationCount == 0) return "0%";
         float percent = (float)LocationUsed.Count*100 / LocationCount;
         return percent.ToString("F2") + "%";
    }
}


public class PrintLabelRequest
{
    public int ReceivingId { get; set; }
    public string Bol { get; set; }
}

public class UserInfo
{
    public string UserName { get; set; }
    public string[] Roles { get; set; }
    public int EmployeeID { get; set; }
}