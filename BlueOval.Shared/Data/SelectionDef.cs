
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;


public class LoadingPartInfo
{
    public int ShippingID { get; set; } = 0;
    public int PartID { get; set; } = 0;

    public string PartNumber { get; set; } = "";
    public int PartQuantity { get; set; } = 0;
    public int PartsOut { get; set; } = 0;
    public string SkidNo { get; set; } = "";
    public DateTime CreatedDate { get; set; }

    public int PickPartQuantity { get; set; } = 0;
    public string PickSkidNo { get; set; } = "";
    public DateTime PickCreatedDate { get; set; }

    public string ShippingStatus { get; set; } = "";
    public string TrailerCode { get; set; } = "";
    public string TrailerNo { get; set; } = "";
 }

public class ReceiverPartInfo
{
    [DisplayName("Receiver")]
    public int ReceivingID { get; set; } = 0;

    [DisplayName("Part Number")]
    public string PartNumber { get; set; } = "";
    [DisplayName("Received Qty")]
    public int PartReceived { get; set; } = 0;
    public int PartQuantity { get; set; } = 0;
    [DisplayName("Trailer No")]
    public string TrailerNo { get; set; } = "";
    [DisplayName("Closed Date")]
    public DateTime DateClosed { get; set; }
}

public class WarningPart
{
    public int PartID { get; set; }
    public string PartNo { get; set; } = "";
    public string Description { get; set; } = "";
    
    public DateTime ShipDate { get; set; }
    public TimeSpan ShipTime { get; set; }
    public int Qty { get; set; } = 0;
    public List<(DateTime ShipDate, int Qty)> warnings { get; set; } = new();
}

public class SelectionDef
{
    public int Index { get; set; }
    public string Name { get; set; }
}

public class SelectWithStyle
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
    public int Style { get; set; } = 0;

    public string GetStyle() 
    {
        return Style switch
        {
            0 => "",
            _ => "color: darkgreen;",
        };
    }
}
//Index:
// 0 All in Receiving Order, but not in movement & shipment
// -1 Special for All in Receiving Order, but not in movement & shipment: LocationID = 0 
public class SelectionDefLoc
{
    public int Index { get; set; }
    public string Name { get; set; }
    public bool HasMovement { get; set; } = false;
    public bool HasReceiveOrder { get; set; } = false;
}

public class PartMovement
{
    public int PartID { get; set; }
    public string PartNo { get; set; }

}
public class PartOperator
{
    public PartOperator()
    {
        Location = string.Empty;
        NewLocation = string.Empty;
        IsValid = true;
        Notes = string.Empty;
        AssignedLoc = null;
    }

    ~PartOperator()
    {
        if (AssignedLoc != null)
        {
            AssignedLoc.Clear();
            AssignedLoc = null;
        }
    }

    public int ID { get; set; }             //Valid for only Table operating, usch as RescanLocation
    public int PartID { get; set; }
    public string PartNo { get; set; }
    public string SkidNo { get; set; }
    public decimal Qty { get; set; }
    public int MaxQty { get; set; }     //Max Qty for Picking Control
    public int LocID { get; set; }
    public int UserID { get; set; }
    public string Location { get; set; }
    //public string Zone { get; set; }
    //------for Move Location
    public decimal NewQty { get; set; }
    public int NewLocID { get; set; }
    public string NewLocation { get; set; }

    //For Displaying current Operating
    public InventoryLocationOp OpType { get; set; }

    public DateTime CreatedDate { get; set; }


    //------------for delete-------------------
    public bool IsSelectedForDel { get; set; }

    //------------for movement----------------
    public bool IsSelectedForMove { get; set; }
    //public string Zone { get; set; }
    //---------------for upload--------------
    public decimal ShippedQty { get; set; }

    //----for Picklist-------------
    public bool IsValid { get; set; }
    public string Notes { get; set; }
    public string Prefix { get; set; }
    public string BaseCode { get; set; }
    public string Suffix { get; set; }
    public List<PartOperator>? AssignedLoc { get; set; }

    //Custom Binding
    public string QtyValue
    {
        get => ((int) Qty).ToString();
        set
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            NumberStyles style = NumberStyles.AllowDecimalPoint;// | NumberStyles.AllowLeadingSign;
            if (int.TryParse(value, style, culture, out int number))
            {
                if(number > 0)
                    Qty = number;
            }
        }
    }

    public string MaxQtyValue
    {
        get => ((int)Qty).ToString();
        set
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            NumberStyles style = NumberStyles.AllowDecimalPoint;// | NumberStyles.AllowLeadingSign;
            if (int.TryParse(value, style, culture, out int number))
            {
                if (number > 0)
                {
                    if( number <= MaxQty)
                        Qty = number;
                    else
                        Qty = MaxQty;
                }
            }
        }
    }

}

//Used by MEMPHIS
public class PartSummaryCtrl
{
    public int IndexS { get; set; }
    public int IndexD { get; set; }
    //Order by Part or Location
    public int PartOrLocactionID { get; set; }
    public string? PartNoOrLocation { get; set; }
    
    //public string? Zone { get; set; }
    public decimal Qty { get; set; }

}

public class InventoryPart
{
    public int ReceiveOrderId { get; set; }
    public int? ReceivingId { get; set; }
    public int? ReceivingPartId { get; set; }
    public int PartId { get; set; }
    [DisplayName("Part Number")]
    public string PartNumber { get; set; }
    public decimal? PartQuantity { get; set; }
    public decimal? ReceivedQty { get; set; }
    public int? BinQty { get; set; }
    public string TrackingInfo { get; set; }
    public int? CreatedById { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? LastModifiedById { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public bool? IsActive { get; set; }
    public string SkidsSerialno { get; set; }
    public string Location { get; set; }

}
public class PartInfo
{
    public int PartID { get; set; }
    public string PartNo { get; set; }
    public string PartDesc { get; set; }
    public int QtyPerBin { get; set; }
    public decimal Weight { get; set; }

    //Frap like, Memphis Site special
    public string SkidNo { get; set; }
}

public class PartWithReceiving
{
    public string ReceivingID { get; set; }
    public string PartNumber { get; set; }
    public string PartReceived { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DateClosed { get; set; }
}

	public class PartWithShipping
	{
		public string ShippingID { get; set; }
		public string PartNumber { get; set; }
		public string BinsOut { get; set; }
		public string PartsOut { get; set; }
		public string ShippingLocationTitle { get; set; }
		public string TrailerNo { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? DateClosed { get; set; }
        public bool? IsExportedToANX {  get; set; }
	}

	public class PartCycle
    {
    public int ID { get; set; }             //Valid for only Table operating, usch as RescanLocation
    public int PartID { get; set; }
    public string PartNo { get; set; }
    public string PartDesc { get; set; }="";
    public string SkidNo { get; set; } = "";
    public decimal PartQty { get; set; }
    public decimal BinQty { get; set; } = 1.0m;
    public int  LocationID { get; set; }
    public string Location { get; set; }
    public string LocationCode { get; set; }
    public int  DstLocationID { get; set; }
    public string DstLocation { get; set; }     //after modif
    public bool IsShipped { get; set; } = false;    //after modif
    public bool IsLoaded { get; set; } = false;    //after modif
    public bool IsScanned { get; set; } = false;    //after modif
    public bool IsNewFound { get; set; } = false;    //after modif

    public DateTime TransactionDate { get; set; }       //used for calculate summary report  
    public DateTime CreatedDate { get; set; }           //used for calculate summary report  
    public string WhseSite { get; set; } = "";

    public bool IsLocationModified()
    {
        if( DstLocationID != LocationID)
            return true;
     
        return false;
    }
    
    
    public string? ContainerNo { get; set; }            //For Battery Park
    public string? ContainerQRCode { get; set; }     //For Battery Park
    public string? PalletNo { get; set; }           //For Battery Park
    public string? PalletQRCode { get; set; }     //For Battery Park

    public string EquipmentOp { get; set; } = "";    //For Battery Park
    public string SectionComp { get; set; } = "";    //For Battery Park
    public string ProcessEquip { get; set; } = "";    //For Battery Park
    public string SectionCompName { get; set; } = "";    //For Battery Park



    public bool IsContainerSelected{ get; set; } = false;    //after modif
    public bool IsPalletSelected{ get; set; } = false;    //after modif

    public bool IsPalletAlreadyOrdered{ get; set; } = false;    //after modif
}

public class ActionCtrol
{
    public ActionCtrol()
    {
    }
    public int ID { get; set; }
    public int RealID { get; set; }
    public DateTime CreateTime { get; set; }
}

public class PartCycleCtrl
{
    public int IndexS { get; set; }
    public int IndexD { get; set; }
    public int PartID { get; set; }
    public string PartNo { get; set; }
    public string PartDesc { get; set; }
    public decimal PartQtySum { get; set; }
    public decimal BinQtySum { get; set; }
    public decimal CyclePartQtySum { get; set; }
    public decimal CycleBinQtySum { get; set; }
    public bool IsCountModified { get; set; }=false;

    public void SetCountModified(List<PartCycle> CyclePartsList)
    {
        for(int n = IndexS; n <= IndexD; n++ )
        {
            if(CyclePartsList[n].IsShipped)
            {
                IsCountModified = true;
                return;
            }
        }
        IsCountModified = false;
    }

}

public class ProductivityInfo: IEquatable<ProductivityInfo>, IComparable<ProductivityInfo>
{
    public string EmpName { get; set; }
    public int EmpID { get; set; }
    public int Total { get; set; }
    public float JPH { get; set; }
    public int[] CatCount { get; set; }
    public ProductivityInfo()
    {
        CatCount = new int[24];// [13];     //Add JPH item at the end 2024.02.01
        EmpName = "";
    }

    public string GetCatCount( int mode, int index)
    {
       int shiftStart = SiteControl.GetShiftStart(mode);
       if(mode == 0 )
        {
            
            int ret = CatCount[shiftStart + index];
            if (ret == 0)
                return "-";
            else
                return ret.ToString();
        }

        index = (shiftStart + index + 12) % 24;
        int rett =  CatCount[index];
        if (rett == 0)
            return "-";
        else
            return rett.ToString();
    }


    public void Add(int Cat,int count, float hours)
    {
        // int index = 0;

        //if(Cat >= shiftStart && Cat < shiftStart + 12)  //6am~6pm
        //{
        //    CatCount[Cat- shiftStart] = count;
        //    Total += count;
        //}
        //else if(Cat >= shiftStart + 12 && Cat < shiftStart + 12 + shiftStart) //6pm~12pm
        //{
        //    CatCount[Cat - (12 + shiftStart)] = count;
        //    Total += count;
        //}
        //else                                  //0am~6am
        //{
        //    CatCount[Cat + shiftStart] = count;
        //    Total += count;
        //}
        CatCount[Cat] = count;
        Total += count;

        if(hours > 0) 
            JPH = Total / hours; 
        else
            JPH = Total;
    }
    public override string ToString()
    {
        return "Name: " + EmpName + "   Count: " + Total;
    }
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        ProductivityInfo objAsPart = obj as ProductivityInfo;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }

    public bool Equals(ProductivityInfo other)
    {
        if(other == null) 
            return false;
        return other.ToString() == ToString();
    }

    // Default comparer
    public int CompareTo(ProductivityInfo? other)
    {
        // A null value means that this object is greater.
        if (other == null)
            return 1;

        if(other.Total == Total)
        {
            return EmpName.CompareTo(other.EmpName);
        }
        else
        {
            //reverse order
            return other.Total.CompareTo(Total);
        }
    }

    public override int GetHashCode()
    {
        return EmpID;
    }
}
public class Productivity
{
    public Productivity()
    {
        CatTotal = new int[24];
        productivityInfos = new List<ProductivityInfo>();
    }
    public string GetCatTotal( int mode, int index)
    {
        int shiftStart = SiteControl.GetShiftStart(mode);
        if (mode == 0)
        {
            int ret = CatTotal[shiftStart + index];
            if (ret == 0)
                return "-";
            else
                return ret.ToString();
        }

        index = (shiftStart + index + 12) % 24;
        int r =  CatTotal[index];
        if (r == 0)
            return "-";
        else
            return r.ToString();
    }

    public void Add(ProductivityInfo info, float totalHours)
    {
        productivityInfos.Add(info);
        for(int n = 0; n <= 23; n++)
        {
            CatTotal[n] += info.CatCount[n];
            Total += info.CatCount[n];
        }
        if (totalHours > 0)
            JPH = Total / totalHours;
        else 
            JPH = Total;
    }
    public int[] CatTotal { get; set; }
    public int Total { get; set; }
    public float JPH { get; set; }
    public List<ProductivityInfo> productivityInfos;
}
public class PalletInfo
{
    public int ID { get; set; }
    public int MasterID { get; set; }
    public int PartID { get; set; }
    public string  PartNo { get; set; }
    public string  SkidNo { get; set; }
    public decimal Qty { get; set; }
    public DateTime CreatedDate { get; set; }

    //Battle Creek
    public string  ContainerNo { get; set; }
    public string  PalletNo { get; set; }
    public string  ContainerQRCode { get; set; }
    public string  PalletQRCode { get; set; }
    public string TrailerNo { get; set; } = "";
    public string DeliveryDoor { get; set; } = "";

    public string Notes { get; set; }
    public bool b6AMto6PM { get; set; }
    public PalletInfo()
    {
        PartNo = "";
        SkidNo = "";
        Notes = "";
    }
    public void Set6AM6PM()
    {
        DateTime dts = new DateTime(CreatedDate.Year, CreatedDate.Month, CreatedDate.Day, 6, 0, 0);
        DateTime dte = dts.AddHours(12);
        if(CreatedDate>=dts && CreatedDate < dte)
            b6AMto6PM = true;
        else
            b6AMto6PM = false;
    }
}

public class InfoSummary
{
    public string Name { get; set; } = "";
    public decimal Qty { get; set; }

}
public class ReceivingOrder
{
    public int ReceiveOrderID { get; set; }
    public int ReceivingId { get; set; }
    public int ReceivingPartID { get; set; }
    public int PartId { get; set; }
    public string PartNumber { get; set; } = "";
    public float PartQuantity { get; set; }
    public float ReceivedQty { get; set; }
    public int QtyPerBin { get; set; }
    public string TrackingInfo { get; set; } = "";
    public int CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
    public string SkidsSerialno { get; set; } = "";
    //display
    public string Operator { get; set; } = "";
}

public class PartCumm
{
    public int PartID { get; set; }
    public string PartNumber { get; set; }
    public int PickedCumm { get; set; }

    public int? NewQty { get;set; }
    public bool IsSelected { get; set; } = false;
}
public class ShippingLoad
{
    public int LoadID { get; set; }
    public int ShippingID { get; set; }
    public int ShippingDetailID { get; set; }
    public int PartId { get; set; }
    public string PartNumber { get; set; } = "";
    public float PartQuantity { get; set; }
    public int CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
    public string SkidNo { get; set; } = "";
    //display
    public string Operator { get; set; } = "";

}

public class KitAssembingInfo
{
    public string GSDB { get; set; } = "";
    //public DateTime ReqDate    { get; set; }
    public DateTime GenDate    { get; set; } = DateTime.Now;
    public string PartNumber { get; set; } = "";
    public int InventoryQty { get; set; }
    public int[] ReqNum { get; set; } = new int[7];

    public int GetBuildQty(int n)
    {
        if (n >= 7)
            n = 6;
        int sum = 0;
        for(int k = 0; k <= n; k++)
            sum += ReqNum[k];

        return sum - InventoryQty;
    }
    ~KitAssembingInfo() 
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        ReqNum = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }
}

public class InventorySummaryReport
{
    public int CustomerID { get; set; }
    public string CustomerName { get; set; } = "";
    public int PartID { get; set; }
    public string PartNumber { get; set; } = "";
    public string PartDescription { get; set; } = "";
    public string Notes { get; set; } = "";
    public int QtyMinLevel { get; set; }
    public int QtyMaxLevel { get; set; }
    public int OpBinQty { get; set; }
    public int OpPartQty { get; set; }
    public int BinsIn { get; set; }
    public int PartsIn { get; set; }
    public int BinsOut { get; set; }
    public int PartsOut { get; set; }
    public int QuarantinedBins { get; set; }
    public int QuarantinedParts { get; set; }
    public int PartsPerBinReceiving { get; set; }
    public int Bal { get; set; }

}
